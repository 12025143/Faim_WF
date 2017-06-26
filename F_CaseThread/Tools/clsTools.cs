using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F_FAIM_ThMana
{
    
    using System.IO;
    using System.Reflection;
    using FACC;
    using F_Entitys;
    using F_Interface;
    public class clsTools
    {

        static void doCreateInstance(clsFaim3 vFaim3, string vFlowName)
        {
            string _fn = F_Const.path_flowDLL + vFlowName + ".dll";
            object _obj = doCreateInstance<object>(
                _fn, "F_FlowTemp", "clsTempSub",
                vFaim3,
                vFlowName
                );
            if (_obj == null) return;
            FACC.F_Log.Debug_1("clsTool_ASM", string.Format("加入流程:{0}", vFlowName));

            IDAL_Temp_Part _Idal = null;
            _obj = doCreateInstance<object>(
                _fn, vFlowName,
                "PartA",
                vFaim3
                );
            _Idal = _obj as IDAL_Temp_Part;
            _Idal.do_New(vFaim3);
            FACC.F_Log.Debug_1("clsTool_ASM", string.Format("加入步骤: {0}_PartA", vFlowName));

            _obj = doCreateInstance<object>(
                _fn,   vFlowName,
                "PartB",
                vFaim3
                );
            _Idal = _obj as IDAL_Temp_Part;
            _Idal.do_New(vFaim3);

            FACC.F_Log.Debug_1("clsTool_ASM", string.Format("按钮: 加入步骤: {0}_PartB", vFlowName));
        }

        // from : file
        static T doCreateInstance<T>(
                            string vFn, string nsName, string clsName,
                            clsFaim3 _faim3, string userClassName="")
        {
            string _name = nsName + "." + clsName;

            string _fn = vFn;
            _fn = do_GetFullFileName(_fn);
            //if (!_fn.Contains("file://"))
            //    return default(T);



            return do_CreateInstance<T>(_fn, _name, _faim3, userClassName);
        }

        static string do_GetFullFileName(string vFn)
        {
            string _rec = vFn;
            if (!vFn.Contains(":"))
                _rec = AppDomain.CurrentDomain.BaseDirectory + vFn;

            if (!File.Exists(vFn))
            {
                F_Log.Debug_1("clsTool_ASM", string.Format("!! 不存在 文件_{0}", vFn));
                return null;
            }
            return _rec;
        }
        static T do_CreateInstance<T>(
                        string vFn, string vName,
                        clsFaim3 vFaim3, string userClassName)
        {
            if (!File.Exists(vFn))
                return default(T);
            byte[] _buf = File.ReadAllBytes(vFn);
            Assembly _asm = Assembly.Load(_buf);

            //Assembly _asm = Assembly.LoadFrom(vFile);
            object obj = null;
            if (string.IsNullOrEmpty(userClassName))
                obj = _asm.CreateInstance(vName, false);
            else
            {
                Type vT = Type.GetType(vName);
                obj = Activator.CreateInstance(
                                    vT,
                                    new object[] { vFaim3, userClassName });
            }
            T _en = (T)obj;//
            if (_en == null)
                F_Log.Debug_1("clsTool_ASM", string.Format("!! 未创建对象_{0}", vName));
            else
                F_Log.Debug_1("clsTool_ASM", string.Format("成功创建对象_{0}", _en.GetType().Name));

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