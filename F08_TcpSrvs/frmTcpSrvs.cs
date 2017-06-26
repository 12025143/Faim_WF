#region // ...
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BeetleEx;
using System.IO;
#endregion 
namespace F08_TcpSrvs
{
    public partial class frmTcpSrvs : Form
    {
        #region //
        clsBLL_SRV _bo_srv = new clsBLL_SRV();
        clsIOCount _cnt_rcv = new clsIOCount();
        clsIOCount _cnt_snd = new clsIOCount();

        string _fn_ui = @"tmp\ui_frm08_TcpSrvs.txt"; 
        List<clsConnects> _lst = null;
        int _RowIndex = -1;
        string _imgPath = System.AppDomain.CurrentDomain.BaseDirectory;
        bool _isModbus = false;
        #endregion 
        #region //
        public frmTcpSrvs()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            gv.AutoGenerateColumns = true;
            gv.AllowUserToAddRows = false;
            gv.AllowUserToDeleteRows = false;

            _lst = clsConnects.doGetDatas(this.txtIP2.Text, Convert.ToInt32(txtPort2.Text), Convert.ToInt32(txtNumber.Text));
            this.gv.DataSource = _lst;

            int j = 0;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                j = i + 1;
                gv.Rows[i].HeaderCell.Value = j.ToString();
            }
            _bo_srv.OnConnect += new clsBLL_SRV.delConnected(_evt_srv_OnConnect);
            _bo_srv.OnDataReceived += new clsBLL_SRV.delDataReceived(_bo_srv_OnDataReceived);


            _doRefreshUI_ini();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Closing(object sender, FormClosingEventArgs e)
        {
            _doSaveUI_ini();
        }
        void _doRefreshUI_ini()
        {
            if (!File.Exists(_fn_ui)) return;
            string[] _arr1 = File.ReadAllLines(_fn_ui);
            foreach (var item in _arr1)
            {
                string[] _arr2 = item.Split('=');
                switch (_arr2[0].Trim())
                {
                    #region //
                    case "ckAsciiSend":
                        this.ckAsciiSend.Checked = _arr2[1].Trim() == "1";
                        break;
                    #endregion
                    #region //
                    case "txtIP":
                        this.txtIP.Text = _arr2[1].Trim();
                        break;
                    case "txtIP2":
                        this.txtIP2.Text = _arr2[1].Trim();
                        break;
                    case "txtPort":
                        this.txtPort.Text = _arr2[1].Trim();
                        break;
                    case "txtPort2":
                        this.txtPort2.Text = _arr2[1].Trim();
                        break;
                    case "txtNumber":
                        this.txtNumber.Text = _arr2[1].Trim();
                        break;
                    case "txtSend":
                        this.txtSend.Text = _arr2[1].Trim();
                        break;
                    #endregion
                }
            }
        }

        void _doSaveUI_ini()
        {
            List<string> _lst = new List<string>();
            _lst.Add("ckAsciiSend=" + (this.ckAsciiSend.Checked ? "1" : "0"));

            _lst.Add("txtIP=" + this.txtIP.Text);
            _lst.Add("txtIP2=" + this.txtIP2.Text);
            _lst.Add("txtPort=" + this.txtPort.Text);
            _lst.Add("txtPort2=" + this.txtPort2.Text);
            _lst.Add("txtNumber=" + this.txtNumber.Text);
            _lst.Add("txtSend=" + this.txtSend.Text);
            File.WriteAllLines(_fn_ui, _lst.ToArray());
        }
        #endregion 
        void _bo_srv_OnDataReceived(byte[] vBuf, string vStr, string vIdPort)
        {
            string _rcv = vStr;

            _isModbus = false;
            if (vBuf.Length > 6)
                _isModbus = (vBuf.Length > 6 && (vBuf[4] * 256 + vBuf[5]) == (vBuf.Length - 6));
            if (_isModbus)
                _rcv = BitConverter.ToString(vBuf).Replace('-', ' ');
            _rcv = string.Format("[{0}]\r\n {1}", vIdPort, _rcv);

            if (this.txtLogRecv.Text.Length > 3000)
                this.txtLogRecv.Text = "";
            if (string.IsNullOrEmpty(this.txtLogRecv.Text)) // 自动清空
            {
                this.txtLogRecv.Text = _rcv;
            }
            else
            {
                this.txtLogRecv.Text += _rcv;
            }
            _cnt_rcv.Idx++;
            _cnt_rcv.Length = vBuf.Length;
            _cnt_rcv.TotalLen += _cnt_rcv.Length;
            this.lb_RcvCount.Text = _cnt_rcv.ToString();
        }

