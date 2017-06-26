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
    using F_Interface;
    #endregion
    public class clsWhile74x : clsBaseWhile
    {
        const string _CMD01_Read = "Send_Receive";
        const string _CMD02_Read = "DI_ReadPort";
        string _currCMD = "";
        cls74xBLL _device = null;
        public clsWhile74x(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vDevNo)
        {
            _devNo = Convert.ToInt32(vDevNo);
            _flowName = string.Format("{0}_{1}", v_faim3.dict_DevCards[_devNo].Name, vDevNo); //[2]
            base.do_New(v_faim3, v_dao_comm, _flowName);
            if (_flowName.Contains("7432"))
                _device = new cls7432(_faim3, _dao_comm, _devNo);
            if (_flowName.Contains("7230"))
                _device = new cls7230(_faim3, _dao_comm, _devNo);
            if (!_dao_comm.dict_DevIo.ContainsKey(_devNo.ToString()))
                _dao_comm.dict_DevIo.Add(_devNo.ToString(), (IFaimIO)_device);
        }
        protected override void do_While(string vFlowId)
        {
            clsFlow _flow = _faim3.dict_Flow[vFlowId];
            _currCMD = _CMD01_Read;
            _ref_i = _devNo * _faim3.sect_iDev;  // 偏移
            _ref_s = _devNo * _faim3.sect_sDev + _faim3.sect_sDev_start;  // 偏移
            #region // 流程条件
            clsThreadInfo _ti = _faim3.dict_Threads[vFlowId].Info;
            _ti.sleep = Convert.ToInt32(_faim3.dict_DevCards[_devNo].thDelay );//[10]);
            _ti.allowBusiness = eSwitch.No ;
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
                #region // 自动方式, 发其它指令
                if (_faim3.Comm_Data.bt_out[0][_ref_i + 64] > 0)  // 根据命令名， 取协议
                {
                    /*
                     _LLine = _faim3.Comm_Data.bt_out[0][_ref_i + _faim3.dict_KV["LLine"]]
                     _Value = _faim3.Comm_Data.bt_out[0][_ref_i + _LLine) '从大数据取出数据
                     */
                    _ti.caseName = "Send";
                    //_ret = DO_WriteLine(_CardNumber, _Port, _LLine, _Value)
                    _device.Send();
                    _faim3.Comm_Data.bt_out[0][_ref_i + 64] = 0;
                }
                #endregion
                else
                {
                    _RunState = eRunState.doLoop;
                    System.Threading.Thread.Sleep(1);
                    if (_RunState == eRunState.doLoop)
                    {
                        _ti.caseName = _CMD01_Read;
                        // _ret = DI_ReadPort(_CardNumber, _Port, _Value)
                        _device._cmdName = _CMD02_Read; // 循环
                        _device.Send_Receive();
                    }
                    _RunState = eRunState.Free;
                }
                // 接收后处理
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
        // 
        public override void doSendStr(string vSndStr)
        {
            _RunState = eRunState.isSend;
            for (int i = 0; i < 30; i++)
            {
                Thread.Sleep(1);
                if (_RunState == eRunState.doSend)
                {
                    //string[] _arr = vSndStr.Split('|');
                    _device.Send();
                    break;
                }
            }
            _RunState = eRunState.Free;
        }
    }
}
