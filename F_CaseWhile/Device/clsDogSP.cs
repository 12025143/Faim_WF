﻿#region //...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_CaseWhile
{
    #region //...
    using F_Entitys;
    using F_Entitys_DAL;
    using Faim3_Drivers;
    using F_Interface;
    using F_Enums;
    #endregion
    public class clsDogSP : clsBaseWhile
    {
        public clsDogSP(clsFaim3 v_faim3, DAL_CommData v_dao_comm, int vDevNo)
        {
            _devNo = Convert.ToInt32(vDevNo);
            _flowName = "d_" + v_faim3.dict_DevCards[_devNo].devID; // 线程名/流程名/设备ID
            base.do_New(v_faim3, v_dao_comm, _flowName);
        }
    }
    public class clsDog485 : clsBaseWhile
    {
        public clsDog485(clsFaim3 v_faim3, DAL_CommData v_dao_comm, int vDevNo)
        {
            _devNo = Convert.ToInt32(vDevNo);
            _flowName = "d_" + v_faim3.dict_DevCards[_devNo].devID; // 线程名/流程名/设备ID
            base.do_New(v_faim3, v_dao_comm, _flowName);
        }
    }
}
