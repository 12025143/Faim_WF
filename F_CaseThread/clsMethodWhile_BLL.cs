using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F_FAIM_ThMana
{
    using System.Threading;
    using System.Reflection;    
    using F_Enums;
    
    using F_Entitys;

    public class clsMethodWhile_BLL
    {
        clsFaim3 _Faim3 = null;
        // 事件
        public event FaimDelegate.delOnMessage OnMessage;

        // 构造
        public clsMethodWhile_BLL(clsFaim3 vFaim3)
        {
            _Faim3 = vFaim3;
        }
        void evt_Message()
        {
            OnMessage("", null);
        }



        public virtual void doClearState(ThreadInfo Vn)
        {
            //clsTempSub.doClearState(Vn);
        }

        // 恢复/停止 指定的流程 
        public virtual void doDevice_OnOff(string itemKey, bool allowDoBusi)
        {
            _Faim3.dict_Threads[itemKey].Info.allowDoBusi = allowDoBusi ;
        }
    }
}
