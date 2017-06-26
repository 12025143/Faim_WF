using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_CaseCmd
{
    #region // ...
    using F_Entitys;
    using F_Enums;
    using FACC;
    using F_Entitys_DAL;
    #endregion
    public partial class F_CaseSub
    {
        static void __NG(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, string _caseName)
        {
            if (_flowName == "DAL_Buttons") return;
            clsFlow _flow = _faim3.dict_Flow[_flowName];
            string _alartCase = _flow.alartCase;
            //if (!_faim3.dict_CaseState.ContainsKey(_alartCase)) return;  // 
            clsCaseState _caseState = _faim3.dict_CaseState[_alartCase];
            Dictionary<string, clsCaseChildInfo> _dictCaseChildInfo = _caseState.dict_CaseChildInfo;
            if (!_dictCaseChildInfo.ContainsKey(_NG_D))
                return; // 指定步骤无此部份指令
            int _lineStart = _dictCaseChildInfo[_NG_D].lineStart;
            if (_lineStart < 0)
                return;                  // 条件：没有指定的结构块起始行号

            _do_WaitLine(_faim3, _dao_comm, _flowName, eWF_State.JMP, _lineStart);
            if (_faim3.isEmergency > 0) return;
            #region // 开始执行

            clsDevTestBits _line = _faim3.lst_DevTestBits[_lineStart];
            if (_line.isDebug == 1) /*#*/
                if (_faim3.dict_KV["isDebug"] != "1") // 非 debug 状态下，不运行 debug 行  cfgDev_Cards.xls
                    return;
            //string _line_vName = _line.vName;
            if (_flow.dictCases.ContainsKey(_line.vName) || 
                _line.vName.ToUpper().Trim() == "NEXT" ||
                _line.vName.ToUpper().Trim() == "NEXTCASE") // 5. 转向目标正确
            {
                _flow.Tag = "GOTO_NG";//  设NG跳转标志
                if (_flow.NGtimes == 0) // 首次，初始设次数
                    _flow.NGtimes = Convert.ToInt32(string.IsNullOrEmpty(_line.HL) ? "1" : _line.HL);
                
                if (_flow.NGtimes < 2) // 到NG次数, 则转向
                {
                    _flow.NGtimes = 0;
                    _caseState.endMode = eCaseFlag.NG;
                    if (_line.vName.ToUpper().Trim() == "NEXT" ||
                        _line.vName.ToUpper().Trim() == "NEXTCASE") // 转向下一步骤 NestCase 的第一行号                    
                        _flow.nextCase = _flow.dictCases[_alartCase].nextCase; // 设跳转步骤名                    
                    else                    
                        _flow.nextCase = _line.vName;                    
                }
                else
                {
                    _flow.NGtimes--;
                }
            }
            else// '流程结束
            {
                F_Log.Debug_1("NG", String.Format("--->>>> {0} NG指定之步骤名不存在", _line.vName));
                _caseState.endMode = eCaseFlag.Exist;
                _flow.nextCase = "Free";
            }
            #endregion
            _line.times++;   // 此行使用的次数
        }
    }
}
