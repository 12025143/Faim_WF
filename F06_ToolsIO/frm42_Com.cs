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
    #region //
    using System.IO.Ports;
    using System.Threading;
    using System.IO;
    using nsFACC;
    #endregion
    public partial class frm42_Com : Form
    {
        bool _isLoad = false;
        int _rcvLen = 0;
        byte[] rcvBuffer = null;
        string sRecv = ""; // 收到的数据
        Dictionary<string, string> dict_echo = null;
        System.IO.Ports.SerialPort _client = null;
        System.Diagnostics.Stopwatch _tmr = null;
        #region // Load
        public frm42_Com()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _isLoad = true;
            _client = this.serialPort1;
            dict_echo = new Dictionary<string, string>();
            string _rec = clsBLL_UI.doEchoDict_Fill(ref dict_echo);
            this.txtEcho.Text = _rec;
            _tmr = new System.Diagnostics.Stopwatch();	//实例化一个计时器
            _tmr.Start();
            _isLoad = false;
        }
        #endregion
        //'连接
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (_client.IsOpen) return;
            _client.PortName = cbPort.Text;
            _client.BaudRate = Convert.ToInt32(cbBound.Text);
            if (_client.IsOpen) _client.Close();
            try
            {
                _client.Open();
            }
            catch (IOException ex)
            {
                //console.Write(ex.Message);
            }
            btnOpen.Enabled = !_client.IsOpen;
            btnClose.Enabled = _client.IsOpen;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_client.IsOpen) _client.Close();
            btnOpen.Enabled = !_client.IsOpen;
            btnClose.Enabled = _client.IsOpen;
            //_dev.do_Disconnect();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRecv.Text))
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "log\\sp_log.txt", txtRecv.Text);
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            do_Send(txtSend.Text);
        }
        void do_Send(string vMsg)
        {
            if (string.IsNullOrEmpty(vMsg)) return;
            if (ckASCII_out.Checked)
            {
                byte[] sndBuffer = ASCIIEncoding.Default.GetBytes(vMsg);
                _client.Write(sndBuffer, 0, sndBuffer.Length);
            }
            else
            {
                string[] _arr = vMsg.Split(' ');
                List<byte> _lst = new List<byte>();
                if (!F_Parse.IsNumeric(_arr[0]))
                {
                    byte[] sndBuffer = ASCIIEncoding.Default.GetBytes(vMsg);
                    _client.Write(sndBuffer, 0, sndBuffer.Length);
                }
                else
                {
                    for (int i = 0; i < _arr.Length; i++)
                    {
                        _lst.Add((byte)Convert.ToInt32(_arr[i], 16));
                    }
                    if (_lst.Count > 0)
                        _client.Write(_lst.ToArray(), 0, _lst.Count);
                }
            }
        }
        private void evt_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(20);
            _rcvLen = _client.BytesToRead;
            rcvBuffer = new byte[_rcvLen];
            _rcvLen = _client.Read(rcvBuffer, 0, rcvBuffer.Length);
            sRecv = clsBLL_UI.doBytesToString(rcvBuffer, _rcvLen, this.ckASCII_In.Checked);  // 结果
            if (_rcvLen > 0)   // 收到
            {
                if (this.txtRecv.Text.Length > Convert.ToInt32(txtShowLen.Text)) this.txtRecv.Text = "";
                if (ckCls.Checked || string.IsNullOrEmpty(this.txtRecv.Text)) // 自动清空
                    this.txtRecv.Text = sRecv;
                else
                    this.txtRecv.Text += sRecv;
                if (ckAuto.Checked) // 自动回复
                {
                    int _delay = 1;
                    if (!string.IsNullOrEmpty(this.txtDelay.Text))
                        _delay = Convert.ToInt32(this.txtDelay.Text);
                    Thread.Sleep(_delay);  // 回复延时
                    string _msg = sRecv.Replace("\r\n", "");
                    do_EchoDict_Change();
                    _msg = clsBLL_UI._ContainsKey(dict_echo, _msg);
                    if (string.IsNullOrEmpty(_msg)) // 协议回复
                    {
                        if (!string.IsNullOrEmpty(this.txtSend.Text))
                        {
                            _msg = this.txtSend.Text;
                        }
                    }
                    _msg = string.Format("{0}", _msg);
                    do_Send(_msg);
                }
            }
        }
        #region // Changed
        private void cbEchoTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            txtSend.Text = cbEchoTxt.Text;
        }
        private void txtEcho_TextChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            do_EchoDict_Change();
        }
        void do_EchoDict_Change()
        {
            if (dict_echo == null)
                dict_echo = new Dictionary<string, string>();
            dict_echo.Clear();
            string[] _arr = txtEcho.Lines;
            foreach (string _str in _arr)
            {
                if (string.IsNullOrEmpty(_str)) continue;
                string[] _arr2 = _str.Split('=');
                if (_arr2.Length < 2) continue;
                dict_echo.Add(_arr2[0], _arr2[1]);
            }
        }
        #endregion
        private void btnRefreshInfo_Click(object sender, EventArgs e)
        {
        }
        private void cbPort_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = !_client.IsOpen;
            btnClose.Enabled = _client.IsOpen;
        }
        private void cbBound_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = !_client.IsOpen;
            btnClose.Enabled = _client.IsOpen;
        }
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
        #endregion
    }
}
