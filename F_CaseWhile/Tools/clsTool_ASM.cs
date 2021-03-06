﻿#region //
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
#endregion
namespace F_CaseWhile
{
    #region //
    using FACC;
    using F_Entitys;
    using F_Interface;
    using F_Entitys_DAL;
    using Faim3_Drivers;
    #endregion
    public class clsTool_ASM
    {
        public static void Init_DictFlow_Dev(ref clsFaim3 _faim3, ref DAL_CommData _dao_comm)
        {
            //'0.isEnable	 1.devNo	  2.Name	 3.cardType	4.card_num	5.port	6.mode	7.BoardID	8.isDebug	9.TryConn	10.thDelay
            // PCI_7432 TCP PCI_7230 204C    RS485 RS232
            //int _idev_debug50 = 50;
            foreach (var item in _faim3.dict_DevCards)
            {
                int _devNo = item.Key;
                if (item.Value.isEnable < 1) continue; // 
                object _dev1_io = null;  // 1设备
                object _dog2_io = null;  // 2看门狗
                object _flow_3 = null;   // 3流程
                string _Name = item.Value.Name.Trim();   // 按类别处理
                switch (_Name)
                {
                    // 如有流程则会自动打开设备
                    case "RS485":
                        _dev1_io = new cls485(_faim3, _dao_comm, _devNo);
                        _dog2_io = new clsDog485(_faim3, _dao_comm, _devNo);
                        if (item.Value.thDelay > 0)    // 有流程延时参数
                            _flow_3 = new clsWhile485(_faim3, _dao_comm, _devNo.ToString());
                        break;
                    case "RS232":
                        _dev1_io = new clsSP(_faim3, _dao_comm, _devNo);
                        _dog2_io = new clsDogSP(_faim3, _dao_comm, _devNo);
                        break;
                    case "TCP":
                        _dev1_io = new clsTCP(_faim3, _dao_comm, _devNo);
                        _dog2_io = new clsDogTcp(_faim3, _dao_comm, _devNo);
                        if (item.Value.thDelay > 0)    // 有流程延时参数 
                            _flow_3 = new clsWhileTcp(_faim3, _dao_comm, _devNo.ToString());
                        break;
                    case "PCI_7230":
                        _dev1_io = new cls7230(_faim3, _dao_comm, _devNo);
                        _dog2_io = new clsDog72x(_faim3, _dao_comm, _devNo);
                        if (item.Value.thDelay > 0)    // 有流程延时参数
                            _flow_3 = new clsWhile74x(_faim3, _dao_comm, _devNo.ToString());
                        break;
                    case "PCI_7432":
                        _dev1_io = new cls7432(_faim3, _dao_comm, _devNo);
                        _dog2_io = new clsDog72x(_faim3, _dao_comm, _devNo);
                        if (item.Value.thDelay > 0)    // 有流程延时参数
                            _flow_3 = new clsWhile74x(_faim3, _dao_comm, _devNo.ToString());
                        break;
                    case "204C":
                        _dev1_io = new cls204C(_faim3, _dao_comm, _devNo);
                        _dog2_io = new clsDog204C(_faim3, _dao_comm, _devNo);
                        if (item.Value.thDelay > 0)    // 有流程延时参数
                            _flow_3 = new clsWhile204(_faim3, _dao_comm, _devNo.ToString());
                        break;
                }
                if (_dev1_io != null)
                {
                    // dict_DevIo 设备字典
                    if (!_dao_comm.dict_DevIo.ContainsKey(_devNo.ToString()))
                        _dao_comm.dict_DevIo.Add(_devNo.ToString(), (IFaimIO)_dev1_io);
                }
                if (_dog2_io != null)
                {
                    //dict_Flow 看门狗 流程字典
                    if (!_faim3.dict_Flow.ContainsKey("d_" + item.Value.devID)) // 使用设备ID，而不是设备号
                    {
                        clsFlow _en = new clsFlow();
                        _en.FlowName = "d_" + item.Value.devID;
                        _en.Remark = "d_" + item.Value.devID;
                        _en.Delay = item.Value.dogDelay;
                        _faim3.dict_Flow.Add(_en.FlowName, _en); // 加入到流程字典中
                    }
                }
                // 重复
                if (_flow_3 != null)
                {
                    // dict_Flow 流程字典
                    if (!_faim3.dict_Flow.ContainsKey(item.Value.devID))
                    {
                        clsFlow _en = new clsFlow();
                        _en.FlowName = item.Value.devID;  //  使用的是设备ID
                        _en.Remark = item.Value.devID;
                        _en.Delay = item.Value.thDelay;
                        _faim3.dict_Flow.Add(_en.FlowName, _en); // 加入到流程中
                    }
                }
            }
        }
        // 产生按钮流程及脚本
        public static void doCreateButton(ref clsFaim3 _faim3, ref DAL_CommData _dao_comm, string[] Lines)
        {
            string _btnName = "DAL_Buttons";
            #region // clsDevTestBits
            List<clsDevTestBits> _lst = _faim3.dict_DevTestBits[_btnName];
            if (_lst != null)  // 
            {
                int i = 0;
                for (i = 0; i < Lines.Length; i++)
                {
                    string _cmd = Lines[i].Trim();
                    if (string.IsNullOrEmpty(_cmd)) continue;

                    if (_cmd.IndexOf(',') < 0)
                    {
                        _cmd = _cmd.Replace(" ", ",");
                        _cmd = _cmd.Replace(",,,", ",,"); // 不存在3个,
                    }

                    if (_cmd.IndexOf(',') > -1)
                    {
                        // 有, 按, 处理, 忽略所有的空格
                        string[] _arr_2 = _cmd.Split(',');
                        if (_arr_2.Length > 0)
                        {
                            if (_arr_2.Length > 0) _lst[i].IfType = _arr_2[0].Trim();//  "O U T";
                            if (_arr_2.Length > 1)
                                _lst[i].vName = _arr_2[1].Trim();// "1DO_9";
                            else
                                _lst[i].vName = "";
                            if (_arr_2.Length > 2)
                                _lst[i].HL = _arr_2[2].Trim();// "1";
                            else
                                _lst[i].HL = "";
                            if (_arr_2.Length > 3)
                                _lst[i].Reset = _arr_2[3].Trim();// "1";
                            else
                                _lst[i].Reset = "";
                        }
                    }
                    else
                    {
                        string[] _arr_1 = _cmd.Split(' ');
                        if (_arr_1.Length > 0)
                        {
                            if (_arr_1.Length > 0)
                                _lst[i].IfType = _arr_1[0].Trim();//  "O U T";

                            if (_arr_1.Length > 1)
                                _lst[i].vName = _arr_1[1].Trim();// "1DO_9";
                            else
                                _lst[i].vName = "";

                            if (_arr_1.Length > 2)
                                _lst[i].HL = _arr_1[2].Trim();// "1";
                            else
                                _lst[i].HL = "";

                            if (_arr_1.Length > 3)
                                _lst[i].Reset = _arr_1[3].Trim();// "1";
                            else
                                _lst[i].Reset = "";
                        }
                    }
                }
                for (int j = i; j < _lst.Count; j++)
                {
                    _lst[j].IfType = "NOP";//  "NOP";
                    _lst[j].vName = "0";// "1DO_9";
                    _lst[j].HL = "";// "1";
                    _lst[j].Reset = "";// "1";
                }
            }
            #endregion
            if (string.IsNullOrEmpty(_btnName)) return;
            if (!_faim3.dict_Threads.ContainsKey(_btnName))
            {
                clsFlow _flow = _faim3.dict_Flow[_btnName];
                if (_flow.nextCase.ToUpper() != "FREE")// 已打开过

                    clsTool_ASM.doCreateInstance(_faim3, _dao_comm, _btnName);
            }
        }
        public static void doCreateInstance(clsFaim3 v_faim3, DAL_CommData v_dao_comm, string vflowName)
        {
            string _fn = F_Const.path_flowDLL + vflowName + ".dll";
            string _ns = "F_CaseWhile";
            object _obj = null;
            if (vflowName == "clsWhile485" || vflowName == "clsWhile74x" || vflowName == "clsWhileTcp")
            {
                // 命名空间 / 类名 / faim3 / dao / new(形参：用户类名)
                _obj = doCreateInstance_byName_Para<object>(
                    _ns,
                    vflowName,
                    v_faim3,
                    v_dao_comm,
                    vflowName   // 
                    );
            }
            else
            {
                #region // flow . do while
                // 命名空间 / 类名 / faim3 / dao / new(形参：用户类名)
                _obj = doCreateInstance_byName_Para<object>(
                    _ns,
                    "clsFlowWhile",
                    v_faim3,
                    v_dao_comm,
                    vflowName  // xxx
                    );
                if (_obj == null)
                {
                    FACC.F_Log.Debug_1("clsTool_ASM", string.Format("！！01 未创建对象 :{0}", vflowName));
                    return;
                }
                FACC.F_Log.Debug_1("clsTool_ASM", string.Format("加入流程:{0}", vflowName));
                // PartA
                IDAL_Temp_Part _Idal = null;
                _obj = doCreateInstance_byFile<object>(
                    _fn,
                    vflowName,
                    "PartA"
                    );
                _Idal = _obj as IDAL_Temp_Part;
                _Idal.do_New(v_faim3, v_dao_comm);
                // PartB
                FACC.F_Log.Debug_1("clsTool_ASM", string.Format("加入步骤: {0}_PartA", vflowName));
                _obj = doCreateInstance_byFile<object>(
                    _fn, vflowName,
                    "PartB"
                    );
                _Idal = _obj as IDAL_Temp_Part;
                _Idal.do_New(v_faim3, v_dao_comm);
                #endregion }
            }
            FACC.F_Log.Debug_1("clsTool_ASM", string.Format("按钮: 加入步骤: {0}_PartB", vflowName));
        }
        // from : file  命名空间 / 类名 / faim3 / dao / new(形参：用户类名)
        static T doCreateInstance_byName_Para<T>(string nsName, string clsName, clsFaim3 v_faim3, DAL_CommData v_dao_comm, string userClassName)
        {
            string _name = nsName + "." + clsName;
            return do_CreateInstance_byName_Para<T>(_name, v_faim3, v_dao_comm, userClassName);
        }
        // 文件名 / 命名空间 / 类名
        static T doCreateInstance_byFile<T>(string vFn, string nsName, string clsName)
        {
            string _fn = vFn;
            _fn = do_GetFullFileName(_fn);
            if (!File.Exists(vFn))
            {
                F_Log.Debug_1("clsTool_ASM", string.Format("--->>>> 02 不存在 文件_{0}", vFn));
                return default(T);
            }
            string _name = nsName + "." + clsName;
            return do_CreateInstance_byFile<T>(_name, _fn);
        }
        static string do_GetFullFileName(string vFn)
        {
            string _rec = vFn;
            if (!vFn.Contains(":"))
                _rec = AppDomain.CurrentDomain.BaseDirectory + vFn;
            if (!File.Exists(vFn))
            {
                F_Log.Debug_1("clsTool_ASM", string.Format("--->>>> 03 不存在 文件_{0}", vFn));
                return null;
            }
            return _rec;
        }
        // from : file  命名空间.类名 / faim3 / dao / new(形参：用户类名)
        static T do_CreateInstance_byName_Para<T>(string vName, clsFaim3 v_faim3, DAL_CommData v_dao_comm, string userClassName)
        {
            object obj = null;
            Type vT = Type.GetType(vName);
            obj = Activator.CreateInstance(
                                vT,
                                new object[] { v_faim3, v_dao_comm, userClassName });
            T _en = (T)obj;//
            if (_en == null)
                F_Log.Debug_1("clsTool_ASM", string.Format("--->>>> 04 未创建对象_{0} {1}", vName, userClassName));
            else
                F_Log.Debug_1("clsTool_ASM", string.Format("成功创建对象_{0}", _en.GetType().Name));
            return _en;
        }
        // 文件名.命名空间 / 类名
        static T do_CreateInstance_byFile<T>(string vName, string vFn)
        {
            if (!File.Exists(vFn))
            {
                F_Log.Debug_1("clsTool_ASM", string.Format("--->>>> 05 文件不存在_{0}{1}", vName, vFn));
                return default(T);
            }
            byte[] _buf = File.ReadAllBytes(vFn);
            Assembly _asm = Assembly.Load(_buf);
            object obj = null;
            obj = _asm.CreateInstance(vName, false);
            T _en = (T)obj;//
            if (_en == null)
            {
                F_Log.Debug_1("clsTool_ASM", string.Format("--->>>> 06 未创建对象_{0}{1}", vName, vFn));
                System.Windows.Forms.MessageBox.Show(string.Format("--->>>> 06 未创建对象_{0}{1}", vName, vFn));
            }
            //else
            //    F_Log.Debug_1("clsTool_ASM", string.Format("成功创建对象_{0}", _en.GetType().Name));
            return _en;
        }
    }
}
//static void doit()
//{
//    //获取类

