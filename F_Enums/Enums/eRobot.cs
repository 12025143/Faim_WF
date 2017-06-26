using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace F_Enums
{
    // 机械手类别
    [Serializable]
    public enum eRobot
    {
        Free = -1,
        Four = 0,    // 4轴
        Six = 1, // 6轴 
    }
  
}