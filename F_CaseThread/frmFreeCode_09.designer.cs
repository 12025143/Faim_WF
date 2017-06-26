namespace F_CaseThread
{
    partial class frmFreeCode_09
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtMethod = new System.Windows.Forms.TextBox();
            this.rd_int = new System.Windows.Forms.RadioButton();
            this.rd_str = new System.Windows.Forms.RadioButton();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCode = new FACC.FastColoredTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(435, 359);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "运行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMethod
            // 
            this.txtMethod.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMethod.Location = new System.Drawing.Point(62, 379);
            this.txtMethod.Margin = new System.Windows.Forms.Padding(4);
            this.txtMethod.Name = "txtMethod";
            this.txtMethod.Size = new System.Drawing.Size(345, 19);
            this.txtMethod.TabIndex = 2;
            this.txtMethod.Text = "doMySub";
            // 
            // rd_int
            // 
            this.rd_int.AutoSize = true;
            this.rd_int.Checked = true;
            this.rd_int.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rd_int.Location = new System.Drawing.Point(71, 350);
            this.rd_int.Margin = new System.Windows.Forms.Padding(4);
            this.rd_int.Name = "rd_int";
            this.rd_int.Size = new System.Drawing.Size(57, 20);
            this.rd_int.TabIndex = 3;
            this.rd_int.TabStop = true;
            this.rd_int.Text = "数值";
            this.rd_int.UseVisualStyleBackColor = true;
            // 
            // rd_str
            // 
            this.rd_str.AutoSize = true;
            this.rd_str.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rd_str.Location = new System.Drawing.Point(147, 351);
            this.rd_str.Margin = new System.Windows.Forms.Padding(4);
            this.rd_str.Name = "rd_str";
            this.rd_str.Size = new System.Drawing.Size(73, 20);
            this.rd_str.TabIndex = 3;
            this.rd_str.Text = "字符串";
            this.rd_str.UseVisualStyleBackColor = true;
            // 
            // txtResult
            // 
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResult.Location = new System.Drawing.Point(240, 346);
            this.txtResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(167, 19);
            this.txtResult.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 382);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "方法";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "代码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 351);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "结果";
            // 
            // txtCode
            // 
            this.txtCode.AllowDrop = true;
            this.txtCode.AutoScrollMinSize = new System.Drawing.Size(0, 132);
            this.txtCode.BackBrush = null;
            this.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCode.DelayedEventsInterval = 500;
            this.txtCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtCode.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.IsReplaceMode = false;
            this.txtCode.Language = FACC.Language.CSharp;
            this.txtCode.LeftBracket = '(';
            this.txtCode.LeftPadding = 17;
            this.txtCode.Location = new System.Drawing.Point(62, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtCode.RightBracket = ')';
            this.txtCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtCode.Size = new System.Drawing.Size(456, 336);
            this.txtCode.TabIndex = 5;
            this.txtCode.Text = "__i = 100 ;\r\nfor (int i = 0; i<10; i++)\r\n{\r\n      __i = __i + i;\r\n}\r\n";
            this.txtCode.WordWrap = true;
            // 
            // frmFreeCode_09
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 406);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rd_str);
            this.Controls.Add(this.rd_int);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtMethod);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFreeCode_09";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自由代码";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtMethod;
        private System.Windows.Forms.RadioButton rd_int;
        private System.Windows.Forms.RadioButton rd_str;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private FACC.FastColoredTextBox txtCode;
    }
}

