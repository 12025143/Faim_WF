
using System;
namespace F_Enums
{
    //2122 应用类
    [Serializable]
    public enum eActionType
    {
        Free = -1, // 
        Feed, // 进料
        Out, // 出料
        Down, // 下降
        Up, // 上升
        Pickup, // 取料
        Plugin, // 插件
    }
}
