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
    public class clsWhile485 : clsBaseWhile
    {
        public string _CMD { get; set; }  // 手动方式
        // {0}{1}| 命令 轴号
        const string _CMD_Read01 = "ReadStatus";
        const string _CMD_Read08 = "ReadPOS";
        const string _CMD_echo01 = "0302";
        const string _CMD_echo02 = "0304";
        int _cnt_Axis15 = 15;  // 共 16 个轴
        cls485 _device = null;
        clsProt485 _bo_protocol;
        string _currCMD = "";
        Dictionary<string, string> _dict_cmd = new Dictionary<string, string>();
        public clsWhile485(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vDevNo)
        {
            _CMD = "";
            _cnt_Axis15 = Convert.ToInt32(v_faim3.dict_KV["cnt_Axis"]);
            _devNo = Convert.ToInt32(vDevNo);
            _flowName = string.Format("{0}_{1}", v_faim3.dict_DevCards[_devNo].Name, _devNo);
            base.do_New(v_faim3, v_dao_comm, _flowName);
            _device = new cls485(_faim3, _dao_comm, _devNo);
            _bo_protocol = new clsProt485(_faim3);
            if (!_dao_comm.dict_DevIo.ContainsKey(_devNo.ToString()))  // 加功能接口

                _dao_comm.dict_DevIo.Add(_devNo.ToString(), (IFaimIO)_device);
            _dao_comm.OnEchoIO += new F_Delegate.delEcho(evt_comm_OnEchoIO);  // 加消息

            _dict_cmd.Add(_CMD_Read01, "");
            _dict_cmd.Add(_CMD_Read08, "");
        }
        // 消息 分解数据后 再传递消息

        void evt_comm_OnEchoIO(string name, int vChanalNo, List<int> lst, int idx, string val)
        {
            #region // 条件
            if (name.ToUpper() != "BT_IN") return;  // 1. 接收类别
            if (idx / _faim3.sect_iDev != _devNo) return; // 2. 设备类别            //int _idx = idx % 100;   
            if (_faim3.Comm_Data.bt_in[0][_devNo * _faim3.sect_iDev + _faim3.snd_iLen] < 1) return; // 3. 无数据

            #endregion
            _faim3.Comm_Data.bt_in[0][_devNo * _faim3.sect_iDev + _faim3.snd_iLen] = 0;   // 设已读

            string _ss = _faim3.Comm_Data.arr_str[0][_faim3.sect_sDev_start + _devNo * _faim3.sect_sDev + 5];
            int _loc = 0;
            int _val = 0;
            // 解析收到的信息

            int _AxisMotionDone = Convert.ToInt32(_faim3.dict_KV["AxisMotionDone"]); //
            int _AxisAlarmstatus = Convert.ToInt32(_faim3.dict_KV["AxisAlarmstatus"]); //
            int _AxisCurrentPos = Convert.ToInt32(_faim3.dict_KV["AxisCurrentPos"]); //
            switch (_currCMD)
            {
                case _CMD_Read01:   // ReadStatus ==> AxisMotionDone & AxisAlarmstatus
                    #region // ReadStatus
                    _loc = _ss.IndexOf(_CMD_echo01);
                    if (_loc > -1)
                    {
                        _ss = _ss.Substring(_loc + 4, 4);
                        _val = Convert.ToInt32(_ss, 16);
                        //_faim3.Comm_Data.bt_in[0][_AxisMotionDone] = (int)_val & 0x4;  // 0000 0100
                        _dao_comm.set_bt_in(_AxisMotionDone, (int)_val & 0x4);  //  
                        //_faim3.Comm_Data.bt_in[0][_AxisAlarmstatus] = (int)_val & 0x100;  //1 0000 0000
                        _dao_comm.set_bt_in(_AxisAlarmstatus, (int)_val & 0x100);  //  
                    }
                    break;
                    #endregion
                case _CMD_Read08:   // ReadPOS ==> AxisCurrentPos
                    #region // ReadPOS
                    _loc = _ss.IndexOf(_CMD_echo02);
                    if (_loc > -1)
                    {
                        _ss = _ss.Substring(_loc + 4, 8);
                        _val = Convert.ToInt32(_ss, 16);
                        //_faim3.Comm_Data.bt_in[0][_AxisCurrentPos] = _val;  //  
                        _dao_comm.set_bt_in(_AxisCurrentPos, _val); //  
                    }
                    break;
                    #endregion
            }
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
            _ti.sleep = _faim3.dict_DevCards[_devNo].thDelay;
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
                    #region // 交替，2 个命令和16个轴号

                    if (_currCMD == _CMD_Read08) // 完成一轮
                    {
                        _currCMD = _CMD_Read01;
                        _currAxisNum++;  // 下一轴号
                        if (_currAxisNum > _cnt_Axis15) _currAxisNum = 0; // 轴号0-15
                    }
                    else if (_currCMD == _CMD_Read01)
                    {
                        _currCMD = _CMD_Read08; // 
                    }
                    #endregion
                    _ti.caseName = _currCMD;
                    _sndStr = _bo_protocol.ReadStatus(_currAxisNum);
                }
                //_sndStr = _bo_protocol.doProtocol_01(_currAxisNum, _currCMD, _devNo);//  _faim3.dict_CmdFormats[_currCMD];
                _RunState = eRunState.doLoop;
                System.Threading.Thread.Sleep(1);
                if (_RunState == eRunState.doLoop)
                    do_Send(_sndStr);       // 发送

                _RunState = eRunState.Free;
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
        public override void doSendStr(string vSndStr)
        {
            _RunState = eRunState.isSend;
            for (int i = 0; i < 30; i++)
            {
                Thread.Sleep(1);
                if (_RunState == eRunState.doSend)
                {
                    string[] _arr = vSndStr.Split('|');
                    for (int j = 1; j < _arr.Length; j++)
                    {
                        if (string.IsNullOrEmpty(_arr[i])) continue;
                        do_Send(_arr[j]);
                    }
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
