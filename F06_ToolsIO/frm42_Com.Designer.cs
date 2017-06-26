namespace FACC
{
    using nsFACC;
    partial class frm42_Com
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm42_Com));
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.txtRecv = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lbport = new System.Windows.Forms.Label();
            this.ckAuto = new System.Windows.Forms.CheckBox();
            this.ckCls = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDelay = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.ckASCII_In = new System.Windows.Forms.CheckBox();
            this.cbBound = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbEchoTxt = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtEcho = new System.Windows.Forms.TextBox();
            this.ckCrLf = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDevNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSRcount = new System.Windows.Forms.TextBox();
            this.txtSRinfo = new System.Windows.Forms.TextBox();
            this.btnRefreshInfo = new System.Windows.Forms.Button();
            this.ckShowStop = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtShowLen = new System.Windows.Forms.TextBox();
            this.ckASCII_out = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.cbPort.Location = new System.Drawing.Point(618, 10);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(83, 24);
            this.cbPort.TabIndex = 0;
            this.cbPort.Text = "COM2";
            this.cbPort.Click += new System.EventHandler(this.cbPort_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 19200;
            this.serialPort1.PortName = "COM2";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.evt_DataReceived);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(736, 41);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关端口";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtRecv
            // 
            this.txtRecv.Location = new System.Drawing.Point(12, 12);
            this.txtRecv.Multiline = true;
            this.txtRecv.Name = "txtRecv";
            this.txtRecv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRecv.Size = new System.Drawing.Size(347, 481);
            this.txtRecv.TabIndex = 2;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(368, 173);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSend.Size = new System.Drawing.Size(347, 83);
            this.txtSend.TabIndex = 2;
            this.txtSend.Text = "OK";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(736, 498);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 35);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "关闭";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbport
            // 
            this.lbport.AutoSize = true;
            this.lbport.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbport.Location = new System.Drawing.Point(542, 12);
            this.lbport.Name = "lbport";
            this.lbport.Size = new System.Drawing.Size(56, 16);
            this.lbport.TabIndex = 3;
            this.lbport.Text = "端口号";
            // 
            // ckAuto
            // 
            this.ckAuto.AutoSize = true;
            this.ckAuto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckAuto.Checked = true;
            this.ckAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAuto.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckAuto.Location = new System.Drawing.Point(492, 121);
            this.ckAuto.Name = "ckAuto";
            this.ckAuto.Size = new System.Drawing.Size(91, 20);
            this.ckAuto.TabIndex = 4;
            this.ckAuto.Text = "自动回复";
            this.ckAuto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckAuto.UseVisualStyleBackColor = true;
            // 
            // ckCls
            // 
            this.ckCls.AutoSize = true;
            this.ckCls.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckCls.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckCls.Location = new System.Drawing.Point(365, 121);
            this.ckCls.Name = "ckCls";
            this.ckCls.Size = new System.Drawing.Size(91, 20);
            this.ckCls.TabIndex = 4;
            this.ckCls.Text = "清空接收";
            this.ckCls.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(365, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "回复延时";
            // 
            // txtDelay
            // 
            this.txtDelay.Location = new System.Drawing.Point(443, 34);
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(83, 26);
            this.txtDelay.TabIndex = 6;
            this.txtDelay.Text = "1";
            this.txtDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(736, 149);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 35);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ckASCII_In
            // 
            this.ckASCII_In.AutoSize = true;
            this.ckASCII_In.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckASCII_In.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckASCII_In.Location = new System.Drawing.Point(365, 96);
            this.ckASCII_In.Name = "ckASCII_In";
            this.ckASCII_In.Size = new System.Drawing.Size(91, 20);
            this.ckASCII_In.TabIndex = 11;
            this.ckASCII_In.Text = "ASCII   ";
            this.ckASCII_In.UseVisualStyleBackColor = true;
            // 
            // cbBound
            // 
            this.cbBound.FormattingEnabled = true;
            this.cbBound.Items.AddRange(new object[] {
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbBound.Location = new System.Drawing.Point(618, 36);
            this.cbBound.Name = "cbBound";
            this.cbBound.Size = new System.Drawing.Size(83, 24);
            this.cbBound.TabIndex = 0;
            this.cbBound.Text = "38400";
            this.cbBound.Click += new System.EventHandler(this.cbBound_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(542, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "波特率";
            // 
            // cbEchoTxt
            // 
            this.cbEchoTxt.BackColor = System.Drawing.Color.Gray;
            this.cbEchoTxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEchoTxt.ForeColor = System.Drawing.Color.Gainsboro;
            this.cbEchoTxt.FormattingEnabled = true;
            this.cbEchoTxt.Items.AddRange(new object[] {
            "OK",
            "FAIL",
            "PASS",
            ""});
            this.cbEchoTxt.Location = new System.Drawing.Point(368, 149);
            this.cbEchoTxt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cbEchoTxt.Name = "cbEchoTxt";
            this.cbEchoTxt.Size = new System.Drawing.Size(346, 24);
            this.cbEchoTxt.TabIndex = 13;
            this.cbEchoTxt.Tag = "cbEchoTxt";
            this.cbEchoTxt.Text = "OK";
            this.cbEchoTxt.SelectedIndexChanged += new System.EventHandler(this.cbEchoTxt_SelectedIndexChanged);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(736, 8);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(90, 35);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "开端口";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtEcho
            // 
            this.txtEcho.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEcho.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEcho.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEcho.ForeColor = System.Drawing.Color.Black;
            this.txtEcho.Location = new System.Drawing.Point(368, 403);
            this.txtEcho.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtEcho.Multiline = true;
            this.txtEcho.Name = "txtEcho";
            this.txtEcho.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEcho.Size = new System.Drawing.Size(346, 130);
            this.txtEcho.TabIndex = 15;
            this.txtEcho.Tag = "txtEcho";
            this.txtEcho.Text = "POS=DONEX10Y20Z30O40A50T60E\r\n?1=Pass1uA100V200\r\n?2=Pass22A300m400\r\n?3=Pass33V500m" +
                "A600\r\n?4=Pass44V700M555M800\r\nS8=X910Y920Z930C940E";
            this.txtEcho.TextChanged += new System.EventHandler(this.txtEcho_TextChanged);
            // 
            // ckCrLf
            // 
            this.ckCrLf.AutoSize = true;
            this.ckCrLf.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckCrLf.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ckCrLf.Location = new System.Drawing.Point(484, 99);
            this.ckCrLf.Name = "ckCrLf";
            this.ckCrLf.Size = new System.Drawing.Size(99, 20);
            this.ckCrLf.TabIndex = 12;
            this.ckCrLf.Text = " 回车换行";
            this.ckCrLf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckCrLf.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(365, 382);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "配对回复";
            // 
            // txtDevNo
            // 
            this.txtDevNo.Location = new System.Drawing.Point(443, 9);
            this.txtDevNo.Name = "txtDevNo";
            this.txtDevNo.Size = new System.Drawing.Size(83, 26);
            this.txtDevNo.TabIndex = 71;
            this.txtDevNo.Text = "1";
            this.txtDevNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(365, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 70;
            this.label8.Text = "设备号";
            // 
            // txtSRcount
            // 
            this.txtSRcount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSRcount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSRcount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSRcount.ForeColor = System.Drawing.Color.Black;
            this.txtSRcount.Location = new System.Drawing.Point(369, 264);
            this.txtSRcount.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSRcount.Multiline = true;
            this.txtSRcount.Name = "txtSRcount";
            this.txtSRcount.Size = new System.Drawing.Size(346, 23);
            this.txtSRcount.TabIndex = 15;
            this.txtSRcount.Tag = "txtEcho";
            this.txtSRcount.TextChanged += new System.EventHandler(this.txtEcho_TextChanged);
            // 
            // txtSRinfo
            // 
            this.txtSRinfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSRinfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSRinfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSRinfo.ForeColor = System.Drawing.Color.Black;
            this.txtSRinfo.Location = new System.Drawing.Point(369, 288);
            this.txtSRinfo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSRinfo.Multiline = true;
            this.txtSRinfo.Name = "txtSRinfo";
            this.txtSRinfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSRinfo.Size = new System.Drawing.Size(347, 90);
            this.txtSRinfo.TabIndex = 15;
            this.txtSRinfo.Tag = "txtEcho";
            this.txtSRinfo.TextChanged += new System.EventHandler(this.txtEcho_TextChanged);
            // 
            // btnRefreshInfo
            // 
            this.btnRefreshInfo.Location = new System.Drawing.Point(736, 306);
            this.btnRefreshInfo.Name = "btnRefreshInfo";
            this.btnRefreshInfo.Size = new System.Drawing.Size(90, 35);
            this.btnRefreshInfo.TabIndex = 1;
            this.btnRefreshInfo.Text = "显示";
            this.btnRefreshInfo.UseVisualStyleBackColor = true;
            this.btnRefreshInfo.Click += new System.EventHandler(this.btnRefreshInfo_Click);
            // 
            // ckShowStop
            // 
            this.ckShowStop.AutoSize = true;
            this.ckShowStop.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckShowStop.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ckShowStop.Location = new System.Drawing.Point(610, 96);
            this.ckShowStop.Name = "ckShowStop";
            this.ckShowStop.Size = new System.Drawing.Size(91, 20);
            this.ckShowStop.TabIndex = 74;
            this.ckShowStop.Text = "停止显示";
            this.ckShowStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckShowStop.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(365, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 73;
            this.label7.Text = "显示长度";
            // 
            // txtShowLen
            // 
            this.txtShowLen.Location = new System.Drawing.Point(443, 59);
            this.txtShowLen.Name = "txtShowLen";
            this.txtShowLen.Size = new System.Drawing.Size(83, 26);
            this.txtShowLen.TabIndex = 72;
            this.txtShowLen.Text = "3000";
            this.txtShowLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ckASCII_out
            // 
            this.ckASCII_out.AutoSize = true;
            this.ckASCII_out.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckASCII_out.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ckASCII_out.Location = new System.Drawing.Point(735, 190);
            this.ckASCII_out.Name = "ckASCII_out";
            this.ckASCII_out.Size = new System.Drawing.Size(91, 20);
            this.ckASCII_out.TabIndex = 11;
            this.ckASCII_out.Text = "ASCII   ";
            this.ckASCII_out.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(269, 499);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frm42_Com
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(850, 545);
            this.Controls.Add(this.ckShowStop);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtShowLen);
            this.Controls.Add(this.txtDevNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSRinfo);
            this.Controls.Add(this.txtSRcount);
            this.Controls.Add(this.txtEcho);
            this.Controls.Add(this.cbEchoTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ckCrLf);
            this.Controls.Add(this.ckASCII_out);
            this.Controls.Add(this.ckASCII_In);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtDelay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckCls);
            this.Controls.Add(this.ckAuto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbport);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtRecv);
            this.Controls.Add(this.btnRefreshInfo);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.cbBound);
            this.Controls.Add(this.cbPort);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm42_Com";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COM";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DoubleClick += new System.EventHandler(this.frm_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ComboBox cbPort;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtRecv;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lbport;
        private System.Windows.Forms.CheckBox ckAuto;
        private System.Windows.Forms.CheckBox ckCls;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDelay;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox ckASCII_In;
        private System.Windows.Forms.ComboBox cbBound;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbEchoTxt;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtEcho;
        private System.Windows.Forms.CheckBox ckCrLf;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDevNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSRcount;
        private System.Windows.Forms.TextBox txtSRinfo;
        private System.Windows.Forms.Button btnRefreshInfo;
        private System.Windows.Forms.CheckBox ckShowStop;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtShowLen;
        private System.Windows.Forms.CheckBox ckASCII_out;
        private System.Windows.Forms.Button btnSave;
    }
}
