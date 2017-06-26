using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BeetleEx
{
    public class clsBLL_SRV
    {
        public delegate void delConnected(bool blConnected, string vIpPorts);
        public event delConnected OnConnect;
        public delegate void delDataReceived(byte[] buf, string sVal, string vIpPorts);
        public event delDataReceived OnDataReceived;

        clsDAL_SRV _srv = new clsDAL_SRV();
        Dictionary<string, Beetle.IChannel> dict_Channel_Listen = new Dictionary<string, Beetle.IChannel>();
        Dictionary<string, Beetle.IChannel> dict_Channel_Connect = new Dictionary<string, Beetle.IChannel>();

        public bool hasEvent { get; set; } // 是否启用事件
        public bool hasCrLf // 有回车换行
        {
            get { return _srv.hasCrLf; }
            set { _srv.hasCrLf = value; }
        }
        public string Rcv_Data { get; set; }

        public clsBLL_SRV()
        {
            hasEvent = true;
            _srv.OnConnect += new clsDAL_SRV.delConnected(_evt_Connected);
            _srv.OnDataReceived += new clsDAL_SRV.delDataReceived(_evt_DataReceived);
        }


        protected void _evt_Connected(bool blConnected, Beetle.ChannelEventArgs e)
        {
            string _IpPorts = string.Format("{0}:{1}",                 
                                            e.Channel.Socket.LocalEndPoint.ToString(), 
                                            e.Channel.EndPoint.Port.ToString());
            if (blConnected)
            {
                if (!dict_Channel_Connect.ContainsKey(_IpPorts))
                    dict_Channel_Connect.Add(_IpPorts, e.Channel);
            }
            else
            {
                if (dict_Channel_Connect.ContainsKey(_IpPorts))
                {
                    dict_Channel_Connect.Remove(_IpPorts);
                }
            }
            if (hasEvent) OnConnect(blConnected, _IpPorts);//e.Channel.Socket.LocalEndPoint.ToString());
        }
        public void Listen(string ip, int port)
        {
            if (!dict_Channel_Listen.ContainsKey(string.Format("{0}:{1}", ip, port)))
            {
                try
                {
                    _srv.Open(ip, port); // ServerBase.Open
                }
                catch (System.Net.Sockets.SocketException _ex)
                {
                    MessageBox.Show(_ex.Message);
                }
                catch (Exception _ex)
                {
                    MessageBox.Show(_ex.Message);
                }
            }
        }
        public void Listen(string vIpPorts)
        {
            string[] _arr = vIpPorts.Split(':');
            Listen(_arr[0], Convert.ToInt16(_arr[1]));
        }
        public void Stop(string ip, int port)
        {
            string _IpPorts = string.Format("{0}:{1}", ip, port);
            if (dict_Channel_Listen.ContainsKey(_IpPorts))
            {
                Beetle.IChannel _channel = dict_Channel_Listen[_IpPorts];
                if (_channel.Socket != null)
                    _channel.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                //    _channel.Socket.Disconnect(true);  
                // _srv.Server.Socket.Disconnect(true);                   
                if (_channel.Server != null) _channel.Server.Dispose();  //
                _channel.Dispose();         //  
                dict_Channel_Listen.Remove(_IpPorts);
            }
            else if (!string.IsNullOrEmpty(_IpPorts))
            {
                //_srv.Server.Socket.Disconnect(true);
                if (_srv.Server != null) _srv.Server.Dispose();
                _srv.Dispose();
            }
            else
            {
                MessageBox.Show("Stop(" + _IpPorts + ")");
            }
        }
        public void Stop(string vIpPorts)
        {
            string[] _arr = vIpPorts.Split(':');
            Stop(_arr[0],Convert.ToInt32(_arr[1]));
            
        }

        protected void _evt_DataReceived(byte[] vBuf, string vStr, Beetle.ChannelEventArgs e)
        {
            if (hasEvent) OnDataReceived(vBuf, vStr,
                                    string.Format("{0}:{1}", 
                                                e.Channel.Socket.LocalEndPoint.ToString(),
                                                e.Channel.EndPoint.Port.ToString()));
            //this.Invoke(new Action<string>(s =>
            //                                {
            //                                    _do_DataReceive(e.Data.Array, _value);
            //                                }),
            //            _value
            //            );
            Rcv_Data = vStr;
        }
        //public int Send(string ip, int port, string vStr)
        //{
        //    string _IpPort = ip + ":" + port.ToString();
        //    return Send(_IpPort, vStr);
        //}
        public int Send(string vIpPort, string vStr)
        {
            if (dict_Channel_Connect.ContainsKey(vIpPort))
            {
                return clsDAL_Pub.Send(dict_Channel_Connect[vIpPort], vStr, hasCrLf);
            }
            return 0;
        }
        //public int Send(string ip, int port, byte[] vBuf)
        //{
        //    return Send(ip + ":" + port.ToString(), vBuf);
        //}
        public int Send(string vIpPort, byte[] vBuf)
        {
            if (dict_Channel_Connect.ContainsKey(vIpPort))
            {
                return clsDAL_Pub.Send(dict_Channel_Connect[vIpPort], vBuf, hasCrLf);
            }
            return 0;
        }
    }
}
