#region //
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_CaseBase
{
    #region //
    using FACC;
    using F_Entitys_DAL;
    using F_Entitys;
    using System.Threading;
    using F_CaseCmd;
    #endregion
    public class DAL_Temp_PartA : F_Temp_Part
    {
        protected int do_New(clsFaim3 v_faim3, DAL_CommData v_dao_comm)
        {
            _ret = do_BaseNew(v_faim3, v_dao_comm);
            if (_dao_comm.dict_Methods.doCase.ContainsKey(_caseName))
            {
                FACC.F_Log.Debug_1(_clsName, String.Format("--->>>> 初始化失败-3: {0} 步骤名 已存在", _caseName));
                _ret = -3;
            }
            if (_ret < 0) return _ret;
            _dao_comm.dict_Methods.doCase.Add(_caseName, new F_Delegate.delCase(PartA));//'通用代理1
            _dao_comm.dict_Methods.doPartA_R.Add(_caseName, new F_Delegate.delCasePartA_R(Part_1)); //'通用代理2
            FACC.F_Log.Debug_1(_clsName, String.Format("初始化 步骤:{0}", _caseName));
            return 0;
        }
        void PartA(DAL_CommData v_dao_comm, string vflowName)
        {
            doPartA(_faim3, v_dao_comm, vflowName);
        }
        bool doPartA(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vflowName)
        {
            string _cls_Name = "DAL_PartA";
            clsLogs _logs = new clsLogs();
            clsFlow _flow = v_faim3.dict_Flow[vflowName];
            string _currCase = _flow.nextCase;
            F_Log.Debug_1(_cls_Name, String.Format("流程:{0}: 步骤:{1}",
                                               vflowName, _currCase));
            _flow.alartCase = _currCase;
            if (!v_faim3.dict_CaseState.ContainsKey(_currCase))
            {
                //'A: 步骤.txt 文件中没有注册该步骤参数
                _logs.No = v_faim3.dict_CaseState[_currCase].No;
                _logs.Name = (_currCase + "_没注册 步骤");
                _flow.RunLog.Add(_logs);
                F_Log.Debug_1(_cls_Name, string.Format("--->>>> 没注册 流程:{0}: 步骤:{1}", vflowName, _currCase));
                return true;
            }
            clsCaseState _en_State = v_faim3.dict_CaseState[_currCase];
            if ((_en_State.Times > 0)) // 逻辑模型: 执行次数至少有一次, 如 <1, 则不执行AR(),  只执行判断, 以实现流程在等待联动消息的逻辑
            {
                F_Delegate.delCasePartA_R _doPartA = _dao_comm.dict_Methods.doPartA_R[_currCase];// 通过代理, 从步骤集中取出一个步骤, 执行其方法()
                // 
                _logs.No = v_faim3.dict_CaseState[_currCase].No;
                _logs.Name = _currCase;
                _flow.RunLog.Add(_logs);
                _doPartA(v_faim3, v_dao_comm);
                // AR
                // Delay
                if ((_en_State.Delay > -1))//
                {
                    Thread.Sleep(_en_State.Delay);
                }
                // 
            }
            if (string.IsNullOrEmpty(_en_State.No))//逻辑模型:  无BT报警号 检测的步骤, 只执行 AR(), 不执行AR(), 实现两个 AR() 串联（直通）的逻辑
            {
                doGet_NextCase(_faim3, ref _flow, _currCase);
            }
            else
            {
                // 逻辑模型:  无BT报警号 无报警延时, 只执行 AR(), 不执行AR(), 实现两个 AR() 串联（直通）的逻辑
                string _testCase = (_currCase + F_Const.fix_CaseT);
                if (_flow.nextCase == _flow.currCase)
                {
                    _flow.alartCase = _currCase;
                    _flow.nextCase = _testCase;  // ==> Test  当前流程名 + _T
                    F_Log.Debug_1(_cls_Name, String.Format("取到 测试步骤名:{0}", _flow.nextCase));
                }
            }
            return true;
        }
        //  先执行配置， 没配置则忽略
        void Part_1(clsFaim3 v_faim3, DAL_CommData v_dao_comm)
        {
            Part_AR(v_faim3, v_dao_comm);
            // xls
            do_PART_A(v_faim3, v_dao_comm, _flowName, _caseName);
        }
        //  Part_A_R  --> Part_AR 空
        public virtual void Part_AR(clsFaim3 v_faim3, DAL_CommData v_dao_comm) //  没有配置， 此行不运行
        {
        }
    }
}
