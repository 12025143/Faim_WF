using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_Enums
{
    //流程状态
    public enum eWF_State
    {
        Free = -1,
        // 暂停，   线程仍在运行
        Break,      // 断点 暂停
        Emergency,  // 急停
        End,        // 流程结束
        JMP,        // 跳转
        Line,       // 单行 暂停
        Math,
        NGwait,     // NG   暂停
        Pause,      // 一般 暂停
        Prev,       // 预置 暂停
        Reset,      // 重置 暂停
        Running,    // 运行中
        Sleep,      //
        Step,       // 单步 暂停
        Send,
        Suspend,    // 挂起 暂停
        Test, 
        Wait,       // 空闲 暂停
    }
}
