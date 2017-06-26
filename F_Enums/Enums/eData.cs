 
using System;
namespace F_Enums
{
    [Serializable]
    public enum eData
    {
        Free = -1,
        Board, // 基板库
        Claw ,  //夹爪库
        Elem ,  //元件库
        ProModel, //生产型号库
        Product , // 生产总数据
        RobotVel, //机械手速度
    }
}
