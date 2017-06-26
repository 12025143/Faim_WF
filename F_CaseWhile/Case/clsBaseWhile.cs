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
    using F_Entitys_DAL;
    using F_Enums;
    using FACC;
    using F_Interface;
    #endregion
    public class clsBaseWhile : Temp_BaseData
    {
        protected DAL_CommData _dao_comm = null;
        protected clsFlow _flow = null;
        protected eRunState _RunState = eRunState.Free;
        // 空
        protected clsBaseWhile()
        { }
        // 线程名/流程名/设备ID, 忽略返回值
        protected int do_New(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vflowName)
        {
            _faim3 = v_faim3;
            _dao_comm = v_dao_comm;
            // 无流程名， 则取类名
            _flowName = (string.IsNullOrEmpty(vflowName)) ? this.GetType().Name : vflowName;
            #region // dict_Threads 线程池
            if (_faim3.dict_Threads.ContainsKey(_flowName)) // 存在key
            {
                FACC.F_Log.Debug_1("do_New()", string.Format("--->>>> {0}流程/设备存在", _flowName));
                return -1;  // 
            }
            clsThread _en = new clsThread(); // 线程消息
            _en.Info = new clsThreadInfo();
            _en.Name = _flowName;
            _en.Info.flowName = _flowName;
            // 专用线程代理   dict_Threads.Add
            _en.Del = new System.Threading.ThreadStart(do_Sub_While); // 实例化代理
            _faim3.dict_Threads.Add(_flowName, _en);
            #endregion
            F_Log.Debug_1("do_New()", string.Format("初始化 流程:{0}", _flowName));
            return 0;
        }
        void do_Sub_While()  // 流程/线程 的代理
        {
            do_While(_flowName);  // _flowName = 线程名/流程名/设备ID
        }
        // 虚 vFlowId： 线程名/流程名/设备ID
        protected virtual void do_While(string vFlowId)
        {
            IFaimIO _dev_io = _dao_comm.dict_DevIo[_devNo.ToString()];
            _ref_i = _devNo * _faim3.sect_iDev;  // 偏移
            _ref_s = _devNo * _faim3.sect_sDev + _faim3.sect_sDev_start;  // 偏移
            #region // 流程条件 allowWhile  allowBusiness
            clsThreadInfo _TheadInfo = _faim3.dict_Threads[vFlowId].Info;
            _TheadInfo.sleep = Convert.ToInt32(_faim3.dict_DevCards[_devNo].dogDelay);
            System.Diagnostics.Stopwatch _tmr = do_th_Name_pre(_TheadInfo); // 计时
            _TheadInfo.allowWhile = eSwitch.On;
            _TheadInfo.allowBusiness = eSwitch.No;
            #endregion
            while (((int)_TheadInfo.allowWhile == (int)eSwitch.On))
            {
                if (!do_allowBusiness(_TheadInfo)) continue; // 流程信息 1
                long _trm_begin = _tmr.ElapsedMilliseconds; // 毫秒
                if ((int)_faim3.dict_DevCards[_devNo].manuSwitch == (int)eSwitch.On)
                {
                    _dev_io.WatchDog();  // do_CMD("do_Connect")
                    // 如果没有连接， 则连接
                    // 如果有连接， 则判断连接状态
                }
                do_end_CalcTimer(_TheadInfo, _tmr, _trm_begin); // 流程信息2
            }
            doClearState(_TheadInfo);
            _tmr.Stop();		//结束计时
        }
        // 虚 空
        public virtual void doSendStr(string vSndStr)
        {
        }
        // 虚 空
        protected virtual void Send_Receive()
        {
        }
        #region // 流程信息1 do_th_Name_Info_begin()
        protected System.Diagnostics.Stopwatch do_th_Name_pre(clsThreadInfo _ti)
        {
            System.Diagnostics.Stopwatch _tmr = new System.Diagnostics.Stopwatch();	//实例化一个计时器
            _tmr.Start();
            return _tmr;
        }
        protected bool do_allowBusiness(clsThreadInfo vTheadInfo)
        {
            vTheadInfo.cnt_heart++;   // 心跳
            System.Threading.Thread.Sleep(vTheadInfo.sleep);
            if ((int)vTheadInfo.allowBusiness == (int)eSwitch.No)
            {
                clsFlow _flow = _faim3.dict_Flow[_flowName];
                _flow.eState = eWF_State.Wait;
                return false; // 未允许运行流程/业务  business
            }
            return true;
        }
        protected void do_end_CalcTimer(clsThreadInfo vTheadInfo, System.Diagnostics.Stopwatch vWatch, long trm_begin)
        {
            long _trm_curr = vWatch.ElapsedMilliseconds;
            // Case 用时= 总时间 - Case 起始时间
            vTheadInfo.tmr_business = _trm_curr - trm_begin;  // 本次流程完成用时 毫秒
            // 线程 总用时间长 
            vTheadInfo.tmr_total = _trm_curr / 1000;   // 运行总时间 (毫秒)
        }
        #endregion
        public static void doClearState(clsThreadInfo vThreadInfo)
        {
            FACC.F_Log.Debug_1("clsBaseWhile", string.Format("流程 状态 复位{0}", vThreadInfo.flowName));
            vThreadInfo.allowBusiness = eSwitch.No;
            vThreadInfo.cnt_heart = -1;
        }
        protected void do_Send(string vSndStr)
        {
            _faim3.Comm_Data.arr_str[0][_ref_s + 1] = vSndStr;
            _faim3.Comm_Data.arr_str[0][_ref_s + _faim3.snd_iLen] = (vSndStr.Length).ToString();
            Send_Receive();
            // 如收到，解析
        }
    }
}
