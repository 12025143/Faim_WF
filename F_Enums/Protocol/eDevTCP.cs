using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_Enums
{
    // 协议
    public enum eDevTCP
    {
        Free = -1,
        BiaoDi_M = 0,// 顶针库托盘3个端点
        CJ_,        // 插件
        Data_,      // 其它
        Data1_,     // 九点
        DetPin_,    // 	
        FetPin_,    // 顶针点
        GD_C,       // 过渡点
        GD_M,       // 	
        GD_NG,      //	
        GD_Q,       // 	
        Mark_,      // MARK
        NG_,        // NG
        OtherData,  // 其它
        PinNG_,     // 顶针点
        PinPaw_,    // 顶针点
        PZ_,        // 拍照
        QL_,        // 取料点
        Re_,        // 抛料、回收
    }
}
