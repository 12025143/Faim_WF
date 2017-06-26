namespace F_CaseThread
{
    partial class frmCaseStep_06
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCaseStep_06));
            this.btnClose = new System.Windows.Forms.Button();
            this.txt_flow = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstCaseTrace = new System.Windows.Forms.ListBox();
            this.btnStep = new System.Windows.Forms.Button();
            this.btn_case_pause_1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFlowName = new FACC.FastColoredTextBox();
            this.txtCaseName = new FACC.FastColoredTextBox();
            this.txtCode = new FACC.FastColoredTextBox();
            this.btnRefreshData = new System.Windows.Forms.Button();
            this.btnShowXLS = new System.Windows.Forms.Label();
            this.txtStepName1 = new System.Windows.Forms.TextBox();
            this.rd_Step = new System.Windows.Forms.RadioButton();
            this.rd_Line = new System.Windows.Forms.RadioButton();
            this.rd_Break = new System.Windows.Forms.RadioButton();
            this.txtStepName = new FACC.FastColoredTextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(842, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(71, 25);
            this.btnClose.TabIndex = 45;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txt_flow
            // 
            this.txt_flow.AutoSize = true;
            this.txt_flow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_flow.Location = new System.Drawing.Point(56, 6);
            this.txt_flow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txt_flow.Name = "txt_flow";
            this.txt_flow.Size = new System.Drawing.Size(26, 16);
            this.txt_flow.TabIndex = 41;
            this.txt_flow.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 42;
            this.label3.Text = "流程";
            // 
            // lstCaseTrace
            // 
            this.lstCaseTrace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCaseTrace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstCaseTrace.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstCaseTrace.FormattingEnabled = true;
            this.lstCaseTrace.ItemHeight = 16;
            this.lstCaseTrace.Location = new System.Drawing.Point(525, 29);
            this.lstCaseTrace.Margin = new System.Windows.Forms.Padding(4);
            this.lstCaseTrace.Name = "lstCaseTrace";
            this.lstCaseTrace.Size = new System.Drawing.Size(389, 320);
            this.lstCaseTrace.TabIndex = 39;
            this.lstCaseTrace.DoubleClick += new System.EventHandler(this.listBox3_DoubleClick);
            // 
            // btnStep
            // 
            this.btnStep.BackColor = System.Drawing.Color.White;
            this.btnStep.Location = new System.Drawing.Point(544, 0);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(75, 26);
            this.btnStep.TabIndex = 46;
            this.btnStep.Text = "运行";
            this.btnStep.UseVisualStyleBackColor = false;
            this.btnStep.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btn_case_pause_1
            // 
            this.btn_case_pause_1.BackColor = System.Drawing.Color.White;
            this.btn_case_pause_1.Location = new System.Drawing.Point(625, 1);
            this.btn_case_pause_1.Name = "btn_case_pause_1";
            this.btn_case_pause_1.Size = new System.Drawing.Size(66, 25);
            this.btn_case_pause_1.TabIndex = 48;
            this.btn_case_pause_1.Text = "复位";
            this.btn_case_pause_1.UseVisualStyleBackColor = false;
            this.btn_case_pause_1.Click += new System.EventHandler(this.btn_case_pause_1_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(438, 581);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 28);
            this.button1.TabIndex = 49;
            this.button1.Text = "执行脚本";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnfrmFreeCode_Click);
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResult.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtResult.Location = new System.Drawing.Point(517, 581);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(305, 23);
            this.txtResult.TabIndex = 54;
            this.txtResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 361);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 43;
            this.label4.Text = "实时脚本";
            // 
            // txtFlowName
            // 
            this.txtFlowName.AllowDrop = true;
            this.txtFlowName.AutoScrollMinSize = new System.Drawing.Size(232, 144);
            this.txtFlowName.BackBrush = null;
            this.txtFlowName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFlowName.DelayedEventsInterval = 500;
            this.txtFlowName.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtFlowName.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFlowName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtFlowName.IsReplaceMode = false;
            this.txtFlowName.LeftBracket = '(';
            this.txtFlowName.LeftPadding = 17;
            this.txtFlowName.Location = new System.Drawing.Point(12, 29);
            this.txtFlowName.Name = "txtFlowName";
            this.txtFlowName.Paddings = new System.Windows.Forms.Padding(0);
            this.txtFlowName.ReadOnly = true;
            this.txtFlowName.RightBracket = ')';
            this.txtFlowName.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtFlowName.Size = new System.Drawing.Size(250, 319);
            this.txtFlowName.TabIndex = 55;
            this.txtFlowName.Text = "\r\n00--工位1变量复位\r\n01--换型\r\n02--间距电机回原点\r\n03--间距电机移动到设定位置\r\n04--设置状态变量";
            this.txtFlowName.SelectionChanged += new System.EventHandler(this.txtFlowName_SelectionChanged);
            this.txtFlowName.Click += new System.EventHandler(this.txtFlowName_Click);
            // 
            // txtCaseName
            // 
            this.txtCaseName.AllowDrop = true;
            this.txtCaseName.AutoScrollMinSize = new System.Drawing.Size(232, 144);
            this.txtCaseName.BackBrush = null;
            this.txtCaseName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCaseName.DelayedEventsInterval = 500;
            this.txtCaseName.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtCaseName.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaseName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtCaseName.IsReplaceMode = false;
            this.txtCaseName.LeftBracket = '(';
            this.txtCaseName.LeftPadding = 17;
            this.txtCaseName.Location = new System.Drawing.Point(268, 29);
            this.txtCaseName.Name = "txtCaseName";
            this.txtCaseName.Paddings = new System.Windows.Forms.Padding(0);
            this.txtCaseName.ReadOnly = true;
            this.txtCaseName.RightBracket = ')';
            this.txtCaseName.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtCaseName.Size = new System.Drawing.Size(250, 319);
            this.txtCaseName.TabIndex = 55;
            this.txtCaseName.Text = "\r\n00--工位1变量复位\r\n01--换型\r\n02--间距电机回原点\r\n03--间距电机移动到设定位置\r\n04--设置状态变量";
            this.txtCaseName.SelectionChanged += new System.EventHandler(this.txtCaseName_SelectionChanged);
            this.txtCaseName.PaintLine += new System.EventHandler<FACC.PaintLineEventArgs>(this.txt_PaintLine);
            this.txtCaseName.LineRemoved += new System.EventHandler<FACC.LineRemovedEventArgs>(this.txt_LineRemoved);
            this.txtCaseName.DoubleClick += new System.EventHandler(this.txtCaseName_DoubleClick);
            this.txtCaseName.Leave += new System.EventHandler(this.txtCaseName_Leave);
            // 
            // txtCode
            // 
            this.txtCode.AllowDrop = true;
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCode.AutoScrollMinSize = new System.Drawing.Size(404, 176);
            this.txtCode.BackBrush = null;
            this.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCode.DelayedEventsInterval = 500;
            this.txtCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtCode.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.IsReplaceMode = false;
            this.txtCode.Language = FACC.Language.CSharp;
            this.txtCode.LeftBracket = '(';
            this.txtCode.LeftPadding = 17;
            this.txtCode.Location = new System.Drawing.Point(12, 381);
            this.txtCode.Name = "txtCode";
            this.txtCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtCode.RightBracket = ')';
            this.txtCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtCode.Size = new System.Drawing.Size(419, 222);
            this.txtCode.TabIndex = 56;
            this.txtCode.Text = resources.GetString("txtCode.Text");
            // 
            // btnRefreshData
            // 
            this.btnRefreshData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshData.Location = new System.Drawing.Point(738, 0);
            this.btnRefreshData.Name = "btnRefreshData";
            this.btnRefreshData.Size = new System.Drawing.Size(80, 26);
            this.btnRefreshData.TabIndex = 57;
            this.btnRefreshData.Text = "刷新流程";
            this.btnRefreshData.UseVisualStyleBackColor = true;
            this.btnRefreshData.Click += new System.EventHandler(this.btnRefreshData_Click);
            // 
            // btnShowXLS
            // 
            this.btnShowXLS.AutoSize = true;
            this.btnShowXLS.Location = new System.Drawing.Point(434, 359);
            this.btnShowXLS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnShowXLS.Name = "btnShowXLS";
            this.btnShowXLS.Size = new System.Drawing.Size(63, 14);
            this.btnShowXLS.TabIndex = 43;
            this.btnShowXLS.Text = "流程文档";
            this.btnShowXLS.Click += new System.EventHandler(this.btnShowXLS_Click);
            // 
            // txtStepName1
            // 
            this.txtStepName1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStepName1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStepName1.Location = new System.Drawing.Point(1032, 294);
            this.txtStepName1.Multiline = true;
            this.txtStepName1.Name = "txtStepName1";
            this.txtStepName1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStepName1.Size = new System.Drawing.Size(526, 210);
            this.txtStepName1.TabIndex = 54;
            this.txtStepName1.WordWrap = false;
            // 
            // rd_Step
            // 
            this.rd_Step.AutoSize = true;
            this.rd_Step.Location = new System.Drawing.Point(267, 8);
            this.rd_Step.Name = "rd_Step";
            this.rd_Step.Size = new System.Drawing.Size(53, 18);
            this.rd_Step.TabIndex = 58;
            this.rd_Step.Text = "单步";
            this.rd_Step.UseVisualStyleBackColor = true;
            // 
            // rd_Line
            // 
            this.rd_Line.AutoSize = true;
            this.rd_Line.Checked = true;
            this.rd_Line.Location = new System.Drawing.Point(329, 8);
            this.rd_Line.Name = "rd_Line";
            this.rd_Line.Size = new System.Drawing.Size(53, 18);
            this.rd_Line.TabIndex = 58;
            this.rd_Line.TabStop = true;
            this.rd_Line.Text = "逐行";
            this.rd_Line.UseVisualStyleBackColor = true;
            // 
            // rd_Break
            // 
            this.rd_Break.AutoSize = true;
            this.rd_Break.Location = new System.Drawing.Point(391, 8);
            this.rd_Break.Name = "rd_Break";
            this.rd_Break.Size = new System.Drawing.Size(53, 18);
            this.rd_Break.TabIndex = 58;
            this.rd_Break.Text = "断点";
            this.rd_Break.UseVisualStyleBackColor = true;
            // 
            // txtStepName
            // 
            this.txtStepName.AllowDrop = true;
            this.txtStepName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStepName.AutoScrollMinSize = new System.Drawing.Size(556, 60);
            this.txtStepName.BackBrush = null;
            this.txtStepName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtStepName.DelayedEventsInterval = 500;
            this.txtStepName.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtStepName.Font = new System.Drawing.Font("Courier New", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStepName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtStepName.IsReplaceMode = false;
            this.txtStepName.LeftBracket = '(';
            this.txtStepName.LeftPadding = 17;
            this.txtStepName.Location = new System.Drawing.Point(437, 381);
            this.txtStepName.Name = "txtStepName";
            this.txtStepName.Paddings = new System.Windows.Forms.Padding(0);
            this.txtStepName.ReadOnly = true;
            this.txtStepName.RightBracket = ')';
            this.txtStepName.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtStepName.Size = new System.Drawing.Size(477, 194);
            this.txtStepName.TabIndex = 59;
            this.txtStepName.Text = "DAL_tStepA_00   DIM_A    Feeder05_unLink        5   -   单步—00   \r\nDAL_tStepA_00  " +
                " DIM_A    Feeder01_unLink        1   -   单步—00   \r\nDAL_tStepA_00   DIM_A    Feed" +
                "er02_unLink        2   -   单步—00   \r\n";
            this.txtStepName.SelectionChanged += new System.EventHandler(this.txtStepName_SelectionChanged);
            // 
            // frmCaseStep_06
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 611);
            this.Controls.Add(this.txtStepName);
            this.Controls.Add(this.rd_Break);
            this.Controls.Add(this.rd_Line);
            this.Controls.Add(this.rd_Step);
            this.Controls.Add(this.btnRefreshData);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtCaseName);
            this.Controls.Add(this.txtFlowName);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_case_pause_1);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txt_flow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnShowXLS);
            this.Controls.Add(this.txtStepName1);
            this.Controls.Add(this.lstCaseTrace);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCaseStep_06";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "联调 0116";
            this.Load += new System.EventHandler(this.form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label txt_flow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstCaseTrace;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btn_case_pause_1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label4;
        private FACC.FastColoredTextBox txtFlowName;
        private FACC.FastColoredTextBox txtCaseName;
        private FACC.FastColoredTextBox txtCode;
        private System.Windows.Forms.Button btnRefreshData;
        private System.Windows.Forms.Label btnShowXLS;
        private System.Windows.Forms.TextBox txtStepName1;
        private System.Windows.Forms.RadioButton rd_Step;
        private System.Windows.Forms.RadioButton rd_Line;
        private System.Windows.Forms.RadioButton rd_Break;
        private FACC.FastColoredTextBox txtStepName;
    }
}