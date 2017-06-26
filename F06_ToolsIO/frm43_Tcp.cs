#region //
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion
namespace FACC
{
    #region //...
    using System.IO.Ports;
    using System.Net.Sockets;
    using System.Net;
    using System.Threading;
    using nsFACC;
    using BeetleEx;
    using System.IO;
    #endregion

    public partial class frm43_Tcp : Form
    {
        #region //  int
        bool _isLoad = false;
        bool m_AllowListen = true;

        int m_MaxLen = 1024 * 1024;

        string m_IP = "";
        string m_Port = "";
        string sRecv = ""; // 收到的数据




        string _fn = @"tmp\ui_frm43_Tcp.txt";
        string m_curr_remote = "";

        Dictionary<string, string> dict_echo = null;

        Form _frm = null;
        clsBLL_SRV _bo_srv = new clsBLL_SRV();
        clsIOCount _cnt_rcv = new clsIOCount();
        clsIOCount _cnt_snd = new clsIOCount();
        #endregion

        #region // Load
        public frm43_Tcp()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void frm_Load(object sender, EventArgs e)
        {
            //_bo_srv.hasEvent = true;
            _isLoad = true;
            dict_echo = new Dictionary<string, string>();
            string _rec = clsBLL_UI.doEchoDict_Fill(ref dict_echo);
            this.lstCompair.Text = _rec;

            _bo_srv.OnConnect += new clsBLL_SRV.delConnected(_evt_Connected);
            _bo_srv.OnDataReceived += new clsBLL_SRV.delDataReceived(_evt_DataReceived);
            _isLoad = false;

            _doRefreshUI_ini();
        }

