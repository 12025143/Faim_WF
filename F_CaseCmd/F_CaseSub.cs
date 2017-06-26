
#region // ...
using System;
using System.Collections.Generic;
#endregion
namespace F_CaseCmd
{
    #region // ...
    using F_Entitys;
    using F_Enums;
    using FACC;
    using F_Entitys_DAL;
    using System.Diagnostics;
    #endregion
    public partial class F_CaseSub
    {
        static string _CMD_TEST = ".BIT.BITN...EQ_AND.NE_AND.GT_AND.GE_AND.LT_AND.LE_AND...EQ_OR.NE_OR.GT_OR.GE_OR.LT_OR.LE_OR...AND_EQ.AND_NE.AND_GT.AND_GE.AND_LT.AND_LE...OR_EQ.OR_NE.OR_GT.OR_GE.OR_LT.OR_LE...EQ.NE.GT.GE.LT.LE.";
        static string _CMD_GOTO = ".NG.GOTO.JMP.NEXT.NEXTCASE.";
        static string _CMD_SEND = ".OUT.SEND.CONN.";
        static string _CMD_MATH = ".ADD.SUB.INC.DEC.";
        static string _CMD_WAIT = ".SLEEP.WAIT.WTON.WTOF";
        static string _CMD_DIM = ".LET.DIM.DIM_AOK.DIM_T.DIM_TOK.BTON.BTOFF";
        static string _CMD_IF = ".IF.IF_EQ.ELSE.ENDIF.EDIF.";
        static string _CMD_WHILE = ".WHILE.WHIL.BREAK.LEAV.ITER.LOOP.EDDO.";
        static string _CMD_NULL = ".NUL.NULL.NOP.";
        // 表
        static string _CMD_TB = ".CTB..TB.";
        // 部份
        static string _CMD_A = "PARTA";
        static string _TESTB = "TESTB";
        static string _CMD_C = "PARTC";
        static string _NG_D = "NGD";

        internal static void do_PARTA(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, string _CaseName)
        {
            _PART_CMD(_faim3, _dao_comm, _flowName, _CaseName, _CMD_A); // 
            //_TEST_2_Pre(_faim3, _dao_comm, _flowName, _CaseName);
        }
        internal static void do_PARTC(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, string _CaseName)
        {
            //_TEST_2_Reset(_faim3, _dao_comm, _flowName, _CaseName);
            _PART_CMD(_faim3, _dao_comm, _flowName, _CaseName, _CMD_C); // 
        }
        // 12 测试
        internal static void _TEST2(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, string _CaseName, ref bool _bl)
        {
            _TEST_2(_faim3, _dao_comm, _flowName, _CaseName, ref _bl);
        }
        // 16 _NG
        internal static void _NG(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, string _CaseName)
        {
            __NG(_faim3, _dao_comm, _flowName, _CaseName); // 
        }

        static void _do_WaitLine(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, eWF_State vStopState, int vCaseLineIdx)
        {
            #region // 是“行步进”调试
            clsFlow _flow = _faim3.dict_Flow[_flowName];
            if (!_flow.isStepLine) return; // 是“行步进”调试
            clsDevTestBits _line = _faim3.lst_DevTestBits[vCaseLineIdx];
            if (_line.isDebug == 1) /*#*/   // cfgDev_testBits #
                if (_faim3.dict_KV["isDebug"] != "1") // 非 debug 状态下，不运行 debug 行  cfgDev_Cards.xls
                    return;
            _flow.caseLineIdx = vCaseLineIdx;
            while (_flow.caseLineIdx == vCaseLineIdx)// 
            {
                if (_faim3.isEmergency > 0)
                    break;  // 急停
                if (_flow.eState == eWF_State.End || _flow.eState == eWF_State.Wait) break;
                if (_flow.eState != vStopState)
                {
                    _flow.eState = vStopState;  //  
                    _dao_comm_set_arr_str800(_faim3, _dao_comm, _flowName, vCaseLineIdx);// “行步进”消息  arr_str[0][800 + _flowIdx]
                }
                System.Threading.Thread.Sleep(100);
            }
            _flow.eState = eWF_State.Running;  // 
            #endregion
        }

