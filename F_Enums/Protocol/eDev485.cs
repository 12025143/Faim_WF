using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_Enums
{
    // 枚举: 命令名称
    public enum eDev485
    {
        Free = -1,
        home_Set_Pos = 12,
        home_Set_PosA = 13,
        MotorOn = 5,
        MotorOff = 6,
        MotorAlarmReset = 7,
        Position_A_Move = 3,
        Position_R_Move = 4,
        ReadStatus = 1,
        ReadPOS = 2,
        StopMove = 137,
    }
}
