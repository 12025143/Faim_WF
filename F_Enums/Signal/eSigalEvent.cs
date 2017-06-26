#region // ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_Enums
{
    public enum eSignalEvent
    {
        Disconnect = -200,
        Connect = -100,
        Free = -1,
        Alarm,      // 报警       R 红色  
        Alart,      // 
        Break,      // 断点 暂停
        Emergency,  // 急停
        Enabled,    // 有效无信号 W 白色 
        End,
        Error,      // 错误       R 红色 是 快
        Close,
        Click,      // 按下       Y 黄色 Out
        ClickDown,  // 非按下     W 常态 Out
        Connected,
        Current,    //  
        Disconnected,
        In,
        Line,       // 单行 暂停
        Math,
        Normal,     // 正常       G 绿色
        NG,
        NGwait,
        Off,
        On,
        Open,
        Out,
        Pass,       // 通过的     G 绿色
        Pause,
        Prev,       // 预置 暂停
        Pickup,     // 取件
        Plugin,     // 插件
        Read,
        Received,
        Reset,
        Running,
        Send,
        Sleep,      //
        Step,       // 步骤  
        Stop,  
        Test,   
        Timeout,   // 超时
        Wait,
        Write,
        Empty = 77,    // 空，无消息
    }
}
