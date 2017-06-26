using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[assembly: log4net.Config.XmlConfigurator(ConfigFile="log4net.config", Watch = true)]
namespace FACC
{
    using System.Diagnostics;
    using System.Reflection;
    using log4net;
    using System.IO;
    // \log\201607\...
    // 使用  FACC.Log.Debug1(
    //                this.GetType().Name, 
    //                Log.getMethodName() + "(" + 参数名 + ")");
    // <!--  OFF6 > FATAL5 > ERROR4 > WARN3 > INFO2 > DEBUG1  > ALL0 -->
    public class Log
    {
        // Debug1
        public static  void Debug_1(string name, string msg)
        {
            ILog _log = LogManager.GetLogger(name);
            _log.Debug(msg);
        }
        // Info2
        public static void Info_2(string name, string msg)
        {
            ILog _log = LogManager.GetLogger(name);
            _log.Info(msg);
        }
        // Warn3
        public static void Warn_3(string name, string msg)
        {
            ILog _log = LogManager.GetLogger(name);
            _log.Warn(msg);
        }
        // Error4
        public static void Error_4(string name, string msg)
        {
            ILog _log = LogManager.GetLogger(name);
            _log.Error(msg);
        }
        // Fatal5
        public static void Fatal_5(string name, string msg)
        {
            ILog _log = LogManager.GetLogger(name);
            _log.Fatal(msg);
        }
        // 使用  Log.WriteDebug(this.GetType().ToString(), Log.doGet_MethodName() + "(" + vPortName + ")");
        public static string doGet_MethodName()
        {
            var _method = new StackFrame(1).GetMethod();
            var _property = (
                from _p in _method.DeclaringType.GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.Static |
                    BindingFlags.Public |
                    BindingFlags.NonPublic)
                where _p.GetGetMethod(true) == _method || _p.GetSetMethod(true) == _method
                    select _p).FirstOrDefault();
            return _property == null ? _method.Name : _property.Name;
        }

    }
}