        void _evt_srv_OnConnect(bool blConnected, string vIpPorts)
        {
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                string _key = string.Format("{0}:{1}:", gv.Rows[i].Cells["ip"].Value, gv.Rows[i].Cells["port"].Value);
                if (!vIpPorts.StartsWith(_key)) continue;

                string[] _arr = vIpPorts.Split(':');
                _RowIndex = i;
                clsConnects _en = _lst[_RowIndex];
                DataGridViewRow _dr = gv.Rows[_RowIndex];

                if (blConnected)
                {
                    _en.clientPort = _arr[2];
                    _en.isConnect = true;
                    _en.Connected = Image.FromFile(_imgPath + @"Images\grid\connect_16.png");
                }
                else
                {
                    _en.clientPort = "";
                    _en.isConnect = false;
                    _en.Connected = Image.FromFile(_imgPath + @"Images\grid\connect_16no.png");
                }
                gv.InvalidateRow(_RowIndex);


            }
        }
        void evt_ctrl_Click(object sender, EventArgs e)
        {
        }
        void doNewRow<T>(ref List<T> vLst)
        {
            if (vLst == null)
            {
                vLst = new List<T>();
            }
            vLst.Add((T)Activator.CreateInstance(typeof(T)));

            gv.Visible = false;
            gv.DataSource = new BindingList<T>(new List<T>());
            gv.DataSource = vLst;
        }

        private void gv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                _RowIndex = -1;
                return; // 内容行
            }
            string _fld = "";
            if (e.ColumnIndex > -1)
                _fld = gv.Columns[e.ColumnIndex].Name;
            if (_fld != "Listen")
            {
                _RowIndex = -2;
                return; // 操作列
            }
            _RowIndex = e.RowIndex;    // 当前行 gv.CurrentRow 
            clsConnects _en = _lst[_RowIndex];
            DataGridViewRow _dr = gv.Rows[_RowIndex];

            if (_en.isListen) // 正在侦听
            {
                _en.isListen = false;
                _en.Listen = Image.FromFile(_imgPath + @"Images\grid\listen_16no.png");
                //_dr.Cells["isListen"].Value = false;
                //_dr.Cells["Listen"].Value = Image.FromFile(_imgPath + @"Images\grid\listen_16no.png");
                _bo_srv.Stop(_en.ip, _en.port); // 停止侦听
            }
            else
            {
                _en.isListen = true;
                _en.Listen = Image.FromFile(_imgPath + @"Images\grid\listen_16.png");
                _bo_srv.Listen(_en.ip, _en.port); // 启动侦听
            }
            gv.InvalidateRow(_RowIndex);

            string _msg = string.Format("{0}", e.RowIndex + 1);
            this.lb_msg.Text = _msg;

        }

        private void gv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string _msg = string.Format("{0}", e.RowIndex + 1);
            this.lb_msg.Text = _msg;
            this.txtIP.Text = gv.Rows[e.RowIndex].Cells[3].Value.ToString();
            this.txtPort.Text = gv.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        //全部侦听
        private void btnListen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _lst.Count; i++)
            {
                _lst[i].isListen = true;
                _lst[i].Listen = Image.FromFile(_imgPath + @"Images\grid\listen_16.png");
                _bo_srv.Listen(_lst[i].ip, _lst[i].port);// 启动侦听
            }

            this.gv.DataSource = _lst;
            this.gv.Refresh();
        }
        //全部停止
        private void btnStop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _lst.Count; i++)
            {
                _lst[i].isListen = false;
                _lst[i].Listen = Image.FromFile(_imgPath + @"Images\grid\listen_16no.png");
                _bo_srv.Stop(_lst[i].ip, _lst[i].port);// 启动侦听
            }

            this.gv.DataSource = _lst;
            this.gv.Refresh();
        }
        //发送
        private void pbSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPort.Text) || string.IsNullOrWhiteSpace(txtIP.Text))
            {
                MessageBox.Show("请选择IP地址和端口号");
                return;
            }
            string _IpPorts = "";
            foreach (var item in _lst)
            {
                if (item.ip == txtIP.Text && item.port == Convert.ToInt32(txtPort.Text) && 
                    !string.IsNullOrEmpty(item.clientPort))
                {
                    _IpPorts = string.Format("{0}:{1}:{2}", item.ip, item.port, item.clientPort);
                    break;
                }
            }

            string _snd = this.txtSend.Text.Trim();
            if (string.IsNullOrEmpty(_snd)) return;

            int _Length = 0;
            if (ckAsciiSend.Checked)
            {
                _Length = _snd.Length;
                _bo_srv.Send(_IpPorts, _snd);
            }
            else
            {
                byte[] _buf = System.Text.ASCIIEncoding.Default.GetBytes(_snd); 
                //string[] _arr = _snd.Split(' ');
                //List<byte> _lst = new List<byte>();
                //for (int i = 0; i < _arr.Length; i++)
                //{
                //    if (string.IsNullOrEmpty(_arr[i])) continue;
                //    _lst.Add((byte)(Convert.ToInt32(_arr[i], 16)));
                //}
                //_Length = _lst.Count;
                _bo_srv.Send(_IpPorts, _buf);
            }
            _cnt_snd.Idx++;
            _cnt_snd.Length = _snd.Length;
            _cnt_snd.TotalLen += _cnt_snd.Length;
            this.lb_SendCount.Text = _cnt_snd.ToString();
            this.txtLogSend.Text += string.Format("[{0}] {1}\r\n", _Length, _snd);// _msg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int j = 0;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                j = i + 1;
                gv.Rows[i].HeaderCell.Value = j.ToString();
            }
            _lst = clsConnects.doGetDatas(this.txtIP2.Text, Convert.ToInt32(txtPort2.Text), Convert.ToInt32(txtNumber.Text));
            gv.DataSource = _lst;
            this.gv.Refresh();
            //gv.Invalidate(); 
        }

    }


    public class clsConnects
    {
        static string _imgPath = Environment.CurrentDirectory + @"\";

        public string id { get; set; }
        public Image Listen { get; set; }
        public Image Connected { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public string clientPort { get; set; }
        public bool isListen { get; set; }
        public bool isConnect { get; set; }
        public string img { get; set; }
        public clsConnects()
        {
            id = "";
            ip = "";
            port = 0;
            isConnect = false;
            isListen = false;
            img = "";
        }
        public static List<clsConnects> doGetDatas(string vIP, int vPort = 5000, int vNumber = 50)
        {
            List<clsConnects> _lst = new List<clsConnects>();
            clsConnects _en = null;
            for (int i = 0; i < vNumber; i++)
            {
                _en = new clsConnects();
                _en.isListen = false;
                _en.ip = vIP;// "192.168.56.1";
                _en.port = vPort + i;
                _en.id = i.ToString();
                _en.isConnect = false;
                _en.Listen = Image.FromFile(_imgPath + @"Images\grid\listen_16no.png");

                _en.Connected = Image.FromFile(_imgPath + @"Images\grid\connect_16no.png");
                _lst.Add(_en);

            }
            return _lst;
        }
    }
}
