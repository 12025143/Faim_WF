#region //...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_CaseWhile
{
    #region //...
    using F_Entitys;
    using F_Enums;
    using F_CaseWhile;
    using Faim3_Drivers;
    using F_Entitys_DAL;
    using System.Threading;
    using FACC;
    using F_Interface;
    #endregion
    public class clsWhileTcp : clsBaseWhile
    {
        public string _CMD { get; set; }  // 手动方式
        // {0}{1}| 命令 轴号
        string _CMD_Read01 = "ReadPOS";     // {0}{1}|03 00 08 00 02
        string _CMD_Read02 = "ReadStatus";  // {0}{1}|03 00 01 00 01 00 .v02.Acc|.v03.Dec|.v04.Vel
        int MaxAxisNum = 15;  // 共 16 个轴
        clsTCP _device = null;
        string _currCMD = "";
        public clsWhileTcp(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vDevNo)
        {
            _CMD = "";
            MaxAxisNum = Convert.ToInt32(v_faim3.dict_KV["cnt_Axis"]);
            _devNo = Convert.ToInt32(vDevNo);
            _flowName = string.Format("{0}_{1}", v_faim3.dict_DevCards[_devNo].Name, vDevNo);
            base.do_New(v_faim3, v_dao_comm, _flowName);
            _device = new clsTCP(_faim3, _dao_comm, _devNo);
            if (!_dao_comm.dict_DevIo.ContainsKey(_devNo.ToString()))
                _dao_comm.dict_DevIo.Add(_devNo.ToString(), (IFaimIO)_device);
        }
        protected override void do_While(string vFlowId)
        {
            clsFlow _flow = _faim3.dict_Flow[vFlowId];
            int _currAxisNum = 0;
            string _sndStr = "";
            _currCMD = _CMD_Read01;
            _ref_i = _devNo * _faim3.sect_iDev;  // 偏移
            _ref_s = _devNo * _faim3.sect_sDev + _faim3.sect_sDev_start;  // 偏移
            #region // 流程条件
            clsThreadInfo _ti = _faim3.dict_Threads[vFlowId].Info;
            _ti.sleep = Convert.ToInt32(_faim3.dict_DevCards[_devNo].thDelay);
            _ti.allowBusiness = eSwitch.No;
            System.Diagnostics.Stopwatch _tmr = do_th_Name_pre(_ti); // 流程条件
            _ti.allowWhile = eSwitch.On;   //控制流程之1, 若为 false 则会 break流程
            #endregion
            while (((int)_ti.allowWhile == (int)eSwitch.On))
            {
                if (_faim3.isEmergency > 0)
                {
                    _flow.eState = eWF_State.Emergency;
                    break;
                }
                if (!do_allowBusiness(_ti)) continue; // 流程信息 1
                long _trm_begin = _tmr.ElapsedMilliseconds; // 毫秒
                if (_RunState == eRunState.isSend)
                {
                    _RunState = eRunState.doSend;
                    continue;
                }
                // 自动方式
                if (string.IsNullOrEmpty(_CMD))  // 根据命令名， 取协议字符串， 填入 arr_str, 写发送长度(触发)
                    _CMD = _faim3.Comm_Data.arr_str[0][_ref_s];
                if (!string.IsNullOrEmpty(_CMD))
                {
                    // 发命令， 要准备命令名 和 轴号
                    _ti.caseName = _CMD;
                    //_sndStr = _bo_protocol.doProtocol_01(-1, _CMD, _devNo);
                    _faim3.Comm_Data.arr_str[0][_ref_s] = "";
                    _CMD = "";
                }
                else
                {
                    if (_currCMD == _CMD_Read02) // 完成一轮
                    {
                        _currCMD = _CMD_Read01;
                        _currAxisNum++;  // 下一轴号
                        if (_currAxisNum > MaxAxisNum) _currAxisNum = 0; // 轴号0-15
                    }
                    else if (_currCMD == _CMD_Read01)
                    {
                        _currCMD = _CMD_Read02; // 
                    }
                    _ti.caseName = _currCMD;
                }
                //_sndStr = _bo_protocol.doProtocol_01(_currAxisNum, _currCMD, _devNo);//  _faim3.dict_CmdFormats[_currCMD];
                _RunState = eRunState.doLoop;
                System.Threading.Thread.Sleep(1);
                if (_RunState == eRunState.doLoop)
                    do_Send(_sndStr);
                _RunState = eRunState.Free;
                do_end_CalcTimer(_ti, _tmr, _trm_begin); // 流程信息2
                if (_RunState == eRunState.isSend)
                {
                    _RunState = eRunState.doSend;
                    continue;
                }
                System.Threading.Thread.Sleep(_ti.sleep);
            }
            doClearState(_ti);
            _tmr.Stop();		//结束计时
        }
        public override void doSendStr(string vSndStr)
        {
            _RunState = eRunState.isSend;
            for (int i = 0; i < 30; i++)
            {
                Thread.Sleep(1);
                if (_RunState == eRunState.doSend)
                {
                    do_Send(vSndStr);
                    break;
                }
            }
            _RunState = eRunState.Free;
        }
        protected override void Send_Receive()
        {
            _device.Send_Receive();
        }
    }
}
