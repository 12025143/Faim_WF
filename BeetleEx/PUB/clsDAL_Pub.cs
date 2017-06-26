using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetleEx
{
    // http://www.ikende.com/doc/Beetle?id=Beetle.BytesMessage
    // http://blog.csdn.net/huanglei0809/article/details/10135183
    // http://www.cnblogs.com/smark/archive/2012/02/21/2361865.html 服务端

    using Beetle;
    using System.Windows.Forms;
    public class clsDAL_Pub
    {
        public static void doDemo(IChannel _channel)
        {
            if (_channel.Socket.Connected)
            {
                Beetle.StringMessage _bsm = new Beetle.StringMessage();
                _bsm.Value = "CreateClient";
                _channel.Send(_bsm);
            }
        }
        public static int Send(IChannel _channel, string vStr, bool hasCrLf)
        {
            //doDemo(_channel);
            if (string.IsNullOrEmpty(vStr)) return 0;
            if (_channel.IsDisposed) return -1;

            bool _ok = false;

            // 处理 CrLf
            bool _CrLf = vStr.EndsWith("&CrLf");
            if (_CrLf) // 优先
            {
                vStr = vStr.Replace("&CrLf", "");
                vStr = vStr + "\r\n";
                //vStr = vStr.Replace("~", ",");
            }
            else if (hasCrLf)
            {
                vStr = vStr + "\r\n";
            }
            try
            {
                Beetle.StringMessage _bsm = new Beetle.StringMessage();
                _bsm.Value = vStr;
                _ok = _channel.Send(_bsm);

            }
            catch (Exception e_)
            {
                MessageBox.Show("-102:" + e_.Message);
                //Console.WriteLine(e_.Message);
                return -102;
            }
            return (_ok ? vStr.Length : 0);
        }
        public static int Send(Beetle.IChannel _channel, byte[] vBuf, bool hasCrLf)
        {
            if (vBuf.Length < 1) return 0;
            if (_channel.IsDisposed) return -1;

            bool _ok = false;

            // 处理 CrLf
            List<byte> _buf = new List<byte>(vBuf);
            if (hasCrLf)
            {
                _buf.Add(10);
                _buf.Add(13);
            }

            try
            {
                Beetle.BytesMessage _bbm = new Beetle.BytesMessage();
                _bbm.Value = _buf.ToArray();
                _ok = _channel.Send(_bbm);
            }
            catch (Exception e_)
            {
                MessageBox.Show("-103:" + e_.Message);
                //Console.WriteLine(e_.Message);
                return -103;
            }
            return _ok ? _buf.Count : 0;
        }
    }
}
