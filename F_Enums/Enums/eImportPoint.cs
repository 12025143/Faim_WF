
using System;
namespace F_Enums
{
    // 1034导入别
    [Serializable]
    public enum eImportPoint
    {
        Free = -1,
        Manual,
        CSV,
        DXF,
        Protell,
    }
    // 1032点位别
    [Serializable]
    public enum ePointType
    {
        Free = -1,
        Normal, // 插件点
        Mark,// Mark点
        BadMark,// BadMark
        BOC,// BOC 中心点
        Trans,// 过渡点
        Retain,// 保留, 暂不使用
    }
}
