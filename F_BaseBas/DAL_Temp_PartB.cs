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
    using F_Entitys;
    using F_Entitys_DAL;
    using F_Enums;
    using System.Windows.Forms;
    using F_CaseCmd;
    #endregion
    public class DAL_Temp_PartB : F_Temp_Part
    {
        //1  PartB --> doPartB --> Part_2 -- Part_BT
        protected int do_New(clsFaim3 v_faim3, DAL_CommData v_dao_comm)
        {
            _ret = do_BaseNew(v_faim3, v_dao_comm);
            _testName = (_flowName + (_caseFix + F_Const.fix_CaseT));//测试步骤方法名

            if (_dao_comm.dict_Methods.doCase.ContainsKey(_testName))
            {
                FACC.F_Log.Debug_1(_clsName, string.Format("", _testName));
                _ret = -30;
            }
            if (_ret < 0) return _ret;
            _dao_comm.dict_Methods.doCase.Add(_testName, new F_Delegate.delCase(PartB));//'通用代理1
            _dao_comm.dict_Methods.doPartB_T.Add(_caseName, new F_Delegate.delCasePartB_T(Part_2));//'通用代理2
            _dao_comm.dict_Methods.doPartB_R.Add(_caseName, new F_Delegate.delCasePartB_R(Part_BR));//'通用代理3
            FACC.F_Log.Debug_1(_clsName, string.Format("初始化 步骤:{0}", _testName));
            return 0;
        }
        void PartB(DAL_CommData v_dao_comm, string vflowName)
        {
            doPartB(_faim3, v_dao_comm, vflowName);
        }
        void doPartB(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vflowName)
        {
            string _cls_Name = "DAL_PartB";
            clsFlow _flow = v_faim3.dict_Flow[vflowName];
            string _alartCase = _flow.alartCase;//取流程名, 无后缀
            clsCaseState _caseState = _faim3.dict_CaseState[_alartCase];
            clsCaseAlarm _enAlarm = v_faim3.dict_CaseAlarm[_alartCase];
            _enAlarm.OnAlarm += new FaimDelegate.delAlarm(_enAlarm_OnAlarm);
            // 
            if ((_enAlarm.maxDelay1 == -1))//逻辑模型 maxDelay1 = -1  不进行测试
            {
                doGet_NextCase(_faim3, ref _flow, _alartCase);
                FACC.F_Log.Debug_1(_clsName, string.Format("逻辑模型 maxDelay1 = -1  不进行测试:{0}", _alartCase));
                return;
            }
            // 带后缀的 方法 名, 只在 dict_Methods.doCase 中

            F_Delegate.delCasePartB_T _doPartB_T = _dao_comm.dict_Methods.doPartB_T[_alartCase];
            bool _bl_BT = true;
            _bl_BT = (_bl_BT & _doPartB_T(v_faim3, v_dao_comm)); // 测试 TEST
            if ((_flow.Tag == "GOTO_ABS" || _flow.Tag == "GOTO_NG" ) )// 
            {
                _flow.Tag = "";
                _flow.stepCase = _flow.currCase;
                return;
            }
            else if ((_flow.Tag == "GOTO_OTHER"))// 
            {
                _flow.Tag = "";
                List<string> _otherCase = _flow.dictCases[_alartCase].otherCase;
                if (_otherCase.Contains(_alartCase))// 若 自定义列表包括了当前步骤名, 如何处理???
                {
                    return;
                }
            }
            else if ((_flow.Tag == "ALAR_ON_NG"))
            {
                _flow.Tag = "";
                _enAlarm.AlarmDrv = eSwitch.On;
                _enAlarm.AlarmIs = true;   // OnAlarm(this);
                //  
                return;
            }
            else if ((_flow.Tag == "ALAR_OFF_NG"))// 
            {
                _flow.Tag = "";
                _enAlarm.AlarmDrv = eSwitch.Off;
                //  
                _enAlarm.AlarmIs = false;   // OnAlarm(this);
                return;
            }
            // 
            if ((_bl_BT && !_enAlarm.AlarmIs))// 
            {
                //  
                _caseState.Result = "";
                _enAlarm.DelayT1 = 0;
                _enAlarm.DelayT2 = 0;
                _flow.NGtimes = 0;
                doGet_NextCase(_faim3, ref _flow, _alartCase);
                F_Log.Debug_1(_cls_Name, (_alartCase + " BT(1)"));
            }
            else
            {
                //  
                if (!_enAlarm.AlarmIs)// 没报警
                {
                    //  
                    _enAlarm.DelayT1++;
                    if (_enAlarm.DelayT1 > _enAlarm.maxDelay1)//  第一圈
                    {
                        _enAlarm.DelayT1 = 0;
                        _enAlarm.DelayT2++;
                        if (_enAlarm.DelayT2 > _enAlarm.maxDelay2)// 第二圈 时间到
                        {
                            _enAlarm.DelayT2 = 0;
                            if (_enAlarm.maxDelay2 == 0)// 
                            {
                                if (_flow.dictCases.ContainsKey(_alartCase))
                                {
                                    _caseState.endMode = eCaseFlag.NG;
                                    _flow.nextCase = v_faim3.dict_CaseAlarm[_alartCase].excpCase;
                                }
                                else
                                {
                                    F_Log.Debug_1(_cls_Name, string.Format("--->>>> {0} 没有指定下一个方法名", _alartCase));
                                    _caseState.endMode = eCaseFlag.Exist;
                                    _flow.nextCase = "Free";//流程结束
                                }
                                return;
                            }
                            else if (_enAlarm.maxDelay2 > 0)
                            {
                                _enAlarm.AlarmIs = true;
                                _enAlarm.AlarmDrv = eSwitch.On;// 报警灯亮
                                //  
                            }
                        }
                    }
                }
                else if (_enAlarm.AlarmIs)//
                {
                    //   
                    if (!string.IsNullOrEmpty(_enAlarm.dlgMessage))//
                    {
                        string _title = (_enAlarm.RemarkA + "，请确认忽略异常！");
                        if ((_enAlarm.dlgMessage.Length > 6))
                        {
                            _title = _enAlarm.dlgMessage;
                        }
                        //
                        DialogResult _dlg = MessageBox.Show(_enAlarm.dlgMessage, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        if ((_dlg == DialogResult.OK))//逻辑模型:  人工出口, 有报警 忽略异常
                        {
                            //  
                            F_Delegate.delCasePartB_R _doPart_BR = v_dao_comm.dict_Methods.doPartB_R[_alartCase];
                            //  测试 RUN
                            _doPart_BR(v_faim3, v_dao_comm);
                            //  
                            System.Threading.Thread.Sleep(_enAlarm.DelayB);
                            if (_flow.dictCases.ContainsKey(_alartCase))
                            {
                                _caseState.endMode = eCaseFlag.NG;
                                _flow.nextCase = v_faim3.dict_CaseAlarm[_alartCase].excpCase;
                            }
                            else
                            {
                                F_Log.Debug_1(_cls_Name, string.Format("--->>>> {0} 没有指定下一个方法名", _alartCase));
                                _caseState.endMode = eCaseFlag.Exist;
                                _flow.nextCase = "Free";//流程结束
                            }
                            _enAlarm.AlarmIs = false;
                            _enAlarm.AlarmDrv = eSwitch.Off;//报警灯灭
                        }
                    }
                    else if (_bl_BT)//逻辑模型:  成功出口, 有报警
                    {
                        _caseState.Result = "";
                        _enAlarm.DelayT1 = 0;
                        _enAlarm.DelayT2 = 0;
                        _flow.NGtimes = 0;
                        _enAlarm.AlarmIs = false;
                        _enAlarm.AlarmDrv = eSwitch.Off;//报警灯灭
                        doGet_NextCase(_faim3, ref _flow, _alartCase);
                        F_Log.Debug_1(_cls_Name, (_alartCase + " BT(2)"));
                    }
                }
            }
            return;
        }
        public bool Part_2(clsFaim3 v_faim3, DAL_CommData v_dao_comm)
        {
            Part_BT(v_faim3, v_dao_comm);
            #region // xls
            do_TEST2(v_faim3, v_dao_comm, _flowName, _caseName, ref _bl);
            if (_bl)
            {
                do_PART_C(v_faim3, v_dao_comm, _flowName, _caseName);
            }
            else
            {
                NG(v_faim3, v_dao_comm, _flowName, _caseName);
            }
            #endregion 
            return _bl;
        }
        //  Part_B_T  --> Part_AR 空
        public virtual bool Part_BT(clsFaim3 v_faim3, DAL_CommData v_dao_comm)
        {
            return _bl;
        }
        // BR
        public virtual void Part_BR(clsFaim3 v_faim3, DAL_CommData v_dao_comm)
        {
        }
        void _enAlarm_OnAlarm(clsCaseAlarm vCaseAlarm)
        {
        }
    }
}
