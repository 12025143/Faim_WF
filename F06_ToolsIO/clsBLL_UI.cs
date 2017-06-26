using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace nsFACC
{
    using System.Threading;
    using System.Net.Sockets;
    public class clsBLL_UI
    {
        public static string _ContainsKey(Dictionary<string, string> dict, string vMsg)
        {
            string ret = "";
            foreach (var item in dict)
            {
                if (vMsg.StartsWith(item.Key))
                {
                    ret = item.Value;
                    break;
                }
            }
            return ret;
        }
        // 读 cfgCmdFormat_t01.ini 文件
        public static string doEchoDict_Fill(ref Dictionary<string, string> dict_echo)
        {
            string _rec = "";
            //string _ch_name = "通讯反馈对照表";
            string _flagKey = "=";
            int _cnt_fld = 2;
            Dictionary<string, string> _ret = new Dictionary<string, string>();
            List<string> _lines = null;
            string _fn = F_Const.path_Config + "cfgCmdFormat_t01.ini";
            _lines = F_Const.doGet_ListTrim(F_Const.doGet_arr(_fn, _flagKey), _flagKey, _cnt_fld, "'");
            string _str1 = "";
            dict_echo.Clear();
            foreach (var item in _lines)
            {
                if (_str1 == "")
                    _str1 = item;
                else
                    _str1 = _str1 + Environment.NewLine + item;
                string[] _arr2 = item.Split('=');
                if (_arr2.Length > 1 && !dict_echo.ContainsKey(_arr2[0].Trim()))
                    dict_echo.Add(_arr2[0].Trim(), _arr2[1].Trim());
            }
            if (_str1.Length > 1)
                _rec = _str1;
            return _rec;
        }
        public static string doBytesToString(byte[] vbuf, int vLen = 0, bool isASC = true)
        {
            string _rec = null;
            int _len = vLen > 0 ? vLen : vbuf.Length;
            if (isASC) // ASCII  "thisisASC-123456"
            {
                _rec = System.Text.ASCIIEncoding.Default.GetString(vbuf);
            }
            else // BTYES  "FF FE 12 23 45"
            {
                for (int i = 0; i < _len; i++)
                {
                    if (i == 0)
                        _rec = Convert.ToString(vbuf[i], 16).PadLeft(2, '0').ToUpper();
                    else
                        _rec = _rec + " " + Convert.ToString(vbuf[i], 16).PadLeft(2, '0').ToUpper();
                }
            }
            return _rec;
        }
    }
}
