#region // ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace F_Enums
{
    [Serializable]
    public enum eSaveType
    {
        Free = -1,
        New,    // 新建
        NoReplace // 不覆盖
    }
}
