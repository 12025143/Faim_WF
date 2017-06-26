using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace F_CaseThread
{
    using FACC;
    using F_Entitys;
    using F_Entitys_DAL;

    public partial class frmFreeCode_09 : Form
    {
        bool _isLoad = false;
        clsFaim3 _faim3 = null;
        DAL_CommData _dao_comm = null;

        frmFreeCode_09()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        public frmFreeCode_09(clsFaim3 v_faim3, DAL_CommData v_dao_comm)
        {
            _faim3 = v_faim3;
            _dao_comm = v_dao_comm;
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _isLoad = true;
            DAL_Faim3.Load_faim3(ref _faim3, ref _dao_comm);

            _isLoad = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            F_MyDLL _boCode = new F_MyDLL(ref _faim3);
            int _type = 0;
            if (rd_str.Checked) _type = 1;
            string _rec = _boCode.doGet_Value(
                                    this.txtMethod.Text,
                                    this.txtCode.Text,
                                    _type).ToString();
            if (_rec.ToString() == "-1")
                _rec = _boCode.ErrorMessage;

            this.txtResult.Text = _rec;
        }

    }
}
