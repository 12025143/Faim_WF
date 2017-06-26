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
    using F_Protocols;
    #endregion
    public partial class F_CaseSub
    {
        static void _TEST_2(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, string _caseName, ref bool _vbl)
        {
            if (_flowName == "DAL_Buttons")
            {
                _vbl = true;
                return;
            }
            bool _mybl = true;
            clsFlow _flow = _faim3.dict_Flow[_flowName];
            string _alartCase = _flow.alartCase;
            clsCaseState _caseState = _faim3.dict_CaseState[_alartCase];
            Dictionary<string, clsCaseChildInfo> _dictCaseChildInfo =
                                    _caseState.dict_CaseChildInfo;
            string PartName = _TESTB;// "TESTB";
            string _IfType = _CMD_TEST;
            #region // 指定步骤无此部份指令
            if (!_dictCaseChildInfo.ContainsKey(PartName))// 条件：指定步骤无此部份指令
            {
                _vbl = _mybl;
                return;
            }
            #endregion
            int _lineStart = _dictCaseChildInfo[PartName].lineStart;  // 起始行号
            if (_lineStart < 0)
                return;                  // 条件：没有指定的结构块起始行号
            int _lineEnd = _dictCaseChildInfo[PartName].lineEnd;  // 终止行号
            if (_lineEnd < 0)   // 只有一行时
                _lineEnd = _lineStart;
            for (int i = _lineStart; i <= _lineEnd; i++) // 文档对象驱动
            {
                if (_faim3.isEmergency > 0)
                    break; // 急停
                #region // for
                clsDevTestBits _line = _faim3.lst_DevTestBits[i];
                if (_line.Remark.ToUpper().StartsWith("CODE") ||
                    _line.IfType.ToUpper() == "NG")
                    continue;    //条件：文档行指定为以代码实现
                if (F_CaseSub._CMD_NULL.Contains(_line.IfType.ToUpper())) // 占位行
                    continue;
                if (_line.isDebug == 1) /*#*/
                    if (_faim3.dict_KV["isDebug"] != "1") // 非 debug 状态下，不运行 debug 行  cfgDev_Cards.xls
                        continue;
                _do_WaitLine(_faim3, _dao_comm, _flowName, eWF_State.Test, i);// 单步
                if (_faim3.isEmergency > 0)
                    break; // 急停
                if (_flow.isStepPass || _faim3.isStepPass != 0) // 步骤直通
                {
                    _mybl = true;
                    continue;
                }
                #region // 开始执行
                _line = _faim3.lst_DevTestBits[i];
                System.Diagnostics.Stopwatch _tmr = new System.Diagnostics.Stopwatch();	//实例化一个计时器
                _tmr.Start();
                long _trm_begin = _tmr.ElapsedMilliseconds; // 毫秒
                int _bit = -1;// 位
                int _val_1 = -1;
                string _str_2 = "   ";
                int _val_2 = F_TransCalc._Get_Value_2(_faim3, _line.HL, ref _str_2);
                int _val_3 = -1;
                if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
                {
                    #region // dict_DevFunction IO口定义 vName : 4D_GS, 6RS_0302, 6D_ER, 6D_RP
                    clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//  
                    _bit = _faim3.sect_iDev * _df.devNo + _df.Index;// 内存位
                    int _ref_1i = -1;
                    int _ref_1s = -1;
                    switch (_df.varName.ToUpper())
                    {
                        // MODBUS
                        case "MODBUS":
                            _ref_1i = _faim3.sect_iDev * _df.devNo;
                            _bit = _faim3.sect_iDev * _df.devNo + _df.Index;
                            // 取比较值
                            _val_1 = _faim3.Comm_Data.bt_in[0][_bit];
                            break;
                        // 74X
                        case "BT_IN":
                            _ref_1i = _faim3.sect_iDev * _df.devNo;
                            _ref_1s = _faim3.sect_sDev * _df.devNo + _faim3.sect_sDev_start;
                            _faim3.Comm_Data.bt_out[0][_ref_1i + Convert.ToInt32(_faim3.dict_KV["LLine"])] = _df.ioIdx;
                              _bit = _faim3.sect_iDev * _df.devNo + _df.ioIdx;
                            // 发出消息 条件：1.bt_out  2._devNo   3.snd_iLen
                            _faim3.Comm_Data.bt_out[0][_ref_1i + _faim3.snd_iLen + 1] = -99;
                            _dao_comm.set_bt_out(_ref_1i + _faim3.snd_iLen + 1, _bit);  // bt_in[164]=6  DI_R1：DI_ReadLine
                            // 取比较值
                            _val_1 = _faim3.Comm_Data.bt_in[0][_bit];
                            break;
                        case "BT_OUT":
                            _ref_1i = _faim3.sect_iDev * _df.devNo;
                            _bit = _faim3.sect_iDev * _df.devNo + _df.Index;
                            // 取比较值
                            _val_1 = _faim3.Comm_Data.bt_out[0][_bit];
                            break;
                        // 204C
                        case "A_ST":
                        case "A_IO":
                            clsProtocol._204C(_faim3, _dao_comm, _df.varName, _df.ioIdx.ToString(), _df.devNo);  // 发出自定义命令                       
                            _val_1 = _faim3.Comm_Data.bt_in[0][_bit]; // 取出值, 可能多次才能取到
                            break;
                        // RS485
                        case "ReadPos":
                        case "ReadStatus":
                        case "READPOS":
                        case "READSTATUS":
                            // 发出命令
                            int _devNo_1 = clsProtocol.getSendData(_faim3, i);
                            _ref_1i = _faim3.sect_iDev * _devNo_1;
                            _ref_1s = _faim3.sect_sDev * _devNo_1 + _faim3.sect_sDev_start;
                            // 发出消息 条件：1.bt_out  2._devNo   3.snd_iLen
                            _faim3.Comm_Data.bt_out[0][_ref_1i + _faim3.snd_iLen] = -99;
                            _dao_comm.set_bt_out(_ref_1i + _faim3.snd_iLen, 1);
                            //
                            _val_3 = F_TransCalc._Get_Value_2(_faim3, _line.Reset);
                            _val_1 = _faim3.Comm_Data.bt_in[0][_bit]; // 取出值, 可能多次才能取到
                            break;
                    }
                    #endregion
                }
                else if (_faim3._dim_dict.ContainsKey(_line.vName))
                {
                    #region // _dim_dict  动态变量
                    _bit = _faim3._dim_dict[_line.vName];// 自定义名为下标, 分配地址, 寻址
                    _val_1 = _faim3._dim[_bit];
                    #endregion
                }
                else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                         _line.vName.ToUpper().StartsWith("CLSFAIM3"))
                {
                    #region // _FAIM3 大数据
                    _bit = 1;
                    _val_1 = Convert.ToInt32(F_TransCalc.doGetValue_byName(_faim3, _line.vName));
                    #endregion
                }
                _line.times++;   // 此行使用的次数
                _line.tmr = (int)(_tmr.ElapsedMilliseconds - _trm_begin);  // 本次流程完成用时
                _faim3.dict_Threads[_flowName].Info.tmr_real += _line.tmr;    // 当前步骤 实际占用总时长 += 每语句使用的时间
                // TEST
                if (_bit > -1)
                {
                    if (_flow.isStepLine || _flow.isStepLine)
                    {
                        int _loc = 0;
                        foreach (var item in _faim3.dict_Flow)
                        {
                            if (item.Key == _flowName)// 流程名 转 流程号
                            {
                                string _msg = string.Format("TEST {0} {1} {2} {3} {4} {5}",
                                  _flow.currCase,
                                  _flowName,
                                  _bit,
                                  _val_1,
                                  _line.IfType,
                                  _val_2
                                  );
                                _dao_comm.set_arr_str(_loc + 800, _msg);
                                break;
                            }
                            _loc++;
                        }
                    }
                    #region // 测试 AND
                    if (_line.IfType == "EQ_AND" || _line.IfType == "AND_EQ" || _line.IfType == "EQ")
                    {
                        _mybl = _mybl && (_val_1 == _val_2);//
                    }
                    else if (_line.IfType == "NE_AND" || _line.IfType == "AND_NE" || _line.IfType == "NE")
                    {
                        _mybl = _mybl && (_val_1 != _val_2);//
                    }
                    else if (_line.IfType == "GT_AND" || _line.IfType == "AND_GT" || _line.IfType == "GT")
                    {
                        _mybl = _mybl && (_val_1 > _val_2);//
                    }
                    else if (_line.IfType == "GE_AND" || _line.IfType == "AND_GE" || _line.IfType == "GE")
                    {
                        _mybl = _mybl && (_val_1 >= _val_2);//
                    }
                    else if (_line.IfType == "LT_AND" || _line.IfType == "AND_LT" || _line.IfType == "LT")
                    {
                        _mybl = _mybl && (_val_1 < _val_2);//
                    }
                    else if (_line.IfType == "LE_AND" || _line.IfType == "AND_LE" || _line.IfType == "LE")
                    {
                        _mybl = _mybl && (_val_1 <= _val_2);//
                    }
                    #endregion  // 测试 AND
                    #region // BIT BITN
                    else if (_line.IfType == "BIT")  // 为高有效
                    {
                        // _val_2= 5; Math.Pow(2, _val_2) = 32.0 = 0x20 = 0010 0000
                        if (_val_2 < 0) _val_2 = 0;
                        bool _rb = false;
                        if (_val_1 > -1)
                            _rb = (_val_1 & (int)Math.Pow(2, _val_2)) == Math.Pow(2, _val_2);
                        else
                            _rb = false;
                        _mybl = _mybl && _rb;//
                    }
                    else if (_line.IfType == "BITN")  // 为低有效
                    {
                        if (_val_2 < 0) _val_2 = 0;
                        bool _rb = false;
                        if (_val_1 > -1)
                            _rb = (_val_1 & (int)Math.Pow(2, _val_2)) != Math.Pow(2, _val_2);
                        else
                            _rb = false;
                        _mybl = _mybl && (_rb);//
                    }
                    #endregion
                    #region // 测试 OR
                    else if (_line.IfType == "EQ_OR" || _line.IfType == "OR_EQ")
                    {
                        _mybl = _mybl || (_val_1 == _val_2);//
                        if (_mybl)
                        {
                            break;
                        }
                    }
                    else if (_line.IfType == "NE_OR" || _line.IfType == "OR_NE")
                    {
                        _mybl = _mybl || (_val_1 != _val_2);//
                        if (_mybl)
                        {
                            break;
                        }
                    }
                    else if (_line.IfType == "GT_OR" || _line.IfType == "OR_GT")
                    {
                        _mybl = _mybl || (_val_1 > _val_2);//
                        if (_mybl)
                        {
                            break;
                        }
                    }
                    else if (_line.IfType == "GE_OR" || _line.IfType == "OR_GE")
                    {
                        _mybl = _mybl || (_val_1 >= _val_2);//
                        if (_mybl)
                        {
                            break;
                        }
                    }
                    else if (_line.IfType == "LT_OR" || _line.IfType == "OR_LT")
                    {
                        _mybl = _mybl || (_val_1 < _val_2);//
                        if (_mybl)
                        {
                            break;
                        }
                    }
                    else if (_line.IfType == "LE_OR" || _line.IfType == "OR_LE")
                    {
                        _mybl = _mybl || (_val_1 <= _val_2);//
                        if (_mybl)
                        {
                            break;
                        }
                    }
                    #endregion // 测试 OR
                }
                #endregion // 开始执行
                #endregion // for
            }
            _flow.isStepPass = false;   // 取消直通
            _vbl = _mybl;
            return;
        }
    }
}
