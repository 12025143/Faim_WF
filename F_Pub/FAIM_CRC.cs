using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FACC
{
    public class FAIM_CRC
    {
        //  "MotorOff6|00 a5 00 00 ef 45|03 10 00 FF 00 02 04 00 A5 00 00 EF 45"
        public static string doPutBuffer_Dict(string vId, ref byte[] vBuf, string vPrevSend)
        {
            int _len = vBuf.Length;
            string _str = FAIM_Parse.doBytesToHexs(vBuf, (_len - 3), "");
            // "031000FF01"
            long _crc = do_CRC(_str);
            List<byte> _lst = FAIM_Parse.doDecToBytes(_crc, 16);
            vBuf[_len - 2] = _lst[1];
            vBuf[_len - 1] = _lst[0];
            string _res = FAIM_Parse.doBytesToHexs(vBuf, -1, " ");
            // "03 10 00 FF 01 XX XX"
            if (!string.IsNullOrEmpty(vPrevSend))
            {
                _res = (vPrevSend + ("|" + _res));
            }
            _res = (vId + ("|" + _res));
            return _res;
        }
        //计算校验和 "1012FFFE" ==>  "0102"
        public static string doCRC(string vStr)
        {
            long _nCrc;
            _nCrc = do_CRC(vStr);
            long _A;
            long _B;
            string _rec;
            _A = (_nCrc & 255);
            _B = (_nCrc & 65280);
            _A = (_A * 256);
            _B = (_B / 256);
            long _i = (_A + _B);
            _rec =Convert.ToString(_i, 16).PadLeft(4, '0');
            if ((_i < 16))
            {
                _rec = "000" + Convert.ToString(_i, 16);
            }
            else if ((_i < 256))
            {
                _rec = "00" + Convert.ToString(_i, 16);
            }
            else if ((_A + _B) < 4096)
            {
                _rec = "0" + Convert.ToString(_i, 16);
            }
            else
            {
                _rec = Convert.ToString(_i, 16);
            }
            return _rec;
        }
        private static long do_CRC(string vStr)
        {
            long _nCrc;
            long _val;
            int _R;
            _nCrc = 65535;
            for (_R = 1; (_R <= vStr.Length); _R = (_R + 2))
            {
                // 0X0Y0Z
                _val =Convert.ToInt64(vStr.Substring(_R, 2));
                _nCrc = _nCrc ^ _val;
                _nCrc = _nCrc & 65535;
                //  FFFF
                for (int T = 1; (T <= 8); T = (T + 1))
                {
                    if ((_nCrc & 1)>0)
                    {
                        _nCrc = (_nCrc / 2)^0xA001;
                        _nCrc = (_nCrc & 65535);
                    }
                    else
                    {
                        _nCrc = _nCrc / 2;
                        _nCrc = (_nCrc & 65535);
                    }
                }
            }
            return _nCrc;
        }
    }
}
