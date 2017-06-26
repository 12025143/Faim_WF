using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FACC
{
    using F_Entitys;
    using Microsoft.VisualBasic;
    using System.Reflection;
    using System.CodeDom.Compiler;
    public class F_MyDLL
    {
        Dictionary<string, string> _dictSubName = new Dictionary<string, string>(); // 指令集
        Dictionary<string, object> _dictFaim3 = new Dictionary<string, object>(); // 大数据映像
        object _obj_f3 = null;
        clsFaim3 _faim3 = null;
        public string ErrorMessage { get; set; }
        F_MyDLL() { }
        public F_MyDLL(ref clsFaim3 v_faim3)
        {
            _faim3 = v_faim3;
            ErrorMessage = "";
        }
        public object doGet_Value(string vMethodName, string vTxt, int returnType)
        {
            if (_dictSubName.ContainsKey(vMethodName)) vTxt = _dictSubName[vMethodName];
            if (vTxt.StartsWith("?"))
            {
               vTxt ="return " + vTxt.Substring(1);
            }
            if (_obj_f3 == null)
                if (vTxt.Contains(";"))
                    _obj_f3 = GetCS_Object_fromSource(vMethodName, vTxt, returnType);
                else
                    _obj_f3 = GetVB_Object_fromSource(vMethodName, vTxt, returnType);
            object _ret = _Invoke(_obj_f3, vMethodName);
            return _ret;
        }
        protected virtual object _Invoke(object obj, string vMethodName)
        {
            if (obj == null) return -1;
            // call <== _obj <== _asm <== _com <== paras
            MethodInfo _do = obj.GetType().GetMethod(vMethodName);
            return _do.Invoke(obj, null);
        }
        protected virtual object GetVB_Object_fromSource(string vMethodName, string vTxt, int returnType)
        {
            List<string> _refdll = new List<string>() { 
                                    "System.dll", 
                                    "F_Enums.dll", 
                                    "F_Entitys.dll", 
                                    "F_Entitys_DAL.dll", 
                                    "F_Const.dll", 
                                    "F_log.dll", 
                                    "System.Windows.Forms.dll" 
            };
            string _namespace = "nsTxtTemp";
            string _className = "clsTxtTemp";
            string _txtType = "";
            switch (returnType)
            {
                case 0:
                    _txtType = "     Public Function  " + vMethodName + "() As Integer  " + Environment.NewLine +
                               "         Dim __i As Integer = 0  " + Environment.NewLine +
                               "         " + vTxt + "  " + Environment.NewLine +
                               "         Return __i   " + Environment.NewLine +
                               "     End Function  " + Environment.NewLine;
                    break;
                case 1:
                    _txtType = "     Public Function  " + vMethodName + "() As String  " + Environment.NewLine +
                               "         Dim __i As Integer = 0  " + Environment.NewLine +
                               "         " + vTxt + "  " + Environment.NewLine +
                               "         Return __i.ToString   " + Environment.NewLine +
                               "     End Function  " + Environment.NewLine;
                    break;
                case 2:
                    _txtType = "     Public Function  " + vMethodName + "() As Boolean  " + Environment.NewLine +
                               "         Dim __i As Integer = 0  " + Environment.NewLine +
                               "         " + vTxt + "  " + Environment.NewLine +
                               "         Return __i = 0  " + Environment.NewLine +
                               "     End Function  " + Environment.NewLine;
                    break;
            }
            string _txt =
                "Imports System  " + Environment.NewLine +
                "Imports F_Entitys  " + Environment.NewLine +
                "Imports F_Entitys_DAL  " + Environment.NewLine +
                "Imports System.Windows.Forms  " + Environment.NewLine +
                "Namespace " + _namespace + " " + Environment.NewLine +
                "  Public Class " + _className + " " + Environment.NewLine +
                "     Dim _faim3 As clsFaim3  " + Environment.NewLine +
                "     Sub New()  " + Environment.NewLine +
                "     End Sub  " + Environment.NewLine +
                "     Sub New(ByVal v_faim3 as clsFaim3)  " + Environment.NewLine +
                "         _faim3 = v_faim3  " + Environment.NewLine +
                "     End Sub  " + Environment.NewLine +
                _txtType + Environment.NewLine +
                "  End Class  " + Environment.NewLine +
                "End Namespace ";
            return GetVB_Object_fromParas(_namespace + "." + _className, _txt, _refdll, _faim3);
        }
        protected virtual object GetCS_Object_fromSource(string vMethodName, string vTxt, int returnType)
        {
            List<string> _refdll = new List<string>() { 
                                    "System.dll", 
                                    "F_Enums.dll", 
                                    "F_Entitys.dll", 
                                    "F_Entitys_DAL.dll", 
                                    "F_Const.dll", 
                                    "F_log.dll", 
                                    "System.Windows.Forms.dll" 
            };
            string _namespace = "nsTxtTemp";
            string _className = "clsTxtTemp";
            string _txtType = "";
            switch (returnType)
            {
                case 0:
                    _txtType = "     public int " + vMethodName + "() " +
                               "     { " +
                               "         int __i = 1; " +
                               "         " + vTxt +
                               "         return __i; " +
                               "     } ";
                    break;
                case 1:
                    _txtType = "     public string " + vMethodName + "() " +
                               "     { " +
                               "         int __i = 2; " +
                               "         " + vTxt +
                               "         return __i.ToString(); " +
                               "     } ";
                    break;
                case 2:
                    _txtType = "     public bool " + vMethodName + "() " +
                               "     { " +
                               "         int __i = 0; " +
                               "         " + vTxt +
                               "         return __i == 0; " +
                               "     } ";
                    break;
            }
            string _txt =
                "namespace " + _namespace +
                "{ " +
                "  using System; " +
                "  using F_Entitys; " +
                "  using F_Entitys_DAL; " +
                "  using System.Windows.Forms; " +
                "  public class " + _className +
                "  { " +
                "     clsFaim3 _faim3 = null; " +
                "     public " + _className + "() " +
                "     { } " +
                "     public " + _className + "(clsFaim3 v_faim3) " +
                "     {" +
                "         _faim3 = v_faim3; " +
                "     }" +
                _txtType +
                "   } " +
                "} ";
            return GetCS_Object_fromParas(_namespace + "." + _className, _txt, _refdll, _faim3);
        }
        object GetVB_Object_fromParas(string vTypeName, string vTxt, List<string> _refdll, clsFaim3 v_faim3)
        {
            // paras
            CompilerParameters _asmParas = new CompilerParameters();
            _asmParas.GenerateExecutable = false;// true;
            //_asmParas.OutputAssembly = "abc";
            _asmParas.TreatWarningsAsErrors = false;
            foreach (var item in _refdll)
                _asmParas.ReferencedAssemblies.Add(item);
            VBCodeProvider _com = new VBCodeProvider();
            CompilerResults _CR = _com.CompileAssemblyFromSource(_asmParas, vTxt.ToString());
            return Get_Object_fromParas(vTypeName, vTxt, _CR, v_faim3);
        }
        object GetCS_Object_fromParas(string vTypeName, string vTxt, List<string> _refdll, clsFaim3 v_faim3)
        {
            // paras
            CompilerParameters _asmParas = new CompilerParameters();
            _asmParas.GenerateExecutable = false;
            _asmParas.GenerateInMemory = true;
            foreach (var item in _refdll)
                _asmParas.ReferencedAssemblies.Add(item);
            // _com <== paras
            //CodeDomProvider _com = new CodeDomProvider();
            CompilerResults _CR = CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromSource(_asmParas, vTxt);
            return Get_Object_fromParas(vTypeName, vTxt, _CR, v_faim3);
        }
        object Get_Object_fromParas(string vTypeName, string vTxt, CompilerResults _CR, clsFaim3 v_faim3)
        {
            object _obj = null;
            ErrorMessage = "";
            if (_CR.Errors.HasErrors)
            {
                Console.WriteLine("编译错误：");
                foreach (CompilerError _err in _CR.Errors)
                {
                    if (string.IsNullOrEmpty(ErrorMessage))
                        ErrorMessage = _err.ErrorText;
                    Console.WriteLine(_err.ErrorText);
                }
            }
            else
            {
                // _asm <== _com <== paras
                Assembly _asm = _CR.CompiledAssembly;
                // _obj <== _asm <== _com <== paras
                if (v_faim3 == null)
                    _obj = _asm.CreateInstance(vTypeName, false);
                else if (v_faim3._dim_dict.Count == 0)
                    _obj = _asm.CreateInstance(vTypeName, false);
                else
                    _obj = _asm.CreateInstance(vTypeName,
                                        false, BindingFlags.Default, null,
                                        new object[] { v_faim3 },
                                        null, null);
            }
            return _obj;
        }
    }
}
