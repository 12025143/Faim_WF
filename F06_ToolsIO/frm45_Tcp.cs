#region //
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#endregion

namespace FACC
{
    using BeetleEx;
    using System.IO;
    public partial class frm45_Tcp : Form
    {
        clsDAL_TCP _dao_tcp = new clsDAL_TCP();

        string _fn = @"tmp\ui_frm45_Tcp.txt";
        public frm45_Tcp()
        {
            InitializeComponent();
        }
        private void frm_Load(object sender, EventArgs e)
        {
            //_dao_tcp.hasCrLf = true;
            _dao_tcp.OnDataReceived += new clsDAL_TCP.delDataReceived(_evt_DataReceived);
            _dao_tcp.OnConnectd += new clsDAL_TCP.delConnectd(_evt_Connected);

            _doRefreshUI_ini();
        }
        private void frm45_Tcp_FormClosing(object sender, FormClosingEventArgs e)
        {
            _doSaveUI_ini();
        }

        void _doRefreshUI_ini()
        {
            if (!File.Exists(_fn)) return;
            string[] _arr1 = File.ReadAllLines(_fn);
            foreach (var item in _arr1)
            {
                string[] _arr2 = item.Split('=');
                switch (_arr2[0].Trim())
                {
                    case "txtIp":
                        this.txtIp.Text = _arr2[1].Trim();
                        break;
                    case "txtPort":
                        this.txtPort.Text = _arr2[1].Trim();
                        break;
                }
            }
        }
        void _doSaveUI_ini()
        {
            List<string> _lst = new List<string>();
            _lst.Add("txtIp=" + this.txtIp.Text);
            _lst.Add("txtPort=" + this.txtPort.Text);
            File.WriteAllLines(_fn, _lst.ToArray());
        }

        // 事件: 连接 参数：连接状态

        void _evt_Connected(bool blConnected, Beetle.ChannelEventArgs e)
        {
            this.Invoke(new Action<object>(o =>
                                        {
                                            _doRefreshUI_cn(blConnected);
                                        }),
                        new object()
                        );
        }

        // 事件: 数据流接收 参数: 接收的数据

        void _evt_DataReceived(byte[] buf, string sVal, Beetle.ChannelEventArgs e)
        {
            this.Invoke(new Action<string>(s =>
                                            {
                                                _doRefreshUI_rcv(sVal);
                                            }),
                        sVal
                        );
        }

        // 连接/断开 后，刷新界面
        void _doRefreshUI_cn(bool _bl)
        {
            cmdSend.Enabled = _bl;
            btnConnect.Enabled = !_bl;
            btnDisconnect.Enabled = _bl;
        }
        // 接收数据 后，刷新界面
        void _doRefreshUI_rcv(string sVal)
        {
            txtLogRecv.AppendText(sVal + "\r\n");
        }

        // 连接
        private void btnConnect_Click(object sender, EventArgs e)
        {
            bool _bl = _dao_tcp.do_Connect(txtIp.Text, Convert.ToInt32(txtPort.Text));
        }
        // 发送

        private void btnSend_Click(object sender, EventArgs e)
        {
            string _msg = txtLogSend.Text;//  +"\r\n";
            _dao_tcp.Send(_msg);
        }
        // 断开
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            _dao_tcp.do_Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _msg = txtLogSend.Text;//  +"\r\n";
            byte[] _buf = System.Text.ASCIIEncoding.Default.GetBytes(_msg);
            _dao_tcp.Send(_buf);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
