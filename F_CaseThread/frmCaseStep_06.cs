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
    using F_CaseThread;
    using FACC;
    using F_Enums;
    using System.Drawing.Drawing2D;
    #endregion
    public partial class frmCaseStep_06 : Form
    {
        bool _isLoad = false;
        clsFaim3 _faim3 = null;
        DAL_CommData _dao_comm = null;

        clsFlow _flow = null;
        string _flowName = "";
        string _caseName = "";

        F_ThManager _bo_ThreadMana = null;//    流程 管理器


        List<clsKeyValue> _lst2 = null;
        List<clsKeyValue> _lst3 = null;

        Dictionary<string, string> _dictNameText = new Dictionary<string, string>();

        frmCaseStep_06()
        {
        }
        public frmCaseStep_06(clsFaim3 v_faim3, DAL_CommData v_dao_comm, F_ThManager v_bo_ThreadMana)
        {
            _faim3 = v_faim3;
            _dao_comm = v_dao_comm;
            _dao_comm.OnEchoIO += new F_Delegate.delEcho(_dao_comm_OnEchoIO);
            _bo_ThreadMana = v_bo_ThreadMana;

            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        // 消息  arr_str[0][800 + 流程序号] = eWF_State + 流程名




        void _dao_comm_OnEchoIO(string name, int vChanalNo, List<int> lst, int idx, string val)
        {
            if (name == "arr_str")
            {
                if (idx < 800 || idx > 899) return;
                // 1._flow.eState , 2._flowName, 3.currCase, 4.IfType=HL
                string[] _arr = val.Split(' ');
                string _WF_State = _arr[0];  // 800 + 流程序号
                string _flowName_in = _arr[0];
                string _currCase_in = "";
                string _caseLineIdx = "";
                string _IfType_in = "";
                if (_arr.Length > 2)
                {
                    _flowName_in = _arr[2];
                }
                if (_arr.Length > 2) _currCase_in = _arr[2];
                if (_arr.Length > 3) _caseLineIdx = _arr[3];
                if (_arr.Length > 4) _IfType_in = _arr[4];
                bool _t = _flowName_in.Contains(F_Const.fix_CaseT); // 测试步骤
                if (_t) _flowName_in = _flowName_in.Replace(F_Const.fix_CaseT, "");

                if (_dictNameText.ContainsKey(_flowName_in))
                {
                    string _remark = _dictNameText[_flowName_in];
                    string _T1 = " ";
                    if (_t) _T1 = "测试中";

                    lstCaseTrace.Items.Add(string.Format(
                                    "{0} [{1}]{2} {3} {4}",
                                    _WF_State, _caseLineIdx,
                                    _IfType_in, _remark, _T1));
                    for (int i = 0; i < _lst3.Count; i++)
                    {
                        if (_lst3[i].key == _caseLineIdx)
                        {
                            do_SelectionVisible(txtStepName, i);
                            break;
                        }
                    }
                    for (int i = 0; i < _lst2.Count; i++)
                    {
                        if (_lst2[i].key == _flowName_in)
                        {
                            do_SelectionVisible(txtCaseName, i);
                            break;
                        }
                    }
                }
                else
                    lstCaseTrace.Items.Add(_flowName_in);

            }
        }

        private void form_Load(object sender, EventArgs e)
        {
            _isLoad = true;
            DAL_Faim3.Load_faim3(ref _faim3, ref _dao_comm);
            dict_Flow_show();
            dict_CaseState_show();
            lstBreakPoint_show();
            _isLoad = false;

        }

        // 流程名： cfgFlowName.ini --> fn_FlowName --> List<CfgListFlow> --> _faim3.dict_Flow
        void dict_Flow_show()
        {
            string _txts = "";
            List<clsKeyValue> _lst = new List<clsKeyValue>();
            foreach (var item in _faim3.dict_Flow)
            {
                if (!_dictNameText.ContainsKey(item.Key)) _dictNameText.Add(item.Key, item.Value.Remark);
                if (!_dictNameText.ContainsKey(item.Value.Remark)) _dictNameText.Add(item.Value.Remark, item.Key);

                if (string.IsNullOrEmpty(_flowName)) Set_Flow(item.Key);

                _lst.Add(new clsKeyValue() { key = item.Key, val = item.Value.Remark });
                if (string.IsNullOrEmpty(_txts))
                    _txts = item.Value.Remark;
                else
                    _txts = _txts + Environment.NewLine + item.Value.Remark;
            }
            txtFlowName.Text = _txts;
        }

        void Set_Flow(string vFlowName)
        {
            _flowName = vFlowName;
            _flow = _faim3.dict_Flow[_flowName];
            txt_flow.Text = _flow.Remark;
        }

        void Set_Case(string vCaseName)
        {
            _caseName = vCaseName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }


        private void txtFlowName_Click(object sender, EventArgs e)
        {
            if (_isLoad) return;

            string _name = txtFlowName[txtFlowName.Selection.Start.iLine].Text;
            if (_name == "") return;
            Set_Flow(_dictNameText[_name]);

            dict_CaseState_show();
            lstBreakPoint_show();
            do_Show_lst_DevTestBits();

        }
        private void btn_case_pause_1_Click(object sender, EventArgs e)
        {
            FACC.F_Log.Debug_1("测", string.Format("按钮: 启动流程", 1));
            _bo_ThreadMana.doStopBusiness(_flowName);

            System.Threading.Thread.Sleep(500);
            _bo_ThreadMana.doStopTh(_flowName);
            if (_runId.Contains(_flowName))// 原有流程
            {
                _runId.Remove(_flowName);
            }
            System.Threading.Thread.Sleep(500);
        }
        List<string> _runId = new List<string>();
        // 运行
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!_runId.Contains(_flowName))// 未开启流程
            {
                _runId.Add(_flowName);
                _bo_ThreadMana.doStartTh(_flowName);
                System.Threading.Thread.Sleep(1000);
            }

            if (rd_Step.Checked)
                _bo_ThreadMana.doStartStep(_flowName, this.rd_Step.Checked);
            if (rd_Line.Checked)
                _bo_ThreadMana.doStartStepLine(_flowName, this.rd_Line.Checked);
            if (rd_Break.Checked)
                _bo_ThreadMana.doStartBreak(_flowName);
        }
        private void btnfrmFreeCode_Click(object sender, EventArgs e)
        {
            //frmFreeCode_09 _frm = new frmFreeCode_09(_faim3, _dao_comm);
            //_frm.Show();
            F_MyDLL _boCode = new F_MyDLL(ref _faim3);
            int _isString = 0;
            //if (this.txtCode.Text.IndexOf(';') > 4) _type = 1; // 字符串
            string _rec = _boCode.doGet_Value(
                                    "doMySub",
                                    this.txtCode.Text,
                                    _isString).ToString();
            if (_rec.ToString() == "-1")
                _rec = _boCode.ErrorMessage;

            this.txtResult.Text = _rec;
        }


        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            lstCaseTrace.Items.Clear();
        }

        #region // 当前行显示

        int _line1 = -1;
        int _line2 = -1;
        int _line3 = -1;
        void do_SelectionVisible(FastColoredTextBox ctrl, int lineNo)
        {
            //_bo_ThreadMana.doStartStepLine(_flowName, this.ckStepLine.Checked);
            ctrl.Selection.Start = new Place(0, lineNo);//转到指定行

            ctrl.DoSelectionVisible();// 显示指定行

            ctrl.Invalidate(); // 重绘效果 
        }
        private void txtFlowName_SelectionChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            _isLoad = true;
            FastColoredTextBox ctrl = sender as FastColoredTextBox;
            //int id = ctrl[ctrl.Selection.Start.iLine].UniqueId;
            if (_line1 > -1) ctrl[_line1].BackgroundBrush = null;
            ctrl[ctrl.Selection.Start.iLine].BackgroundBrush = Brushes.LightBlue;
            //System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            _line1 = ctrl.Selection.Start.iLine;
            _isLoad = false;

        }
        private void txtCaseName_SelectionChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            FastColoredTextBox ctrl = sender as FastColoredTextBox;
            //int id = ctrl[ctrl.Selection.Start.iLine].UniqueId;
            if (_line2 > -1) ctrl[_line2].BackgroundBrush = null;
            ctrl[ctrl.Selection.Start.iLine].BackgroundBrush = Brushes.DodgerBlue;
            //System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            _line2 = ctrl.Selection.Start.iLine;

        }
        private void txtStepName_SelectionChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            FastColoredTextBox ctrl = sender as FastColoredTextBox;
            if (_line3 > -1) ctrl[_line3].BackgroundBrush = null;
            ctrl[ctrl.Selection.Start.iLine].BackgroundBrush = Brushes.CadetBlue;
            _line3 = ctrl.Selection.Start.iLine;
        }
        #endregion

        // 刷新流程
        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            DAL_Faim3._RefreshFlow(ref _faim3);
            do_Show_lst_DevTestBits();
        }
        private void btnShowXLS_Click(object sender, EventArgs e)
        {
            //lstCaseTrace.Visible = false;
            //txtShowXLS.Visible = true;

            do_Show_lst_DevTestBits();
        }
        void do_Show_lst_DevTestBits()
        {
            string _txts = "";
            string _flag1 = "";
            //int j = 0;
            _lst3 = new List<clsKeyValue>();
            for (int i = 0; i < _faim3.lst_DevTestBits.Count; i++)
            {
                clsDevTestBits _item = _faim3.lst_DevTestBits[i];
                if (!_item.flowCase.StartsWith(_flowName))
                {
                    if (_txts != "") break;
                    continue;
                }
                string[] _arr = _item.flowCase.Split('_');
                string _str = string.Format("[{0}] {1}  {2} , {3} = {4} , {5}  ",
                    // ((++j).ToString()).PadLeft(2, ' '),
                                i.ToString().PadLeft(2, ' '),
                                (_flag1 != _arr[_arr.Length - 1]) ? _arr[_arr.Length - 1] : "  ",
                                _item.IfType.PadRight(7, ' '),
                                _item.vName,
                                _item.HL,
                                _item.Reset
                                );
                _lst3.Add(new clsKeyValue() { key = i.ToString(), val = i.ToString() });

                if (_flag1 != _arr[_arr.Length - 1]) _flag1 = _arr[_arr.Length - 1];
                _txts = (string.IsNullOrEmpty(_txts)) ? _str : _txts + Environment.NewLine + _str;


            }
            bool _temp = _isLoad;
            _isLoad = true;
            txtStepName.Text = _txts;
            _isLoad = _temp;
        }

        // 步骤名： cfgCaseName.ini --> XX_Case.xls --> fn_CaseName --> List<CfgListCase>() --> _faim3.dict_CaseState

        void dict_CaseState_show()
        {
            if (string.IsNullOrEmpty(_flowName)) return;
            //this.listBox2.Items.Clear();
            _caseName = "";

            string _txts = "";
            _lst2 = new List<clsKeyValue>();
            foreach (var item in _faim3.dict_CaseState)
            {
                if (item.Key.StartsWith(_flowName))
                {
                    if (!_dictNameText.ContainsKey(item.Key)) _dictNameText.Add(item.Key, item.Value.Remark);
                    if (!_dictNameText.ContainsKey(item.Value.Remark)) _dictNameText.Add(item.Value.Remark, item.Key);

                    if (string.IsNullOrEmpty(_caseName)) Set_Case(item.Key);

                    _lst2.Add(new clsKeyValue() { key = item.Key, val = item.Value.Remark });

                    _txts = string.IsNullOrEmpty(_txts) ?
                                item.Value.Remark :
                                _txts + Environment.NewLine + item.Value.Remark;
                }
            }
            bool _temp = _isLoad;
            _isLoad = true;
            txtCaseName.Text = _txts;
            _isLoad = _temp;
        }

        private void txtCaseName_DoubleClick(object sender, EventArgs e)
        {

            string _name = txtCaseName.Selection.Text;
            txt_DoubleClick(_name);
            txt_ChangMark(txtCaseName);
        }

        void txt_DoubleClick(string _caseText)
        {
            if (string.IsNullOrEmpty(_caseText)) return;
            _caseName = _dictNameText[_caseText];
            if (_flow.lstBreakPoint.Contains(_caseName))
            {
                _flow.lstBreakPoint.Remove(_caseName);
            }
            else
            {
                _flow.lstBreakPoint.Add(_caseName);
            }
        }

        int _refTop = 5;
        int _refLeft = 4;
        HashSet<int> _hash_id = new HashSet<int>();
        List<int> _lstMark = new List<int>();

        // 断点名： clsFlow.lstBreakPoint 
        void lstBreakPoint_show()
        {
            if (string.IsNullOrEmpty(_flowName)) return;

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
        }
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

        private void txtCaseName_Leave(object sender, EventArgs e)
        {

        }
    }
}
