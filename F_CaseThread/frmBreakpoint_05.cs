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

namespace F_CaseThread
{
    #region //
    using F_Entitys;
    using F_Entitys_DAL;
    using System.Drawing.Drawing2D;
    using FACC;
    #endregion

    public partial class frmBreakpoint_05 : Form
    {
        bool _isLoad = false;
        clsFaim3 _faim3 = null;
        DAL_CommData _dao_comm = null;

        clsFlow _flow = null;
        string _flowName = "";
        string _caseName = "";
        Dictionary<string, string> _dictNameText = new Dictionary<string, string>();

        int _refTop = 5;
        int _refLeft = 4;
        HashSet<int> _hash_id = new HashSet<int>();
        List<int> _lstMark = new List<int>();
        frmBreakpoint_05()
        {
        }
        public frmBreakpoint_05(clsFaim3 v_faim3, DAL_CommData v_dao_comm)
        {
            _faim3 = v_faim3;
            _dao_comm = v_dao_comm;

            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void form_Load(object sender, EventArgs e)
        {
            _isLoad = true;
            DAL_Faim3.Load_faim3(ref _faim3, ref _dao_comm);
            //_dao_comm.OnEchoIO += new F_Delegate.delEcho(evt_OnEchoIO);

            dict_Flow_show();
            dict_CaseState_show();
            lstBreakPoint_show();

            _isLoad = false;
        }

        // 流程名： cfgFlowName.ini --> fn_FlowName --> List<CfgListFlow> --> _faim3.dict_Flow
        void dict_Flow_show()
        {
            List<clsKeyValue> _lst = new List<clsKeyValue>();

            string _txts = "";
            this.lstFlowName.Items.Clear();
            foreach (var item in _faim3.dict_Flow)
            {
                if (string.IsNullOrEmpty(_flowName)) Set_Flow(item.Key);

                if (!_dictNameText.ContainsKey(item.Key)) _dictNameText.Add(item.Key, item.Value.Remark);
                if (!_dictNameText.ContainsKey(item.Value.Remark)) _dictNameText.Add(item.Value.Remark, item.Key);

                _lst.Add(new clsKeyValue() { key = item.Key, val = item.Value.Remark });

                if (string.IsNullOrEmpty(_txts))
                    _txts = item.Value.Remark;
                else
                    _txts = _txts + Environment.NewLine + item.Value.Remark;
            }
            txtFlowName.Text = _txts;
            this.lstFlowName.DataSource = _lst;
            this.lstFlowName.ValueMember = "key";
            this.lstFlowName.DisplayMember = "val";
        }
        void Set_Flow(string vFlowName)
        {
            _flowName = vFlowName;
            _flow = _faim3.dict_Flow[_flowName];
            txt_flow.Text = _flow.Remark;
        }
        // 步骤名： cfgCaseName.ini --> XX_Case.xls --> fn_CaseName --> List<CfgListCase>() --> _faim3.dict_CaseState
        void dict_CaseState_show()
        {
            if (string.IsNullOrEmpty(_flowName)) return;
            _caseName = "";

            List<clsKeyValue> _lst = new List<clsKeyValue>();
            string _txts = "";
            foreach (var item in _faim3.dict_CaseState)
            {
                if (item.Key.StartsWith(_flowName))
                {
                    if (!_dictNameText.ContainsKey(item.Key)) _dictNameText.Add(item.Key, item.Value.Remark);
                    if (!_dictNameText.ContainsKey(item.Value.Remark)) _dictNameText.Add(item.Value.Remark, item.Key);

                    if (string.IsNullOrEmpty(_caseName)) Set_Case(item.Key);

                    _lst.Add(new clsKeyValue() { key = item.Key, val = item.Value.Remark });
                    if (string.IsNullOrEmpty(_txts))
                        _txts = item.Value.Remark;
                    else
                        _txts = _txts + Environment.NewLine + item.Value.Remark;

                }
            }
            txtCaseName.Text = _txts;

            this.lstCaseName.DataSource = new BindingList<clsKeyValue>(new List<clsKeyValue>());
            this.lstCaseName.DataSource = _lst;
            this.lstCaseName.ValueMember = "key";
            this.lstCaseName.DisplayMember = "val";
        }
        void Set_Case(string vCaseName)
        {
            _caseName = vCaseName;
            txt_case2.Text = _faim3.dict_CaseState[_caseName].Remark;
        }
        // 断点名： clsFlow.lstBreakPoint 
        void lstBreakPoint_show()
        {
            if (string.IsNullOrEmpty(_flowName)) return;
            this.listBox3.Items.Clear();

            _lstMark.Clear();
            _hash_id.Clear();
            txtCaseName.Invalidate();
           
            for (int i = 0; i < txtCaseName.Lines.Count; i++)
            {
                string _caseText = txtCaseName.Lines[i];
                if (_flow.lstBreakPoint.Contains(_dictNameText[_caseText]))
                {
                    int id = txtCaseName[i].UniqueId;
                    txt_ChangMark(id);
                    txtCaseName.Invalidate();
                }
            }
            foreach (var _item in _flow.lstBreakPoint)
            {
                this.listBox3.Items.Add(_dictNameText[_item]);

            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            string _name = lstFlowName.SelectedValue.ToString();
            Set_Flow(_name);

            dict_CaseState_show();
            lstBreakPoint_show();
        }

        private void lstFlowName_Click(object sender, EventArgs e)
        {
            if (_isLoad) return;

            string _name = txtFlowName[txtFlowName.Selection.Start.iLine].Text;
            if (_name == "") return;
            Set_Flow(_dictNameText[_name]);

            dict_CaseState_show();
            lstBreakPoint_show();
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            Set_Case(lstCaseName.SelectedValue.ToString());
        }
        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            string _name = lstCaseName.Text;
            txt_DoubleClick(lstCaseName.Text);
        }
        void txt_DoubleClick(string _caseText)
        {
            if (string.IsNullOrEmpty(_caseText)) return;
            _caseName = _dictNameText[_caseText];
            if (_flow.lstBreakPoint.Contains(_caseName))
            {
                _flow.lstBreakPoint.Remove(_caseName);
                listBox3.Items.Remove(_caseText);
            }
            else
            {
                _flow.lstBreakPoint.Add(_caseName);
                listBox3.Items.Add(_caseText); ;
            }
        }
        private void lstCaseName_DoubleClick(object sender, EventArgs e)
        {
            string _name = txtCaseName.Selection.Text;
            txt_DoubleClick(_name);
            txt_ChangMark(txtCaseName);

        }
        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            _flow.lstBreakPoint.Remove(_dictNameText[listBox3.Text]);

            _isLoad = true;
            listBox3.Items.Remove(listBox3.Text);
            _isLoad = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstCaseName_Load(object sender, EventArgs e)
        {

        }
        #region // Paint
        void txt_ChangMark(FACC.FastColoredTextBox ctrl)
        {
            int _uid = txtCaseName[ctrl.Selection.Start.iLine].UniqueId;
            txt_ChangMark(_uid);
            ctrl.Invalidate();
        }
        void txt_ChangMark(int uid)
        {
            //int id = lstCaseName[ctrl.Selection.Start.iLine].UniqueId;
            if (_hash_id.Contains(uid))
            {
                _lstMark.Remove(uid);
                _hash_id.Remove(uid);
            }
            else
            {
                _lstMark.Add(uid);
                _hash_id.Add(uid);
            }
        }
        void txt_PaintLine(object sender, FACC.PaintLineEventArgs e)
        {

            //draw bookmark
            if (_hash_id.Contains(txtCaseName[e.LineIndex].UniqueId))
            {
                e.Graphics.FillEllipse(new LinearGradientBrush(new Rectangle(0 + _refLeft, e.LineRect.Top + _refTop,
                    15, 15), Color.LightPink, Color.Red, 45), 0 + _refLeft, e.LineRect.Top + _refTop, 15, 15);

                //e.Graphics.FillEllipse(new LinearGradientBrush(new Rectangle(0, e.LineRect.Top, 15, 15), Color.White, Color.PowderBlue, 45), 0, e.LineRect.Top, 15, 15);
                e.Graphics.DrawEllipse(Pens.PowderBlue, 0 + _refLeft, e.LineRect.Top + _refTop, 15, 15);
            }
        }
        void txt_LineRemoved(object sender, FACC.LineRemovedEventArgs e)
        {
            foreach (int _uid in e.RemovedLineUniqueIds)
            {
                if (_hash_id.Contains(_uid))
                {
                    _hash_id.Remove(_uid);
                    _lstMark.Remove(_uid);
                }
            }
        }
        #endregion

        int _line1 = -1;
        private void txtFlowName_SelectionChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            //_isLoad = true;
            FastColoredTextBox ctrl = sender as FastColoredTextBox;
            //int id = ctrl[ctrl.Selection.Start.iLine].UniqueId;
            if (_line1 > -1) ctrl[_line1].BackgroundBrush = null;
            ctrl[ctrl.Selection.Start.iLine].BackgroundBrush = Brushes.LightBlue;
            //System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            _line1 = ctrl.Selection.Start.iLine;
            //_isLoad = false;

        }





    }

}
