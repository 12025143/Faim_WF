using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetleEx
{
    public class clsBLL_TCP2
    {
        public delegate void delConnected(bool blConnected, string IpPort);
        public event delConnected OnConnected;
        public delegate void delDataReceived(byte[] buf, string sStr, string IpPort);
        public event delDataReceived OnDataReceived;

        // 变量
        clsDAL_TCP m_dao = null;
        List<string> m_ErrorCodes = new List<string>() { "Fail", "Disconnect" };

        #region // 属性
        public bool Connected { get; set; } // 连接状态
        public int ErrorCode 
        {
            get; set; 
        }
        public bool hasEvent { get; set; } // 是否启用事件
        public bool hasCrLf  // 有回车换行
        {
            get {
                if (m_dao != null)
                    return m_dao.hasCrLf;
                else
                    return false;
            }
            set
            {
                if (m_dao != null)
                    m_dao.hasCrLf = value;
            }

        }
        public string Rcv_Data { get; set; }
        #endregion 

        #region // 初始化
        public clsBLL_TCP2()
        {
            InitMe();
        }
        void InitMe()
        {
            hasCrLf = true;
            //hasEvent = true;
            Connected = false;
            Rcv_Data = "";
            ErrorCode = -1;

            m_dao = new clsDAL_TCP();
            m_dao.hasCrLf = true;
            m_dao.OnConnect += new clsDAL_TCP.delConnect(_evt_Connected);
            m_dao.OnDataReceived += new clsDAL_TCP.delDataReceived(_evt_DataReceived);

        }
        #endregion

        #region // 事件
        // 连接状态

        void _evt_Connected(bool vblConnected, string vIpPort)
        {
            Connected = vblConnected;
            if (hasEvent) OnConnected(vblConnected, vIpPort);
        }
        // 接收数据
        void _evt_DataReceived(byte[] vBuf, string vStr, string IpPort)
        {
            Rcv_Data = vStr;
            if (hasEvent) OnDataReceived(vBuf, vStr, IpPort); 
        }
        #endregion


        public void do_Connect(string ip, int port)
        {
            m_dao.do_Connect(ip, port);
        }
        /* 
         * 参数: 发送的串, 比对的串, 比对门限(默认 20 ms), 错误码
         * 返回：接收到的值
         * 错误码：0：正确 1：比对错误  100：Fail发送失败 101：Disconnect连接断开 121：TCP未连接 122：超时
         * */
        public string do_SendData(string vSendStr, string vCompStr, int vTimeOut, ref int ErrCode)
        {
            if (!Connected)  // 21：TCP未连接    Disconnect连接断开
            {
                ErrCode = 121;
                return "";
            }          

            Rcv_Data = "";
            m_dao.Send(vSendStr);  // 发送数据

            // 延时取数据
            int _t1 = Environment.TickCount;
            while (Environment.TickCount - _t1 < vTimeOut * 0.98)
            {
                if (!string.IsNullOrEmpty(Rcv_Data)) break; // 0 收到
                System.Threading.Thread.Sleep(5);
            }
            //for (int i = 0; i < vTimeOut; i++) // 22：超时
            //{
            //    if (!string.IsNullOrEmpty(Rcv_Data)) break; // 0 收到
            //    System.Threading.Thread.Sleep(1);
            //}

            if (string.IsNullOrEmpty(Rcv_Data)) // 22：超时
                ErrCode = 122;
            else if (vSendStr.StartsWith(vCompStr))// 1：接收错误
                ErrCode = 1;
            else
                ErrCode = m_ErrorCodes.IndexOf(Rcv_Data) + 101; // 包含错误指令
            return Rcv_Data;
        }

        public void do_Disconnect()
        {
            m_dao.do_Disconnect(); 
        }

    }
}
