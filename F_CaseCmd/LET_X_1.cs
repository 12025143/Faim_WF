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
        // 赋值
        static void _LET_X_old(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, string _caseName, string vIfType)
        {
            clsFlow _flow = _faim3.dict_Flow[_flowName];
            #region // 条件
            Dictionary<string, clsCaseChildInfo> _dictCaseChildInfo =
                                _faim3.dict_CaseState[_flow.alartCase].dict_CaseChildInfo;
            // string vIfType = "Line";
            if (!_dictCaseChildInfo.ContainsKey(vIfType)) return;
            int _lineStart = _dictCaseChildInfo[vIfType].lineStart;  // 起始行号
            string _IfType = vIfType + ".GOTO.ADD.DEC.INC.";
            if (_lineStart < 0) return;                  // 条件：没有指定的结构块起始行号
            clsDevTestBits _line = _faim3.lst_DevTestBits[_lineStart];
            if (_line.flowCase != _caseName) return; // 条件：顺序执行时， 如步骤名不对， 则结束当前结构
            if (!_IfType.Contains(_line.IfType.ToUpper() + ".")) return;//条件：没有结构块指定的命令
            #endregion

            int _cnt_break = 0; // 计数器
            for (int i = _lineStart; i < _faim3.lst_DevTestBits.Count; i++)// 文档对象驱动
            {
                if (_faim3.isEmergency > 0) break;
                #region // for
                _line = _faim3.lst_DevTestBits[i];
                if (_line.Remark.ToUpper().StartsWith("CODE")) continue;    //条件：文档行指定为以代码实现
                if (_line.flowCase != _caseName) break; // 条件：顺序执行时， 如步骤名不对， 则结束当前结构
                if (!_IfType.Contains(_line.IfType.ToUpper() + ".")) break;//条件：没有结构块指定的命令
                _do_WaitLine(_faim3, _dao_comm, _flowName, eWF_State.Line, i);
                #region // 开始执行

                System.Diagnostics.Stopwatch _tmr = new System.Diagnostics.Stopwatch();	//实例化一个计时器
                _tmr.Start();
                long _trm_begin = _tmr.ElapsedMilliseconds; // 毫秒

                int _bit = -1;// 位
                int _val_2 = F_TransCalc._Get_Value_2(_faim3, _line.HL);
                #region // SLEEP GOTO ADD DEC INC
                if ("SLEEP" == _line.vName.ToUpper()) // 
                {
                    System.Threading.Thread.Sleep(_val_2);
                }
                else if ("GOTO" == _line.IfType.ToUpper())
                {
                    _flow.nextCase = _line.vName;
                }
                else if ("ADD" == _line.IfType.ToUpper())
                {
                    if (_faim3._dim_dict.ContainsKey(_line.vName))
                    {
                        #region // _dim_dict 动态变量
                        _bit = _faim3._dim_dict[_line.vName];// 内存位 自定义名为下标, 分配地址, 寻址
                        _faim3._dim[_bit] += _val_2;  // 直接设置, 用以联动     dim[Feeder05_unLink] = Value 
                        #endregion
                    }
                    else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                             _line.vName.ToUpper().StartsWith("CLSFAIM3"))
                    {
                        int _ls = Convert.ToInt32(F_TransCalc.doGetValue_byName(_faim3, _line.vName));
                        F_TransCalc.doSetValue_byName(_faim3, _line.vName, _ls + _val_2);
                    }
                }
                else if ("DEC" == _line.IfType.ToUpper())
                {
                    if (_faim3._dim_dict.ContainsKey(_line.vName))
                    {
                        #region // _dim_dict 动态变量
                        _bit = _faim3._dim_dict[_line.vName];// 内存位 自定义名为下标, 分配地址, 寻址
                        _faim3._dim[_bit] -= _val_2;  // 直接设置, 用以联动     dim[Feeder05_unLink] = Value 
                        #endregion
                    }
                    else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                             _line.vName.ToUpper().StartsWith("CLSFAIM3"))
                    {
                        int _ls = Convert.ToInt32(F_TransCalc.doGetValue_byName(_faim3, _line.vName));
                        F_TransCalc.doSetValue_byName(_faim3, _line.vName, _ls - _val_2);
                    }
                }
                else if ("INC" == _line.IfType.ToUpper())
                {
                    if (_faim3._dim_dict.ContainsKey(_line.vName))
                    {
                        #region // _dim_dict 动态变量
                        _bit = _faim3._dim_dict[_line.vName];// 内存位 自定义名为下标, 分配地址, 寻址
                        _faim3._dim[_bit] += 1;  // 直接设置, 用以联动     dim[Feeder05_unLink] = Value 
                        #endregion
                    }
                    else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                             _line.vName.ToUpper().StartsWith("CLSFAIM3"))
                    {
                        int _ls = Convert.ToInt32(F_TransCalc.doGetValue_byName(_faim3, _line.vName));
                        F_TransCalc.doSetValue_byName(_faim3, _line.vName, _ls - 1);
                    }
                }
                #endregion
                else if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
                {
                    #region // BT_OUT
                    clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//
                    int _devNo = _df.devNo;//设备号
                    int _refIdx = _faim3.sect_idev * _devNo;
                    _bit = _refIdx + _df.Index;//内存位
                    if (_df.varName.ToUpper() == "BT_OUT")
                    {
                        _faim3.Comm_Data.bt_out[0][_bit] = _val_2;//输出电位
                    }
                    #endregion

                }
                else if (_faim3._dim_dict.ContainsKey(_line.vName))
                {
                    #region // _dim_dict 动态变量 dim[Feeder05_unLink] <== Value
                    _bit = _faim3._dim_dict[_line.vName];// 内存位
                    _faim3._dim[_bit] = _val_2;  // 直接设置, 用以联动    
                    #endregion
                }
                else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                         _line.vName.ToUpper().StartsWith("CLSFAIM3"))
                {
                    F_TransCalc.doSetValue_byName(_faim3, _line.vName, _val_2);
                }
                else if (_cnt_break > 0)  // 截断
                {
                    break;
                }
                #endregion
                _cnt_break++;
                _line.times++;   // 此行使用的次数 

                _line.tmr  = (int)(_tmr.ElapsedMilliseconds - _trm_begin);  // 本次流程完成用时
                _faim3.dict_Threads[_flowName].Info.tmr_real += _line.tmr;    // 当前步骤 实际占用总时长 += 每语句使用的时间
                #endregion
            }
        }
        static void _SetValue(clsFaim3 _faim3, clsDevTestBits _line, int _val_2)
        {
            int _bit = -1;// 位
            if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
            {
                #region // BT_OUT
                clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//
                int _devNo = _df.devNo;//设备号
                int _refIdx = _faim3.sect_idev * _devNo;
                _bit = _refIdx + _df.Index;//内存位
                if (_df.varName.ToUpper() == "BT_OUT")
                {
                    _faim3.Comm_Data.bt_out[0][_bit] = _val_2;//输出电位
                }
                #endregion

            }
            else if (_faim3._dim_dict.ContainsKey(_line.vName))
            {
                #region // _dim_dict 动态变量 dim[Feeder05_unLink] <== Value
                _bit = _faim3._dim_dict[_line.vName];// 内存位
                _faim3._dim[_bit] = _val_2;  // 直接设置, 用以联动    
                #endregion
            }
            else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                     _line.vName.ToUpper().StartsWith("CLSFAIM3"))
            {
                F_TransCalc.doSetValue_byName(_faim3, _line.vName, _val_2);
            }
        }
    }
}
