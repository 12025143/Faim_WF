using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FACC
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Interop;
    using System.Windows;
    using System.Windows.Forms;
    using System.IO;
    using System.Threading;
    using System.Windows.Shapes;
    using System.Drawing.Drawing2D;
    using System.Net.NetworkInformation;
    using System.Security.Cryptography;
    // winState 0:hide 1:normal 3:最大化  6:最小化

    public class clsToolApp
    {
        Process _app = null;
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        public static bool _hasRunning(string _appName)
        {
            //string _appName = "F03_Case_Test";// System.Reflection.Assembly.GetExecutingAssembly().Location; // _appName = "..\release\F_ServerWin.exe"  
            bool _bl = false;
            Process _app = Process.GetCurrentProcess(); // _app.ProcessName
            Process[] _arr = Process.GetProcessesByName(_appName);
            for (int i = 0; i < _arr.Length; i++)
            {
                Process _item = _arr[i];
                if (_item.Id != _app.Id)
                {
                    DialogResult _dlg = System.Windows.Forms.MessageBox.Show("内存已有程序副本, 需要删除它吗 ！", "Dialog",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    if (_dlg == DialogResult.Yes)
                    {
                        clsToolApp._CloseExe(_item);  //SetForegroundWindow(_item.MainWindowHandle);
                        Thread.Sleep(500);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("程序不能运行两个及以上的副本。", "Dialog");
                        return true;
                    }
                }
            }
            return _bl;
        }
        public static void _CloseExe(Process App)
        {
            if (App == null) return;
            try
            {
                //if (App.CloseMainWindow()) return;
                App.Kill();
                App.Close();
            }
            catch (InvalidOperationException ex) // 进程(11816)已退出，因此无法处理请求。
            {
                Console.Write(ex.Message);
            }
            App = null;
        }
        static bool _AppIsRunning(string appName)
        {
            string _Name = appName.Replace(System.IO.Path.DirectorySeparatorChar, '_'); // 
            System.Threading.Mutex _mutex = new System.Threading.Mutex(false, _Name);
            bool _isRunning = !_mutex.WaitOne(0, false);
            return _isRunning;
        }
        static int _GetAppId()
        {
            Process _app = Process.GetCurrentProcess();
            foreach (Process _item in Process.GetProcessesByName(_app.ProcessName))
            {
                if (_item.Id != _app.Id)
                {
                    //SetForegroundWindow(_item.MainWindowHandle);
                    break;
                }
            }
            return _app.Id;
        }
        public void doOpenExe(Form _this, string exeName, eWinSizeState winState)
        {
            if (_app != null) return;
            _app = Process.Start(exeName);  //"notebook.exe"
            IntPtr _chWnd = _app.MainWindowHandle;
            while (_chWnd == IntPtr.Zero)
            {
                _chWnd = _app.MainWindowHandle;
            }
            if (_this != null)
            {
                IntPtr _phWnd = _this.Handle;
                SetParent(_chWnd, _phWnd);
            }
            ShowWindowAsync(_chWnd, (int)winState); // winState = 3
        }
        public void doOpenWpf(Window _this, string exeName, eWinSizeState winState)
        {
            if (_app != null) return;
            _app = Process.Start(exeName);  //"notebook.exe"
            IntPtr _chWnd = _app.MainWindowHandle;
            while (_chWnd == IntPtr.Zero)
            {
                _chWnd = _app.MainWindowHandle;
            }
            if (_this != null)
            {
                IntPtr _phWnd = new WindowInteropHelper(_this).Handle;
                SetParent(_chWnd, _phWnd);
            }
            ShowWindowAsync(_chWnd, (int)winState); // winState = 3
        }
        public void doCloseExe()
        {
            _CloseExe(_app);
        }

        //暂不用
        static bool Ping(string IP, string data = "")
        {
            Ping _Ping = new Ping(); //构造Ping实例
            PingReply reply = null;
            if (string.IsNullOrEmpty(data))
            {
                reply = _Ping.Send(IP);
            }
            else
            {
                PingOptions _PingOptions = new PingOptions();   //Ping 选项设置
                _PingOptions.DontFragment = true;
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;  //设置超时时间
                //调用同步 send 方法发送数据,将返回结果保存至PingReply实例
                reply = _Ping.Send(IP, timeout, buffer, _PingOptions);
            }
            return reply.Status == IPStatus.Success;
            //if (reply.Status == IPStatus.Success)
            //{
            //lst_PingResult.Items.Add("答复的主机地址：" + reply.Address.ToString());
            //lst_PingResult.Items.Add("往返时间：" + reply.RoundtripTime);
            //lst_PingResult.Items.Add("生存时间（TTL）：" + reply.Options.Ttl);
            //lst_PingResult.Items.Add("是否控制数据包的分段：" + reply.Options.DontFragment);
            //lst_PingResult.Items.Add("缓冲区大小：" + reply.Buffer.Length);
            //}
        }

        public static string _getFile_MD5(string vFn)
        {
            if (!File.Exists(vFn)) return "   ";
            try
            {   
                FileStream _fs = new FileStream(vFn, FileMode.Open);
                MD5 _md5 = new MD5CryptoServiceProvider();
                //MD5 _md5 = MD5.Create();
                byte[] _buf = _md5.ComputeHash(_fs);
                _fs.Close();

                StringBuilder _sb = new StringBuilder();
                for (int i = 0; i < _buf.Length; i++)
                {
                    _sb.Append(_buf[i].ToString("x2"));
                }
                return F_TransCalc.doBytesToStr(_buf);
            }
            catch (Exception ex)
            {
                throw new Exception("_getFile_MD5() :" + ex.Message);
            }
        }

    }
}