        private void frm_Closing(object sender, FormClosingEventArgs e)
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
                    #region //
                    case "ckAsciiRcev":
                        this.ckAsciiRcev.Checked = _arr2[1].Trim() == "1";
                        break;
                    case "ckAsciiSend":
                        this.ckAsciiSend.Checked = _arr2[1].Trim() == "1";
                        break;
                    case "ckClearRcev":
                        this.ckClearRcev.Checked = _arr2[1].Trim() == "1";
                        break;
                    case "ckShowStop":
                        this.ckShowStop.Checked = _arr2[1].Trim() == "1";
                        break;
                    case "ckEchoAuto":
                        this.ckEchoAuto.Checked = _arr2[1].Trim() == "1";
                        break;
                    case "ckEchoComp":
                        this.ckEchoComp.Checked = _arr2[1].Trim() == "1";
                        break;
                    case "ckCrLf":
                        this.ckCrLf.Checked = _arr2[1].Trim() == "1";
                        break;
                    #endregion
                    #region //
                    case "txtEcho":
                        this.txtSend.Text = _arr2[1].Trim();
                        break;
                    case "txtEchoDelay":
                        this.txtEchoDelay.Text = _arr2[1].Trim();
                        break;
                    case "cbEchoTxt":
                        this.cbEcho.Text = _arr2[1].Trim();
                        break;
                    case "cbIP":
                        this.cbIp.Text = _arr2[1].Trim();
                        break;
                    case "cbPort":
                        this.cbPort.Text = _arr2[1].Trim();
                        break;
                    case "txtShowLen":
                        this.txtShowLen.Text = _arr2[1].Trim();
                        break;
                    #endregion
                }
            }
        }

        void _doSaveUI_ini()
        {
            List<string> _lst = new List<string>();
            _lst.Add("ckAsciiRcev=" + (this.ckAsciiRcev.Checked ? "1" : "0"));
            _lst.Add("ckAsciiSend=" + (this.ckAsciiSend.Checked ? "1" : "0"));
            _lst.Add("ckClearRcev=" + (this.ckClearRcev.Checked ? "1" : "0"));
            _lst.Add("ckShowStop=" + (this.ckShowStop.Checked ? "1" : "0"));
            _lst.Add("ckEchoAuto=" + (this.ckEchoAuto.Checked ? "1" : "0"));
            _lst.Add("ckEchoComp=" + (this.ckEchoComp.Checked ? "1" : "0"));
            _lst.Add("ckCrLf=" + (this.ckCrLf.Checked ? "1" : "0"));

            _lst.Add("txtEcho=" + this.txtSend.Text);
            _lst.Add("txtEchoDelay=" + this.txtEchoDelay.Text);
            _lst.Add("cbEchoTxt=" + this.cbEcho.Text);
            _lst.Add("cbIP=" + this.cbIp.Text);
            _lst.Add("cbPort=" + this.cbPort.Text);
            _lst.Add("txtShowLen=" + this.txtShowLen.Text);
            File.WriteAllLines(_fn, _lst.ToArray());
        }
        #endregion
        #region // _evt
        bool _isModbus = false;
        void _evt_DataReceived(byte[] vBuf, string vStr, string vIdPort)
        {
            string _rcv = vStr;
            if (this.txtLogRecv.Text.Length > Convert.ToInt32(txtShowLen.Text)) this.txtLogRecv.Text = "";

            _isModbus = false;
            if (vBuf.Length > 6)
                _isModbus = (vBuf.Length > 6 && (vBuf[4] * 256 + vBuf[5]) == (vBuf.Length - 6));
            if (_isModbus)
            {
                vStr = BitConverter.ToString(vBuf);
            }
            if (ckClearRcev.Checked || string.IsNullOrEmpty(this.txtLogRecv.Text)) // 自动清空
                this.txtLogRecv.Text = vStr;
            else
                this.txtLogRecv.Text += vStr;

            if (ckEchoAuto.Checked) // 自动回复
            {
                #region // 回复延时
                if (!string.IsNullOrEmpty(this.txtEchoDelay.Text))
                    Thread.Sleep(Convert.ToInt32(this.txtEchoDelay.Text));  // 
                #endregion

                string _snd = this.cbEcho.Text.Trim();
                if (ckEchoComp.Checked) // 比对回复
                {
                    do_EchoDict_Change();
                    string _sVal = vStr.Replace("\r\n", "");  //  调整要比对的值


                    _sVal = clsBLL_UI._ContainsKey(dict_echo, _sVal);
                    if (!string.IsNullOrEmpty(_sVal)) // 
                    {
                        _snd = _sVal;
                    }
                }
                if (_isModbus)
                {
                    List<byte> _lst = new List<byte>();
                    if (vBuf.Length == 13 && vBuf[7] == 3)
                    {
                        for (int i = 0; i < 9; i++)
                            _lst.Add(vBuf[i]);  // 9个字节


                        for (int i = 0; i < vBuf[12]; i++)
                        {
                            _lst.Add((byte)(i + 1));  //                       
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 10; i++)
                            _lst.Add(vBuf[i]);  // 10个字节


                        _lst.AddRange(System.Text.ASCIIEncoding.Default.GetBytes(_snd)); // 数据
                    }
                    byte[] _arr = BitConverter.GetBytes(_lst.Count - 6); // 调整长度
                    _lst[4] = _arr[1];
                    _lst[5] = _arr[0];
                    _bo_srv.Send(vIdPort, _lst.ToArray());
                    _cnt_snd.Length = _lst.Count;
                    _snd = BitConverter.ToString(_lst.ToArray());
                }
                else
                {
                    _bo_srv.Send(vIdPort, _snd);
                    _cnt_snd.Length = _snd.Length;
                }
                // _cnt_snd.text
                _cnt_snd.Idx++;
                _cnt_snd.TotalLen += _cnt_snd.Length;
                this.lb_SendCount.Text = _cnt_snd.ToString();
                this.txtLogSend.Text = string.Format("[{0}] \r\n{1}", _cnt_snd.Length, _snd);// _msg;
            }
            _cnt_rcv.Idx++;
            _cnt_rcv.Length = vBuf.Length;
            _cnt_rcv.TotalLen += _cnt_rcv.Length;
            this.lb_RcvCount.Text = _cnt_rcv.ToString();
        }

        void _evt_Connected(bool blConnected, string IdPort)
        {
            _IdPort = IdPort;
            lbConnect.Text = blConnected ? "已连接 客户端" : " 断开 客户端";
            if (blConnected)
                listBox1.Items.Add(IdPort);
            else
                listBox1.Items.Remove(listBox1.Text);
            btnSend.Enabled = blConnected;
            //btnSend2.Enabled = blConnected;
        }
        #endregion

        #region // btn
        string _IdPort = "";
        // 启动侦听
        private void btnListen_Click(object sender, EventArgs e)
        {
            _IdPort = this.cbIp.Text + ":" + this.cbPort.Text;
            _bo_srv.Listen(this.cbIp.Text, Convert.ToInt32(cbPort.Text));

            lbConnect.Text = "启动侦听";
            btnListen.Enabled = false;
            btnStop.Enabled = true;
        }
        // 停止侦听
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_IdPort))
            {
                if (!string.IsNullOrEmpty(this.listBox1.Text))
                {
                    _bo_srv.Stop(this.listBox1.Text);
                }
            }
            else
            {
                _bo_srv.Stop(_IdPort);
                _IdPort = "";
            }
            lbConnect.Text = "停止侦听";
            btnListen.Enabled = true;
            btnStop.Enabled = false;
        }
        // 发送



        private void btnSend_Click(object sender, EventArgs e)
        {
            string _snd = this.txtSend.Text.Trim();
            if (string.IsNullOrEmpty(_snd)) return;
            int _Length = 0;
            if (ckAsciiSend.Checked)
            {
                _Length = _snd.Length;
                _bo_srv.Send(this.listBox1.Text, _snd);
            }
            else
            {
                string[] _arr = _snd.Split(' ');
                List<byte> _lst = new List<byte>();
                for (int i = 0; i < _arr.Length; i++)
                {
                    if (string.IsNullOrEmpty(_arr[i])) continue;
                    _lst.Add((byte)(Convert.ToInt32(_arr[i], 16)));
                }
                _Length = _lst.Count;
                _bo_srv.Send(this.listBox1.Text, _lst.ToArray());
            }

            _cnt_snd.Idx++;
            _cnt_snd.Length = _snd.Length;
            _cnt_snd.TotalLen += _cnt_snd.Length;
            this.lb_SendCount.Text = _cnt_snd.ToString();
            this.txtLogSend.Text = string.Format("[{0}] \r\n{1}", _Length, _snd);// _msg;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOpenComFrm_Click(object sender, EventArgs e)
        {
            if (_frm == null)
            {
                _frm = new frm42_Com();
            }
            try
            {
                _frm.Show();
            }
            catch (ObjectDisposedException)
            {
                _frm = new frm42_Com();
                _frm.Show();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (_frm == null)
            {
                _frm = new frm45_Tcp();
            }
            try
            {
                _frm.Show();
            }
            catch (ObjectDisposedException)
            {
                _frm = new frm45_Tcp();
                _frm.Show();
            }
        }
        #endregion

        #region // Changed
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            m_curr_remote = listBox1.Text;
        }
        private void cbEchoTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            txtSend.Text = cbEcho.Text;
        }
        private void txtEcho_TextChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
        }
        void do_EchoDict_Change()
        {
            dict_echo.Clear();
            string[] _arr = lstCompair.Lines;
            foreach (string _str in _arr)
            {
                if (string.IsNullOrEmpty(_str.Trim())) continue;
                string[] _arr2 = _str.Split('=');
                if (_arr2.Length > 1)
                    dict_echo.Add(_arr2[0].Trim(), _arr2[1].Trim());
            }
        }
        #endregion

        #region // 鼠标移动位置变量
        Point _mouseOff;//
        bool _leftFlag;//标签是否为左键






        private void frm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseOff = new Point(-e.X, -e.Y); //得到变量的值









                _leftFlag = true;                  //点击左键按下时标注为true;
            }
        }
        private void frm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(_mouseOff.X, _mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }
        private void frm_MouseUp(object sender, MouseEventArgs e)
        {
            if (_leftFlag)
            {
                _leftFlag = false;//释放鼠标后标注为false;
            }
        }
        private void frm_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            (new F_Win()).SetWindowRegion(this);
        }
        private void frm43_Tcp_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            txtLogSend.Text = "";
            _cnt_snd.Clear();
            this.lb_SendCount.Text = _cnt_snd.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtLogRecv.Text = "";
            _cnt_rcv.Clear();
            lb_RcvCount.Text = _cnt_rcv.ToString();
        }

    }
}
