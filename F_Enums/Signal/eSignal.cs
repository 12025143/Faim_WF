#region // ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_Enums
{
    // 供料器信号
    public enum eSignal
    {
        Free = -1,
        // 准备	 
        Ready,
        // 松开
        Release,
        // 夹紧
        Grip,
        // 取料
        Take,
    }
}
