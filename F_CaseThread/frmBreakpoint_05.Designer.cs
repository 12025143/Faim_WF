namespace F_CaseThread
{
    partial class frmBreakpoint_05
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstFlowName = new System.Windows.Forms.ListBox();
            this.lstCaseName = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.txt_case2 = new System.Windows.Forms.TextBox();
            this.txt_flow = new System.Windows.Forms.TextBox();
            this.txtCaseName = new FACC.FastColoredTextBox();
            this.txtFlowName = new FACC.FastColoredTextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 35;
            this.label3.Text = "流程:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(505, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "断点标记";
            // 
            // lstFlowName
            // 
            this.lstFlowName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstFlowName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstFlowName.FormattingEnabled = true;
            this.lstFlowName.ItemHeight = 16;
            this.lstFlowName.Location = new System.Drawing.Point(661, 19);
            this.lstFlowName.Margin = new System.Windows.Forms.Padding(4);
            this.lstFlowName.Name = "lstFlowName";
            this.lstFlowName.Size = new System.Drawing.Size(104, 384);
            this.lstFlowName.TabIndex = 33;
            this.lstFlowName.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // lstCaseName
            // 
            this.lstCaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstCaseName.FormattingEnabled = true;
            this.lstCaseName.ItemHeight = 16;
            this.lstCaseName.Location = new System.Drawing.Point(772, 45);
            this.lstCaseName.Margin = new System.Windows.Forms.Padding(4);
            this.lstCaseName.Name = "lstCaseName";
            this.lstCaseName.Size = new System.Drawing.Size(186, 164);
            this.lstCaseName.TabIndex = 33;
            this.lstCaseName.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            this.lstCaseName.DoubleClick += new System.EventHandler(this.listBox2_DoubleClick);
            // 
            // listBox3
            // 
            this.listBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 16;
            this.listBox3.Location = new System.Drawing.Point(772, 221);
            this.listBox3.Margin = new System.Windows.Forms.Padding(4);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(186, 164);
            this.listBox3.TabIndex = 33;
            this.listBox3.DoubleClick += new System.EventHandler(this.listBox3_DoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(473, 388);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 34);
            this.btnClose.TabIndex = 38;
            this.btnClose.Text = "完成标记";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txt_case2
            // 
            this.txt_case2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_case2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_case2.Location = new System.Drawing.Point(772, 19);
            this.txt_case2.Name = "txt_case2";
            this.txt_case2.Size = new System.Drawing.Size(261, 19);
            this.txt_case2.TabIndex = 39;
            // 
            // txt_flow
            // 
            this.txt_flow.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txt_flow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_flow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_flow.Location = new System.Drawing.Point(71, 8);
            this.txt_flow.Name = "txt_flow";
            this.txt_flow.Size = new System.Drawing.Size(251, 19);
            this.txt_flow.TabIndex = 39;
            // 
            // txtCaseName
            // 
            this.txtCaseName.AllowDrop = true;
            this.txtCaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCaseName.AutoScrollMinSize = new System.Drawing.Size(0, 144);
            this.txtCaseName.BackBrush = null;
            this.txtCaseName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCaseName.DelayedEventsInterval = 500;
            this.txtCaseName.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtCaseName.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaseName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtCaseName.IsReplaceMode = false;
            this.txtCaseName.LeftBracket = '(';
            this.txtCaseName.LeftPadding = 17;
            this.txtCaseName.Location = new System.Drawing.Point(248, 33);
            this.txtCaseName.Name = "txtCaseName";
            this.txtCaseName.Paddings = new System.Windows.Forms.Padding(0);
            this.txtCaseName.ReadOnly = true;
            this.txtCaseName.RightBracket = ')';
            this.txtCaseName.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtCaseName.Size = new System.Drawing.Size(329, 349);
            this.txtCaseName.TabIndex = 40;
            this.txtCaseName.Text = "\r\n00--工位1变量复位\r\n01--换型\r\n02--间距电机回原点\r\n03--间距电机移动到设定位置\r\n04--设置状态变量";
            this.txtCaseName.WordWrap = true;
            this.txtCaseName.PaintLine += new System.EventHandler<FACC.PaintLineEventArgs>(this.txt_PaintLine);
            this.txtCaseName.LineRemoved += new System.EventHandler<FACC.LineRemovedEventArgs>(this.txt_LineRemoved);
            this.txtCaseName.Load += new System.EventHandler(this.lstCaseName_Load);
            this.txtCaseName.DoubleClick += new System.EventHandler(this.lstCaseName_DoubleClick);
            // 
            // txtFlowName
            // 
            this.txtFlowName.AllowDrop = true;
            this.txtFlowName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFlowName.AutoScrollMinSize = new System.Drawing.Size(0, 144);
            this.txtFlowName.BackBrush = null;
            this.txtFlowName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFlowName.DelayedEventsInterval = 500;
            this.txtFlowName.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtFlowName.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFlowName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtFlowName.IsReplaceMode = false;
            this.txtFlowName.LeftBracket = '(';
            this.txtFlowName.LeftPadding = 17;
            this.txtFlowName.Location = new System.Drawing.Point(12, 33);
            this.txtFlowName.Name = "txtFlowName";
            this.txtFlowName.Paddings = new System.Windows.Forms.Padding(0);
            this.txtFlowName.ReadOnly = true;
            this.txtFlowName.RightBracket = ')';
            this.txtFlowName.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtFlowName.Size = new System.Drawing.Size(230, 388);
            this.txtFlowName.TabIndex = 40;
            this.txtFlowName.Text = "\r\n00--工位1变量复位\r\n01--换型\r\n02--间距电机回原点\r\n03--间距电机移动到设定位置\r\n04--设置状态变量";
            this.txtFlowName.WordWrap = true;
            this.txtFlowName.SelectionChanged += new System.EventHandler(this.txtFlowName_SelectionChanged);
            this.txtFlowName.Click += new System.EventHandler(this.lstFlowName_Click);
            // 
            // frmBreakpoint_05
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 431);
            this.Controls.Add(this.txtFlowName);
            this.Controls.Add(this.txtCaseName);
            this.Controls.Add(this.txt_flow);
            this.Controls.Add(this.txt_case2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstCaseName);
            this.Controls.Add(this.lstFlowName);
            this.Controls.Add(this.listBox3);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmBreakpoint_05";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "断点管理";
            this.Load += new System.EventHandler(this.form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstFlowName;
        private System.Windows.Forms.ListBox lstCaseName;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txt_case2;
        private System.Windows.Forms.TextBox txt_flow;
        private FACC.FastColoredTextBox txtCaseName;
        private FACC.FastColoredTextBox txtFlowName;
    }
}