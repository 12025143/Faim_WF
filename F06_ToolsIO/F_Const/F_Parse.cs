using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace nsFACC
{
    public class F_Parse
    {
        // CrLf
        public static byte[] doStringToBytes(string vStr)
        {
            List<byte> _lst = null;
            bool _CrLf = vStr.EndsWith("&CrLf");
            if (_CrLf)
            {
                vStr = vStr.Replace("&CrLf", "");
                vStr = vStr.Replace("~", ",");
            }
            //int _sndLen = vStr.Length;
            byte[] _ascii = System.Text.ASCIIEncoding.Default.GetBytes(vStr);
            _lst = new List<byte>(_ascii);
            if (_CrLf)
            {
                _lst.Add(0x0D);
                _lst.Add(0x0A);
            }
            return _lst.ToArray();
        }
        static Array Redim(Array src, int len)
        {
            Type _t = src.GetType().GetElementType();
            Array _ret = Array.CreateInstance(_t, len);
            Array.Copy(src, 0, _ret, 0, Math.Min(src.Length, len));
            return _ret;
        }
        //buf(&H03,&H0A,&H00,&HFF,&H01)  ==> strin: "03 10 00 FF 01"  字节数组 转字符串
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
                    // item.ToString("X").PadLeft(2, "0")  'Convert.ToString(item, 16)
                }
                else
                {
                    _rec = _rec + vSplit + Convert.ToString(item, 16).PadLeft(2, '0');
                }
            }
            return _rec;
        }
        //257 ==> List(&H01, &H02) 十进制 转 字节数组
        public static List<byte> doDecToBytes(long vVal, long vMode)
        {
            List<byte> _rec = new List<byte>();
            long _val = vVal;
            int _fix = Convert.ToInt32(((vMode == 16) ? (vMode * vMode) : vMode));
            while ((_val > 0))
            {
                long _ls = (_val % _fix);// 模取余 9 
                _val = Convert.ToInt64(Math.Floor(Convert.ToDouble(_val) / Convert.ToDouble(_fix)));//除取整 11
                _rec.Insert(0, Convert.ToByte(_ls));//高位放高位 (0)=9 (1)=1 (2)=2
            }
            return _rec;
        }
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
        //    '(257, 4) ==> "0102"  十进制 转 定长二进制
        public static string doDecToHexs(double vVal, int vMode = 16, int vLength = 2, string split_flag = " ")
        {
            List<byte> _lst = doDecToBytes(vVal, vMode, vLength);
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
        public static string _ArrayToString(List<byte> arr)
        {
            return _ArrayToString(arr.ToArray(), " ", arr.Count);
        }
        public static string _ArrayToString(List<byte> arr, string split_flag)
        {
            return _ArrayToString(arr.ToArray(), split_flag, arr.Count);
        }
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
        public static bool IsNumeric(string val)
        {
            return Regex.IsMatch(val, @"^[+-]?\d*[.]?\d*$");
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
        public static int _ConverToInt(string _Name)
        {
            int _val = -999;
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
                _val = Convert.ToInt32(_Name);
            }
            else 
            { }
            return _val;
        }
    }
}
