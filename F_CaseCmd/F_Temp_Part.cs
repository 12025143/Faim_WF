#region //...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_CaseCmd
{
    #region //...
    using F_Entitys;
    using F_Enums;
    using FACC;
    using F_Entitys_DAL;
    #endregion
    public class F_Temp_Part : Temp_Data
    {
        protected int _ret;
        protected DAL_CommData _dao_comm = null;
        protected F_Temp_Part()
        { }
        protected int do_BaseNew(clsFaim3 v_faim3, DAL_CommData v_dao_comm)
        {
            _faim3 = v_faim3;
            _dao_comm = v_dao_comm;
            _ret = do_InitData();
            return _ret;
        }
        int do_InitData()
        {
            //'从类名取编号
            _clsName = this.GetType().Name;
            int _idx = _clsName.LastIndexOf('_');// InStrRev(_clsName, "_");
            //if (_idx > 0) _idx += 1;
            _caseFix = _clsName.Substring(_idx, _clsName.Length - _idx); //'提取 编号位
            _caseName = _flowName + _caseFix;//' 步骤/方法名 = 流/线程名 + _编号
            if (!_faim3.dict_CaseAlarm.ContainsKey(_caseName))
            {
                FACC.F_Log.Debug_1(_clsName, String.Format("--->>>> 初始化失败-10: {0} 报警对象不存在(dict_CaseAlarm)", _caseName));
                return -10;
            }
            //_testBits = _faim3.dict_CaseAlarm[_caseName].testBits;
            clsFlow _flow = _faim3.dict_Flow[_flowName];
            if (!_flow.dictCases.ContainsKey(_caseName))
            {
                FACC.F_Log.Debug_1(_clsName, String.Format("--->>>> 初始化失败-20: {0} 步骤对象 不存在(dictCases)", _caseName));
                return -20;
            }
            _otherCase = _flow.dictCases[_caseName].otherCase;
            return 0;
        }
        // 01 DIM_A
        // 02 "AR" "OUT_A"
        protected virtual void do_PART_A(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vflowName, string vCaseName)
        {
            F_CaseSub.do_PARTA(v_faim3, v_dao_comm, vflowName, vCaseName);
        }
        protected virtual void do_PART_C(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vflowName, string vCaseName)
        {
            F_CaseSub.do_PARTC(v_faim3, v_dao_comm, vflowName, vCaseName);
        }
        // 04 "DIM_AOK"
        // 05
        // 11 "DIM_T" 
        // 12 测试 TEST
        protected virtual void do_TEST2(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vflowName, string vCaseName, ref bool vbl)
        {
            F_CaseSub._TEST2(v_faim3, v_dao_comm, vflowName, vCaseName, ref vbl);
        }
        // 13 OUT_T
        //protected virtual void OUT_T(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vflowName, string vCaseName)
        //{
        //    F_CaseSub._OUT_T(v_faim3, v_dao_comm, vflowName, vCaseName);
        //}
        // 14 DIM_TOK 
        // 15 OUT_TOK
        // 16 NG
        protected virtual void NG(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vflowName, string vCaseName)
        {
            F_CaseSub._NG(v_faim3, v_dao_comm, vflowName, vCaseName);
        }
        //'直接以值正(1)/负(0)决定输出1/0  _do2_AR(_devNo, 6, eSwitch.On)
        internal void doSend(clsFaim3 _faim3, int vDevNo, int vBit, eHL val)
        {
            int _bit = vBit;//
            if (Math.Abs(_bit) < 99 && 99 < _faim3.sect_iDev) // 需要设备号，以调整绝对寻址
            {
                _bit = vDevNo * _faim3.sect_iDev + Math.Abs(vBit);//
            }
            _faim3.Comm_Data.bt_out[0][_bit] = ((int)val > 0 ? 1 : 0);//
        }
        protected void btOUT(int vDevNo, int vBit, eHL val)
        {
            doSend(_faim3, vDevNo, vBit, val);
        }
        //protected void btOUT(int vDevNo, int vBit)
        //{
        //    doSend(_faim3, vDevNo, vBit, (eHL)(vBit > 0 ? 1 : 0), _sect_iDev);
        //}
        //'计划中取下一流程名, 用于一步延时后进行下一步
        //'直接以值正(1)/负(0)决定输出1/0  _do2_AR(_devNo, 6, eSwitch.On)
        //protected void doSend(int vDevNo, int vBit, eHL val)
        //{
        //    btOUT(_faim3, vDevNo, vBit, val, _sect_iDev);
        //}
        //'直接以位置正(1)/负(0)决定输出1/0  _do2_AR(_devNo, -6)
        public static void doGet_NextCase(clsFaim3 _faim3, ref clsFlow _flow, string vCaseName)
        {
            string _alartCase = _flow.alartCase;
            //if (_faim3.dict_CaseState.ContainsKey(_alartCase)) return;  // 
            clsCaseState _caseState = _faim3.dict_CaseState[_alartCase];
            if (_flow.dictCases.ContainsKey(vCaseName))
            {
                _caseState.endMode = eCaseFlag.Next;
                _flow.nextCase = _flow.dictCases[vCaseName].nextCase;
            }
            else// '流程结束
            {
                F_Log.Debug_1("DAL_Part", String.Format("--->>>> {0} 没有指定下一个步骤名", vCaseName));
                _caseState.endMode = eCaseFlag.Exist;
                _flow.nextCase = "Free";
            }
        }
    }
}
