#region // ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_Enums
{

    public enum eMessageStyle
    {
        Free = -1,  // 
        Nomal = 0,  // 正常
        Debug,      // 调试
        Info,       // 信息
        Warnning,   // 警告
        Error,      // 错误
        Falal,      // 致命
    }
}
