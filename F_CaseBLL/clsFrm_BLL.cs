#region // ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion 
namespace F_CaseBLL
{
    #region // ...
    using F_Entitys;
    using F_Entitys_DAL;
    using F_CaseThread;
    using F_CaseWhile;
    #endregion 
    public class clsFrm_BLL
    {
        clsFaim3 _faim3 = null;
        DAL_CommData _dao_comm = null;
        F_ThManager _bo_ThreadMana = null;
        public clsFrm_BLL(clsFaim3 v_faim3, DAL_CommData v_dao_comm, F_ThManager v_bo_ThreadMana)
        {
            _faim3 = v_faim3;
            _dao_comm = v_dao_comm;
            _bo_ThreadMana = v_bo_ThreadMana;
        }

        // 执行指定的 流程
        public void doRunFlow(string vflowName)
        {
            if (string.IsNullOrEmpty(vflowName)) return;
            if (!_faim3.dict_Threads.ContainsKey(vflowName))
            {
                clsFlow _flow = _faim3.dict_Flow[vflowName];
                if (_flow.nextCase.ToUpper() != "FREE")// 存在
                    clsTool_ASM.doCreateInstance(_faim3, _dao_comm, vflowName);
            }
            _bo_ThreadMana.doRunFlow(vflowName);
        }

        // 根据控件的 名称 执行指定的xls命令
        public void do_BtnbyName(string vBtnId)
        {
            string[] lines = tb_btnScript.getScript(_faim3, vBtnId);
            do_BtnbyLines(lines);
        }
        // 根据文本 执行指定的xls命令
        public void do_BtnbyLine(string vLine)
        {
            do_BtnbyLines(new string[] { vLine });
        }
        // 根据多行文本 执行指定的xls命令
        public void do_BtnbyLines(List<string> vLines)
        {
            do_BtnbyLines(vLines.ToArray());
        }
        // 根据多行文本 执行指定的xls命令
        public void do_BtnbyLines(string[] vLines)
        {
            // 将指令替换“DAL_Buttons”的占位行后， 启动“DAL_Buttons”流程

            clsTool_ASM.doCreateButton(ref _faim3, ref _dao_comm, vLines);
            _bo_ThreadMana.doRunFlow(); // 缺省为: _bo_ThreadMana.doRunFlow("DAL_Buttons");
        }

    }
}
