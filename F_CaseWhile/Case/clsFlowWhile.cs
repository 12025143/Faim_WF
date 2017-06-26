#region //
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_CaseWhile
{
    #region //
    using F_Entitys;
    using F_Enums;
    using F_Entitys_DAL;
    using FACC;
    #endregion
    public class clsFlowWhile : clsBaseWhile
    {
        static string _CMD_TEST = ".BIT.BITN...EQ_AND.NE_AND.GT_AND.GE_AND.LT_AND.LE_AND...EQ_OR.NE_OR.GT_OR.GE_OR.LT_OR.LE_OR...AND_EQ.AND_NE.AND_GT.AND_GE.AND_LT.AND_LE...OR_EQ.OR_NE.OR_GT.OR_GE.OR_LT.OR_LE...EQ.NE.GT.GE.LT.LE.";
        static string _CMD_NG = "NG";
        static string _CMD_A = "PARTA";
        static string _TESTB = "TESTB";
        static string _CMD_C = "PARTC";
        static string _NG_D = "NGD";
        public clsFlowWhile(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vflowName)
        {
            base.do_New(v_faim3, v_dao_comm, vflowName);
            if (!_faim3.dict_Flow.ContainsKey(vflowName))
            {
                FACC.F_Log.Debug_1(this.GetType().Name, "--->>>> 无  流程 " + vflowName);
                return;
            }
            _flow = _faim3.dict_Flow[vflowName];
        }
        protected override void do_While(string vFlowId)
        {
            //int _loc = 0;
            _bl = true;
            #region // _flow 初始化

            _flow.currCase = _flow.firstCase;  // 恢复 第1步

            _flow.alartCase = _flow.firstCase;  // 恢复
            //if (_faim3.dict_CaseState.ContainsKey(_flow.alartCase))
            //{
            clsCaseState _caseState = _faim3.dict_CaseState[_flow.alartCase];
            _caseState.beginMode = eCaseFlag.Next;
            _caseState.endMode = eCaseFlag.Next;
            //}
            _flow.nextCase = _flow.firstCase;
            _flow.lastCase = "Free";
            _flow.stepCase = "Free"; // 初始化单步的步骤名

            _flow.breakSkipCase = ""; // 断点无效的步骤名
            _flow.times += 1;    // 累加, 显示流程 启动的次数

            _flow.Tag = "";
            _flow.NGtimes = 0;
            #endregion
            #region // 日志 1 2
            F_Log.Debug_1(this.GetType().Name, String.Format("*** 流程开始:{0}.{1}, 状态:{2}, 计数:{3} ***",
                                                    vFlowId,
                                                    _flow.currCase,
                                                    _flow.eState.ToString(),
                                                    _flow.times));
            // 日志 2
            clsLogs _logs2 = new clsLogs();
            _logs2.No = _flow.currCase;
            _logs2.Name = "*** " + _flow.currCase + " _开始";
            _flow.RunLog.Add(_logs2); // 累加, 内存日志
            #endregion
            #region // 流程条件
            clsThreadInfo _ti = _faim3.dict_Threads[vFlowId].Info;
            _ti.allowWhile = eSwitch.Yes;   //控制流程之1, 线程终止  false  
            _ti.allowBusiness = eSwitch.No; // 控制流程之2 业务等待/暂停 
            System.Diagnostics.Stopwatch _tmr = do_th_Name_pre(_ti); // 计时
            #endregion
            _faim3.dict_Threads[_flowName].Info.tmr_real = 0; // 当前步骤 实际占用总时长



            #region // while
            while (((int)_ti.allowWhile == (int)eSwitch.Yes))  // 允许线程运行 1
            {
                #region // break
                // 急停
                if (_faim3.isEmergency > 0)
                {
                    _flow.eState = eWF_State.Emergency;
                    _ti.allowWhile = eSwitch.No;
                    _ti.allowBusiness = eSwitch.No;
                    break;
                }
                // 最后一步


                if (_flow.nextCase.ToUpper() == "FREE")
                {
                    break;// 结束流程
                }
                #endregion
                #region // continue
                // 暂停
                if (!do_allowBusiness(_ti))  //_flow.eState = eWF_State.Wait;
                    continue; // 线程延时+ 业务是否允许 流程信息 2
                if (do_NG_hasTimes())
                    continue; //_flow.currCase
                #endregion
                // 显示执行后的 curr
                //_dao_comm_set_arr_str800();
                //string _caseName_old = _flow.currCase;
                clsCaseState _caseState_old = _faim3.dict_CaseState[_flow.alartCase];
                _flow.currCase = _flow.nextCase;  // 准备执行下一步, 即将下一步设为当前步骤


                if (_flow.currCase.EndsWith(F_Const.fix_CaseT)) // “_T”, 测试步骤
                {
                }
                else
                {
                    _flow.alartCase = _flow.currCase; // 非测试步骤


                    clsCaseState _caseState_new = _faim3.dict_CaseState[_flow.alartCase];
                    if (_caseState_new.beginMode == eCaseFlag.Free)
                        _caseState_new.beginMode = _caseState_old.endMode;
                    //if (_caseName_old.EndsWith(F_Const.fix_CaseT)) // “_T”, 测试步骤
                    //    _caseState_new.beginMode = eCaseFlag.TT;
                    //else
                }
                //_dao_comm_set_arr_str800();
                // 指定预执行的 curr
                do_WaitBreak();
                do_WaitStep();

                _flow.caseTimes++; // 累加, 显示步骤 通过的次数


                // 状态为运行中


                _flow.eState = eWF_State.Running;
                long _trm_begin = _tmr.ElapsedMilliseconds; // 毫秒
                _ti.caseName = _flow.currCase;
                if (_dao_comm.dict_Methods.doCase.ContainsKey(_flow.currCase)) // 条件：Case名存在，XX() 或 XX_T()
                {
                    if (!_flow.currCase.EndsWith(F_Const.fix_CaseT)) // 条件：无“_T”, 则新步骤开始， 先找到该配置文档的行号
                    {
                        _ti.cnt_business++; // Case完成数


                        // 发消息, 上一步骤信息和本步骤位置
                        doGet_CmdLineStart(_flow.currCase, ref _faim3);  // 预处理 分解Case的各语法结构
                    }
                    #region // 流程代码 代理1   Switch  &  Select Case
                    F_Delegate.delCase _delCase = _dao_comm.dict_Methods.doCase[_flow.currCase]; //通过代理, 从步骤集中取出一个步骤, 执行其方法()                    
                    _delCase(_dao_comm, _flow.FlowName);        // 执行步骤
                    do_end_CalcTimer(_ti, _tmr, _trm_begin); // 流程信息2
                    #endregion
                    if (_flow.Delay > -1) System.Threading.Thread.Sleep(_flow.Delay);
                }
                else  // 结束 flow
                {
                    _ti.allowWhile = eSwitch.Off;               // 控制流程之2 , 步骤名不存在 无方法


                    _flow.eState = eWF_State.End;
                    FACC.F_Log.Debug_1(this.GetType().Name, String.Format("--->>>> 无 步骤 :{0}.{1}->{2} - 状态:{3}, 计数:{4} ",
                                                                vFlowId,
                                                                _flow.lastCase, _flow.currCase,
                                                                _flow.eState.ToString(),
                                                                _flow.times));
                }
            }
            #endregion
            do_End_Flow(_ti, _tmr, vFlowId);
        }
        // 预处理每步骤中的“行结构段”


        void doGet_CmdLineStart(string vCaseName, ref clsFaim3 _faim3)
        {
            if (vCaseName.EndsWith(F_Const.fix_CaseT)) return;// 当前是BT测试步骤, 则略过


            #region // 子步骤结构信息对象 _dictCaseChildInfo
            if (_faim3.dict_CaseState.ContainsKey(vCaseName))
                _faim3.dict_CaseState[vCaseName].dict_CaseChildInfo.Clear();    // 若存在,则清除原有的子步骤结构信息


            else
                _faim3.dict_CaseState.Add(vCaseName, new clsCaseState());   // 新建步骤状态对象



            Dictionary<string, clsCaseChildInfo> _dictCaseChildInfo = _faim3.dict_CaseState[vCaseName].dict_CaseChildInfo;
            #endregion
            int _hasLine = 0;
            string _cmd_Part = _CMD_A; // PARTA
            string _currKey = _CMD_A; // 当前结构名


            List<cls_if> _lst_if = new List<cls_if>();   // if 语法包

            List<cls_if> _lst_while = new List<cls_if>();   // while 语法包

            List<cls_if> _lst_proc = new List<cls_if>();   // procedure 语法包


            _faim3.dict_CaseState[vCaseName].dict_IF.Clear();
            _faim3.dict_CaseState[vCaseName].dict_ELSE.Clear();
            _faim3.dict_CaseState[vCaseName].dict_WHILE.Clear();
            _faim3.dict_CaseState[vCaseName].dict_LOOP.Clear();
            _faim3.dict_CaseState[vCaseName].dict_PROC.Clear();
            _faim3.dict_CaseState[vCaseName].lst_PROC.Clear();
            #region // for
            int i = 0;
            for (i = 0; i < _faim3.lst_DevTestBits.Count; i++)// 文档对象驱动
            {
                clsDevTestBits _line = _faim3.lst_DevTestBits[i];  // 指令行
                string _IfType = _line.IfType.ToUpper();  // 行中的逻辑命令
                //if (_IfType == "TITL" || _IfType == "TITLE")
                //{
                //    clsFlow _flow = _faim3.dict_Flow[_flowName];
                //    string _alartCase = _flow.alartCase;        // _caseName 可能后带有 _T 标记
                //    _faim3.dict_CaseState[_alartCase].Remark = _line.vName.Trim();
                //}
                #region // 找起始
                if (vCaseName != _line.flowCase) // 不是当前步骤 CaseName
                {
                    if (_hasLine > 0)
                    {
                        if (_dictCaseChildInfo.ContainsKey(_currKey))
                            if (_dictCaseChildInfo[_currKey].lineEnd < 0)
                                _dictCaseChildInfo[_currKey].lineEnd = i - 1; // 加第N部份的终止行号
                        break; // 当前步骤已处理完成
                    }
                    continue; // 还未开始, 即未到当前步骤
                }
                #endregion
                _hasLine++;

                if (_CMD_TEST.Contains(_IfType)) // 1. TEST 测试类型 找到 
                {
                    #region // EQ.NE.GT.GE.LT.LE
                    if (_dictCaseChildInfo.ContainsKey(_TESTB)) continue; // 已加载,首位置
                    clsCaseChildInfo _en = new clsCaseChildInfo();
                    _en.lineStart = i;
                    _en.lineCurr = i;
                    _en.tag = _TESTB;
                    _dictCaseChildInfo.Add(_TESTB, _en); // 结构名 --> 行号
                    _currKey = _TESTB;
                    if (_dictCaseChildInfo.ContainsKey(_cmd_Part))
                        if (_dictCaseChildInfo[_cmd_Part].lineEnd < 0)
                            _dictCaseChildInfo[_cmd_Part].lineEnd = i - 1; // 加第1部份的终止行号
                    _cmd_Part = _CMD_C; //切换到第2部份
                    _lst_if.Clear();
                    #endregion
                }
                else if (_CMD_NG == _IfType) // 2. NG 测试类型 找到
                {
                    #region // NG
                    if (_dictCaseChildInfo.ContainsKey(_NG_D))
                    {
                        continue; // 已加载,首位置


                    }
                    clsCaseChildInfo _en = new clsCaseChildInfo();
                    _en.lineStart = i;
                    _en.lineCurr = i;
                    _dictCaseChildInfo.Add(_NG_D, _en); // 结构名 --> 行号
                    if (_dictCaseChildInfo.ContainsKey(_TESTB))
                        if (_dictCaseChildInfo[_TESTB].lineEnd < 0)
                            _dictCaseChildInfo[_TESTB].lineEnd = i - 1;
                    #endregion
                }
                else // PARTA  PARTC
                {
                    #region // IF WHILE
                    int _cnt_if = 0;
                    int _cnt_while = 0;
                    int _cnt_proc = 0;
                    #region // PROC
                    if (_line.IfType.ToUpper().StartsWith("PROC") ||
                            _line.IfType.ToUpper().StartsWith("BGSR"))
                    {
                        _lst_proc.Add(new cls_if());  // 加一个对象
                        _cnt_proc = _lst_proc.Count;
                        _lst_proc[_cnt_proc - 1]._if = i; // 记录起始
                    }
                    #endregion
                    #region // WHIL
                    else if (_line.IfType.ToUpper().StartsWith("WHIL") ||
                       _line.IfType.ToUpper().StartsWith("DW")) // WHIL  WHILE  WHILE_XX  DWXX
                    {
                        _lst_while.Add(new cls_if());  // 加一个对象
                        _cnt_while = _lst_while.Count;
                        _lst_while[_cnt_while - 1]._if = i; // 记录起始
                    }
                    #endregion
                    else if (_line.IfType.ToUpper().StartsWith("IF"))
                    {
                        _lst_if.Add(new cls_if());  // 加一个对象
                        _cnt_if = _lst_if.Count;
                        _lst_if[_cnt_if - 1]._if = i; // 记录循环点
                    }
                    else
                    {
                        switch (_IfType)
                        {
                            #region // TAG ENDP
                            case "TAG":
                                if (!_line.vName.Trim().StartsWith(_flowName)) // 不符合流程步骤命名
                                    if (!_faim3.dict_CaseState[vCaseName].dict_TAG.ContainsKey(_line.vName.Trim())) // 不存在
                                        _faim3.dict_CaseState[vCaseName].dict_TAG.Add(_line.vName.Trim(), i);
                                break;
                            case "GOTO":
                            case "RETU":
                                break;
                            case "ENDP":
                            case "EDSR":
                                _cnt_proc = _lst_proc.Count;
                                if (_cnt_proc > 0) // 有对象
                                {
                                    _faim3.dict_CaseState[vCaseName].dict_PROC.Add(_line.vName.Trim(), _lst_proc[_cnt_proc - 1]._if); // 记录跳入点
                                    _faim3.dict_CaseState[vCaseName].dict_PROC.Add(_lst_proc[_cnt_proc - 1]._if.ToString(), i); // 记录跳出点
                                    _lst_proc.RemoveAt(_cnt_proc - 1); // 清理
                                }
                                break;
                            #endregion
                            #region // WHILE
                            //case "WHIL":
                            //case "WHILE":
                            //    _lst_while.Add(new cls_if());  // 加一个对象

                            //    _cnt_loop = _lst_while.Count;
                            //    _lst_while[_cnt_loop - 1]._if = i; // 记录循环点 循环
                            //    break;
                            case "LEAV":    // 循环 
                            case "BREAK":
                                _cnt_while = _lst_while.Count;
                                if (_cnt_while > 0) // 有对象

                                    _lst_while[_cnt_while - 1]._endif = i; // 记录跳出点

                                break;
                            case "LOOP":    // 循环 
                            case "EDDO":
                            case "ITER":
                                _cnt_while = _lst_while.Count;
                                if (_cnt_while > 0) // 有对象
                                {
                                    _faim3.dict_CaseState[vCaseName].dict_WHILE.Add(_lst_while[_cnt_while - 1]._if, i); // i 跳出点

                                    if (_lst_while[_cnt_while - 1]._endif > 0)
                                        _faim3.dict_CaseState[vCaseName].dict_WHILE.Add(_lst_while[_cnt_while - 1]._endif, i); // i 跳出点


                                    _faim3.dict_CaseState[vCaseName].dict_LOOP.Add(i, _lst_while[_cnt_while - 1]._if); // _if 循环点


                                    _lst_while.RemoveAt(_cnt_while - 1); // 清理
                                }
                                break;
                            #endregion
                            #region // IF ELSE
                            case "ELSE":
                                _cnt_if = _lst_if.Count;
                                if (_cnt_if > 0) // 有对象
                                    _lst_if[_cnt_if - 1]._else = i; // 记录分支
                                break;
                            case "EDIF":
                            case "ENDIF":
                                _cnt_if = _lst_if.Count;
                                if (_cnt_if > 0) // 有对象
                                {
                                    if (_lst_if[_cnt_if - 1]._else > 0)
                                    {
                                        _faim3.dict_CaseState[vCaseName].dict_IF.Add(
                                                                _lst_if[_cnt_if - 1]._if,
                                                                _lst_if[_cnt_if - 1]._else); // 
                                        _faim3.dict_CaseState[vCaseName].dict_ELSE.Add(
                                                                _lst_if[_cnt_if - 1]._else,
                                                                i); // 
                                    }
                                    else
                                    {
                                        _faim3.dict_CaseState[vCaseName].dict_IF.Add(
                                                                _lst_if[_cnt_if - 1]._if,
                                                                i); // 
                                    }
                                    _lst_if.RemoveAt(_cnt_if - 1); // 清理
                                }
                                break;
                            #endregion
                        }
                    }
                    #endregion
                    if (_dictCaseChildInfo.ContainsKey(_cmd_Part))
                        continue; // 已加载,首位置

                    // 结构段
                    clsCaseChildInfo _en = new clsCaseChildInfo();
                    _en.lineStart = i;
                    _en.lineCurr = i;
                    _en.tag = _cmd_Part;
                    _dictCaseChildInfo.Add(_cmd_Part, _en); // 结构名 --> 行号
                    _currKey = _cmd_Part;
                    if (_dictCaseChildInfo.ContainsKey(_TESTB))
                    {
                        if (_dictCaseChildInfo[_TESTB].lineEnd < 0)
                            _dictCaseChildInfo[_TESTB].lineEnd = i - 1;
                    }
                }
            }
            if (_hasLine > 0)
                if (_dictCaseChildInfo.ContainsKey(_currKey))
                    if (_dictCaseChildInfo[_currKey].lineEnd < 0)
                        _dictCaseChildInfo[_currKey].lineEnd = i - 1; // 加第N部份的终止行号

            #endregion
        }
        void do_End_Flow(clsThreadInfo _ti, System.Diagnostics.Stopwatch _tmr, string vFlowId)
        {
            // _flow
            _flow.lastCase = _flow.currCase;
            if (_flow.eState == eWF_State.Running) _flow.eState = eWF_State.End;
            _dao_comm_set_arr_str800();// 消息  arr_str[0][800 + _flowNo]
            // _ti
            _ti.allowWhile = eSwitch.No;
            _ti.allowBusiness = eSwitch.No;
            _ti.cnt_heart = -1;
            // _tmr
            _tmr.Stop();		//结束计时
            #region // 日志
            // _log1
            clsLogs _logs = new clsLogs();
            _logs.No = _flow.currCase;
            _logs.Name = _flow.currCase + "_结束 *** ";
            _flow.RunLog.Add(_logs);
            // _log 2
            FACC.F_Log.Debug_1(this.GetType().Name, String.Format("--- 流程:{0} 结束, 状态:{1} ---",
                                                        vFlowId,
                                                        _flow.eState.ToString()));
            #endregion
        }
        // 发出 NG 消息 _flow.currCase
        bool do_NG_hasTimes()
        {
            bool _hasTimes = false;
            if (_flow.currCase.EndsWith(F_Const.fix_CaseT)) return false; // 直通 当前正在BT测试, 与NG无关,
            // 不是测试, 则返回1.循环, 2.发消息


            _hasTimes = _flow.NGtimes > 0;
            if (_hasTimes && _flow.isStepBy && _flow.stepCase != "") // 发消息的条件: 是单步，单步有效， NG计数器没归0
            {
                if (_flow.eState != eWF_State.NGwait)   // 辟免重发
                {
                    _flow.eState = eWF_State.NGwait;
                    _dao_comm_set_arr_str800();// 消息  arr_str[0][800 + _flowNo]
                }
                if (_flow.Delay > -1) System.Threading.Thread.Sleep(_flow.Delay);// WV
            }
            return _hasTimes;
        }
        // 单步
        void do_WaitStep()
        {
            if (_flow.currCase.EndsWith(F_Const.fix_CaseT)) return;// 当前是BT测试步骤
            if (!_flow.isStepBy) return; // 实时改变后，当前不是单步
            _flow.stepCase = _flow.currCase;
            while (_flow.stepCase == _flow.currCase) //
            {
                if (_faim3.isEmergency > 0)
                {
                    _flow.eState = eWF_State.Emergency;
                    break;
                }
                if (!_flow.isStepBy) break; // 当前不是单步
                //if (_flow.eState == eWF_State.End || _flow.eState == eWF_State.Wait) break;
                if (_flow.eState != eWF_State.Step)
                {
                    _flow.eState = eWF_State.Step;  //  
                    _dao_comm_set_arr_str800();// 消息  arr_str[0][800 + _flowNo]
                }
                if (_flow.eState != eWF_State.Step) break;
                if (_flow.Delay > -1) System.Threading.Thread.Sleep(_flow.Delay);// WV
            }
            _flow.eState = eWF_State.Running;
        }
        // 断点
        void do_WaitBreak()
        {
            if (_flow.currCase.EndsWith(F_Const.fix_CaseT)) return;// 直通: 当前是BT测试步骤
            if (!_flow.lstBreakPoint.Contains(_flow.currCase)) return;// 直通: 当前不在断点集



            _flow.breakSkipCase = _flow.currCase;
            while (_flow.breakSkipCase == _flow.currCase) // 
            {
                if (_faim3.isEmergency > 0)
                {
                    _flow.eState = eWF_State.Emergency;
                    break;
                }
                if (!_flow.lstBreakPoint.Contains(_flow.breakSkipCase)) break;// 实时改变后，当前不在断点集


                //if (_flow.eState == eWF_State.End || _flow.eState == eWF_State.Wait) break;
                if (_flow.eState != eWF_State.Break)
                {
                    _flow.eState = eWF_State.Break;  //
                    _dao_comm_set_arr_str800();// 消息  arr_str[0][800 + _flowNo]
                }
                if (_flow.eState != eWF_State.Break) break;
                if (_flow.Delay > -1) System.Threading.Thread.Sleep(_flow.Delay);// WV
            }
            _flow.eState = eWF_State.Running;
        }
        void _dao_comm_set_arr_str800()
        {
            //if (_flow.currCase.EndsWith(F_Const.fix_CaseT)) return;// 直通: 当前是BT测试步骤
            #region // 消息  arr_str[0][800 + _flowNo]
            int _flowIdx = 0;
            foreach (var item in _faim3.dict_Flow)
            {
                if (item.Key == _flowName)
                {
                    // 800+流程号  当前步骤, 流程名, 流程状态, 
                    _dao_comm.set_arr_str(_flowIdx + 800,
                                          string.Format("{0} {1} {2}",
                                                _flow.currCase,
                                                _flowName,
                                                _flow.eState.ToString()
                                        ));
                    break;
                }
                _flowIdx++;
            }
            #endregion
        }
    }
}
