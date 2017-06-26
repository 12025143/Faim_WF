#region // ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion
namespace BeetleEx
{
    public class clsDAL_TCP : Beetle.ServerBase
    {

        public delegate void delConnect(bool blConnected, string IpPort);
        public event delConnect OnConnect;
        public delegate void delDataReceived(byte[] buf, string sVal, string IpPort);
        public event delDataReceived OnDataReceived;

        Beetle.IChannel m_channel = null;

        int Count = 0;
        bool _Connected = false;
        public bool Connected // 连接状态
        {
            get
            {
                return _Connected;
            }
            set
            {
                _Connected = value;
            }
        }
        public int ErrorCode { get; set; }
        public bool hasCrLf { get; set; } // 有回车换行

        public bool hasEvent { get; set; } // 是否启用事件
        public string IP { get; set; }
        public int Port { get; set; }
        public string Rcv_Data { get; set; }

        public clsDAL_TCP()
        {

            IP = "";
            Port = 0;
            ErrorCode = -1;
            hasEvent = true;
            hasCrLf = false;
            Beetle.TcpUtils.Setup("beetle");//初始化组件

        }
        // 连接事件，不触发？
        protected override void OnConnected(object sender, Beetle.ChannelEventArgs e)
        {
            base.OnConnected(sender, e);
            //m_channel = e.Channel;
            _Connected = m_channel.Socket.Connected && !m_channel.IsDisposed;
            //if (hasEvent) OnConnectd(_Connected, e);
        }
        // 连接方法主动发连接事件
        public bool do_Connect(string ip, int port)
        {
            if (_Connected) return true;
            IP = ip;
            Port = port;
            try
            {
                m_channel = Beetle.TcpServer.CreateClient(ip, port); // 连接到指定IP的端口服务
                System.Threading.Thread.Sleep(100);

                m_channel.ChannelDisposed += _evt_Disposed; // 连接断开事件
                m_channel.DataReceive = _evt_DataReceived; // 绑定数据流接收事件   
                m_channel.ChannelError += (o, err) => // 组件错误
                {
                    Console.WriteLine(err.Exception.Message);
                };
                m_channel.BeginReceive();  // 开始接收数据
                _Connected = m_channel.Socket.Connected; // ;
                if (_Connected)
                {
                    IP = ip;
                    Port = port;
                    if (hasEvent) OnConnect(_Connected, string.Format("{0}:{1}", IP, Port));
                }
                return _Connected;// m_channel.Socket.Connected;
            }
            catch (System.Net.Sockets.SocketException ex_)
            {
                ErrorCode = -101;
                // MessageBox.Show("-101: 请确认[" + ip + ":" + port.ToString() + "]服务器已打开. ");
                Console.WriteLine(ex_.Message); // 由于目标计算机积极拒绝，无法连接
            }
            catch (Exception e_)
            {
                ErrorCode = -100;
                MessageBox.Show("-100:" + e_.Message);
                //Console.WriteLine(e_.Message);
            }
            return false;
        }
        // 连接断开事件
        protected void _evt_Disposed(object sender, Beetle.ChannelDisposedEventArgs e)
        {
            _Connected = m_channel.Socket.Connected && !m_channel.IsDisposed;
            if (hasEvent) OnConnect(_Connected, string.Format("{0}:{1}", IP, Port));
            IP = "";
            Port = 0;
            //Invoke(new Action<object>(o =>
            //                        {
            //                            _do_Connected();
            //                        }),
            //        new object());
        }
        public void do_Disconnect()
        {
            if (m_channel == null) return;
            if (m_channel.Socket == null) return;

            if (!(m_channel.Socket.Connected && !m_channel.IsDisposed)) return;
            m_channel.Dispose();
        }
        public int Send(byte[] vBuf, bool vhasCrLf = true)
        {
            if (!_Connected) return 0;
            return clsDAL_Pub.Send(m_channel, vBuf, vhasCrLf);
        }
        //public int Send(byte[] vBuf)
        //{
        //    if (!_Connected) return 0;
        //    return clsDAL_Pub.Send(m_channel, vBuf, hasCrLf);
        //}
        public int Send(string vStr, bool vhasCrLf = true)
        {
            if (!_Connected) return 0;
            return clsDAL_Pub.Send(m_channel, vStr, vhasCrLf);
        }
        //public int Send(string vStr)
        //{
        //    if (!_Connected) return 0;
        //    return clsDAL_Pub.Send(m_channel, vStr, hasCrLf);
        //}

        // 连接断开事件
        // 绑定数据流接收事件 
        protected void _evt_DataReceived(object sender, Beetle.ChannelReceiveEventArgs e)
        {
            System.Threading.Interlocked.Increment(ref Count);
            string _str = e.Channel.Coding.GetString(e.Data.Array, e.Data.Offset, e.Data.Count);
            byte[] _buf = new byte[e.Data.Count];
            Array.Copy(e.Data.Array, 0, _buf, 0, Math.Min(e.Data.Array.Length, _buf.Length));
            if (hasEvent) OnDataReceived(_buf, _str, string.Format("{0}:{1}", IP, Port));  // e.Data.Array
            //this.Invoke(new Action<string>(s =>
            //                                {
            //                                    _do_DataReceive(e.Data.Array, _value);
            //                                }),
            //            _value
            //            );
            Rcv_Data = _str;// clsDAL_Pub.doBytesToString(_buf, _buf.Length, false);

        }

    }
}
