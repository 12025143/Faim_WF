using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FACC
{
    public class FAIM_Parse
    {
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
                    _rec = Convert.ToString(item, 16).PadLeft(2,'0');
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
        public  static List<byte> doDecToBytes(long vVal, long vMode)
        {
            List<byte> _rec = new List<byte>();
            long _val = vVal;
            int _fix =Convert.ToInt32(((vMode == 16) ? (vMode * vMode) : vMode));
            // 119
            while ((_val > 0))
            {
                long _ls = (_val % _fix);// 模取余 9 
                _val =Convert.ToInt64( Math.Floor(Convert.ToDouble( _val) / Convert.ToDouble(_fix)));//除取整 11
                _rec.Insert(0,Convert.ToByte(_ls));//高位放高位 (0)=9 (1)=1 (2)=2
            }
            return _rec;
        }
    //    '(257, 4) ==> "0102"  十进制 转 定长二进制
    //'Shared Function doDecToHex(ByVal vVal As Long, Optional ByVal vLength As Integer = 2) As String
    //'    Dim _rec As String = Hex(vVal).PadLeft(vLength, "0")  ' "A" ==> "0A"
    //'    Return _rec
    //'End Function
    }
}
