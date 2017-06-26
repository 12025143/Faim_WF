#region // ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_Enums
{
    // 状态 信号
    public enum eState
    {
        Free = -1,
        // 运动中
        Moving,
        // 到位
        Placed,
        // 报警中
        Alarmed,
        // 上
        Top,
        // 中
        Medi,
        // 下
        Bottom,
        // 要料
        Ready,
    }
}
