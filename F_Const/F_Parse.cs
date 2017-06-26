using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace FACC
{
    public class F_Parse
    {
        /*-----------------------------
         * Asciid码 "12345" ==> 字节码 {0x31, 0x32, 0x33, 0x34, 0x35}
         * 识别：&CrLf  &Cr
        -------------------------------*/
        public static byte[] doStringToBytes(string vAscii)
        {
            List<byte> _lst = null;
            bool _CrLf = vAscii.EndsWith("&CrLf");
            bool _Cr = vAscii.EndsWith("&Cr");

            if (_CrLf)
            {
                vAscii = vAscii.Replace("&CrLf", "");
                vAscii = vAscii.Replace("~", ",");
            }
            else if (_Cr)
            {
                vAscii = vAscii.Replace("&Cr", "");
                vAscii = vAscii.Replace("~", ",");
            }

            byte[] _ascii = ASCIIEncoding.Default.GetBytes(vAscii);
            _lst = new List<byte>(_ascii);
            if (_CrLf)
            {
                _lst.Add(0x0D);
                _lst.Add(0x0A);
            }
            else if (_Cr)
            {
                _lst.Add(0x0D);
            }
            return _lst.ToArray();
        }



        /*----------------------------- 
         * 字节码 {0x31, 0x32, 0x33, 0x34, 0x35} ==> 字符串 "31 32 33 34 35"
         * 参数： vBuf      字节码
         *        vLocation 转换位置
         *        vSplit    分隔符
        -------------------------------*/
        public static string doBytesToHexs(byte[] vBuf, long vLocation, string vSplit)
        {
            string _rec = "";
            long _len = ((vLocation > 0) ? vLocation : vBuf.Length);
            for (int i = 0; i <= (_len - 1); i++)
            {
                byte item = vBuf[i];
                if ((_rec == ""))
                {
                    _rec = Convert.ToString(item, 16).PadLeft(2, '0');
                }
                else
                {
                    _rec = _rec + vSplit + Convert.ToString(item, 16).PadLeft(2, '0');
                }
            }
            return _rec;
        }



        /* ？？？ ----------------------------- 
         * 十进制数 257  ==> 字节码 {0x01, 0x02}  
         * 参数： vVal      数值
         *        vMode16   模式 取值(16, 非16)
        -------------------------------*/
        public static List<byte> doDecToBytes(long vVal, long vMode16)
        {
            List<byte> _rec = new List<byte>();
            long _val = vVal;
            int _fix = Convert.ToInt32(((vMode16 == 16) ? (vMode16 * vMode16) : vMode16));
            while ((_val > 0))
            {
                long _ls = (_val % _fix);// 模取余 9 
                _val = Convert.ToInt64(Math.Floor(Convert.ToDouble(_val) / Convert.ToDouble(_fix)));//除取整 11
                _rec.Insert(0, Convert.ToByte(_ls));// 小端：高位放高位 (0)=9 (1)=1 (2)=2
            }
            return _rec;
        }



        /*-----------------------------  
         * 十进制数 258  ==> 字节码 {0x03, 0x01}  小端
         * 参数： vVal      数值
         *        vMode16   模式     取值(16, 非16)
         *        vLength   字节码宽 取值(2, 4, 8 )
        -------------------------------*/
        public static List<byte> doDecToBytes(double vVal, int vMode16 = 16, int vLength = 2)
        {
            List<byte> _rec = new List<byte>();// As New List(Of Byte)
            double _fix = (vMode16 == 16) ? vMode16 * vMode16 : vMode16;
            double _int = vVal;
            while (_int > 0)
            {
                byte _mod = (byte)(_int % _fix);// 模取余
                _int = Math.Floor(_int / _fix); // 除取整 
                _rec.Insert(0, _mod);// '小端：低位放余
            }
            int _leftpad = vLength - _rec.Count;
            for (int i = 0; i < _leftpad; i++)
            {
                _rec.Insert(0, 0);
            }
            return _rec;
        }

        //    '(257, 4) ==> "0102"  十进制 转 定长二进制

        /*-----------------------------  
         * 十进制数 258  ==> 字符串 03 01  小端
         * 参数： vVal      数值
         *        vMode16   模式     取值(16, 非16)
         *        vLength   字节码宽 取值(2, 4, 8 )
         *        split_flag分隔符   缺省值为空格
        -------------------------------*/
        public static string doDecToHexs(double vVal, int vMode16 = 16, int vLength = 2, string split_flag = " ")
        {
            List<byte> _lst = doDecToBytes(vVal, vMode16, vLength);
            return _ArrayToString(_lst, split_flag);
        }
        public static string _ArrayToString(List<int> arr)
        {
            return _ArrayToString(arr.ToArray(), " ", arr.Count);
        }
        public static string _ArrayToString(List<int> arr, string split_flag)
        {
            return _ArrayToString(arr.ToArray(), split_flag, arr.Count);
        }
        public static string _ArrayToString(List<int> arr, string split_flag, int length)
        {

            string _res = "";// BitConverter.ToString(arr.ToArray());
            string _ls = "";
            int _len = length == 0 ? arr.Count : length;
            for (int i = 0; i < _len; i++)
            {
                _ls = Convert.ToString(arr[i], 16).PadLeft(2, '0').ToUpper();
                _res = _res == "" ? _ls : _res + split_flag + _ls;
            }
            return _res;
        }
        public static string _ArrayToString(List<byte> arr)
        {
            return _ArrayToString(arr.ToArray(), " ", arr.Count);
        }


        /*-----------------------------  
         * 列表 []  ==> 字符串 03 01  小端
         * 参数： arr       列表
         *        split_flag分隔符 
        -------------------------------*/
        public static string _ArrayToString(List<byte> arr, string split_flag)
        {
            return _ArrayToString(arr.ToArray(), split_flag, arr.Count);
        }

        /*-----------------------------  
         * 列表 [0x31, 0x32, 0x33, 0x34, 0x35] ==> 字符串 "31 32 33 34 35"
         * 参数： arr       列表
         *        split_flag分隔符 
         *        length    转换长度
        -------------------------------*/
        public static string _ArrayToString(List<byte> arr, string split_flag, int length)
        {
            string _res = "";
            string _ls = "";
            int _len = length == 0 ? arr.Count : length;
            for (int i = 0; i < _len; i++)
            {
                _ls = Convert.ToString(arr[i], 16).PadLeft(2, '0').ToUpper();
                _res = _res == "" ? _ls : _res + split_flag + _ls;
            }
            return _res;
        }


        public static string _ArrayToString(int[] arr)
        {
            return _ArrayToString(arr, " ", arr.Length);
        }
        public static string _ArrayToString(int[] arr, string split_flag)
        {
            return _ArrayToString(arr, split_flag, arr.Length);
        }
        public static string _ArrayToString(int[] arr, string split_flag, int length)
        {
            string _res = "";
            string _ls = "";
            int _len = length == 0 ? arr.Length : length;
            for (int i = 0; i < _len; i++)
            {
                _ls = Convert.ToString(arr[i], 16).PadLeft(2, '0').ToUpper();
                _res = _res == "" ? _ls : _res + split_flag + _ls;
            }
            return _res;
        }
        public static string _ArrayToString(byte[] arr)
        {
            return _ArrayToString(arr, " ", arr.Length);
        }
        public static string _ArrayToString(byte[] arr, string split_flag)
        {
            return _ArrayToString(arr, split_flag, arr.Length);
        }
        public static string _ArrayToString(byte[] arr, string split_flag, int length)
        {
            string _res = "";
            string _ls = "";
            int _len = length == 0 ? arr.Length : length;
            for (int i = 0; i < _len; i++)
            {
                _ls = Convert.ToString(arr[i], 16).PadLeft(2, '0').ToUpper();
                _res = _res == "" ? _ls : _res + split_flag + _ls;
            }
            return _res;
        }


        public static int _ConverToInt(string _Name)
        {
            int _val = 0;
            if (string.IsNullOrEmpty(_Name))
            {
                //_val = -999;
            }
            else if (_Name.ToUpper().StartsWith("&H"))
            {
                _Name = _Name.ToUpper().Replace("&H", "");
                _val = Convert.ToInt32(_Name, 16);
            }
            else if (_Name.ToUpper().StartsWith("0X"))
            {
                _Name = _Name.ToUpper().Replace("0X", "");
                _val = Convert.ToInt32(_Name, 16);
            }
            else if (_Name.StartsWith("0"))
            {
                _val = Convert.ToInt32(_Name, 2);
            }
            else if (Regex.IsMatch(_Name, @"^[+-]?\d*[.]?\d*$"))
            {
                _val = Convert.ToInt32(_Name);
            }
            else
            {
                //_val = -999;
            }
            return _val;
        }
        public static int _ConverToVal(string _Name, ref string outStr)
        {
            int _val_ = 0;
            outStr = "   ";
            if (string.IsNullOrEmpty(_Name))
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
            else if (Regex.IsMatch(_Name, @"^[+-]?\d*[.]?\d*$"))
            {
                _val_ = Convert.ToInt32(_Name);
            }
            else
            {
                _val_ = -999;
                outStr = _Name;
            }
            return _val_;
        }
        public static float _ConverTofloat(string _Name)
        {
            float _val = -999;
            if (string.IsNullOrEmpty(_Name))
            {
            }
            else if (_Name.ToUpper().StartsWith("&H"))
            {
                _Name = _Name.ToUpper().Replace("&H", "");
                _val = Convert.ToInt32(_Name, 16);
            }
            else if (_Name.ToUpper().StartsWith("0X"))
            {
                _Name = _Name.ToUpper().Replace("0X", "");
                _val = Convert.ToInt32(_Name, 16);
            }
            else if (_Name.StartsWith("0"))
            {
                _val = Convert.ToInt32(_Name, 2);
            }
            else if (Regex.IsMatch(_Name, @"^[+-]?\d*[.]?\d*$"))
            {
                _val = Convert.ToSingle(_Name);
            }
            else
            { }
            return _val;
        }
        public static double _ConverToDouble(string _Name)
        {
            double _val = -999.0;
            if (string.IsNullOrEmpty(_Name))
            {
            }
            else if (_Name.ToUpper().StartsWith("&H"))
            {
                _Name = _Name.ToUpper().Replace("&H", "");
                _val = Convert.ToInt32(_Name, 16);
            }
            else if (_Name.ToUpper().StartsWith("0X"))
            {
                _Name = _Name.ToUpper().Replace("0X", "");
                _val = Convert.ToInt32(_Name, 16);
            }
            else if (_Name.StartsWith("0"))
            {
                _val = Convert.ToInt32(_Name, 2);
            }
            else if (Regex.IsMatch(_Name, @"^[+-]?\d*[.]?\d*$"))
            {
                _val = Convert.ToDouble(_Name);
            }
            else 
            { }
            return _val;
        }

        public static string[] doTxetBoxSelect(TextBox vCtrl)
        {
            string[] _arr = null;
            string _str = "";
            vCtrl.Focus();
            _str = vCtrl.SelectedText;  // 1. 取选择内容
            if (string.IsNullOrEmpty(_str) || _str.Length < 3) // 没有选择,或太短
            {
                int _idx = vCtrl.GetFirstCharIndexOfCurrentLine();
                int _line = vCtrl.GetLineFromCharIndex(_idx);
                _str = vCtrl.Lines[_line]; // 2. 取当前行
                vCtrl.SelectionStart = _idx;  //设置起始位置 
                vCtrl.SelectionLength = _str.Length;  //设置长度
                //textBox1.Select(_idx, _str.Length);
                //textBox1.ScrollToCaret();
            }
            if (string.IsNullOrEmpty(_str)) return null;
            _arr = _str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //_arr = _str.Split('\r');
            //for (int i = 0; i < _arr.Length; i++)
            //{
            //    _arr[i] = _arr[i].Replace("\n", "");
            //}
            return _arr;
        }


        public static bool IsNumeric(string val)
        {
            return Regex.IsMatch(val, @"^[+-]?\d*[.]?\d*$");
        }
        static Array Redim(Array src, int len)
        {
            Type _t = src.GetType().GetElementType();
            Array _ret = Array.CreateInstance(_t, len);
            Array.Copy(src, 0, _ret, 0, Math.Min(src.Length, len));
            return _ret;
        }
    }
}
