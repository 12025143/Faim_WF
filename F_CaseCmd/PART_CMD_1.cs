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
    using F_Protocols;
    #endregion
    public partial class F_CaseSub
    {
        // _PART_CMD("PARTA")  "PARTC"
        static void _PART_CMD(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, string _caseName, string PartName)
        {
            #region // 条件
            clsFlow _flow = _faim3.dict_Flow[_flowName];
            string _alartCase = _flow.alartCase;        // _caseName 可能后带有 _T 标记
            clsCaseState _caseState = _faim3.dict_CaseState[_alartCase];
            Dictionary<string, clsCaseChildInfo> _dictCaseChildInfo =
                                                _caseState.dict_CaseChildInfo;
            if (!_dictCaseChildInfo.ContainsKey(PartName))
                return; // 条件：指定步骤无此部份指令



            int _lineStart = _dictCaseChildInfo[PartName].lineStart;
            if (_lineStart < 0)
                return;                  // 条件：没有指定的结构块起始行号



            int _lineEnd = _dictCaseChildInfo[PartName].lineEnd;
            if (_lineEnd < 0)   // 只有一行时
                _lineEnd = _lineStart;
            #endregion

            // "DIM.GOTO.ADD.DEC.INC.OUT.OUT_A.OUT_T.OUT_AOK.OUT_TOK"
            for (int i = _lineStart; i <= _lineEnd; i++)// 文档对象驱动
            {
                #region // for
                if (_faim3.isEmergency > 0)
                    break;  // 急停

                #region // 忽略 CODE isDebug #
                clsDevTestBits _line = _faim3.lst_DevTestBits[i];
                if (_line.Remark.ToUpper().StartsWith("CODE"))
                    continue;    //条件：文档行指定为以代码实现
                if (F_CaseSub._CMD_NULL.Contains(_line.IfType.ToUpper())) // 占位行
                    continue;

                if (_line.isDebug == 1) /*#*/   // cfgDev_testBits #
                    if (_faim3.dict_KV["isDebug"] != "1") // 非 debug 状态下，不运行 debug 行  cfgDev_Cards.xls
                        continue;
                #endregion

                #region // 暂停别
                eWF_State _state = eWF_State.Free;
                #region // 可置于 vName 列的指令
                string _IfType = _line.vName.ToUpper();
                switch (_IfType)
                {
                    case "SLEEP":
                    case "DELAY":
                    case "WAIT":  // WAIT 2
                    case "TIMW":  // WAIT 2
                        _state = eWF_State.Sleep;
                        _do_WaitLine(_faim3, _dao_comm, _flowName, _state, i); // 
                        break;
                    case "GOTO":
                    case "JMP":
                    case "NEXT":
                    case "NEXTCASE":
                        _state = eWF_State.JMP;
                        _do_WaitLine(_faim3, _dao_comm, _flowName, _state, i); // 
                        break;
                }
                #endregion
                #region // 可置于 IfType 列的指令
                if (_state == eWF_State.Free)   //  
                {
                    _IfType = _line.IfType.ToUpper();
                    switch (_IfType)    // IfType
                    {
                        case "SLEEP":
                        case "DELAY":
                        case "TIMW":  // WAIT 1
                            _state = eWF_State.Sleep;
                            _do_WaitLine(_faim3, _dao_comm, _flowName, _state, i); // 
                            break;
                        case "GOTO":
                        case "JMP":
                        case "NEXT":
                        case "NEXTCASE":
                            _state = eWF_State.JMP;
                            _do_WaitLine(_faim3, _dao_comm, _flowName, _state, i); // 
                            break;
                        case "ADD":
                        case "SUB":
                        case "INC":
                        case "DEC":
                            _state = eWF_State.Math;
                            _do_WaitLine(_faim3, _dao_comm, _flowName, _state, i); // 
                            break;
                        case "OUT":
                        case "SEND":
                        case "MBUS":
                        case "CONN":
                            _state = eWF_State.Send;
                            _do_WaitLine(_faim3, _dao_comm, _flowName, _state, i); // 
                            break;

                        default: // 其它
                            _do_WaitLine(_faim3, _dao_comm, _flowName, eWF_State.Line, i); //
                            break;
                    }
                }
                #endregion
                #endregion

                if (_faim3.isEmergency > 0)
                    break;  // 急停


                #region // 开始执行
                System.Diagnostics.Stopwatch _tmr = new System.Diagnostics.Stopwatch();	//实例化一个计时器
                _tmr.Start();
                long _trm_begin = _tmr.ElapsedMilliseconds; // 毫秒


                #region // TITL TBNO
                string _str_1 = "  ";  // 值 vName  
                string _str_2 = "  ";  // 值 HL

                int _val_1 = 0; // 值: vName
                int _val_2 = F_TransCalc._Get_Value_2(_faim3, _line.HL, ref _str_2); // 值: HL
                int _val_3 = 0; // 值: Reset
                int _bit = -1;// 位
                if ("TITL" == _line.IfType.ToUpper() ||     // 流程步骤名
                    "TITLE" == _line.IfType.ToUpper())
                {
                    _caseState.Remark = _line.vName.Trim();
                    //continue;
                }
                else if ("TBNO" == _line.IfType.ToUpper())  // 设置当前使用的表号
                {
                    _flow.tableNo = Convert.ToInt32(_line.vName.Trim());
                    //continue;
                }
                #endregion

                #region // SLEEP DELAY TIMW WAIT
                else if ("SLEEP" == _line.IfType.ToUpper() ||       // IfType
                         "DELAY" == _line.IfType.ToUpper() ||
                         "TIMW" == _line.IfType.ToUpper() ||
                         "WAIT" == _line.IfType.ToUpper() ||
                         "SLEEP" == _line.vName.ToUpper() ||        // vName
                         "DELAY" == _line.vName.ToUpper() ||
                         "TIMW" == _line.vName.ToUpper() ||
                         "WAIT" == _line.vName.ToUpper())
                {
                    System.Threading.Thread.Sleep(_val_2);
                }
                #endregion

                #region  // 设备 _204C: A_ST A_IO LOAD HOME STOP EMG MOVA MOVR MOVL MOVI ARCR ARCA STAT INPUT
                else if (clsProtocol.doExistCmd_204(_line.IfType.ToUpper()) > -1) // 指令
                {
                    int _devNo = -1;//设备号
                    if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
                    {
                        clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//
                        _devNo = _df.devNo;//设备号
                    }
                    else
                    {
                        _devNo = clsProtocol.do_GetDevNo(_faim3, _line.vName);
                    }
                    if (_devNo > -1)  // Send
                        clsProtocol._204C(_faim3, _dao_comm, _line.IfType, _line.HL, _devNo);
                }
                #endregion

                #region // ADD SUB INC DEC
                else if ("ADD" == _line.IfType.ToUpper() || "ADD" == _line.vName.ToUpper())
                {
                    _do_math(_faim3, _line, _val_2);
                }
                else if ("SUB" == _line.IfType.ToUpper() || "SUB" == _line.vName.ToUpper())
                {
                    _do_math(_faim3, _line, -_val_2);
                }
                else if ("INC" == _line.IfType.ToUpper() || "INC" == _line.vName.ToUpper())
                {
                    _do_math(_faim3, _line, 1);
                }
                else if ("DEC" == _line.IfType.ToUpper() || "DEC" == _line.vName.ToUpper())
                {
                    _do_math(_faim3, _line, -1);
                }
                #endregion

                #region // CONN
                else if (_line.IfType.ToUpper().StartsWith("CONN")) // 设备连接
                {
                    _val_1 = F_TransCalc._Get_Value_2(_faim3, _line.vName); // 值: vName
                    int _ref_i_len = _faim3.sect_iDev * _val_1 + _faim3.snd_iLen;
                    // out
                    _faim3.Comm_Data.bt_out[0][_ref_i_len] = -99; // 
                    if (_val_2 > 0)
                        _dao_comm.set_bt_out(_ref_i_len, (int)eSwitch.Connect);     // bt_out[164]=-100
                    else
                        _dao_comm.set_bt_out(_ref_i_len, (int)eSwitch.Disconnect);  // bt_out[164]=-200
                }
                #endregion

                #region //  BTOF BTON TEXT TBSE LET  DIM
                else if (_line.IfType.ToUpper().StartsWith("BTOF"))
                {
                    #region // dict_DevFunction
                    if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
                    {
                        // '0devNo  	1driverName   	2Remark     	3ioIdx    	4Off|On  	5varName   	6Index      	registerId	Enabled	9CardName
                        clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//
                        _bit = _faim3.sect_iDev * _df.devNo + _df.Index;//内存位

                        if (_df.varName.ToUpper() == "BT_OUT")
                        {
                            if (_faim3.Comm_Data.bt_out[0][_bit] >= (int)Math.Pow(2, _val_2))
                            {
                                _faim3.Comm_Data.bt_out[0][_bit] = _faim3.Comm_Data.bt_out[0][_bit] & ~(int)Math.Pow(2, _val_2);//输出电位
                            }
                        }
                        else if (_df.varName.ToUpper() == "BT_IN")
                        {
                            if (_faim3.Comm_Data.bt_in[0][_bit] >= (int)Math.Pow(2, _val_2))
                            {
                                _faim3.Comm_Data.bt_in[0][_bit] = _faim3.Comm_Data.bt_in[0][_bit] & ~(int)Math.Pow(2, _val_2);//输出电位
                            }
                        }
                    }
                    #endregion
                }
                else if (_line.IfType.ToUpper().StartsWith("BTON"))
                {
                    #region // dict_DevFunction
                    if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
                    {
                        clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//
                        _bit = _faim3.sect_iDev * _df.devNo + _df.Index;//内存位
                        if (_df.varName.ToUpper() == "BT_OUT")  // 5 varName   
                        {
                            _faim3.Comm_Data.bt_out[0][_bit] = _faim3.Comm_Data.bt_out[0][_bit] | (int)Math.Pow(2, _val_2);//输出电位
                        }
                    }
                    else if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
                    {
                        clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//
                        _bit = _faim3.sect_iDev * _df.devNo + _df.Index;//内存位
                        if (_df.varName.ToUpper() == "BT_IN")  // 5 varName   
                        {
                            _faim3.Comm_Data.bt_in[0][_bit] = _faim3.Comm_Data.bt_in[0][_bit] | (int)Math.Pow(2, _val_2);//输出电位
                        }
                    }
                    #endregion
                }
                else if (_line.IfType.ToUpper().StartsWith("TEXT")) // 全局字符串变量赋值
                {
                    _val_1 = F_TransCalc._Get_Value_2(_faim3, _line.vName); // 行号 键

                    if (_faim3.ui_buffer.Count > _val_1)
                        _faim3.ui_buffer[_val_1] = _str_2;
                }
                else if (_line.IfType.ToUpper().StartsWith("DIM"))
                {
                    #region // dict_DevFunction  BT_OUT BT_IN MODBUS ARR_INT_REG
                    if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
                    {
                        clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//
                        int _devNo = _df.devNo;//设备号
                        int _ref_i = _faim3.sect_iDev * _devNo;
                        _bit = _ref_i + _df.Index;//内存位
                        if (_df.varName.ToUpper() == "BT_OUT")
                        {
                            _faim3.Comm_Data.bt_out[0][_bit] = _val_2;//
                        }
                        else if (_df.varName.ToUpper() == "BT_IN" || _df.varName.ToUpper() == "MODBUS")
                        {
                            _faim3.Comm_Data.bt_in[0][_bit] = _val_2;//
                        }
                        else if (_df.varName.ToUpper() == "ARR_INT")
                        {
                            _faim3.Comm_Data.arr_int[_bit] = _val_2;//
                        }
                        else if (_df.varName.ToUpper() == "ARR_INT_REG")
                        {
                            _faim3.Comm_Data.arr_int_reg[_bit] = _val_2;//
                        }
                        else if (_df.varName.ToUpper() == "ARR_STR")
                        {
                            _faim3.Comm_Data.arr_str[0][_bit] = _str_2;//
                        }
                    }
                    #endregion
                    #region // _dim_dict 动态变量 dim[Feeder05_unLink] <== Value
                    else if (_faim3._dim_dict.ContainsKey(_line.vName))
                    {
                        _bit = _faim3._dim_dict[_line.vName];// 内存位
                        _faim3._dim[_bit] = _val_2;  // 直接设置, 用以联动    
                    }
                    #endregion
                    #region // 变量
                    else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                             _line.vName.ToUpper().StartsWith("CLSFAIM3"))
                    {
                        F_TransCalc.doSetValue_byName(_faim3, _line.vName, _val_2);
                    }
                    #endregion
                    else
                    {
                        _val_1 = F_TransCalc._Get_Value_2(_faim3, _line.vName);
                        _faim3.Comm_Data.arr_int[_val_1] = _val_2;//
                    }
                }
                else if (_line.IfType.ToUpper().StartsWith("TBSE"))  // 给指定的表单元赋值
                {
                    _val_3 = F_TransCalc._Get_Value_2(_faim3, _line.Reset); // 行号 键

                    if (_faim3.dict_CommonInfo.ContainsKey(_val_3) || _flow.tableNo == 0) // 0.dict_CommonInfo
                    {
                        #region //dict_CommonInfo   val_01/val_02/val_03/arr_01/arr_02/arr_03
                        _val_1 = F_TransCalc._Get_Value_2(_faim3, _line.vName, ref _str_1); // 列


                        switch (_str_1)
                        {
                            case "val_01":
                                _faim3.dict_CommonInfo[_val_3].val_01 = _line.HL.Split(' ')[0];
                                break;
                            case "val_02":
                                _faim3.dict_CommonInfo[_val_3].val_02 = _line.HL.Split(' ')[0];
                                break;
                            case "val_03":
                                _faim3.dict_CommonInfo[_val_3].val_03 = _line.HL.Split(' ')[0];
                                break;
                            case "arr_01":
                                _faim3.dict_CommonInfo[_val_3].arr_1 = _line.HL;
                                break;
                            case "arr_02":
                                _faim3.dict_CommonInfo[_val_3].arr_2 = _line.HL;
                                break;
                            case "arr_03":
                                _faim3.dict_CommonInfo[_val_3].arr_3 = _line.HL;
                                break;
                        }
                        #endregion
                    }
                    else if (_faim3.dict_Modbus.ContainsKey(_val_3) || _flow.tableNo == 1)  // 1.dict_Modbus
                    {
                        #region //dict_Modbus   Loc_No/Name/transFlag/propFlag/Length/unitFlag/functionCode/Address/val01/val02/val03/val04
                        _val_1 = F_TransCalc._Get_Value_2(_faim3, _line.vName, ref _str_1); // 列


                        switch (_str_1)
                        {
                            case "Name":
                                _faim3.dict_Modbus[_val_3].Name = _line.HL;
                                break;
                            case "transFlag":
                                _faim3.dict_Modbus[_val_3].transFlag = _line.HL;
                                break;
                            case "propFlag":
                                _faim3.dict_Modbus[_val_3].propFlag = _line.HL;
                                break;
                            case "Length":
                                _faim3.dict_Modbus[_val_3].Length = _line.HL;
                                break;
                            case "unitFlag":
                                _faim3.dict_Modbus[_val_3].unitFlag = _line.HL;
                                break;
                            case "functionCode":
                                _faim3.dict_Modbus[_val_3].functionCode = _line.HL;
                                break;
                            case "Address":
                                _faim3.dict_Modbus[_val_3].Address = _line.HL;
                                break;
                            case "val01":
                                _faim3.dict_Modbus[_val_3].val01 = _line.HL;
                                break;
                            case "val02":
                                _faim3.dict_Modbus[_val_3].val02 = _line.HL;
                                break;
                            case "val03":
                                _faim3.dict_Modbus[_val_3].val03 = _line.HL;
                                break;
                            case "val04":
                                _faim3.dict_Modbus[_val_3].val04 = _line.HL;
                                break;
                        }
                        #endregion
                    }
                    else if (_faim3.dict_PointInfo.ContainsKey(_val_3) || _flow.tableNo == 2) // 2.dict_PointInfo
                    {
                        #region // dict_PointInfo   Loc_No/m_Id/Axis_Id/Distance/Max_Speed/Acc/Dcl/Remark
                        _val_1 = F_TransCalc._Get_Value_2(_faim3, _line.vName, ref _str_1); // 列
                        //_val_2 = F_TransCalc._Get_Value_2(_faim3, _line.HL, ref _str_2); // 列
                        switch (_str_1)
                        {
                            case "Axis_Id":
                                _faim3.dict_PointInfo[_val_3].Axis_Id = _val_2;
                                break;
                            case "Distance":
                                _faim3.dict_PointInfo[_val_3].Distance = _val_2;
                                break;
                            case "Max_Speed":
                                _faim3.dict_PointInfo[_val_3].Max_Speed = _val_2;
                                break;
                            case "Acc":
                                _faim3.dict_PointInfo[_val_3].Acc = _val_2;
                                break;
                            case "Dcl":
                                _faim3.dict_PointInfo[_val_3].Dcl = _val_2;
                                break;
                            case "Remark":
                                _faim3.dict_PointInfo[_val_3].Remark = _line.HL;
                                break;
                        }
                        #endregion
                    }
                }
                #endregion

                #region // IF
                else if (_line.IfType.ToUpper().StartsWith("IF")) // IF_XX 
                {
                    bool _result = do_Compare(_faim3, _line);
                    if (!_result) // 为假，则跳
                    {
                        if (_faim3.dict_CaseState[_caseName].dict_IF.ContainsKey(i)) // 取跳转到 ELSE 的位置，正好转到ELSE 的下一位置
                            i = _faim3.dict_CaseState[_caseName].dict_IF[i];
                    }
                }
                else if ("ELSE" == _line.IfType.ToUpper()) // ELSE 
                {
                    if (_faim3.dict_CaseState[_caseName].dict_ELSE.ContainsKey(i)) // 取跳转到 ENDIF  位置
                        i = _faim3.dict_CaseState[_caseName].dict_ELSE[i];
                    continue;
                }
                else if ("ENDIF" == _line.IfType.ToUpper() ||
                         "EDIF" == _line.IfType.ToUpper())// ENDIF   EDIF
                {
                    continue;
                }
                #endregion

                #region // WHILE
                else if (_line.IfType.ToUpper().StartsWith("WHIL") || // WHIL WHIL_XX WHILE WHILE_XX
                         _line.IfType.ToUpper().StartsWith("DW"))     // DWXX DW_XX
                {
                    bool _result = do_Compare(_faim3, _line);
                    if (!_result) // 为假，则跳
                    {
                        if (_faim3.dict_CaseState[_caseName].dict_WHILE.ContainsKey(i)) // 跳出 取跳转到 LOOP 的位置  
                            i = _faim3.dict_CaseState[_caseName].dict_WHILE[i];
                    }
                }
                else if ("LEAV" == _line.IfType.ToUpper() ||
                         "BREAK" == _line.IfType.ToUpper()) // LEAV/Break
                {
                    if (_faim3.dict_CaseState[_caseName].dict_WHILE.ContainsKey(i)) // 跳出 取跳转到 LOOP 的位置



                        i = _faim3.dict_CaseState[_caseName].dict_WHILE[i];
                }
                else if ("LOOP" == _line.IfType.ToUpper() ||    // LOOP/EDDO 
                         "EDDO" == _line.IfType.ToUpper() ||
                         "ITER" == _line.IfType.ToUpper())        //  ITER/Continue 
                {
                    if (_faim3.dict_CaseState[_caseName].dict_LOOP.ContainsKey(i)) // 循环 取跳转到 WHILE 的上一位置
                        i = _faim3.dict_CaseState[_caseName].dict_LOOP[i] - 1;
                    continue;
                }
                #endregion

                #region // GOTO JMP NEXT
                else if ("NEXT" == _line.IfType.ToUpper() ||
                         "NEXTCASE" == _line.IfType.ToUpper()) // 程序控制
                {
                    _flow.Tag = "GOTO_ABS";// 设绝对跳转标志



                    _caseState.endMode = eCaseFlag.Goto;
                    _flow.nextCase = _flow.dictCases[_alartCase].nextCase; // 设跳转步骤名
                    _line.times++;   // 此行使用的次数



                    _line.tmr = (int)(_tmr.ElapsedMilliseconds - _trm_begin);  // 本次流程完成用时
                    _faim3.dict_Threads[_flowName].Info.tmr_real += _line.tmr;    // 当前步骤 累加 实际占用总时长 += 每语句使用的时间
                    break;
                }
                else if ("GOTO" == _line.IfType.ToUpper() ||
                         "JMP" == _line.IfType.ToUpper()) // 程序控制 GOTO NEXTCASE
                {
                    if (_faim3.dict_CaseState[_caseName].dict_TAG.ContainsKey(_line.vName.Trim()))
                    {
                        i = _faim3.dict_CaseState[_caseName].dict_TAG[_line.vName.Trim()];  // 转向当前的步骤的既定标记
                    }
                    else
                    {
                        _flow.Tag = "GOTO_ABS";// 设绝对跳转标志



                        _caseState.endMode = eCaseFlag.Goto;
                        if (_line.vName.ToUpper().Trim() == "NEXT" ||
                            _line.vName.ToUpper().Trim() == "NEXTCASE") // 转向下一步骤 NestCase 的第一行号
                            _flow.nextCase = _flow.dictCases[_alartCase].nextCase; // 设跳转步骤名
                        else
                            _flow.nextCase = _line.vName; // 设跳转步骤名
                        _line.times++;   // 此行使用的次数



                        _line.tmr = (int)(_tmr.ElapsedMilliseconds - _trm_begin);  // 本次流程完成用时
                        _faim3.dict_Threads[_flowName].Info.tmr_real += _line.tmr;    // 当前步骤 累加 实际占用总时长 += 每语句使用的时间
                    }
                    break;
                }
                #endregion

                #region // EXIT TAG
                else if ("EXIT" == _line.IfType.ToUpper()) // 任务管理
                {
                    _flow.nextCase = "Free";
                }
                else if ("TAG" == _line.IfType.ToUpper())// 程序控制
                {
                    break;
                }
                #endregion

                #region // CALL PROC ENDP 子程序



                else if (_line.IfType.ToUpper().StartsWith("CALL") ||
                         _line.IfType.ToUpper().StartsWith("EXSR"))   // CALL/EXSR 取跳转到 进入 PROC 的位置 
                {
                    if (_faim3.dict_CaseState[_caseName].dict_PROC.ContainsKey(_line.vName.Trim())) // vName
                    {
                        _faim3.dict_CaseState[_caseName].lst_PROC.Insert(0, i);
                        i = _faim3.dict_CaseState[_caseName].dict_PROC[_line.vName.Trim()];
                    }
                }
                else if (_line.IfType.ToUpper().StartsWith("PROC") ||
                         _line.IfType.ToUpper().StartsWith("BGSR")) // PROC/ BGSR 跳出 取跳转到 ENDP 的位置 
                {
                    if (_faim3.dict_CaseState[_caseName].dict_PROC.ContainsKey(i.ToString())) // 跳出 取跳转到 ENDP 的位置  
                        i = _faim3.dict_CaseState[_caseName].dict_PROC[i.ToString()];
                }
                else if (_line.IfType.ToUpper().StartsWith("EDSR") || // 跳出 取跳转到 进入 PROC 的位置 
                         _line.IfType.ToUpper().StartsWith("RETU") || // 
                         _line.IfType.ToUpper().StartsWith("ENDP"))   // 
                {
                    if (_faim3.dict_CaseState[_caseName].lst_PROC.Count > 0)
                    {
                        i = _faim3.dict_CaseState[_caseName].lst_PROC[0];
                        _faim3.dict_CaseState[_caseName].lst_PROC.RemoveAt(0);
                    }
                }
                #endregion

                #region // OUT SEND MBUS
                else if (_line.IfType.ToUpper().StartsWith("MBUS"))
                {
                    #region // MBUS 	,5 	,保留 	,*Loc
                    // clsModBus _en = _faim3.dict_Modbus[_val_2];
                    _val_3 = F_TransCalc._Get_Value_2(_faim3, _line.Reset); // 文档的值: 数值


                    List<byte> _lst = clsModBus._getModbusFix(_faim3, _val_3);

                    int _devNo_1 = Convert.ToInt32(_line.vName);//设备号


                    int _ref_1i = _faim3.sect_iDev * _devNo_1;
                    int _ref_1s = _faim3.sect_sDev * _devNo_1 + _faim3.sect_sDev_start;

                    string _val = BitConverter.ToString(_lst.ToArray()).Replace("-", " ");

                    // ---发出消息 条件：1.bt_out  2._devNo   3.snd_iLen
                    _faim3._sss[_ref_1s] = _val;    // 
                    //_dao_comm.set_arr_str(_ref_1s, _val);  // 
                    _faim3.Comm_Data.bt_out[0][_ref_1i + _faim3.snd_iLen] = -99;    // 
                    _dao_comm.set_bt_out(_ref_1i + _faim3.snd_iLen, _lst.Count);  // 
                    #endregion



                    #region // <64 byte
                    //for (int _t = 0; _t < _lst.Count; _t++)
                    //{
                    //    _faim3.Comm_Data.bt_out[0][_t] = _lst[_t]; // 输出
                    //}

                    //// ---发出消息 条件：1.bt_out  2._devNo   3.snd_iLen
                    //_faim3.Comm_Data.bt_out[0][_ref_1i + _faim3.snd_iLen] = -99;    // 
                    //_dao_comm.set_bt_out(_ref_1i + _faim3.snd_iLen, _lst.Count);  // 
                    #endregion
                }
                else if (_line.IfType.ToUpper().StartsWith("OUT") ||
                         _line.IfType.ToUpper().StartsWith("SEND"))
                {
                    if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
                    {
                        #region // OUT
                        clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//
                        if (_df.varName.ToUpper() == "BT_OUT")
                        {
                            int _devNo_1 = _df.devNo;//设备号


                            int _ref_1i = _faim3.sect_iDev * _devNo_1;
                            //int _ref_1s = _faim3.sect_sDev * _devNo_1 + _faim3.sect_sDev_start;
                            _bit = _ref_1i + _df.Index;//内存位



                            // 特殊 vName ==> driverName ==> _df.ioIdx ==> LLine
                            _faim3.Comm_Data.bt_out[0][_ref_1i + Convert.ToInt32(_faim3.dict_KV["LLine"])] = _df.ioIdx;
                            // HL ==> _val_2 => Value
                            _faim3.Comm_Data.bt_out[0][_ref_1i + Convert.ToInt32(_faim3.dict_KV["Value"])] = _val_2;
                            //通用 bt_out[_df.Index]-->_df.ioIdx  _val_2-->H/L
                            _faim3.Comm_Data.bt_out[0][_bit] = _val_2; // 输出电位  bt_out[106]=6   bt_out[6]= 9>0

                            // --- 发出消息 条件：1.bt_out  2._devNo   3.snd_iLen
                            _faim3.Comm_Data.bt_out[0][_ref_1i + _faim3.snd_iLen] = -99; // 
                            _dao_comm.set_bt_out(_ref_1i + _faim3.snd_iLen, _bit);  // bt_out[164]=6  
                        }
                        #endregion
                    }
                    else if (_faim3.dict_CmdFormats.ContainsKey(_line.vName))
                    {
                        #region // 协议表 字符串 Ascii码 snd_sLen=1
                        int _devNo_1 = clsProtocol.getSendData(_faim3, i); // 返回待发送数据的 设备号



                        int _ref_1i = _faim3.sect_iDev * _devNo_1;
                        int _ref_1s = _faim3.sect_sDev * _devNo_1 + _faim3.sect_sDev_start; // 设发送数据的长度, 发送的是字符域的数据



                        // 发出消息 条件：1.bt_out  2._devNo   3.snd_iLen
                        _faim3.Comm_Data.bt_out[0][_ref_1i + _faim3.snd_iLen] = -99;
                        _dao_comm.set_bt_out(_ref_1i + _faim3.snd_iLen, 1);
                        #endregion
                    }
                    else if (_faim3._dim_dict.ContainsKey(_line.vName))
                    {
                        // _sss  [0 ~ 200] 区域
                        #region // _dim_dict 动态变量 dim[Feeder05_unLink] <== _line.HL
                        _bit = _faim3._dim_dict[_line.vName];// 内存位



                        if (_str_2 == "    ")
                            _faim3._dim[_bit] = _val_2;  // 值    直接设置, 用以联动  
                        else
                            _faim3._sss[_bit] = _str_2;  // 字符串 
                        #endregion
                    }
                    else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                             _line.vName.ToUpper().StartsWith("CLSFAIM3"))
                    {
                        // 空 大数据



                    }
                }
                #endregion

                _line.times++;   // 当前行使用的次数
                _line.tmr = (int)(_tmr.ElapsedMilliseconds - _trm_begin);  // 当前行完成用时

                _faim3.dict_Threads[_flowName].Info.tmr_real += _line.tmr;    // 当前步骤 累加 实际占用总时长 += 每语句使用的时间
                #endregion // 开始执行

                #endregion // for
            }
        }
        #region // compare
        static bool do_Compare(clsFaim3 _faim3, clsDevTestBits _line)
        {
            bool _mybl = true;
            string _str_2 = "   ";
            int _bit = -1;
            int _val_1 = -1;
            int _val_2 = F_TransCalc._Get_Value_2(_faim3, _line.HL, ref _str_2);

            if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
            {
                #region // dict_DevFunction IO口定义
                clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//  
                _bit = _df.devNo * _faim3.sect_iDev + _df.Index;// 内存位

                switch (_df.varName.ToUpper())
                {
                    case "BT_OUT":
                        _val_1 = _faim3.Comm_Data.bt_out[0][_bit];
                        break;
                    case "BT_IN":
                    //_val_1 = _faim3.Comm_Data.bt_in[0][_bit];
                    //break;
                    case "BT_ST":
                    //clsProtocol._204C(_faim3, _dao_comm, _df.devNo, "A_ST", _df.ioIdx.ToString());  // 发出命令                       
                    //_val_1 = _faim3.Comm_Data.bt_in[0][_bit]; // 取出值, 可能多次才能取到
                    //break;
                    case "BT_IO":
                        //clsProtocol._204C(_faim3, _dao_comm, _df.devNo, "A_IO", _df.ioIdx.ToString());  // 发出命令                       
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
            // 可以比较
            if (_bit > -1)
            {
                #region // 比较 IF_EQ  IF_NE  IF_GT  IF_GE  IF_LT  IF_LE
                if (_line.IfType == "EQ" ||
                    _line.IfType == "IF" || _line.IfType == "IF_EQ" ||
                    _line.IfType == "WHILE" || _line.IfType == "WHIL")
                {
                    _mybl = (_val_1 == _val_2);//
                }
                else if (_line.IfType == "NE" || _line.IfType == "IF_NE" || _line.IfType == "WHILE_NE")
                {
                    _mybl = (_val_1 != _val_2);//
                }
                else if (_line.IfType == "GT" || _line.IfType == "IF_GT" || _line.IfType == "WHILE_GT")
                {
                    _mybl = (_val_1 > _val_2);//
                }
                else if (_line.IfType == "GE" || _line.IfType == "IF_GE" || _line.IfType == "WHILE_GE")
                {
                    _mybl = (_val_1 >= _val_2);//
                }
                else if (_line.IfType == "LT" || _line.IfType == "IF_LT" || _line.IfType == "WHILE_LT")
                {
                    _mybl = (_val_1 < _val_2);//
                }
                else if (_line.IfType == "LE" || _line.IfType == "IF_LE" || _line.IfType == "WHILE_LE")
                {
                    _mybl = (_val_1 <= _val_2);//
                }
                else
                    _mybl = false;
                #endregion  // 测试 AND
            }
            return _mybl;
        }
        #endregion
    }
}
