#region // ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
#endregion
namespace F_CaseThread
{
    using F_Enums;
    using FACC;
    using F_Entitys;
    using F_Entitys_DAL;
    // 流程管理器

    public class F_ThManager
    {
        clsFaim3 _faim3 = null;
        DAL_CommData _dao_comm = null;
        // 事件
        //public event FaimDelegate.delOnMessage OnMessage;
        // 变量
        //clsMethodWhile_BLL bo_MethodWhile = null;
        // 构造

        public F_ThManager(clsFaim3 v_faim3, DAL_CommData v_dao_comm)
        {
            _faim3 = v_faim3;
            _dao_comm = v_dao_comm;
            //bo_MethodWhile = new clsMethodWhile_BLL(_faim3);
            //bo_MethodWhile.OnMessage += new FaimDelegate.delOnMessage(evt_OnMessage_MethodWhile);
        }
        //void do_Message()
        //{
        //    OnMessage("", null);
        //}
        int evt_OnMessage_MethodWhile(string thName, object obj)
        {
            return 0;
        }
        // 开 所有流程

        public void doStartTh()
        {
            FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("流程池: 可启动{0}个", _faim3.dict_Threads.Count));
            foreach (var _info in _faim3.dict_Threads)
            {
                doStartTh(_info.Key);
            }
        }
        // 关 所有流程 
        public void doStopTh()
        {
            foreach (var item in _faim3.dict_Threads)
            {
                doStopTh(item.Key);
            }
        }
        // 开 指定的业务

        public void doStartTh(string vflowName)
        {
            if (string.IsNullOrEmpty(vflowName)) return;
            if (!_faim3.dict_Threads.ContainsKey(vflowName))
            {
                FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("--->>>>! 线程没有启动 1:{0}", vflowName));
                return;
            }
            clsThread _info = _faim3.dict_Threads[vflowName];
            if (_info.th != null)// 原有流程
            {
                FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("准备 清除原有 流程:{0}", vflowName));
                _info.th = _info.th;
                if ((_info.th != null) &&
                    (_info.th.ThreadState != ThreadState.Aborted) &&
                    (_info.th.ThreadState != ThreadState.Stopped)) // :已启动, 停止
                {
                    FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("停止 运行原有 流程:{0}", _info.th.Name));
                    _info.th.Abort();
                    _info.Info.cnt_heart = -1;
                    Thread.Sleep(10);
                }
                _info.th = null;
                // 清除
                FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("完成 清除原有 流程:{0}", vflowName));
            }
            if (_info.Del != null) // 原有代理
            {
                FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("存在代理，绑定:{0}", vflowName));
                _info.th = new Thread(_info.Del);  // 用方法名指定代理
                _info.Info.allowWhile = eSwitch.On;
                FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("启动 流程-->流程池:{0}", vflowName));
                _info.th.Start();
            }
            else
            {
                FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("--->>>> 不存在 代理 :{0}", vflowName));
            }
        }
        // 关 指定的业务 
        public void doStopTh(string vflowName)
        {
            if (string.IsNullOrEmpty(vflowName)) return;
            FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("准备 关闭 流程:{0}", vflowName));
            //bo_MethodWhile.doClearState(_faim3.dict_Threads[vflowName].Info);
            _faim3.dict_Threads[vflowName].Info.allowWhile = eSwitch.Off;
            Thread.Sleep(10);
            if ((_faim3.dict_Threads[vflowName].th != null) &&
                (_faim3.dict_Threads[vflowName].th.ThreadState != ThreadState.Aborted) &&
                (_faim3.dict_Threads[vflowName].th.ThreadState != ThreadState.Stopped)) // :已启动, 停止
            {
                _faim3.dict_Threads[vflowName].th.Abort();
                _faim3.dict_Threads[vflowName].Info.cnt_heart = -1; 
                Thread.Sleep(10);
                FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("完成 关闭 流程:{0}", vflowName));
            }
            else
            {
                FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("--->>>> 无需关闭 流程未启动:{0}", vflowName));
                _faim3.dict_Threads[vflowName].th = null;
            }
        }
        // 停 所有业务

        public void doStopBusiness()
        {
            foreach (var item in _faim3.dict_Threads)
            {
                doStopBusiness(item.Key);
            }
        }
        // 启 所有业务

        public void doStartBusiness()
        {
            foreach (var item in _faim3.dict_Threads)
            {
                doStartBusiness(item.Key);
            }
        }
        // 停 指定的 业务
        public void doStopBusiness(string vflowName)
        {
            if (string.IsNullOrEmpty(vflowName)) return;
            set_Business_OnOff(vflowName, eSwitch.No);
        }
        // 执行 指定的流程

        public void doRunFlow(string vflowName ="DAL_Buttons", int runMode = 3)
        {
            if (string.IsNullOrEmpty(vflowName)) return;
            if (!_faim3.dict_Threads.ContainsKey(vflowName)) return;
            clsThread _info = _faim3.dict_Threads[vflowName];
            if (_info.th == null ||
                _info.th.ThreadState == System.Threading.ThreadState.Aborted ||
                _info.th.ThreadState == System.Threading.ThreadState.Stopped) // :已启动, 停止 未开启流程
            {
                doStartTh(vflowName);
                System.Threading.Thread.Sleep(100);
            }
            doRunMode(vflowName, runMode);
        }
        // 执行 指定的 业务
        public void doStartBusiness(string vflowName)
        {
            if (string.IsNullOrEmpty(vflowName)) return;
            set_Business_OnOff(vflowName, eSwitch.Yes);
        }
        // 1逐行 2单步 3断点 续运行

        void doRunMode(string vflowName, int runMode)
        {
            if (string.IsNullOrEmpty(vflowName)) return;
            clsFlow _flow = _faim3.dict_Flow[vflowName];
            _flow.stepCase = "";        // 单步2
            _flow.caseLineIdx = -1;     // 单行2
            _flow.isStepLine = runMode == 1; // 单行1
            _flow.isStepBy = runMode == 2;       // 单步1
            _flow.breakSkipCase = "";// _flow.currCase; // 断点
            if (_faim3.dict_Threads.ContainsKey(vflowName))
                //if (!_faim3.dict_Threads[vflowName].Info.allowDoBusi)
                _faim3.dict_Threads[vflowName].Info.allowBusiness = eSwitch.Yes;
        }
        // 恢复/停止 指定的流程 的业务

        void set_Business_OnOff(string vflowName, eSwitch allowDoBusiness)
        {
            if (string.IsNullOrEmpty(vflowName)) return;
            if (!_faim3.dict_Threads.ContainsKey(vflowName))
            {
                FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("--->>>>! 线程没有启动 2:{0}", vflowName));
                return;
            }
            if ((int)allowDoBusiness != (int)eSwitch.Yes)
                _faim3.dict_Threads[vflowName].Info.allowBusiness = eSwitch.Close;
            else
            {
                _faim3.dict_Threads[vflowName].Info.allowBusiness = eSwitch.Yes;
                clsFlow _flow = _faim3.dict_Flow[vflowName];
                // 无效
                _flow.isStepBy = false;       // 单步1
                _flow.stepCase = "";        // 单步2
                _flow.breakSkipCase = _flow.currCase; // 断点
                _flow.isStepLine = false; // 单行1
                _flow.caseLineIdx = -1;     // 单行2
            }
            FACC.F_Log.Debug_1("clsThManagerBLL", string.Format("流程:{0}-->{1}", vflowName, allowDoBusiness.ToString()));
        }
    }
}
