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
    using F_Protocols;
    #endregion
    public class clsWhile204 : clsBaseWhile
    {
        public string _CMD { get; set; }  // 手动方式
        const string _CMD01_Read_status = "APS_motion_status";
        const string _CMD02_Read_input = "APS_read_d_input";
        string _currCMD = "";
        //int _use_Axis = 7;  //  0111
        List<int> _lstAxisId = new List<int>();
        cls204C _device = null;
        Dictionary<string, string> _dict_cmd = new Dictionary<string, string>();
        clsProtocol _protocol = null;
        public clsWhile204(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vDevNo)
        {
            _CMD = "";
            int _bit = 1;
            // 常数
            int _use_Axis = Convert.ToInt32(v_faim3.dict_KV["use_Axis"]);
            for (int i = 0; i < 8; i++)
            {
                if ((_use_Axis & _bit) == _bit)
                {
                    _lstAxisId.Add(i);
                }
                _bit = _bit << 1;
            }
            // 参数
            _devNo = Convert.ToInt32(vDevNo);
            _flowName = string.Format("{0}_{1}", v_faim3.dict_DevCards[_devNo].Name, _devNo); // [2]
            // 初始化 基类
            base.do_New(v_faim3, v_dao_comm, _flowName);
            _protocol = new clsProtocol(_faim3, _dao_comm); // 协议
            if (!_dao_comm.dict_DevIo.ContainsKey(_devNo.ToString()))  // 注册设备号 对应 设备对象
            {
                _device = new cls204C(_faim3, _dao_comm, _devNo); // 设备
                _dao_comm.dict_DevIo.Add(_devNo.ToString(), (IFaimIO)_device);
            }
            _dao_comm.OnEchoIO += new F_Delegate.delEcho(evt_comm_OnEchoIO);  // 加消息

            _dict_cmd.Add(_CMD01_Read_status, ""); // 第1命令
            _dict_cmd.Add(_CMD02_Read_input, ""); // 第2命令
        }
        // 消息 分解数据后 再传递消息

        void evt_comm_OnEchoIO(string name, int vChanalNo, List<int> lst, int idx, string val)
        {
            #region // 条件
            if (name.ToUpper() != "BT_IN") return;  // 1. 接收类别
            if (idx / _devNo * _faim3.sect_iDev != _devNo) return; // 2. 设备类别      
            if (_faim3.Comm_Data.bt_in[0][_devNo * _faim3.sect_iDev + _faim3.snd_iLen] < 1) return; // 3. 无数据

            #endregion
            _faim3.Comm_Data.bt_in[0][_devNo * _faim3.sect_iDev + _faim3.snd_iLen] = 0;   // 设已读

            string _ss = _faim3.Comm_Data.arr_str[0][_faim3.sect_sDev_start + _devNo * _faim3.sect_sDev + 5];
            //int _loc = 0;
            //int _val = 0;
            // 解析收到的信息

            switch (_currCMD)
            {
                case _CMD01_Read_status:   // 
                    break;
                case _CMD02_Read_input:   // 
                    break;
            }
        }
        protected override void do_While(string vFlowId)
        {
            clsFlow _flow = _faim3.dict_Flow[vFlowId];
            int _currAxisIdx = 0;  // 当前轴下标号
            _ref_i = _faim3.sect_iDev * _devNo;  // 偏移
            _ref_s = _faim3.sect_sDev * _devNo + _faim3.sect_sDev_start;  // 偏移
            _currCMD = _CMD01_Read_status;
            string _sndStr = "";
            #region // 流程条件
            clsThreadInfo _ti = _faim3.dict_Threads[vFlowId].Info;
            _ti.sleep = _faim3.dict_DevCards[_devNo].thDelay; // [10]
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
                #region // 自动方式, 发其它指令

                if (string.IsNullOrEmpty(_CMD))  // 根据命令名， 取协议字符串， 填入 arr_str, 写发送长度(触发)
                    _sndStr = _faim3.Comm_Data.arr_str[0][_ref_s]; // XX XX XX XX XX
                if (!string.IsNullOrEmpty(_CMD))
                {
                    _ti.caseName = _CMD;
                    _faim3.Comm_Data.arr_str[0][_ref_s] = "";
                    _CMD = "";
                }
                #endregion
                else
                {
                    #region // 交替，2 个命令和轴号
                    if (_currCMD == _CMD02_Read_input) // 完成一轮
                    {
                        _currCMD = _CMD01_Read_status;
                        _currAxisIdx++;  // 下一轴号
                        if (_currAxisIdx >= _lstAxisId.Count) _currAxisIdx = 0; // 轴号
                    }
                    else if (_currCMD == _CMD01_Read_status)
                    {
                        _currCMD = _CMD02_Read_input; // 
                    }
                    #endregion
                    _ti.caseName = _currCMD;
                    clsProtocol._204C(_faim3, _dao_comm, _currCMD, _lstAxisId[_currAxisIdx].ToString(), _devNo);
                }
                _RunState = eRunState.doLoop;
                System.Threading.Thread.Sleep(1);
                if (_RunState == eRunState.doLoop)
                {
                }
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
        protected override void Send_Receive()
        {
            _device.Send_Receive();
        }
    }
}