//    Assembly _asm0 = Assembly.LoadFrom("");
//    Type _type = _asm0.GetType("nsName.clsName", true, true); //命名空间 + 类名
//    //获取模块
//    Module[] modules = _asm0.GetModules();
//    foreach (Module module in modules)
//    {
//        Console.WriteLine("模块 name:" + module.Name);
//    }
//    //创建类的实例
//    object obj = Activator.CreateInstance(_type, true);
//    //获取私有字段
//    FieldInfo[] myfields = _type.GetFields(BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);
//    for (int i = 0; i < myfields.Length; i++)
//    {
//        Console.WriteLine("字段名：{0},类型：{1}", myfields[i].Name, myfields[i].FieldType);
//    }
//    //获取公共属性

//    PropertyInfo[] Propertys = _type.GetProperties();
//    for (int i = 0; i < Propertys.Length; i++)
//    {
//        // Propertys[i].SetValue(Propertys[i], i, null); //设置值

//        // Propertys[i].GetValue(Propertys[i],null); //获取值

//        Console.WriteLine("属性名：{0},类型：{1}", Propertys[i].Name, Propertys[i].PropertyType);
//    }
//    //调用静态方法

//    int result = (int)_type.InvokeMember("StaticPlus", BindingFlags.InvokeMethod, null, null, new object[] { 2, 3 });
//    Console.WriteLine("调用静态方法-结果是：{0}", result);
//    //调用非静态方法

