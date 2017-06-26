using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace nsFACC
{
    using System.Windows.Forms;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Net.NetworkInformation;
    using System.Net;
    public class F_Win
    {
        public void SetWindowRegion(Form frm, int radius = 16)
        {
            GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(-1, -1, frm.Width + 1, frm.Height);
            FormPath = GetRoundedRectPath(rect, radius);
            frm.Region = new System.Drawing.Region(FormPath);
        }
        GraphicsPath GetRoundedRectPath(System.Drawing.Rectangle rect, int radius = 16)
        {
            int diameter = radius;
            System.Drawing.Rectangle arcRect = new System.Drawing.Rectangle(rect.Location, new System.Drawing.Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //   左上角 
            path.AddArc(arcRect, 185, 90);
            //   右上角 
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 275, 90);
            //   右下角 
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 356, 90);
            //   左下角 
            arcRect.X = rect.Left;
            arcRect.Width += 2;
            arcRect.Height += 2;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }
        public static bool Ping(string IP, string data = "")
        {
            Ping _Ping = new Ping(); //构造Ping实例
            PingReply reply = null;
            if (string.IsNullOrEmpty(data))
            {
                reply = _Ping.Send(IP);
            }
            else
            {
                PingOptions _PingOptions = new PingOptions();   //Ping 选项设置
                _PingOptions.DontFragment = true;
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;  //设置超时时间
                //调用同步 send 方法发送数据,将返回结果保存至PingReply实例
                reply = _Ping.Send(IP, timeout, buffer, _PingOptions);
            }
            return reply.Status == IPStatus.Success;
            //if (reply.Status == IPStatus.Success)
            //{
            //lst_PingResult.Items.Add("答复的主机地址：" + reply.Address.ToString());
            //lst_PingResult.Items.Add("往返时间：" + reply.RoundtripTime);
            //lst_PingResult.Items.Add("生存时间（TTL）：" + reply.Options.Ttl);
            //lst_PingResult.Items.Add("是否控制数据包的分段：" + reply.Options.DontFragment);
            //lst_PingResult.Items.Add("缓冲区大小：" + reply.Buffer.Length);
            //}
        }
        public static bool isPortActive(int vPort)
        {
            bool _ret = false;
            IPGlobalProperties _IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] _TcpConnectionInformation = _IPGlobalProperties.GetActiveTcpConnections();
            foreach (var item in _TcpConnectionInformation)
            {
                if (item.RemoteEndPoint.Port!= vPort) continue;
                _ret = true;
                break;
            }
            //var _res = from n in _IPEndPoint
            //         where n.Port == vPort
            //         select n;
            //return (_res != null);
            //IPEndPoint[] _IPEndPoint = _IPGlobalProperties.GetActiveTcpListeners();//获取所有的监听连接
            //foreach (IPEndPoint endPoint in _IPEndPoint)
            //{
            //    if (endPoint.Port != vPort) continue;
            //    _ret = true;
            //    break;
            //}
            return _ret;
        }
    }
}
