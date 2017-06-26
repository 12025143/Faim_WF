namespace F08_TcpSrvs
{
    partial class frmTcpSrvs
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTcpSrvs));
            this.gv = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Listen = new System.Windows.Forms.DataGridViewImageColumn();
            this.Connected = new System.Windows.Forms.DataGridViewImageColumn();
            this.ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isListen = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isConnect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.img = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLogSend = new System.Windows.Forms.TextBox();
            this.txtLogRecv = new System.Windows.Forms.TextBox();
            this.lb_msg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnListen = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pbSend = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.ckAsciiSend = new System.Windows.Forms.CheckBox();
            this.txtPort2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIP2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lb_SendCount = new System.Windows.Forms.Label();
            this.lb_RcvCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSend)).BeginInit();
            this.SuspendLayout();
            // 
            // gv
            // 
            this.gv.AllowUserToAddRows = false;
            this.gv.AllowUserToDeleteRows = false;
            this.gv.AllowUserToResizeRows = false;
            this.gv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Listen,
            this.Connected,
            this.ip,
            this.port,
            this.clientPort,
            this.isListen,
            this.isConnect,
            this.img});
            this.gv.Location = new System.Drawing.Point(15, 61);
            this.gv.MultiSelect = false;
            this.gv.Name = "gv";
            this.gv.ReadOnly = true;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.gv.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gv.RowTemplate.Height = 23;
            this.gv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv.ShowCellToolTips = false;
            this.gv.Size = new System.Drawing.Size(603, 237);
            this.gv.TabIndex = 0;
            this.gv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_CellClick);
            this.gv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_CellContentClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 5;
            // 
            // Listen
            // 
            this.Listen.DataPropertyName = "Listen";
            this.Listen.HeaderText = "侦听";
            this.Listen.Name = "Listen";
            this.Listen.ReadOnly = true;
            this.Listen.Width = 40;
            // 
            // Connected
            // 
            this.Connected.DataPropertyName = "Connected";
            this.Connected.HeaderText = "连接";
            this.Connected.Name = "Connected";
            this.Connected.ReadOnly = true;
            this.Connected.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Connected.Width = 40;
            // 
            // ip
            // 
            this.ip.DataPropertyName = "ip";
            this.ip.HeaderText = "IP";
            this.ip.Name = "ip";
            this.ip.ReadOnly = true;
            this.ip.Width = 130;
            // 
            // port
            // 
            this.port.DataPropertyName = "port";
            this.port.HeaderText = "端口";
            this.port.Name = "port";
            this.port.ReadOnly = true;
            this.port.Width = 80;
            // 
            // clientPort
            // 
            this.clientPort.DataPropertyName = "clientPort";
            this.clientPort.HeaderText = "远端口";
            this.clientPort.Name = "clientPort";
            this.clientPort.ReadOnly = true;
            this.clientPort.Width = 80;
            // 
            // isListen
            // 
            this.isListen.DataPropertyName = "isListen";
            this.isListen.HeaderText = "启动状态";
            this.isListen.Name = "isListen";
            this.isListen.ReadOnly = true;
            this.isListen.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isListen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isListen.Width = 60;
            // 
            // isConnect
            // 
            this.isConnect.DataPropertyName = "isConnect";
            this.isConnect.HeaderText = "连接状态";
            this.isConnect.Name = "isConnect";
            this.isConnect.ReadOnly = true;
            this.isConnect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isConnect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isConnect.Width = 60;
            // 
            // img
            // 
            this.img.DataPropertyName = "img";
            this.img.HeaderText = "img";
            this.img.Name = "img";
            this.img.ReadOnly = true;
            this.img.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.img.Visible = false;
            // 
            // txtLogSend
            // 
            this.txtLogSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtLogSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogSend.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLogSend.ForeColor = System.Drawing.Color.Black;
            this.txtLogSend.Location = new System.Drawing.Point(305, 320);
            this.txtLogSend.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtLogSend.Multiline = true;
            this.txtLogSend.Name = "txtLogSend";
            this.txtLogSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogSend.Size = new System.Drawing.Size(313, 135);
            this.txtLogSend.TabIndex = 73;
            this.txtLogSend.Tag = "txtEcho";
            // 
            // txtLogRecv
            // 
            this.txtLogRecv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtLogRecv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogRecv.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLogRecv.Location = new System.Drawing.Point(15, 320);
            this.txtLogRecv.Multiline = true;
            this.txtLogRecv.Name = "txtLogRecv";
            this.txtLogRecv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogRecv.Size = new System.Drawing.Size(270, 207);
            this.txtLogRecv.TabIndex = 72;
            // 
            // lb_msg
            // 
            this.lb_msg.AutoSize = true;
            this.lb_msg.Location = new System.Drawing.Point(87, 37);
            this.lb_msg.Name = "lb_msg";
            this.lb_msg.Size = new System.Drawing.Size(23, 12);
            this.lb_msg.TabIndex = 74;
            this.lb_msg.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 74;
            this.label1.Text = "接收的信息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 74;
            this.label2.Text = "发送的消息";
            // 
            // txtSend
            // 
            this.txtSend.BackColor = System.Drawing.Color.White;
            this.txtSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSend.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSend.ForeColor = System.Drawing.Color.Black;
            this.txtSend.Location = new System.Drawing.Point(304, 499);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSend.Size = new System.Drawing.Size(283, 25);
            this.txtSend.TabIndex = 75;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 77;
            this.label3.Text = "当前行";
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(412, 32);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(75, 23);
            this.btnListen.TabIndex = 78;
            this.btnListen.Text = "全侦听";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(493, 33);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 79;
            this.btnStop.Text = "全停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pbSend
            // 
            this.pbSend.Image = ((System.Drawing.Image)(resources.GetObject("pbSend.Image")));
            this.pbSend.Location = new System.Drawing.Point(593, 499);
            this.pbSend.Name = "pbSend";
            this.pbSend.Size = new System.Drawing.Size(25, 25);
            this.pbSend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSend.TabIndex = 80;
            this.pbSend.TabStop = false;
            this.pbSend.Click += new System.EventHandler(this.pbSend_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 470);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 81;
            this.label4.Text = "IP地址";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(348, 467);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 21);
            this.txtIP.TabIndex = 82;
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(454, 470);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 83;
            this.label5.Text = "端口";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(487, 467);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(66, 21);
            this.txtPort.TabIndex = 84;
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ckAsciiSend
            // 
            this.ckAsciiSend.AutoSize = true;
            this.ckAsciiSend.Checked = true;
            this.ckAsciiSend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAsciiSend.Location = new System.Drawing.Point(564, 470);
            this.ckAsciiSend.Name = "ckAsciiSend";
            this.ckAsciiSend.Size = new System.Drawing.Size(54, 16);
            this.ckAsciiSend.TabIndex = 85;
            this.ckAsciiSend.Text = "ASCII";
            this.ckAsciiSend.UseVisualStyleBackColor = true;
            // 
            // txtPort2
            // 
            this.txtPort2.Location = new System.Drawing.Point(244, 4);
            this.txtPort2.Name = "txtPort2";
            this.txtPort2.Size = new System.Drawing.Size(64, 21);
            this.txtPort2.TabIndex = 89;
            this.txtPort2.Text = "5010";
            this.txtPort2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(208, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 88;
            this.label6.Text = "端口";
            // 
            // txtIP2
            // 
            this.txtIP2.Location = new System.Drawing.Point(89, 4);
            this.txtIP2.Name = "txtIP2";
            this.txtIP2.Size = new System.Drawing.Size(113, 21);
            this.txtIP2.TabIndex = 87;
            this.txtIP2.Text = "192.168.56.1";
            this.txtIP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 86;
            this.label7.Text = "起始IP地址";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(351, 4);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(56, 21);
            this.txtNumber.TabIndex = 91;
            this.txtNumber.Text = "50";
            this.txtNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(315, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 90;
            this.label8.Text = "数量";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(412, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 79;
            this.button1.Text = "刷新列表";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(493, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 92;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lb_SendCount
            // 
            this.lb_SendCount.AutoSize = true;
            this.lb_SendCount.Location = new System.Drawing.Point(410, 301);
            this.lb_SendCount.Name = "lb_SendCount";
            this.lb_SendCount.Size = new System.Drawing.Size(35, 12);
            this.lb_SendCount.TabIndex = 93;
            this.lb_SendCount.Text = "0 0/0";
            // 
            // lb_RcvCount
            // 
            this.lb_RcvCount.AutoSize = true;
            this.lb_RcvCount.Location = new System.Drawing.Point(108, 301);
            this.lb_RcvCount.Name = "lb_RcvCount";
            this.lb_RcvCount.Size = new System.Drawing.Size(35, 12);
            this.lb_RcvCount.TabIndex = 94;
            this.lb_RcvCount.Text = "0 0/0";
            // 
            // frmTcpSrvs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 537);
            this.Controls.Add(this.lb_RcvCount);
            this.Controls.Add(this.lb_SendCount);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPort2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtIP2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ckAsciiSend);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pbSend);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_msg);
            this.Controls.Add(this.txtLogSend);
            this.Controls.Add(this.txtLogRecv);
            this.Controls.Add(this.gv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTcpSrvs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP服务器组";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSend)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.TextBox txtLogSend;
        private System.Windows.Forms.TextBox txtLogRecv;
        private System.Windows.Forms.Label lb_msg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.PictureBox pbSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.CheckBox ckAsciiSend;
        private System.Windows.Forms.TextBox txtPort2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIP2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewImageColumn Listen;
        private System.Windows.Forms.DataGridViewImageColumn Connected;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn port;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientPort;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isListen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isConnect;
        private System.Windows.Forms.DataGridViewTextBoxColumn img;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lb_SendCount;
        private System.Windows.Forms.Label lb_RcvCount;
    }
}

