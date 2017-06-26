using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_Enums
{
    public enum eEpsonStatus
    {
        Free = -1,
        Teach = 0,
        Auto,
        Warning,
        SError,
        Safeguard,
        EStop,
        Error,
        Paused,
        Running,
        Ready,
    }
}
