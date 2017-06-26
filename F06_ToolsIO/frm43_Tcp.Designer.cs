namespace FACC
{
    using nsFACC;
    partial class frm43_Tcp
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm43_Tcp));
            this.txtLogRecv = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.lbport = new System.Windows.Forms.Label();
            this.ckEchoAuto = new System.Windows.Forms.CheckBox();
            this.ckClearRcev = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEchoDelay = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckAsciiRcev = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ckCrLf = new System.Windows.Forms.CheckBox();
            this.cbEcho = new System.Windows.Forms.ComboBox();
            this.lstCompair = new System.Windows.Forms.TextBox();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.cbIp = new System.Windows.Forms.ComboBox();
            this.txtDevNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLogSend = new System.Windows.Forms.TextBox();
            this.ckShowStop = new System.Windows.Forms.CheckBox();
            this.txtShowLen = new System.Windows.Forms.TextBox();
            this.btnOprnComFrm = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnListen = new System.Windows.Forms.Button();
            this.ckAsciiSend = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.ckEchoComp = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.btnClearRecv = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClearSend = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_RcvCount = new System.Windows.Forms.Label();
            this.lb_SendCount = new System.Windows.Forms.Label();
            this.lbConnect = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLogRecv
            // 
            this.txtLogRecv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtLogRecv.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLogRecv.Location = new System.Drawing.Point(8, 31);
            this.txtLogRecv.Multiline = true;
            this.txtLogRecv.Name = "txtLogRecv";
            this.txtLogRecv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogRecv.Size = new System.Drawing.Size(349, 358);
            this.txtLogRecv.TabIndex = 2;
            // 
            // txtSend
            // 
            this.txtSend.BackColor = System.Drawing.Color.White;
            this.txtSend.Location = new System.Drawing.Point(8, 513);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSend.Size = new System.Drawing.Size(297, 29);
            this.txtSend.TabIndex = 2;
            this.txtSend.Text = "OK";
            // 
            // lbport
            // 
            this.lbport.AutoSize = true;
            this.lbport.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbport.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbport.Location = new System.Drawing.Point(363, 10);
            this.lbport.Name = "lbport";
            this.lbport.Size = new System.Drawing.Size(21, 14);
            this.lbport.TabIndex = 3;
            this.lbport.Text = "IP";
            // 
            // ckEchoAuto
            // 
            this.ckEchoAuto.AutoSize = true;
            this.ckEchoAuto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckEchoAuto.Checked = true;
            this.ckEchoAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckEchoAuto.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckEchoAuto.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckEchoAuto.Location = new System.Drawing.Point(359, 153);
            this.ckEchoAuto.Name = "ckEchoAuto";
            this.ckEchoAuto.Size = new System.Drawing.Size(82, 18);
            this.ckEchoAuto.TabIndex = 4;
            this.ckEchoAuto.Text = "自动回复";
            this.ckEchoAuto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckEchoAuto.UseVisualStyleBackColor = true;
            // 
            // ckClearRcev
            // 
            this.ckClearRcev.AutoSize = true;
            this.ckClearRcev.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckClearRcev.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckClearRcev.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckClearRcev.Location = new System.Drawing.Point(360, 86);
            this.ckClearRcev.Name = "ckClearRcev";
            this.ckClearRcev.Size = new System.Drawing.Size(82, 18);
            this.ckClearRcev.TabIndex = 4;
            this.ckClearRcev.Text = "清空接收";
            this.ckClearRcev.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(488, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "回复延时(毫秒)";
            // 
            // txtEchoDelay
            // 
            this.txtEchoDelay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEchoDelay.Location = new System.Drawing.Point(627, 148);
            this.txtEchoDelay.Name = "txtEchoDelay";
            this.txtEchoDelay.Size = new System.Drawing.Size(64, 21);
            this.txtEchoDelay.TabIndex = 6;
            this.txtEchoDelay.Text = "1";
            this.txtEchoDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(363, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "端口号";
            // 
            // ckAsciiRcev
            // 
            this.ckAsciiRcev.AutoSize = true;
            this.ckAsciiRcev.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckAsciiRcev.Checked = true;
            this.ckAsciiRcev.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAsciiRcev.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckAsciiRcev.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckAsciiRcev.Location = new System.Drawing.Point(8, 13);
            this.ckAsciiRcev.Name = "ckAsciiRcev";
            this.ckAsciiRcev.Size = new System.Drawing.Size(96, 18);
            this.ckAsciiRcev.TabIndex = 10;
            this.ckAsciiRcev.Text = "接收 ASCII";
            this.ckAsciiRcev.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(516, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(173, 88);
            this.listBox1.TabIndex = 11;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // ckCrLf
            // 
            this.ckCrLf.AutoSize = true;
            this.ckCrLf.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckCrLf.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckCrLf.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckCrLf.Location = new System.Drawing.Point(352, 173);
            this.ckCrLf.Name = "ckCrLf";
            this.ckCrLf.Size = new System.Drawing.Size(89, 18);
            this.ckCrLf.TabIndex = 12;
            this.ckCrLf.Text = " 回车换行";
            this.ckCrLf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckCrLf.UseVisualStyleBackColor = true;
            // 
            // cbEcho
            // 
            this.cbEcho.BackColor = System.Drawing.Color.White;
            this.cbEcho.ForeColor = System.Drawing.Color.Black;
            this.cbEcho.FormattingEnabled = true;
            this.cbEcho.Items.AddRange(new object[] {
            "OK",
            "FAIL",
            "PASS",
            ""});
            this.cbEcho.Location = new System.Drawing.Point(570, 169);
            this.cbEcho.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cbEcho.Name = "cbEcho";
            this.cbEcho.Size = new System.Drawing.Size(121, 24);
            this.cbEcho.TabIndex = 13;
            this.cbEcho.Tag = "cbEchoTxt";
            this.cbEcho.Text = "OK";
            this.cbEcho.SelectedIndexChanged += new System.EventHandler(this.cbEchoTxt_SelectedIndexChanged);
            // 
            // lstCompair
            // 
            this.lstCompair.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lstCompair.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstCompair.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstCompair.ForeColor = System.Drawing.Color.Black;
            this.lstCompair.Location = new System.Drawing.Point(366, 194);
            this.lstCompair.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lstCompair.Multiline = true;
            this.lstCompair.Name = "lstCompair";
            this.lstCompair.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lstCompair.Size = new System.Drawing.Size(325, 382);
            this.lstCompair.TabIndex = 14;
            this.lstCompair.Tag = "txtEcho";
            this.lstCompair.Text = "POS=DONEX10Y20Z30O40A50T60E\r\n?1=Pass1uA100V200\r\n?2=Pass22A300m400\r\n?3=Pass33V500m" +
                "A600\r\n?4=Pass44V700M555M800\r\nS8=X910Y920Z930C940E";
            this.lstCompair.TextChanged += new System.EventHandler(this.txtEcho_TextChanged);
            // 
            // cbPort
            // 
            this.cbPort.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Items.AddRange(new object[] {
            "5000",
            "6000",
            "7000",
            "8000",
            "9000"});
            this.cbPort.Location = new System.Drawing.Point(428, 34);
            this.cbPort.Margin = new System.Windows.Forms.Padding(4);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(57, 22);
            this.cbPort.TabIndex = 66;
            this.cbPort.Text = "5000";
            // 
            // cbIp
            // 
            this.cbIp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbIp.FormattingEnabled = true;
            this.cbIp.Items.AddRange(new object[] {
            "192.168.56.1",
            "192.168.40.56",
            "192.168.40.15",
            "192.168.40.1",
            "10.0.2.15"});
            this.cbIp.Location = new System.Drawing.Point(428, 7);
            this.cbIp.Margin = new System.Windows.Forms.Padding(4);
            this.cbIp.Name = "cbIp";
            this.cbIp.Size = new System.Drawing.Size(132, 22);
            this.cbIp.TabIndex = 67;
            this.cbIp.Text = "192.168.56.1";
            // 
            // txtDevNo
            // 
            this.txtDevNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDevNo.Location = new System.Drawing.Point(893, 257);
            this.txtDevNo.Name = "txtDevNo";
            this.txtDevNo.Size = new System.Drawing.Size(36, 23);
            this.txtDevNo.TabIndex = 69;
            this.txtDevNo.Text = "1";
            this.txtDevNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(890, 296);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 68;
            this.label8.Text = "设备号";
            // 
            // txtLogSend
            // 
            this.txtLogSend.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLogSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogSend.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLogSend.ForeColor = System.Drawing.Color.Black;
            this.txtLogSend.Location = new System.Drawing.Point(9, 390);
            this.txtLogSend.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtLogSend.Multiline = true;
            this.txtLogSend.Name = "txtLogSend";
            this.txtLogSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogSend.Size = new System.Drawing.Size(349, 122);
            this.txtLogSend.TabIndex = 71;
            this.txtLogSend.Tag = "txtEcho";
            // 
            // ckShowStop
            // 
            this.ckShowStop.AutoSize = true;
            this.ckShowStop.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckShowStop.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckShowStop.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckShowStop.Location = new System.Drawing.Point(360, 106);
            this.ckShowStop.Name = "ckShowStop";
            this.ckShowStop.Size = new System.Drawing.Size(82, 18);
            this.ckShowStop.TabIndex = 12;
            this.ckShowStop.Text = "停止显示";
            this.ckShowStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckShowStop.UseVisualStyleBackColor = true;
            // 
            // txtShowLen
            // 
            this.txtShowLen.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShowLen.Location = new System.Drawing.Point(428, 59);
            this.txtShowLen.Name = "txtShowLen";
            this.txtShowLen.Size = new System.Drawing.Size(57, 23);
            this.txtShowLen.TabIndex = 6;
            this.txtShowLen.Text = "3000";
            // 
            // btnOprnComFrm
            // 
            this.btnOprnComFrm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOprnComFrm.FlatAppearance.BorderSize = 0;
            this.btnOprnComFrm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOprnComFrm.ForeColor = System.Drawing.Color.Gray;
            this.btnOprnComFrm.Image = global::nsFACC.Properties.Resources.Port_24px_530255_easyicon_net;
            this.btnOprnComFrm.Location = new System.Drawing.Point(693, 477);
            this.btnOprnComFrm.Name = "btnOprnComFrm";
            this.btnOprnComFrm.Size = new System.Drawing.Size(42, 45);
            this.btnOprnComFrm.TabIndex = 7;
            this.btnOprnComFrm.Tag = "";
            this.toolTip1.SetToolTip(this.btnOprnComFrm, "串口工具");
            this.btnOprnComFrm.UseVisualStyleBackColor = false;
            this.btnOprnComFrm.Click += new System.EventHandler(this.btnOpenComFrm_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSend.Enabled = false;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.ForeColor = System.Drawing.Color.Gray;
            this.btnSend.Image = global::nsFACC.Properties.Resources.system_run_5_32px_540096_easyicon_net;
            this.btnSend.Location = new System.Drawing.Point(315, 506);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(42, 45);
            this.btnSend.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnSend, "发送");
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Gray;
            this.btnExit.Image = global::nsFACC.Properties.Resources.application_exit_32px_539518_easyicon2;
            this.btnExit.Location = new System.Drawing.Point(693, 537);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(42, 45);
            this.btnExit.TabIndex = 1;
            this.btnExit.Tag = "关闭";
            this.toolTip1.SetToolTip(this.btnExit, "关闭窗口");
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnStop.Enabled = false;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.ForeColor = System.Drawing.Color.Gray;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(690, 46);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(42, 45);
            this.btnStop.TabIndex = 1;
            this.btnStop.Tag = "关";
            this.toolTip1.SetToolTip(this.btnStop, "停止侦听");
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnListen
            // 
            this.btnListen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnListen.FlatAppearance.BorderSize = 0;
            this.btnListen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListen.ForeColor = System.Drawing.Color.Gray;
            this.btnListen.Image = ((System.Drawing.Image)(resources.GetObject("btnListen.Image")));
            this.btnListen.Location = new System.Drawing.Point(695, 10);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(29, 31);
            this.btnListen.TabIndex = 1;
            this.btnListen.Tag = "侦听";
            this.toolTip1.SetToolTip(this.btnListen, "侦听");
            this.btnListen.UseVisualStyleBackColor = false;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // ckAsciiSend
            // 
            this.ckAsciiSend.AutoSize = true;
            this.ckAsciiSend.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckAsciiSend.Checked = true;
            this.ckAsciiSend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAsciiSend.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckAsciiSend.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckAsciiSend.Location = new System.Drawing.Point(8, 544);
            this.ckAsciiSend.Name = "ckAsciiSend";
            this.ckAsciiSend.Size = new System.Drawing.Size(96, 18);
            this.ckAsciiSend.TabIndex = 10;
            this.ckAsciiSend.Text = "发出 ASCII";
            this.ckAsciiSend.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(725, 250);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 27);
            this.btnSave.TabIndex = 73;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            // 
            // ckEchoComp
            // 
            this.ckEchoComp.AutoSize = true;
            this.ckEchoComp.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckEchoComp.Checked = true;
            this.ckEchoComp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckEchoComp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckEchoComp.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckEchoComp.Location = new System.Drawing.Point(487, 173);
            this.ckEchoComp.Name = "ckEchoComp";
            this.ckEchoComp.Size = new System.Drawing.Size(82, 18);
            this.ckEchoComp.TabIndex = 4;
            this.ckEchoComp.Text = "配对回复";
            this.ckEchoComp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckEchoComp.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Gray;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(693, 426);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(42, 45);
            this.button3.TabIndex = 7;
            this.button3.Tag = "";
            this.toolTip1.SetToolTip(this.button3, "TCP客户端工具");
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnClearRecv
            // 
            this.btnClearRecv.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearRecv.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClearRecv.Location = new System.Drawing.Point(156, 368);
            this.btnClearRecv.Name = "btnClearRecv";
            this.btnClearRecv.Size = new System.Drawing.Size(53, 21);
            this.btnClearRecv.TabIndex = 74;
            this.btnClearRecv.Text = "清空";
            this.btnClearRecv.UseVisualStyleBackColor = true;
            this.btnClearRecv.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(153, 568);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "0602";
            // 
            // btnClearSend
            // 
            this.btnClearSend.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearSend.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClearSend.Location = new System.Drawing.Point(156, 491);
            this.btnClearSend.Name = "btnClearSend";
            this.btnClearSend.Size = new System.Drawing.Size(53, 21);
            this.btnClearSend.TabIndex = 74;
            this.btnClearSend.Text = "清空";
            this.btnClearSend.UseVisualStyleBackColor = true;
            this.btnClearSend.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(362, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "显示长度";
            // 
            // lb_RcvCount
            // 
            this.lb_RcvCount.AutoSize = true;
            this.lb_RcvCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_RcvCount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lb_RcvCount.Location = new System.Drawing.Point(123, 14);
            this.lb_RcvCount.Name = "lb_RcvCount";
            this.lb_RcvCount.Size = new System.Drawing.Size(42, 14);
            this.lb_RcvCount.TabIndex = 5;
            this.lb_RcvCount.Text = "0 0/0";
            // 
            // lb_SendCount
            // 
            this.lb_SendCount.AutoSize = true;
            this.lb_SendCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_SendCount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lb_SendCount.Location = new System.Drawing.Point(123, 544);
            this.lb_SendCount.Name = "lb_SendCount";
            this.lb_SendCount.Size = new System.Drawing.Size(42, 14);
            this.lb_SendCount.TabIndex = 5;
            this.lb_SendCount.Text = "0 0/0";
            // 
            // lbConnect
            // 
            this.lbConnect.AutoSize = true;
            this.lbConnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbConnect.ForeColor = System.Drawing.Color.Red;
            this.lbConnect.Location = new System.Drawing.Point(576, 9);
            this.lbConnect.Name = "lbConnect";
            this.lbConnect.Size = new System.Drawing.Size(62, 16);
            this.lbConnect.TabIndex = 90;
            this.lbConnect.Text = "------";
            // 
            // frm43_Tcp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(739, 589);
            this.Controls.Add(this.lbConnect);
            this.Controls.Add(this.btnClearSend);
            this.Controls.Add(this.btnClearRecv);
            this.Controls.Add(this.txtLogSend);
            this.Controls.Add(this.txtDevNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbIp);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.lstCompair);
            this.Controls.Add(this.cbEcho);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.ckAsciiSend);
            this.Controls.Add(this.ckAsciiRcev);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnOprnComFrm);
            this.Controls.Add(this.txtShowLen);
            this.Controls.Add(this.txtEchoDelay);
            this.Controls.Add(this.lb_SendCount);
            this.Controls.Add(this.lb_RcvCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckEchoComp);
            this.Controls.Add(this.lbport);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtLogRecv);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ckCrLf);
            this.Controls.Add(this.ckEchoAuto);
            this.Controls.Add(this.ckShowStop);
            this.Controls.Add(this.ckClearRcev);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm43_Tcp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Closing);
            this.Load += new System.EventHandler(this.frm_Load);
            this.DoubleClick += new System.EventHandler(this.frm43_Tcp_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtLogRecv;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lbport;
        private System.Windows.Forms.CheckBox ckEchoAuto;
        private System.Windows.Forms.CheckBox ckClearRcev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEchoDelay;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckAsciiRcev;
        private System.Windows.Forms.Button btnOprnComFrm;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox ckCrLf;
        private System.Windows.Forms.ComboBox cbEcho;
        private System.Windows.Forms.TextBox lstCompair;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.ComboBox cbIp;
        private System.Windows.Forms.TextBox txtDevNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLogSend;
        private System.Windows.Forms.CheckBox ckShowStop;
        private System.Windows.Forms.TextBox txtShowLen;
        private System.Windows.Forms.CheckBox ckAsciiSend;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox ckEchoComp;
        private System.Windows.Forms.Button btnClearRecv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnClearSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_RcvCount;
        private System.Windows.Forms.Label lb_SendCount;
        private System.Windows.Forms.Label lbConnect;
    }
}
