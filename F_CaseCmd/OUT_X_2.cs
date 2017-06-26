using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_CaseCmd
{
    #region // ...
    using F_Entitys;
    using F_Enums;
    using FACC;
    using F_Entitys_DAL;
    using F_Protocols;
    #endregion
    public partial class F_CaseSub
    {
        // 输出
        static void _OUT_X_old(clsFaim3 _faim3, DAL_CommData _dao_comm, string _flowName, string _caseName, string vIfType)
        {
            string _IfType = vIfType + ".GOTO.ADD.DEC.INC.";
            clsFlow _flow = _faim3.dict_Flow[_flowName];
            string _alartCase = _flow.alartCase;
            Dictionary<string, clsCaseChildInfo> _dictCaseChildInfo =
                                _faim3.dict_CaseState[_alartCase].dict_CaseChildInfo;

            if (!_dictCaseChildInfo.ContainsKey(vIfType))
                return; // 条件：指定步骤无此部份指令
            int _lineStart = _dictCaseChildInfo[vIfType].lineStart;  // 起始行号
            if (_lineStart < 0)                return; // 条件：没有指定的结构块起始行号
            clsDevTestBits _line = _faim3.lst_DevTestBits[_lineStart];
            if (_line.flowCase != _caseName)                 return; // 条件：顺序执行时， 如步骤名不对， 则结束当前结构
            if (!_IfType.Contains(_line.IfType.ToUpper() + "."))                 return; //条件：没有结构块指定的命令
            int _cnt_break = 0; // 计数器
            for (int i = _lineStart; i < _faim3.lst_DevTestBits.Count; i++)// 文档对象驱动
            {
                if (_faim3.isEmergency > 0)
                    break;
                #region // for
                _line = _faim3.lst_DevTestBits[i];
                if (_line.Remark.ToUpper().StartsWith("CODE")) 
                    continue;    //条件：文档行指定为以代码实现
                if (!(_line.isDebug == 1 /*#*/ && _faim3.dict_KV["isDebug"] == "1")) // 在 debug 状态下，才运行 debug 行  cfgKV.ini
                    continue;
                if (_line.flowCase != _caseName)                     break; // 条件：顺序执行时， 如步骤名不对， 则结束当前结构
                if (!_IfType.Contains(_line.IfType.ToUpper() + "."))                     break;//条件：没有结构块指定的命令

                #region // 开始执行
                _do_WaitLine(_faim3, _dao_comm, _flowName, eWF_State.Line, i);
                System.Diagnostics.Stopwatch _tmr = new System.Diagnostics.Stopwatch();	//实例化一个计时器
                _tmr.Start();
                long _trm_begin = _tmr.ElapsedMilliseconds; // 毫秒

                int _bit = -1;// 位
                int _val_2 = F_TransCalc._Get_Value_2(_faim3, _line.HL);
                if ("SLEEP" == _line.vName.ToUpper()) // 必须作为第一判断
                {
                    System.Threading.Thread.Sleep(_val_2);
                }
                #region // GOTO ADD DEC INC
                else if ("GOTO" == _line.IfType.ToUpper())
                {
                    _flow.nextCase = _line.vName;
                }
                else if ("ADD" == _line.IfType.ToUpper())
                {
                    if (_faim3._dim_dict.ContainsKey(_line.vName))
                    {
                        #region // _dim_dict 动态变量
                        _bit = _faim3._dim_dict[_line.vName];// 内存位 自定义名为下标, 分配地址, 寻址
                        _faim3._dim[_bit] += _val_2;  // 直接设置, 用以联动     dim[Feeder05_unLink] = Value 
                        #endregion
                    }
                }
                else if ("DEC" == _line.IfType.ToUpper())
                {
                    if (_faim3._dim_dict.ContainsKey(_line.vName))
                    {
                        #region // _dim_dict 动态变量
                        _bit = _faim3._dim_dict[_line.vName];// 内存位 自定义名为下标, 分配地址, 寻址
                        _faim3._dim[_bit] -= _val_2;  // 直接设置, 用以联动     dim[Feeder05_unLink] = Value 
                        #endregion
                    }
                }
                else if ("INC" == _line.IfType.ToUpper())
                {
                    if (_faim3._dim_dict.ContainsKey(_line.vName))
                    {
                        #region // _dim_dict 动态变量
                        _bit = _faim3._dim_dict[_line.vName];// 内存位 自定义名为下标, 分配地址, 寻址
                        _faim3._dim[_bit] += 1;  // 直接设置, 用以联动     dim[Feeder05_unLink] = Value 
                        #endregion
                    }
                }
                #endregion
                else if (_faim3.dict_DevFunction.ContainsKey(_line.vName))
                {
                    #region // 输出 OUT IO 类
                    clsDevFunction _df = _faim3.dict_DevFunction[_line.vName];//
                    int _devNo = _df.devNo;//设备号
                    int _ref_i = _faim3.sect_idev * _devNo;
                    _bit = _ref_i + _df.Index;//内存位
                    if (_df.varName.ToUpper() == "BT_OUT")
                    {
                        // 特殊 vName ==> driverName ==> _df.ioIdx ==> LLine
                        _faim3.Comm_Data.bt_out[0][_ref_i + Convert.ToInt32(_faim3.dict_KV["LLine"])] = _df.ioIdx;
                        // HL ==> _val_2 => Value
                        _faim3.Comm_Data.bt_out[0][_ref_i + Convert.ToInt32(_faim3.dict_KV["Value"])] = _val_2;
                        //通用 bt_out[_df.Index]-->_df.ioIdx  _val_2-->H/L
                        _faim3.Comm_Data.bt_out[0][_bit] = _val_2; // 输出电位  bt_out[106]=6   bt_out[6]= 9>0
                        // 发出消息 条件：1.bt_out 2._devNo 3.snd_lenLoc
                        _faim3.Comm_Data.bt_out[0][_faim3.sect_idev * _devNo + _faim3.snd_lenLoc] = -1; // 
                        _dao_comm.set_bt_out(_faim3.sect_idev * _devNo + _faim3.snd_lenLoc, _bit);  // bt_out[164]=6  
                    }
                    #endregion
                }
                else if (_faim3.dict_CmdFormat.ContainsKey(_line.vName))
                {
                    #region // 输出 OUT 协议类

                    clsProtocol _protocol = new clsProtocol(_faim3, _dao_comm);
                    // 返回待发送数据的绝对位置
                    int _ret = _protocol.getSendData(_line.vName, _val_2, _line.HL);
                    // 设发送数据的长度, 发送的是字符域的数据 
                    //int _ref_s = _faim3.sect_sDev_start + _faim3.sect_sDev * _ret;
                    //_faim3._sss[_ref_s + _faim3.snd_slen] = "0";
                    //_dao_comm.set_arr_str(_ref_s + _faim3.snd_slen, "1");
                    // 发送
                    _dao_comm.set_arr_str(_ret, "1");
                     #endregion
                }
                else if (_faim3._dim_dict.ContainsKey(_line.vName))
                {
                    #region // _dim_dict 动态变量 dim[Feeder05_unLink] <== Value
                    _bit = _faim3._dim_dict[_line.vName];// 内存位
                    _faim3._dim[_bit] = _val_2;  // 直接设置, 用以联动    
                    #endregion
                }
                else if (_line.vName.ToUpper().StartsWith("_FAIM3") ||
                         _line.vName.ToUpper().StartsWith("CLSFAIM3"))
                {
                    // 大数据 空
                }
                else if (_cnt_break > 0)  // 截断
                {
                    break;
                }
                #endregion
                _cnt_break++;
                _line.times++;   // 此行使用的次数
                _line.tmr = (int)(_tmr.ElapsedMilliseconds - _trm_begin);  // 本次流程完成用时
                _faim3.dict_Threads[_flowName].Info.tmr_real += _line.tmr;    // 当前步骤 实际占用总时长 += 每语句使用的时间
                #endregion
            }
        }
    }
}