//    result = (int)_type.InvokeMember("Plus", BindingFlags.InvokeMethod, null, obj, new object[] { 3, 4 });
//    Console.WriteLine("调用非静态方法-结果是：{0}", result);
//    EventInfo[] Myevents = _type.GetEvents();
//    foreach (EventInfo einfo in Myevents)
//    {
//        Console.WriteLine("事件：{0}", einfo.Name);
//    }
//    Type type = Type.GetType("System.String");
//    object[] parameters = new object[1];
//    char[] lpChar = { 't', 'e', 's', 't' };
//    parameters[0] = lpChar;
//    object _obj = type.Assembly.CreateInstance("ReflectionTest.Test", true, System.Reflection.BindingFlags.Default, null, parameters, null, null);
//    //http://blog.csdn.net/pukuimin1226/article/details/7773026
//}
//static Assembly NewAssembly()
//{
//    //创建编译器实例。  
//    CSharpCodeProvider provider = new CSharpCodeProvider();
//    //设置编译参数。  
//    CompilerParameters cp = new CompilerParameters();
//    cp.GenerateExecutable = false;
//    cp.GenerateInMemory = true;
//    // Generate an executable instead of 
//    // a class library.
//    //cp.GenerateExecutable = true;
//    // Set the assembly file name to generate.
//    cp.OutputAssembly = "c:\\1.dll";
//    // Generate debug information.
//    cp.IncludeDebugInformation = true;
//    // Save the assembly as a physical file.
//    cp.GenerateInMemory = false;
//    // Set the level at which the compiler 
//    // should start displaying warnings.
//    cp.WarningLevel = 3;
//    // Set whether to treat all warnings as errors.
//    cp.TreatWarningsAsErrors = false;
//    // Set compiler argument to optimize output.
//    cp.CompilerOptions = "/optimize";
//    cp.ReferencedAssemblies.Add("System.dll");
//    //cp.ReferencedAssemblies.Add("System.Core.dll");
//    cp.ReferencedAssemblies.Add("System.Data.dll");
//    //cp.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
//    cp.ReferencedAssemblies.Add("System.Deployment.dll");
//    cp.ReferencedAssemblies.Add("System.Design.dll");
//    cp.ReferencedAssemblies.Add("System.Drawing.dll");
//    cp.ReferencedAssemblies.Add("System.Windows.Forms.dll");
//    //创建动态代码。  
//    StringBuilder classSource = new StringBuilder();
//    classSource.Append("using System;using System.Windows.Forms;\npublic  class  DynamicClass: UserControl \n");
//    classSource.Append("{\n");
//    classSource.Append("public DynamicClass()\n{\nInitializeComponent();\nConsole.WriteLine(\"hello\");}\n");
//    classSource.Append("private System.ComponentModel.IContainer components = null;\nprotected override void Dispose(bool disposing)\n{\n");
//    classSource.Append("if (disposing && (components != null)){components.Dispose();}base.Dispose(disposing);\n}\n");
//    classSource.Append("private void InitializeComponent(){\nthis.SuspendLayout();this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);");
//    classSource.Append("this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;this.Name = \"DynamicClass\";this.Size = new System.Drawing.Size(112, 74);this.ResumeLayout(false);\n}");
//    //创建属性。  
//    /*************************在这里改成需要的属性******************************/
//    classSource.Append(propertyString("aaa"));
//    classSource.Append(propertyString("bbb"));
//    classSource.Append(propertyString("ccc"));
//    classSource.Append("}");
//    System.Diagnostics.Debug.WriteLine(classSource.ToString());
//    //编译代码。  
//    CompilerResults result = provider.CompileAssemblyFromSource(cp, classSource.ToString());
//    if (result.Errors.Count > 0)
//    {
//        for (int i = 0; i < result.Errors.Count; i++)
//            Console.WriteLine(result.Errors[i]);
//        Console.WriteLine("error");
//        return null;
//    }
//    //获取编译后的程序集。  
//    Assembly assembly = result.CompiledAssembly;
//    return assembly;
//}
//static string propertyString(string propertyName)
//{
//    StringBuilder sbProperty = new StringBuilder();
//    sbProperty.Append(" private  int  _" + propertyName + "  =  0;\n");
//    sbProperty.Append(" public  int  " + "" + propertyName + "\n");
//    sbProperty.Append(" {\n");
//    sbProperty.Append(" get{  return  _" + propertyName + ";}  \n");
//    sbProperty.Append(" set{  _" + propertyName + "  =  value;  }\n");
//    sbProperty.Append(" }");
//    return sbProperty.ToString();
//}