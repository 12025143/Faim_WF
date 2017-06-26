#region //
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion 
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
        // 测试变量 复位
        private static void _TEST_2_Reset(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, string _caseName)
        {
            clsFlow _flow = _faim3.dict_Flow[_flowName];
            string _alartCase = _flow.alartCase;
            Dictionary<string, clsCaseChildInfo> _dictCaseChildInfo =
                                    _faim3.dict_CaseState[_alartCase].dict_CaseChildInfo;
            string PartName = _TESTB;// "TESTB";
            if (!_dictCaseChildInfo.ContainsKey(PartName))
                return; // 没有此段
            int _lineStart = _dictCaseChildInfo[PartName].lineStart;  // 起始行号
            if (_lineStart < 0)
                return;                  // 条件：没有指定的结构块起始行号

            int _lineEnd = _dictCaseChildInfo[PartName].lineEnd;  // 终止行号
            if (_lineEnd < 0)   // 只有一行时
                _lineEnd = _lineStart;  
            for (int i = _lineStart; i <= _lineEnd; i++)// 文档对象驱动
            {
                if (_faim3.isEmergency > 0) 
                    break; // 急停
                #region // for
                clsDevTestBits _line = _faim3.lst_DevTestBits[i];
                if (_line.Remark.ToUpper().StartsWith("CODE") ||
                    _line.IfType.ToUpper() == "NG") 
                    continue;    //条件：文档行指定为以代码实现
                if (_line.isDebug == 1) /*#*/
                    if (_faim3.dict_KV["isDebug"] != "1") // 非 debug 状态下，不运行 debug 行  cfgDev_Cards.xls
                        continue;
                if (string.IsNullOrEmpty(_line.Reset) || _line.Reset == "-") 
                    continue; // 无复位值

                _do_WaitLine(_faim3, _dao_comm, _flowName, eWF_State.Reset, i); // 单步
                if (_faim3.isEmergency > 0)
                    break; // 急停
                #region // 开始执行
                _line = _faim3.lst_DevTestBits[i];
                int _bit = -1;// 位
                int _val_3 = F_TransCalc._Get_Value_2(_faim3, _line.Reset);
                if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
                {
                    #region // dict_DevFunction IO口定义
                    clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//  
                    _bit = _df.devNo * _faim3.sect_iDev + _df.Index;// 内存位

                    _faim3.Comm_Data.bt_in[0][_bit] = _val_3;// 还原值

                    #endregion
                }
                else if (_faim3._dim_dict.ContainsKey(_line.vName))
                {
                    #region // _dim_dict 动态变量测试
                    _bit = _faim3._dim_dict[_line.vName];// 内存位 自定义名为下标, 分配地址, 寻址
                    _faim3._dim[_bit] = _val_3;
                    #endregion
                }
                else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                         _line.vName.ToUpper().StartsWith("CLSFAIM3"))
                {
                    #region // _FAIM3 大数据
                    F_TransCalc.doSetValue_byName(_faim3, _line.vName, _val_3);
                    #endregion
                }
                #endregion
                #endregion
            }
        }
    }
}