        static void _dao_comm_set_arr_str800(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, int caseLineIdx)
        {
            int _loc = 0;
            string _msg = "";
            clsFlow _flow = _faim3.dict_Flow[_flowName];
            if (!_flow.isStepLine) return; // 是“行步进”调试
            clsCaseState _caseState = _faim3.dict_CaseState[_flow.alartCase];
            _msg = _dao_comm_set_msg(_faim3, _flowName, caseLineIdx, ref _loc);
            if (!string.IsNullOrEmpty(_msg))
                _dao_comm.set_arr_str(_loc + 800, _msg);
        }
        static string _dao_comm_set_msg(clsFaim3 _faim3, string _flowName, int caseLineIdx, ref int _loc)
        {
            string _msg = "";
            clsFlow _flow = _faim3.dict_Flow[_flowName];
            clsCaseState _caseState = _faim3.dict_CaseState[_flow.alartCase];
            clsDevTestBits _line = _faim3.lst_DevTestBits[caseLineIdx];
            foreach (var item in _faim3.dict_Flow)
            {
                if (item.Key == _flowName)// 流程名 转 流程号
                {
                    // 0当前步骤, 1流程名, 1流程状态/步骤, 3行号, 4命令语句:5变量名=6值 7.转入模式 8.转出模式
                    _msg = string.Format("{0} {1} {2} {3} {4}{5} {6} {7} {8} {9}",
                                            _flow.currCase,
                                            _flowName,
                                            _flow.eState.ToString(),
                                            caseLineIdx,
                                            _line.isDebug == 1 ? "#" : ".",
                                            _line.IfType,
                                            _line.vName,
                                            _line.HL,
                                            _caseState.beginMode,
                                            _caseState.endMode
                                            );
                    break;
                }
                _loc++;
            }
            return _msg;
        }
        static int _Get_Value_2x(clsFaim3 v_faim3, string strFld)
        {
            string _str_2 = string.Empty;
            return F_TransCalc._Get_Value_2(v_faim3, strFld, ref _str_2);
        }
        static void _do_math(clsFaim3 _faim3, clsDevTestBits _line, int _val_2)
        {
            int _bit = 0;
            if (_faim3._dim_dict.ContainsKey(_line.vName))
            {
                _bit = _faim3._dim_dict[_line.vName];   // 内存位 自定义名为下标, 分配地址, 寻址
                _faim3._dim[_bit] += _val_2;            // 直接设置, 无联动     dim[Feeder05] = Value 
            }
            else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                     _line.vName.ToUpper().StartsWith("CLSFAIM3"))
            {
                int _ls = Convert.ToInt32(F_TransCalc.doGetValue_byName(_faim3, _line.vName));
                F_TransCalc.doSetValue_byName(_faim3, _line.vName, _ls + _val_2);
            }
        }
        protected Stopwatch do_th_Name_pre()
        {
            Stopwatch _tmr = new Stopwatch();	//实例化一个计时器
            _tmr.Start();
            return _tmr;
        }
    }
}
/*
 ------- 位赋位
BTON  2DI_15  3  //  (2DI_15) = 0
BTOF  2DI_15  3  //  (2DI_15) = 1
------- 比较
             值比较
EQ    2DI_15  1  // 测试状态字位置  (2DI_15) == 1
EQ    2DI_15  0  // 测试状态字位置  (2DI_15) == 0
             ST状态位比较
BIT   6D_ST   3  // 测试状态字的第16位 () == 1
BITN  2D_ST   15 // 测试状态字的第16位 () == 0
BIT   0A_ST   3  // 测试状态字, 第4位  0 轴的 (0A_ST_03) == 1
BITN  0A_ST   3  // 测试状态字, 第4位  0 轴的 (0A_ST_03) == 0
TEST  保留
TESTN 保留
驱动  clsProtocol._204C(Read)
驱动  clsProtocol._7432(Read)
驱动  clsProtocol._7230(Read)
 * 
------- 输出
              值输出OUT   协议
OUT   2DO_11  0    // 输出   (2D_11) = 0
OUT   2DO_11  1    // 输出   (2D_11) = 1
SEND  保留
              I/O位输出
SETN  2D_IO   11   //        2D_11 = 0
SET   2D_IO   11   // 输出   2D_11 = 1
 */