using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_Enums
{
    // 时刻类别
    public enum eTimeType
    {
        Free = -1,
        pcBoot, // 电脑开机 
        sysBoot,  // 系统开机 
        softBoot  // 软件系统开启 
    }
}
