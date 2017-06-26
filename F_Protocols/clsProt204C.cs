#region //
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_Protocols
{
    #region //
    using F_Entitys_DAL;
    using F_Entitys;
    using FACC;
    #endregion
    public partial class clsProtocol
    {
        // MOVR 204C 011/3  -- 命令 设备 轴号/行号
        public static void _204C(clsFaim3 _faim3, DAL_CommData _dao_comm, int idx)
        {
            clsDevTestBits _line = _faim3.lst_DevTestBits[idx];
            clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//
            _204C(_faim3, _dao_comm, _line.IfType, _line.HL, _df.devNo);
        }
        // 设备号的来源可能为多个因素

        public static void _204C(clsFaim3 _faim3, DAL_CommData _dao_comm, string IfType, string HL, int _devNo)
        {
            int _ref_1i = _devNo * _faim3.sect_iDev;  // 偏移
            int _ref_1s = _devNo * _faim3.sect_sDev + _faim3.sect_sDev_start;  // 偏移
            // 行号为消息

            _faim3._sss[_ref_1s + _faim3.snd_sAsc] = IfType; // 命令
            int _val_2 = F_TransCalc._Get_Value_2(_faim3, HL); // 文档的值: 数值


            _faim3._sss[_ref_1s + _faim3.snd_sLen] = _val_2.ToString(); // 轴号表/行号
            _faim3.Comm_Data.bt_out[0][_ref_1i] = -99;
            _dao_comm.set_bt_out(_ref_1i, _devNo);  // 设备 消息
        }
    }
}
