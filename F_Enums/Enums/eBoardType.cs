
using System;
namespace F_Enums
{
    // 1021板别
    [Serializable]
    public enum eBoardType
    {
        Free = -1,
        S = 0,//单板
        M = 1,//矩阵基板
        NM = 2,//非矩阵基板
    }
}
