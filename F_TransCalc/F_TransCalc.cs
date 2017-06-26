using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
namespace FACC
{
    //using F_Entitys;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using F_Entitys;
    using System.Text;
    using System.Net;
    public class F_TransCalc
    {
        // 类似 val.StringFromat()  模板, 大数据对象, 属生名列表, 每个值间的分隔符, 首字符
        public static string StringFormat(string vTemp, List<string> vParas, string vSplitFlag = " ", string vfirstSplitFlag = "")
        {
            string _rec = vTemp;
            for (int i = 0; i < vParas.Count; i++)
            {
                string _ss = vParas[i];// doGetValue_byName(vObj, vParas[i]);
                if (_rec.Contains("{" + i.ToString() + "}"))
                    _rec = _rec.Replace("{" + i.ToString() + "}", _ss);
                //else
                //    _rec = _rec + ((i == 0) ? vfirstSplitFlag : vSplitFlag) + _ss;
            }
            return _rec;
        }
        // SetRegisterSub . v02 . Addr    = 100
        // SetRegisterSub . v00 . cmdName = "ABC"
        // _faim3  .Comm_Data . blTrace   = T/F
        static void _doSetValue_Name(object vObj, string valName, object vValue)
        {
            string _ss = valName;
            _ss = _ss.Replace("String()", "");
            _ss = _ss.Replace("String", "");
            string[] _arr = _ss.Split('.');
            object _obj = vObj;
            for (int _i = 1; _i < _arr.Length; _i++)
            {
                _obj = do_Name_to_Object(_obj, _arr[_i]);//
                if (_obj == null)
                {
                    FACC.F_Log.Debug_1("", string.Format("--->>>> 子变量名不存在:{0}-{1}", valName, _arr[_i]));
                    return;
                }
                else
                {
                    //doSetObject_byName(_obj, _arr[i + 1], vValue);
                    return;
                }
            }
        }
        static void _doSetObject_byName(object vObj, string valName, object vValue)
        {
            object _obj = do_Name_to_Object(vObj, valName);
            Type _t = vObj.GetType();
            PropertyInfo _pi = _t.GetProperty(valName);// AgrtName
            //if (_pi == null)
            //{
            //FieldInfo _fi = _t.GetField(valName); // Protocal
            //_fi.SetValue(vObj, vValue);
            //_obj = _fi.GetValue(vObj);
            //}
            if (_pi != null)
            {
                if (vValue.GetType() == typeof(System.String))
                    if (string.IsNullOrEmpty(vValue.ToString()))
                        _pi.SetValue(vObj, 0, null);
                    else if (vValue.ToString() == "T" || vValue.ToString() == "F")
                        _pi.SetValue(vObj, vValue.ToString() == "T", null);
                    else
                    {
                        try
                        {
                            _pi.SetValue(vObj, Convert.ToInt32(vValue), null);
                        }
                        catch (FormatException ex)
                        {
                            _pi.SetValue(vObj, vValue, null);
                            Console.WriteLine(ex.Message);
                        }
                    }
            }
        }

