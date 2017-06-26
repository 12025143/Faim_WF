#region //...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NPOI.SS.UserModel;
#endregion
namespace nsFACC
{
    // 流程名 可决定 流程的后续文件 自动引导加载
    public class F_Const
    {
        #region //  path
        public static string path_cfg = @"cfg\";          // 
        public static string path_Config = "";  // 系统配置文件路径
        public static string path_Enum = "";    // 系统常数配置文件路径
        public static string path_flowDLL = @"flowDLL\";    // 流程dll文件夹
        public static string path_Language = @"Language\";          // 多语言文件包 
        public static string path_Img = @"images\"; // 系统图路径
        public static string path_plugins = "";             // 插件文件路径 
        public static string path_System_Img = @"images\system\"; // 系统图路径
        public static string path_System_ini = @"ini\";     // 系统 ini 路径
        public static string path_tbInfo = @"users\tb\";          // 产品表信息
        public static string path_User = @"users\";          // 用户文件夹
        #endregion
        #region //  fn
        public static string fn_CaseName = "";      // 方法注册文件(动态)
        public static string fn_CmdFormat = "";     // 协议文件格式
        public static string fn_CmdFormat_1 = "";   // 协议文件格式1
        public static string fn_CmdFormat_2 = "";   // 协议文件格式2
        public static string fn_CmdFormat_3 = "";   // 协议文件格式3
        public static string fn_DAL_Dim_dict = "";  // 驱动卡注册列表
        public static string fn_Dev_Cards = "";     // 驱动卡注册列表
        public static string fn_Dev_Function = "";  // 驱动卡IO功能注册列表
        public static string fn_Dev_testBits = "";  // 
        public static string fn_FlowName = "";      // 流程注册文件(动态)
        public static string fn_NoName = "";        // 编号名称文件(动态)
        public static string fn_KV = "";            //  
        public static string fn_SignaLamp = "";     // 
        public static string fn_SignalStyle = "";   // 
        public static string fn_WinInfo = "";       // 
        public static string fn_WinShow = "";       // 
        public static string ftb_AxleInfo = "";     // 
        public static string ftb_btnScript = "";    // 
        public static string ftb_CommonInfo = "";   // 通用格式
        public static string ftb_PointInfo = "";    // 
        public static string fn_ResFiles = "";      //
        #endregion
        #region //  class
        public static string enabled_threads = "ThCase_01,RobotCalSub";   // 有效线程/流程名 配置可控流程名
        public static string enabled_plugins = ".HelloFacc.HelloDemo.";   // 有效插件名 配置可控插件名  
        public static string fix_CaseT = "_T";              // 流程步骤的第二部份的后缀
        #endregion
        #region //  cnt
        public static int cnt_TrackAction = 3;
        public static int cnt_XianTi = 3;
        public static int cnt_Point_NG = 5;
        public static int cnt_Point_Claws = 5;
        public static int cnt_Point_capCanKao = 1;
        public static int cnt_Point_LoadMet = 10;
        public static int cnt_Point_Marks = 2;
        #endregion
        static F_Const()
        {
            string _fn = System.AppDomain.CurrentDomain.BaseDirectory + @"Config\cfgConst.ini";
            InitMe_Const(_fn);
        }
        static void InitMe_Const(string vFn)
        {
            List<string> _lst = doGet_arr(vFn, ",");
            foreach (var item in _lst)
            {
                #region // 条件
                if (string.IsNullOrEmpty(item.Trim())) continue;
                if (item.StartsWith("'")) continue; // 分离注释1
                if (item.StartsWith("=")) continue; // 分离无key
                string[] _arr1 = item.Split('='); // 分离 key value
                if (_arr1.Length < 2) continue; // 数量不足
                #endregion
                string[] _arr2 = _arr1[1].Split('\'');// 分离注释2
                string _pt = System.AppDomain.CurrentDomain.BaseDirectory;
                switch (_arr1[0].Trim())
                {
                    #region // path
                    case "path_cfg":
                        path_cfg = _pt + _arr2[0].Trim();
                        break;
                    case "path_Config":
                        path_Config = _pt + _arr2[0].Trim();
                        break;
                    case "path_Enum":
                        path_Enum = _pt + _arr2[0].Trim();
                        break;
                    case "path_flowDLL":
                        path_flowDLL = _pt + _arr2[0].Trim();
                        break;
                    case "path_Language":
                        path_Language = _pt + _arr2[0].Trim();
                        break;
                    case "path_Img":
                        path_Img = _pt + _arr2[0].Trim();
                        break;
                    case "path_plugins":
                        path_plugins = _pt + _arr2[0].Trim();
                        break;
                    case "path_System_Img":
                        path_System_Img = _pt + _arr2[0].Trim();
                        break;
                    case "path_System_ini":
                        path_System_ini = _pt + _arr2[0].Trim();
                        break;
                    case "path_tbInfo":
                        path_tbInfo = _pt + _arr2[0].Trim();
                        break;
                    case "path_User":
                        path_User = _pt + _arr2[0].Trim();
                        break;
                    #endregion
                    #region // fn
                    case "fn_CaseName":
                        fn_CaseName = _arr2[0].Trim();
                        break;
                    case "fn_CmdFormat":
                        fn_CmdFormat = _arr2[0].Trim();
                        break;
                    case "fn_CmdFormat_1":
                        fn_CmdFormat_1 = _arr2[0].Trim();
                        break;
                    case "fn_CmdFormat_2":
                        fn_CmdFormat_2 = _arr2[0].Trim();
                        break;
                    case "fn_CmdFormat_3":
                        fn_CmdFormat_3 = _arr2[0].Trim();
                        break;
                    case "fn_DAL_Dim_dict":
                        fn_DAL_Dim_dict = _arr2[0].Trim();
                        break;
                    case "fn_Dev_Cards":
                        fn_Dev_Cards = _arr2[0].Trim();
                        break;
                    case "fn_Dev_Function":
                        fn_Dev_Function = _arr2[0].Trim();
                        break;
                    case "fn_Dev_testBits":
                        fn_Dev_testBits = _arr2[0].Trim();
                        break;
                    case "fn_FlowName":
                        fn_FlowName = _arr2[0].Trim();
                        break;
                    case "fn_NoName":
                        fn_NoName = _arr2[0].Trim();
                        break;
                    case "fn_KV":
                        fn_KV = _arr2[0].Trim();
                        break;
                    case "fn_SignaLamp":
                        fn_SignaLamp = _arr2[0].Trim();
                        break;
                    case "fn_SignalStyle":
                        fn_SignalStyle = _arr2[0].Trim();
                        break;
                    case "fn_WinInfo":
                        fn_WinInfo = _arr2[0].Trim();
                        break;
                    case "fn_WinShow":
                        fn_WinShow = _arr2[0].Trim();
                        break;
                    #endregion
                    #region // ftb
                    case "ftb_AxleInfo":
                        ftb_AxleInfo = _arr2[0].Trim();
                        break;
                    case "ftb_btnScript":
                        ftb_btnScript = _arr2[0].Trim();
                        break;
                    case "ftb_PointInfo":
                        ftb_PointInfo = _arr2[0].Trim();
                        break;
                    case "ftb_CommonInfo":
                        ftb_CommonInfo = _arr2[0].Trim();
                        break;
                    case "fn_ResFiles":
                        fn_ResFiles = _arr2[0].Trim();
                        break;
                    #endregion
                    #region // class
                    case "enabled_threads":
                        enabled_threads = _arr2[0].Trim();
                        break;
                    case "enabled_plugins":
                        enabled_plugins = _arr2[0].Trim();
                        break;
                    case "fix_CaseT":
                        fix_CaseT = _arr2[0].Trim();
                        break;
                    #endregion
                }
            }
            #region //  fn
            doGetFullFileName(ref fn_CaseName, path_Config);
            doGetFullFileName(ref fn_CmdFormat, path_Config);
            doGetFullFileName(ref fn_CmdFormat_1, path_Config);
            doGetFullFileName(ref fn_CmdFormat_2, path_Config);
            doGetFullFileName(ref fn_CmdFormat_3, path_Config);
            doGetFullFileName(ref fn_DAL_Dim_dict, path_Config);
            doGetFullFileName(ref fn_Dev_Cards, path_Config);
            doGetFullFileName(ref fn_Dev_Function, path_Config);
            doGetFullFileName(ref fn_Dev_testBits, path_Config);
            doGetFullFileName(ref fn_FlowName, path_Config);
            doGetFullFileName(ref fn_NoName, path_Config);
            doGetFullFileName(ref fn_KV, path_Config);
            doGetFullFileName(ref fn_ResFiles, path_Config);
            doGetFullFileName(ref fn_SignaLamp, path_Config);
            doGetFullFileName(ref fn_SignalStyle, path_Config);
            doGetFullFileName(ref fn_WinInfo, path_cfg);
            doGetFullFileName(ref fn_WinShow, path_cfg);
            doGetFullFileName(ref ftb_AxleInfo, path_tbInfo);
            doGetFullFileName(ref ftb_btnScript, path_tbInfo);
            doGetFullFileName(ref ftb_PointInfo, path_tbInfo);
            doGetFullFileName(ref ftb_CommonInfo, path_tbInfo);
            #endregion
        }
        static void do_CSV_XLS(ref string vFn)
        {
            vFn = vFn.Replace(".csv", ".xls");
            if (File.Exists(vFn)) return;
            vFn = vFn.Replace(".xls", ".csv");
        }
        public static List<string> doGet_ListTrim(List<string> vLst, string vFlagSplit, int vFieldNum = 2, string vFlagRemark = "'")
        {
            if (vLst == null) return null;
            List<string> _lst = null;
            for (int i = 0; i < vLst.Count; i++)
            {
                string item = vLst[i].Trim();
                #region // 条件
                if (string.IsNullOrEmpty(item)) continue;   // 空行
                if (item.StartsWith(vFlagRemark)) continue; // 注释1
                if (item.StartsWith(vFlagSplit + vFlagSplit)) continue;    // 无key
                string[] _flds = item.Split(vFlagSplit[0]);    // 分离 key value
                if (_flds.Length < vFieldNum) continue; // 数量不足
                #endregion
                if (_lst == null) _lst = new List<string>();
                _lst.Add(item);
            }
            return _lst;
        }
        public static List<string> doGet_arr(string vfn, string vFlagSplit, string vFlagRemark = "'", bool allSheets = false)
        {
            string _fn = vfn.Trim().ToUpper();
            if (_fn.EndsWith("\\")) return null; // 目录
            if (!_fn.Contains("."))
                _fn = _fn + ".XLS";
            if (!File.Exists(_fn))
            {
                if (_fn.EndsWith(".XLS"))
                    _fn = _fn.Replace(".XLS", ".CSV");
                if (!File.Exists(_fn))
                {
                    if (_fn.EndsWith(".CSV")) _fn = _fn.Replace(".CSV", ".XLS");
                }
                if (!File.Exists(_fn))
                {
                    if (_fn.EndsWith(".TXT")) _fn = _fn.Replace(".TXT", ".XLS");
                }
                if (!File.Exists(_fn))
                {
                    MessageBox.Show(_fn + " not Exist!",
                                    "MessageBox",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    throw new Exception(_fn + " not Exist!");
                }
            }
            RemoveFileReadOnly(_fn);
            List<string> _lst = null;
            if (_fn.EndsWith(".XLS") ||
                _fn.EndsWith(".XLSX"))
            {
                _lst = do_Get_arr(_fn, vFlagSplit, allSheets);
            }
            else if (_fn.EndsWith(".CFG") ||
                     _fn.EndsWith(".TXT") ||
                     _fn.EndsWith(".INI") ||
                     _fn.EndsWith(".CSV"))
            {
                string _fn_fs = _fn;
                if (_fn.ToUpper().EndsWith("cfgConst.ini".ToUpper()))
                {
                    string[] _arr_0 = File.ReadAllLines(_fn_fs, Encoding.Default);
                    _lst = new List<string>(_arr_0);
                }
                else
                {
                    _fn_fs = path_User + "ls.TXT";
                    File.Copy(_fn, _fn_fs, true);  // 备份
                    string[] _arr_1 = File.ReadAllLines(_fn_fs, Encoding.Default);
                    File.Delete(_fn_fs);  // 删除
                    _lst = new List<string>(_arr_1);
                }
            }
            if (_lst == null) return null;
            int _len = _lst.Count;
            for (int i = _len - 1; i > -1; i--)
            {
                string item = _lst[i].Trim();
                if (string.IsNullOrEmpty(item) ||   // 空行
                    item.StartsWith(vFlagRemark) ||
                    item.StartsWith("[") ||
                    item.StartsWith("【") ||
                    item[0] == 34 ||
                    item.StartsWith("-") ||
                    item.StartsWith("=") ||
                    item.StartsWith("//") ||
                    item.StartsWith("\\") ||
                    item.ToUpper().StartsWith("REM"))
                    _lst.RemoveAt(i);
            }
            return _lst;
        }
        // 从文件中读取
        public static List<string> do_Get_arr(string vfn, string vFlagKey, bool allSheets = false) // 
        {
            if (!File.Exists(vfn)) return null;
            #region // _fn ==> _fs
            if (!Directory.Exists(path_User))
            {
                MessageBox.Show(path_User + " not Exist!", "MessageBox",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw new Exception(path_User + " not Exist!");
            }
            FileStream _fs = null;
            string _fn_fs = path_User + "ls.xls";
            File.Copy(vfn, _fn_fs, true);
            _fs = File.OpenRead(_fn_fs);
            #endregion
            #region // _wk <== _fs <== _fn
            //_fs = new FileStream(vfn, FileMode.Open, FileAccess.Read);
            //NPOI.HSSF.UserModel.HSSFWorkbook _wk = new NPOI.HSSF.UserModel.HSSFWorkbook(_fs);//工作表
            IWorkbook _wk = WorkbookFactory.Create(_fs);
            _fs.Close();
            File.Delete(_fn_fs);
            int _sheet_cnt = _wk.NumberOfSheets; // 表数量
            // EXCEL中，第1个表，起始位置为第3行，第1列 plan_excel=1,3,1
            //string[] _arr = txtTableNo.Text.Split('|');
            //for (int _jj = 0; _jj < _arr.Length; _jj++) _arr[_jj] = _arr[_jj].Trim();
            // _sheet <== _wk <== _fs <== _fn
            int _sheet_idx = 0; // 表号 缺省为0
            List<string> _lst = new List<string>();
            for (_sheet_idx = 0; _sheet_idx < _sheet_cnt; _sheet_idx++)
            {
                ISheet _sheet = _wk.GetSheetAt(_sheet_idx);        // 获取当前表 _sheet
                int _start_row = 0; // 表的起始读行号 ;
                int _start_col = 0;// 表的起始读列号 ;
                int _row_cnt = _sheet.LastRowNum; // 表的总行数
                // 定位读  _sheet.GetRow(2).GetCell(1)
                #region // read
                for (int _i = _start_row; _i < _row_cnt + 1; _i++)   // 遍历当前表 每一行                     
                {
                    // _row <== _sheet <== _wk <== _fs <== _fn
                    IRow _row = _sheet.GetRow(_i);     // 获取当前行    _row  
                    if (_row == null) continue;
                    string _str = "";
                    int _col_cnt = _row.LastCellNum; // 此行最后一个cell列号
                    for (int _j = _start_col; _j < _col_cnt + 1; _j++)    // 遍历每一列  
                    {
                        // _cell 〈== _row <== _sheet <== _wk <== _fs <== _fn
                        ICell _cell = _row.GetCell(_j);  // 获取当前网格  cell
                        string _val = "";
                        if (_cell != null)
                        {
                            if (_cell.CellType == CellType.Numeric)
                                _val = _cell.NumericCellValue.ToString();
                            else if (_cell.CellType == CellType.String)
                                _val = _cell.StringCellValue;
                        }
                        if (_j == _start_col)
                            _str = _val;
                        else
                            _str = _str + vFlagKey + _val;
                    }
                    // 为空
                    if (string.IsNullOrEmpty(_str)) continue;
                    if (_str.Trim().Length < 2) continue;
                    _lst.Add(_str);
                }
                #endregion
                if (!allSheets) break;
            }
            #endregion
            return _lst;
        }
        public static int DictVal<T>(Dictionary<T, int> vDict, T vKey)
        {
            if (vDict.ContainsKey(vKey))
                return vDict[vKey];
            else
                return -1;
        }
        public static string DictVal<T>(Dictionary<T, string> vDict, T vKey)
        {
            if (vDict.ContainsKey(vKey))
                return vDict[vKey].ToString();
            else
                return null;
        }
        //public static string DictVal(Dictionary<string, string> vDict, string vKey)
        //{
        //    if (vDict.ContainsKey(vKey))
        //        return vDict[vKey];
        //    else
        //        return null;
        //}
        //public static string DictVal(Dictionary<int, string> vDict, int vKey)
        //{
        //    if (vDict.ContainsKey(vKey))
        //        return vDict[vKey];
        //    else
        //        return null;
        //}
        //public static int DictVal(Dictionary<string, int> vDict, string vKey)
        //{
        //    if (vDict.ContainsKey(vKey))
        //        return vDict[vKey];
        //    else
        //        return -1;
        //}
        //public static int DictVal(Dictionary<int, int> vDict, int vKey)
        //{
        //    if (vDict.ContainsKey(vKey))
        //        return vDict[vKey];
        //    else
        //        return -1;
        //}
        public static bool EnumIsDefined<T>(string vName)
        {
            //Enum.IsDefined(T, V);
            //string[] _names = Enum.GetNames(typeof(eDev485));
            return Enum.IsDefined(typeof(T), vName);
        }
        static T getEnumName<T>(string vName)
        {
            if (EnumIsDefined<T>(vName))
                return (T)Enum.Parse(typeof(T), vName);
            return default(T);
        }
        static void doGetFullFileName(ref string vFn, string vPath = null)
        {
            string _fn = vFn;
            if (!vFn.Contains(":"))
                if (string.IsNullOrEmpty(vPath))
                    _fn = AppDomain.CurrentDomain.BaseDirectory + vFn;
                else
                    _fn = vPath + vFn;
            _fn = _fn.Replace(".csv", ".xls");
            if (!File.Exists(_fn))
                _fn = _fn.Replace(".xls", ".csv");
            vFn = _fn;
        }
        static void RemoveFileReadOnly(string fn)
        {
            if (!File.Exists(fn)) return;
            FileInfo fi = new FileInfo(fn);
            if ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                fi.Attributes = fi.Attributes & ~FileAttributes.ReadOnly;
        }
        static int doCRC(string[] vbuf)
        {
            int _crc = 0;
            int _i;
            int _t;
            int _ret = 0;
            _crc = 0xFFFF;
            for (_i = 0; _i < vbuf.Length; _i++)
            {
                _crc = _crc ^ Convert.ToInt32(vbuf[_i], 16);
                _crc = _crc & 0xFFFF;
                for (_t = 1; _t < 9; _t++)
                {
                    if ((_crc & 0x1) == 0x1)
                    {
                        _crc = (_crc / 2) ^ 0xA001;
                    }
                    else
                    {
                        _crc = (_crc / 2);
                    }
                    _crc = _crc & 0xFFFF;
                }
            }
            int _a = _crc & 0xFF;
            int _b = _crc & 65280;
            _ret = _a * 0x100 + _b / 0x100;
            return _ret;
        }
    }
}
