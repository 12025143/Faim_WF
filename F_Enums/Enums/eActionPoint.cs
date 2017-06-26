
using System;
namespace F_Enums
{
    // 1101 点别
    [Serializable]
    public enum eActionPoint
    {
        Free = -1, // 
        Cap, // 拍照点
        NG, // 抛料点
        Pickup, // 取料点
        Plugin, // 插件点
    }
}