        // 根据对象属性名, 设置其值
        public static string doSetValue_byName(object vObj, string valName, object sNew)
        {
            return do_ObjectValue_byName(vObj, valName, sNew);
        }
        static void do_Set_Value(object vObj, string valName, object vNew)
        {
            Type _t = vObj.GetType();
            PropertyInfo _pi = _t.GetProperty(valName);// 属性




            if (_pi == null)
            {
                #region // _fi
                FieldInfo _fi = _t.GetField(valName); // Protocal 字段
                if (vNew.GetType() == typeof(System.Int32))
                {
                    _fi.SetValue(vObj, vNew);  // 设数值



                }
                else if (vNew.GetType() == typeof(System.String))
                {
                    if (string.IsNullOrEmpty(vNew.ToString())) // 是空, 值0
                    {
                        _fi.SetValue(vObj, 0);
                    }
                    else if (vNew.ToString() == "T" || vNew.ToString() == "F")  // 布尔值
                    {
                        _fi.SetValue(vObj, vNew.ToString() == "T");
                    }
                    else if (vNew.ToString() == "Y" || vNew.ToString() == "N")
                    {
                        _fi.SetValue(vObj, vNew.ToString() == "Y");
                    }
                    else
                    {
                        try
                        {
                            _fi.SetValue(vObj, Convert.ToInt32(vNew));  // 设数值



                        }
                        catch (FormatException ex)
                        {
                            _fi.SetValue(vObj, vNew); // 设原值



                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region // _pi
                if (vNew.GetType() == typeof(System.Int32))
                {
                    _pi.SetValue(vObj, vNew, null); // 设原值



                }
                else if (vNew.GetType() == typeof(System.String))
                {
                    if (string.IsNullOrEmpty(vNew.ToString())) // 是空, 值0
                    {
                        _pi.SetValue(vObj, 0, null);
                    }
                    else if (vNew.ToString() == "T" || vNew.ToString() == "F")  // 布尔值
                    {
                        _pi.SetValue(vObj, vNew.ToString() == "T", null);
                    }
                    else if (vNew.ToString() == "Y" || vNew.ToString() == "N")
                    {
                        _pi.SetValue(vObj, vNew.ToString() == "Y", null);
                    }
                    else
                    {
                        try
                        {
                            _pi.SetValue(vObj, Convert.ToInt32(vNew), null);  // 设数值



                        }
                        catch (FormatException ex)
                        {
                            _pi.SetValue(vObj, vNew, null); // 设原值



                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                #endregion
            }
        }
        // 根据对象属性名, 取其值
        public static string doGetValue_byName(object vObj, string valName)
        {
            return do_ObjectValue_byName(vObj, valName, null);
        }
        static string do_ObjectValue_byName(object vObj, string valName, object sNew)
        {
            string _rec = null;
            string _ss = valName;
            _ss = _ss.Replace("String()", "");
            _ss = _ss.Replace("String", "");
            string[] _arr = _ss.Split('.');
            int _len = _arr.Length;
            object _obj = vObj;
            for (int i = 1; i < _len; i++)
            {
                _obj = do_Name_to_Object(_obj, _arr[i]);//
                if (_obj == null)
                {
                    FACC.F_Log.Debug_1("", string.Format("--->>>> 子变量名不存在:{0}-{1}", valName, _arr[i]));
                    return null;
                }
                if (sNew != null && i == _len - 2)
                {
                    do_Set_Value(_obj, _arr[i + 1], sNew);
                    break;
                }
                if (i == _len - 1)
                    _rec = _obj.ToString();
            }
            return _rec;
        }
        static object do_Name_to_Object(object vObj, string valName)
        {
            object _obj = null;
            Type _t = vObj.GetType();
            PropertyInfo _pi = _t.GetProperty(valName);// AgrtName 属性



            if (_pi == null)
            {
                FieldInfo _fi = _t.GetField(valName); // Protocal 字段
                _obj = _fi.GetValue(vObj);
            }
            else
            {
                _obj = _pi.GetValue(vObj, null);
            }
            return _obj;
        }

        // 取值
        public static int _Get_Value_2(clsFaim3 _faim3, string strFld, ref string outStr)
        {
            string _Name = strFld.Replace("*", ""); // *strFld ==> _ls 有星号表明不是值,而是"变量名"
            outStr = "   ";
            int _bit = -1;
            int _val_ = 0;
            if (string.IsNullOrEmpty(_Name))
            {
                _val_ = -999;
                outStr = _Name;
            }
            else if (_faim3.dict_DevFunction.ContainsKey(_Name))  // IO集变量2DI_0[_ls]
            {
                #region // dict_DevFunction IO 口定义
                clsDevFunction _df = _faim3.dict_DevFunction[_Name];//  
                _bit = _df.devNo * _faim3.sect_iDev + _df.Index;//  
                if (_df.varName.ToUpper() == "BT_IN")
                    _val_ = _faim3.Comm_Data.bt_in[0][_bit];
                else if (_df.varName.ToUpper() == "BT_OUT")
                    _val_ = _faim3.Comm_Data.bt_out[0][_bit];
                #endregion
            }
            else if (_faim3._dim_dict.ContainsKey(_Name)) // 动态变量[_Name]
            {
                #region // _dim_dict  动态变量

                if (_Name.ToUpper().StartsWith("&H"))
                {
                    _Name = _Name.ToUpper().Replace("&H", "");
                    _val_ = Convert.ToInt32(_Name, 16);
                }
                else if (_Name.ToUpper().StartsWith("0X"))
                {
                    _Name = _Name.ToUpper().Replace("0X", "");
                    _val_ = Convert.ToInt32(_Name, 16);
                }
                //else if (_Name.StartsWith("0"))
                //{
                //    _val_ = Convert.ToInt32(_Name, 2);
                //}
                else if (F_TransCalc.IsNumeric(_Name))
                {
                    _val_ = Convert.ToInt32(_Name);
                }
                else
                {
                    _bit = _faim3._dim_dict[_Name];// 自定义名为下标, 分配地址, 寻址
                    _val_ = _faim3._dim[_bit];
                    outStr = _Name;
                }
                #endregion
            }
            else if (_Name.ToUpper().StartsWith("_FAIM3") ||// 大数据  _faim3 || clsfraim3
                     _Name.ToUpper().StartsWith("CLSFAIM3"))
            {
                #region // _FAIM3 大数据
                _bit = 1;
                _val_ = Convert.ToInt32(F_TransCalc.doGetValue_byName(_faim3, _Name));
                outStr = _Name;
                #endregion
            }
            else
            {
                #region // 常数
                if (_Name.ToUpper().StartsWith("-"))
                {
                    _val_ = -999;
                    outStr = _Name;
                }
                else if (_Name.ToUpper().StartsWith("&H"))
                {
                    _Name = _Name.ToUpper().Replace("&H", "");
                    _val_ = Convert.ToInt32(_Name, 16);
                }
                else if (_Name.ToUpper().StartsWith("0X"))
                {
                    _Name = _Name.ToUpper().Replace("0X", "");
                    _val_ = Convert.ToInt32(_Name, 16);
                }
                else if (_Name.StartsWith("0"))
                {
                    _val_ = Convert.ToInt32(_Name, 2);
                }
                else if (F_TransCalc.IsNumeric(_Name))
                {
                    _val_ = Convert.ToInt32(_Name);
                }
                else
                {
                    _val_ = -999;
                    outStr = _Name;
                }
                #endregion
            }
            return _val_;
        }
        public static int _Get_Value_2(clsFaim3 v_faim3, string strFld)
        {
            string _str_2 = "   ";
            return _Get_Value_2(v_faim3, strFld, ref _str_2);
        }


        // 十进制转十六进制 Dec ==> HEX
        public static string doDecToHexs(double vVal, int vMode = 16, int vLength = 2, string split_flag = " ")
        {
            List<byte> _lst = doDecToBytes(vVal, vMode, vLength);
            string _rec = "";
            foreach (var item in _lst)
            {
                if (_rec == "")
                    _rec = Convert.ToString(item, 16).PadLeft(2, '0').ToUpper();
                else
                    _rec = _rec + split_flag + Convert.ToString(item, 16).PadLeft(2, '0').ToUpper();
            }
            return _rec;
        }

        // 十进制转十六进制 Dec ==> BYTES
        public static List<byte> doDecToBytes(double vVal, int vMode = 16, int vLength = 2)
        {
            List<byte> _rec = new List<byte>();// As New List(Of Byte)
            double _dev = vVal;
            double _fix = (vMode == 16) ? vMode * vMode : vMode;
            while (_dev > 0)
            {
                byte _val = (byte)(_dev % _fix);// 模取余
                _dev = Math.Floor(_dev / _fix); // 除取整 
                //_rec.Add(_val);        // 高位放高位
                _rec.Insert(0, _val);// '高位放高位
            }
            int _leftpad = vLength - _rec.Count;
            for (int i = 0; i < _leftpad; i++)
            {
                _rec.Insert(0, 0);
            }
            return _rec;
        }

        // 十六进制转字符串 BYTES ==> STR
        public static string doBytesToStr(List<byte> vBuf, string splitFlag = " ")
        {
            return doBytesToStr(vBuf.ToArray(), splitFlag); 
        }
        public static string doBytesToStr(byte[] vBuf, string splitFlag = "")
        {
            StringBuilder _sb = new StringBuilder();
            for (int i = 0; i < vBuf.Length; i++)
            {
                if (i > 0) 
                    _sb.Append(splitFlag);
                _sb.Append(vBuf[i].ToString("X2"));
            }
            return _sb.ToString();
        }
        // 参数：字节码， 长度， 是否为ASCII（是：12345 ; 否：31 32 33 34)
        public static string doBytesToStr(byte[] vbuf, int vLen, bool isASC = true, string splitFlag = " ")
        {
            string _rec = null;
            int _len = vLen > 0 ? vLen : vbuf.Length;
            if (isASC) // ASCII/是  "this is ASCII-123456"
            {
                _rec = System.Text.ASCIIEncoding.Default.GetString(vbuf);
            }
            else // BTYES/否  "FF FE 31 32 33"
            {
                _rec = Convert.ToString(vbuf[0], 16).PadLeft(2, '0').ToUpper();
                for (int i = 1; i < _len; i++)
                {
                    _rec = _rec + splitFlag + Convert.ToString(vbuf[i], 16).PadLeft(2, '0').ToUpper();
                }
            }
            return _rec;
        }

        // 是否是数值
        public static bool IsNumeric(string val)
        {
            return Regex.IsMatch(val, @"^[+-]?\d*[.]?\d*$");
        }
        // 是否是整数
        public static bool IsInt(string val)
        {
            return Regex.IsMatch(val, @"^[+-]?\d*$");
        }


        // 校验和
        public static int doCRC(string vBytes)
        {
            return doCRC(vBytes.Split(' '));
        }
        public static int doCRC(string[] vbuf)
        {
            int _crc = 0;
            int _i;
            int _t;
            int _ret = 0;
            _crc = 0xFFFF;
            for (_i = 0; _i < vbuf.Length; _i++)
            {
                _crc = _crc ^ Convert.ToInt32(vbuf[_i], 16);
                _crc = _crc & 0xFFFF;
                for (_t = 1; _t < 9; _t++)
                {
                    if ((_crc & 0x1) == 0x1)
                    {
                        _crc = (_crc / 2) ^ 0xA001;
                    }
                    else
                    {
                        _crc = (_crc / 2);
                    }
                    _crc = _crc & 0xFFFF;
                }
            }
            int _a = _crc & 0xFF;
            int _b = _crc & 65280;
            _ret = _a * 0x100 + _b / 0x100;
            return _ret;
        }
    }
}