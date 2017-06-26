using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_Enums
{
    // 流程中步骤跳转的类别
    public enum  eCaseFlag
    {
        Free = -1,
        Next, // 正常的下一步
        TT, // 测试步骤
        Goto,// 绝对跳转
        NG,// 测试后否定跳转
        Code,//代码处理
        Exist, // 不存在
    }
}
