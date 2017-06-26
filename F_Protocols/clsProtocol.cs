#region // ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
#endregion
namespace F_Protocols
{
    #region // ...
    using F_Entitys;
    using F_Enums;
    using FACC;
    using F_Entitys_DAL;
    #endregion
    public partial class clsProtocol
    {
        #region // init
        clsFaim3 _faim3 = null;
        DAL_CommData _dao_comm = null;
        clsProtocol() { }
        public clsProtocol(clsFaim3 v_faim3, DAL_CommData dao_comm)
        {
            _faim3 = v_faim3;
            _dao_comm = dao_comm;
        }
        #endregion
        // 返回待发送数据的 设备号

        public static int getSendData(clsFaim3 _faim3, int idx)
        {
            clsDevTestBits _line = _faim3.lst_DevTestBits[idx];
            return getSendData(_faim3, _line.vName, _line.HL, _line.Reset);
        }
        static int getSendData(clsFaim3 _faim3, string vCmdName, string vHL, string vReset)
        {
            int _devNo = -1;
            if (_faim3.dict_CmdFormats.ContainsKey(vCmdName))
                _devNo = Convert.ToInt32(_faim3.dict_CmdFormats[vCmdName].devNo);//[0] cfgCmdFormat.xls
            else if (_faim3.dict_DevFunction.ContainsKey(vCmdName))
            {
                _devNo = _faim3.dict_DevFunction[vCmdName].devNo;//[0] cfgDev_Function.xls
                vCmdName = _faim3.dict_DevFunction[vCmdName].varName;
            }
            else
            {
                F_Log.Error_4("clsProtocol.getSendData()",
                    string.Format(" --->>>> 此命令不存在 {0} {1} {2}", vCmdName, vHL, vReset));
                return -1; //没有此命令 模板
            }
            int _ref_1i = _faim3.sect_iDev * _devNo;
            int _ref_1s = _faim3.sect_sDev * _devNo + _faim3.sect_sDev_start;
            string _str_HL = "   ";
            string _str_Reset = "   ";
            int _val_HL = F_TransCalc._Get_Value_2(_faim3, vHL, ref _str_HL);
            int _val_Reset = F_TransCalc._Get_Value_2(_faim3, vReset, ref _str_Reset);
            clsCommonInfo _en = null;
            if (_val_Reset > 0)
            {
                if (_faim3.dict_CommonInfo.ContainsKey(_val_Reset)) // 可能是表值 tb_CommonInfo.xls
                {
                    _en = _faim3.dict_CommonInfo[_val_Reset];
                }
            }

            switch ((eDeviceName)_devNo)
            {
                case eDeviceName.RS485_6:
                    #region // RS485_6
                    if (_en != null) // 有表值 tb_CommonInfo.xls
                    {
                        #region // SeekHome Home_Dir
                        switch (vCmdName)
                        {
                            case "SeekHome":
                                SeekHome _SeekHome = _faim3._SeekHome;
                                _SeekHome.v02.OutputNum = F_Parse._ConverToInt(clsFaim3.StringFormat(_faim3, _en.val_01));
                                _SeekHome.v03.Condition = F_Parse._ConverToInt(clsFaim3.StringFormat(_faim3, _en.val_02));
                                break;
                            case "Home_Dir":
                                Home_Dir _Home_Dir = _faim3._Home_Dir;
                                _Home_Dir.v02.Acc = F_Parse._ConverToDouble(clsFaim3.StringFormat(_faim3, _en.val_01));
                                _Home_Dir.v03.Dec = F_Parse._ConverToInt(clsFaim3.StringFormat(_faim3, _en.val_02));
                                _Home_Dir.v04.Vel = F_Parse._ConverToDouble(clsFaim3.StringFormat(_faim3, _en.val_03));
                                _Home_Dir.v05.Location = F_Parse._ConverTofloat(clsFaim3.StringFormat(_faim3, _en.arr_01[0]));
                                _Home_Dir.v06.Location = F_Parse._ConverTofloat(clsFaim3.StringFormat(_faim3, _en.arr_01[1]));
                                break;
                        }
                        #endregion
                    }
                    clsProt485 _485 = new clsProt485(_faim3);
                    _485.Out_Ready(vCmdName, _val_HL);   // 485 对象
                    break;
                    #endregion
                case eDeviceName.TCP_5:
                case eDeviceName.TCP_8:
                case eDeviceName.TCP_9:
                    string _Format2 = _faim3.dict_CmdFormats[vCmdName].Format;//[2]; 模板   
                    //clsRobot_AZ _az = new clsRobot_AZ(_faim3);
                    List<string> _lstPara = new List<string>();
                    if (_val_HL != -999)
                    {
                        // $GetVariable,{0}&CrLf
                        if (!string.IsNullOrEmpty(vHL)) _lstPara.Add(vHL);
                    }
                    else if (_str_HL != "   ")
                    {
                        if (!string.IsNullOrEmpty(vHL)) _lstPara.Add(_str_HL);
                    }
                    if (_en != null) // 有表值
                    {
                        // J Point {0} {1} {2} {3}
                        if (!string.IsNullOrEmpty(_en.val_01)) _lstPara.Add(clsFaim3.StringFormat(_faim3, _en.val_01));
                        if (!string.IsNullOrEmpty(_en.val_02)) _lstPara.Add(clsFaim3.StringFormat(_faim3, _en.val_02));
                        if (!string.IsNullOrEmpty(_en.val_03)) _lstPara.Add(clsFaim3.StringFormat(_faim3, _en.val_03));
                        if (!string.IsNullOrEmpty(_en.arr_1)) _lstPara.Add(clsFaim3.StringFormat(_faim3, _en.arr_1));
                        if (!string.IsNullOrEmpty(_en.arr_2)) _lstPara.Add(clsFaim3.StringFormat(_faim3, _en.arr_2));
                        if (!string.IsNullOrEmpty(_en.arr_3)) _lstPara.Add(clsFaim3.StringFormat(_faim3, _en.arr_3));
                    }
                    _faim3._sss[_ref_1s] = "";  // 非标准二进制字节码


                    string _cmd = F_TransCalc.StringFormat(_Format2, _lstPara);
                    _faim3._sss[_ref_1s + _faim3.snd_sAsc] = _cmd;  // 

                    string _Echo4 = _faim3.dict_CmdFormats[vCmdName].Echo;//[4]; 反馈
                    _faim3._sss[_ref_1s + _faim3.test_sLoc] = _Echo4;

                    break;
            }
            return _devNo;
        }
        // 根据设备名读取设备号 条件：名称 ， 如失败，返回-1
        public static int do_GetDevNo(clsFaim3 _faim3, string vName)
        {
            int _devNo = -1;
            string[] _arr = vName.Split('_'); // 204C_3  ==> 204C, 3
            if (_arr.Length < 2) return _devNo;
            foreach (var item in _faim3.dict_DevCards) // clsDev_Cards.xls
            {
                // 0.isEnable	 1.devNo	  2.Name	 3.cardType	4.card_num
                if (item.Value.devNo.ToString() == _arr[1].ToUpper().Trim() &&
                    item.Value.Name == _arr[0].ToUpper().Trim())
                {
                    _devNo = item.Value.devNo;
                    break;
                }
            }
            return _devNo;
        }
        public int do_GetDevNo(string vName)
        {
            return do_GetDevNo(_faim3, vName);
        }
        // SVON HOME MOVR LOAD 用于取设备号
        public static int doExistCmd_204(string vCmdName)
        {
            int _rec = -1;
            List<string> _lst = new List<string>() { 
                "SVON", "SVOF", 
                
                "HOME", "APS_home_move",
                "STOP", "APS_stop_move", 
                "EMG",  "APS_emg_stop",
              
                "MOVR", "APS_relative_move", 
                "MOVA", "APS_absolute_move", 
                "MOVL", "APS_absolute_linear_move",
                "MOVI", "APS_relative_linear_move",  
                "ARCR", "APS_relative_arc_move", 
                "ARCA", "APS_absolute_arc_move", 
                
                //"EXSR", "BGSR", "EDSR", 
                //"CALL", "PROG", "ENDO", "EXIT",  
                //"EXPG", "ABPG", "SSPG", "RSPG",
                //"LOAD", "SAVE"
                
                //"BTON", "BTOF",
                //"BITN", "BIT",
            };
            _rec = _lst.IndexOf(vCmdName);
            return _rec;
        }
    }
}
