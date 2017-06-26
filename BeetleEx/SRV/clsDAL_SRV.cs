using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BeetleEx
{
    public class clsDAL_SRV : Beetle.ServerBase
    {
        int Count = 0;
        Beetle.IChannel m_channel = null;


        public delegate void delConnected(bool blConnected, Beetle.ChannelEventArgs e);
        public event delConnected OnConnect;
        public delegate void delDataReceived(byte[] buf, string sVal, Beetle.ChannelEventArgs e);
        public event delDataReceived OnDataReceived;

        public bool hasCrLf { get; set; } // 有回车换行
        public bool hasEvent { get; set; } // 是否启用事件

        public string Rcv_Data { get; set; }

        public clsDAL_SRV()
        {
            hasEvent = true;
            Beetle.TcpUtils.Setup("beetle"); // 初始化组件   
        }

        protected override void OnConnected(object sender, Beetle.ChannelEventArgs e)
        {
            base.OnConnected(sender, e);
            m_channel = e.Channel;
            if (hasEvent) OnConnect(true, e);
        }
        protected override void OnDisposed(object sender, Beetle.ChannelDisposedEventArgs e)
        {
            base.OnDisposed(sender, e);
            m_channel = null;
            if (hasEvent) OnConnect(false, (Beetle.ChannelEventArgs)e);
        }

        public int Send(byte[] vBuf)
        {
            return clsDAL_Pub.Send(m_channel, vBuf, hasCrLf);
        }
        public int Send(string vStr)
        {
            return clsDAL_Pub.Send(m_channel, vStr, hasCrLf);
        }

        protected override void OnReceive(object sender, Beetle.ChannelReceiveEventArgs e)
        {
            System.Threading.Interlocked.Increment(ref Count);
            string _str = e.Channel.Coding.GetString(e.Data.Array, e.Data.Offset, e.Data.Count);
            byte[] _buf = new byte[e.Data.Count];
            Array.Copy(e.Data.Array, 0, _buf, 0, Math.Min(e.Data.Array.Length, _buf.Length));
            if (hasEvent) OnDataReceived(_buf, _str, (Beetle.ChannelEventArgs)e);
            Rcv_Data = _str;
        }

        protected override void OnError(object sender, Beetle.ChannelErrorEventArgs e)
        {
            base.OnError(sender, e);
            //C.WriteLine("{0} 出错 {1}!", e.Channel.EndPoint, e.Exception.Message);
        }
    }
}
