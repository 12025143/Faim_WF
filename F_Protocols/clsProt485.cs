using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_Protocols
{
    using System.Reflection;
    using F_Entitys;
    using F_Enums;
    using FACC;
    public class clsProt485
    {
        clsFaim3 _faim3 = null;
        clsProt485() { }
        public clsProt485(clsFaim3 vFaim3)
        {
            _faim3 = vFaim3;
        }
        // 返回待发送数据的绝对位置
        public int Out_Ready(string vCmdName, int vAxisNum)
        {
            int _ret = 0;
            string _result = "";
            string[] _names = Enum.GetNames(typeof(eDev485));
            if (Array.IndexOf(_names, vCmdName) > -1)
            {
                #region // 参数 SimpleFormat
                //int _AxisNum = Convert.ToInt32(vAxisNum);
                _result = doSimpleToString(new SimpleFormat()
                {
                    v01_AxisNum = vAxisNum,
                    v00_cmdName = (eDev485)Enum.Parse(
                                                typeof(eDev485),
                                                vCmdName)
                });
                #endregion
            }
            else
            {
                #region // 对象Home_Dir OutputLine Position_Set_Pos SeekHome SetRegisterSub
                switch (vCmdName)
                {
                    case "Home_Dir":
                        _faim3._Home_Dir.v01.AxisNum = vAxisNum;
                        _result = Home_Dir(_faim3._Home_Dir);
                        break;
                    case "OutputLine":
                        _faim3._OutputLine.v01.AxisNum = vAxisNum;
                        _result = OutputLine(_faim3._OutputLine);
                        break;
                    case "Position_Set_Pos":
                        _faim3._Position_Set_Pos.v01.AxisNum = vAxisNum;
                        _result = Position_Set_Pos(_faim3._Position_Set_Pos);
                        break;
                    case "SeekHome":
                        _faim3._SeekHome.v01.AxisNum = vAxisNum;
                        _result = SeekHome(_faim3._SeekHome);
                        break;
                    case "SetRegisterSub":
                        _faim3._SetRegisterSub.v01.AxisNum = vAxisNum;
                        _result = SetRegisterSub(_faim3._SetRegisterSub);
                        break;
                }
                #endregion
            }
            #region // 置预发的数据
            int _devNo = -1;
            // 设备号
            if (_faim3.dict_CmdFormats.ContainsKey(vCmdName))
                _devNo = Convert.ToInt32(_faim3.dict_CmdFormats[vCmdName].devNo);//[0] cfgCmdFormat.xls
            else if (_faim3.dict_DevFunction.ContainsKey(vCmdName))
                _devNo = _faim3.dict_DevFunction[vCmdName].devNo;//[0] cfgDev_Function.xls
            else
            {
                F_Log.Error_4("clsProt485.Out_Ready()",
                    string.Format(" --->>>> 设备号不存在 {0} vAxisNum：{1}", vCmdName, vAxisNum));
                return -1; //没有此命令 模板
            }
            int _ref_1i = _faim3.sect_iDev * _devNo;
            int _ref_1s = _faim3.sect_sDev * _devNo + _faim3.sect_sDev_start;// 偏移位

            string[] _arr = _result.Split('|');// 分解模板
            string _send = _arr.Length > 1 ? _arr[1] : _arr[0];
            int _crc = F_TransCalc.doCRC(_send);  // AA BB CC DD CRC CRC
            _send = _send + " " + F_TransCalc.doDecToHexs(_crc);
            _faim3._sss[_ref_1s] = _send;// 置 数据部份
            _faim3._sss[_ref_1s + _faim3.snd_sAsc] = "";  // 无ASCII, 置空 
            _faim3._sss[_ref_1s + _faim3.test_sLoc] = vCmdName;  // 置 发出的命令
            _faim3._sss[_ref_1s + _faim3.snd_sLen] = "0"; // 置 长度为空
            #endregion
            _ret = _ref_1s + _faim3.snd_sLen;
            return _ret;
        }
        // 简单协议格式一: ---------------------------------
        // 最优使用   方法:1 取当前方法的名字, 要求, 方法名与内置功能名称一致




        // home_Set_Pos,{0}{1}|01 02 03 04 05
        public string home_Set_Pos(int vAxisNum)
        {
            return doSimpleToString(new SimpleFormat()
            {
                v01_AxisNum = vAxisNum,
                v00_cmdName = (eDev485)Enum.Parse(
                                            typeof(eDev485),
                                            MethodBase.GetCurrentMethod().Name)
            });
        }
        public string home_Set_PosA(int vAxisNum)
        {
            return doSimpleToString(new SimpleFormat()
            {
                v01_AxisNum = vAxisNum,
                v00_cmdName = (eDev485)Enum.Parse(
                                            typeof(eDev485),
                                            MethodBase.GetCurrentMethod().Name)
            });
        }
        public string MotorOn(int vAxisNum)
        {
            return doSimpleToString(new SimpleFormat()
            {
                v01_AxisNum = vAxisNum,
                v00_cmdName = (eDev485)Enum.Parse(
                                            typeof(eDev485),
                                            MethodBase.GetCurrentMethod().Name)
            });
        }
        public string MotorOff(int vAxisNum)
        {
            return doSimpleToString(new SimpleFormat()
            {
                v01_AxisNum = vAxisNum,
                v00_cmdName = (eDev485)Enum.Parse(
                                            typeof(eDev485),
                                            MethodBase.GetCurrentMethod().Name)
            });
        }
        public string MotorAlarmReset(int vAxisNum)
        {
            return doSimpleToString(new SimpleFormat()
            {
                v01_AxisNum = vAxisNum,
                v00_cmdName = (eDev485)Enum.Parse(
                                            typeof(eDev485),
                                            MethodBase.GetCurrentMethod().Name)
            });
        }
        public string Position_A_Move(int vAxisNum)
        {
            return doSimpleToString(new SimpleFormat()
            {
                v01_AxisNum = vAxisNum,
                v00_cmdName = (eDev485)Enum.Parse(
                                            typeof(eDev485),
                                            MethodBase.GetCurrentMethod().Name)
            });
        }
        public string Position_R_Move(int vAxisNum)
        {
            return doSimpleToString(new SimpleFormat()
            {
                v01_AxisNum = vAxisNum,
                v00_cmdName = (eDev485)Enum.Parse(
                                            typeof(eDev485),
                                            MethodBase.GetCurrentMethod().Name)
            });
        }
        public string ReadStatus(int vAxisNum)
        {
            return doSimpleToString(new SimpleFormat()
            {
                v01_AxisNum = vAxisNum,
                v00_cmdName = (eDev485)Enum.Parse(
                                            typeof(eDev485),
                                            MethodBase.GetCurrentMethod().Name)
            });
        }
        public string ReadPOS(int vAxisNum)
        {
            return doSimpleToString(new SimpleFormat()
            {
                v01_AxisNum = vAxisNum,
                v00_cmdName = (eDev485)Enum.Parse(
                                            typeof(eDev485),
                                            MethodBase.GetCurrentMethod().Name)
            });
        }
        public string StopMove(int vAxisNum)
        {
            return doSimpleToString(new SimpleFormat()
            {
                v01_AxisNum = vAxisNum,
                v00_cmdName = (eDev485)Enum.Parse(
                                            typeof(eDev485),
                                            MethodBase.GetCurrentMethod().Name)
            });
        }
        // 简单协议格式一: 最优使用   方法:2 约束为枚举方法

        public string _toString_Enum(int vAxisNum, eDev485 eName)
        {
            return doSimpleToString(new SimpleFormat() { v01_AxisNum = vAxisNum, v00_cmdName = eName });
        }
        public string doSimpleToString(SimpleFormat vEn)
        {
            string _rec = "";
            string _cmdName = vEn.v00_cmdName.ToString();
            string _format = _faim3.dict_CmdFormats[_cmdName].Format;//[2];
            _rec = string.Format(_format, _cmdName, vEn.v01_AxisNum.ToString("X").PadLeft(2, '0'));
            return _rec;
        }
        // 复杂协议格式二: 最优使用 ---------------------------
        // 方法2: 取当前数据类的名字, 要求,数据类与内置功能名称一致




        // ReadStatus,{0}{1}|01 02 03 04 05 v02.Acc_2|v03.Dec_2
        public string doObjectToString(object vEn)
        {
            string _rec = "";
            Base_Set_Para _en = vEn as Base_Set_Para;
            if (_en.v00.cmdName == eDev485obj.Free) return _rec;
            string _format = _faim3.dict_CmdFormats[_en.v00.cmdName.ToString()].Format;//[2];
            string _split_flag = " ";
            string[] _arr1 = _format.Split(_split_flag[0]);
            for (int i = 0; i < _arr1.Length; i++)
            {
                if (!_arr1[i].Contains('.')) continue;
                string[] _ar_0 = _arr1[i].Split('|');
                for (int j = 0; j < _ar_0.Length; j++)
                {
                    string[] _ar_1 = _ar_0[j].Split('_');
                    int _len = 2;
                    if (_ar_1.Length > 1) _len = Convert.ToInt32(_ar_1[1]);
                    string _v_1 = F_TransCalc.doGetValue_byName(vEn, _ar_1[0]);
                    if (j == 0)
                        _arr1[i] = F_TransCalc.doDecToHexs(
                                               F_Parse._ConverToDouble(_v_1), 16, _len, _split_flag);
                    else
                        _arr1[i] = _arr1[i] + _split_flag +
                                   F_TransCalc.doDecToHexs(
                                               F_Parse._ConverToDouble(_v_1), 16, _len, _split_flag);
                }
            }
            //
            string _k = _en.v00.cmdName.ToString() + "_k";
            if (_faim3.dict_CmdFormats.ContainsKey(_k))
            {
                _k = _faim3.dict_CmdFormats[_k].Format;//[2];
                string[] _ar_11 = _k.Split('|');
                foreach (var item in _ar_11)
                {
                    string[] _arr12 = item.Split('_');
                    int _idx = Convert.ToInt32(_arr12[0]) - 1;
                    int _add = Convert.ToInt32(_arr12[1], 16);
                    int _mul = Convert.ToInt32(_arr12[2], 16);
                    int _val = Convert.ToInt32(_arr1[_idx], 16);
                    _val = _add + _val * _mul;
                    _arr1[_idx] = _val.ToString("X").PadLeft(2, '0');
                }
            }
            for (int i = 0; i < _arr1.Length; i++)
            {
                if (_rec == "")
                    _rec = _arr1[i];
                else
                    _rec = _rec + _split_flag + _arr1[i];
            }
            _rec = string.Format(_rec, _en.v00.cmdName.ToString(), _en.v01.AxisNum.ToString("X").PadLeft(2, '0'));
            return _rec;
        }
        // 较好使用
        // 方法1: 取当前方法的名字, 要求, 方法名与内置功能名称一致

        public string Home_Dir(Home_Dir vEn)
        {
            if (vEn.v00.cmdName == eDev485obj.Free)
                vEn.v00.cmdName = (eDev485obj)Enum.Parse(
                                            typeof(eDev485obj),
                                            vEn.GetType().Name);
            return doObjectToString(vEn);
        }
        public string OutputLine(OutputLine vEn)
        {
            if (vEn.v00.cmdName == eDev485obj.Free)
                vEn.v00.cmdName = (eDev485obj)Enum.Parse(
                                            typeof(eDev485obj),
                                            vEn.GetType().Name);
            // 调整位




            return doObjectToString(vEn);
        }
        public string Position_Set_Pos(Position_Set_Pos vEn)
        {
            if (vEn.v00.cmdName == eDev485obj.Free)
                vEn.v00.cmdName = (eDev485obj)Enum.Parse(
                                            typeof(eDev485obj),
                                            vEn.GetType().Name);
            return doObjectToString(vEn);
        }
        public string SeekHome(SeekHome vEn)
        {
            if (vEn.v00.cmdName == eDev485obj.Free)
                vEn.v00.cmdName = (eDev485obj)Enum.Parse(
                                            typeof(eDev485obj),
                                            vEn.GetType().Name);
            // 调整位

            return doObjectToString(vEn);
        }
        public string SetRegisterSub(SetRegisterSub vEn)
        {
            if (vEn.v00.cmdName == eDev485obj.Free)
                vEn.v00.cmdName = (eDev485obj)Enum.Parse(
                                            typeof(eDev485obj),
                                            vEn.GetType().Name);
            // 调整位




            return doObjectToString(vEn);
        }
        /* 
         * 把需要的参数，顺序放到设备域的 bt_out数组中
         * 此函数 取 数组中的数据放到 对象中，组合为字符串
         * --- 用法比较麻烦 ---
         * int _ref = vDevNo * _faim3.sect_iDev;  // 偏移
         * _faim3.Comm_Data._bt_out[0][_ref + 1] = 12;
         * _faim3.Comm_Data._bt_out[0][_ref + 2] = X;
         * _faim3.Comm_Data._bt_out[0][_ref + 3] = Y;
         * call doProtocol_01(-1, "home_set_pos", 2)
         * ------
         */
        string doProtocol_01(int vAxisNum = -1, string vCmdName = "", int vDevNo = 0)
        {
            string _res = "";
            if (string.IsNullOrEmpty(vCmdName)) return _res;  // 命令名为空


            //if (vAxisNum < 0) return _res;
            int _ref = vDevNo * _faim3.sect_iDev;  // 偏移
            if (vAxisNum < 0)
                vAxisNum = _faim3.Comm_Data._bt_out[0][_ref + 1]; // 无轴号，则自动取轴号
            int i = 0;
            switch (vCmdName.ToLower())
            {
                #region //..
                case "home_set_pos":
                    _res = home_Set_Pos(vAxisNum);
                    break;
                case "home_set_posa":
                    _res = home_Set_PosA(vAxisNum);
                    break;
                case "motoron":
                    _res = MotorOn(vAxisNum);
                    break;
                case "motoroff":
                    _res = MotorOff(vAxisNum);
                    break;
                case "motoralarmreset":
                    _res = MotorAlarmReset(vAxisNum);
                    break;
                case "readstatus":
                    _res = ReadStatus(vAxisNum);
                    break;
                case "readpos":
                    _res = ReadPOS(vAxisNum);
                    break;
                case "stopmove":
                    _res = StopMove(vAxisNum);
                    break;
                case "position_a_move":
                    _res = Position_A_Move(vAxisNum);
                    break;
                case "position_r_move":
                    _res = Position_R_Move(vAxisNum);
                    break;
                case "setregistersub":
                    SetRegisterSub _en1 = new SetRegisterSub();
                    _en1.v00.cmdName = eDev485obj.SetRegisterSub;
                    _en1.v01.AxisNum = vAxisNum;
                    i = _ref + 2;
                    _en1.v02.Addr = _faim3.Comm_Data._bt_out[0][i++];
                    _en1.v03.Acc = _faim3.Comm_Data._bt_out[0][i++];
                    _res = SetRegisterSub(_en1);
                    break;
                case "outputline":
                    OutputLine _OutputLine = new OutputLine();
                    _OutputLine.v00.cmdName = eDev485obj.OutputLine;
                    _OutputLine.v01.AxisNum = vAxisNum;
                    i = _ref + 2;
                    _OutputLine.v02.InputNum = _faim3.Comm_Data._bt_out[0][i++];
                    _OutputLine.v03.Condition = _faim3.Comm_Data._bt_out[0][i++];
                    _res = OutputLine(_OutputLine);
                    break;
                case "seekhome":
                    SeekHome _SeekHome = new SeekHome();
                    _SeekHome.v00.cmdName = eDev485obj.SeekHome;
                    _SeekHome.v01.AxisNum = vAxisNum;
                    i = _ref + 2;
                    _SeekHome.v02.OutputNum = _faim3.Comm_Data._bt_out[0][i++];
                    _SeekHome.v03.Condition = _faim3.Comm_Data._bt_out[0][i++];
                    _res = SeekHome(_SeekHome);
                    break;
                case "home_dir":
                    Home_Dir _Home_Dir = new Home_Dir();
                    _Home_Dir.v00.cmdName = eDev485obj.Home_Dir;
                    _Home_Dir.v01.AxisNum = vAxisNum;
                    i = _ref + 2;
                    _Home_Dir.v02.Acc = _faim3.Comm_Data._bt_out[0][i++];
                    _Home_Dir.v03.Dec = _faim3.Comm_Data._bt_out[0][i++];
                    _Home_Dir.v04.Vel = _faim3.Comm_Data._bt_out[0][i++];
                    _Home_Dir.v05.Location = _faim3.Comm_Data._bt_out[0][i++];
                    _Home_Dir.v06.Location = _faim3.Comm_Data._bt_out[0][i++];
                    _res = Home_Dir(_Home_Dir);    // 一般使用



                    break;
                case "position_set_pos":
                    Position_Set_Pos _Position_Set_Pos = new Position_Set_Pos();
                    _Position_Set_Pos.v00.cmdName = eDev485obj.Position_Set_Pos;
                    _Position_Set_Pos.v01.AxisNum = vAxisNum;
                    i = _ref + 2;
                    _Position_Set_Pos.v02.Acc = _faim3.Comm_Data._bt_out[0][i++];
                    _Position_Set_Pos.v03.Dec = _faim3.Comm_Data._bt_out[0][i++];
                    _Position_Set_Pos.v04.Vel = _faim3.Comm_Data._bt_out[0][i++];
                    _Position_Set_Pos.v05.Location = _faim3.Comm_Data._bt_out[0][i++];
                    _Position_Set_Pos.v06.Location = _faim3.Comm_Data._bt_out[0][i++];
                    _res = Position_Set_Pos(_Position_Set_Pos);
                    break;
                #endregion
            }
            return _res;
        }
    }
}
