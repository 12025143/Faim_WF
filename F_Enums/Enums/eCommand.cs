
using System;
namespace F_Enums
{
    //  J 协议 步号
    [Serializable]
    public enum eCommand
    {
        Free = -1,
         XY=0, //XY轴移动
         XA=1, //X轴世界坐标模式移动
         XR=2, //关节1移动
         YA=3, //Y轴世界坐标模式移动         YR=4, //关节2移动
         ZA=5, //Z轴世界坐标模式移动         ZR=6, //关节3移动
         UA=7, //U轴世界坐标模式移动         UR=8, //关节4移动
        VA=9, //V轴世界坐标模式移动
        VR=10,  //关节5移动
        WA=11, //W轴世界坐标模式移动
        WR=12, //关节6移动
        Point=13, //点移动    
    }
}
 