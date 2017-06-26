using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_Enums
{
    // 命令
    public enum eDevRobot
    {
        Free = -1,
        Abort = 0,   //  = #Abort
        Continue,    //  = #Continue,0
        Home,        //  = #Home
        Login,       //  = #Login,0
        Logout,      //  = #Logout
        Pause,       //  = #Pause
        Reset,       //  = #Reset,0
        SetMotorsOn, //  = #SetMotorsOn,0
        SetMotorsOff,//  = #SetMotorsOff,0
        Start,       //  = #Start,0
        Start0,      // 
        Stop,        //  = #Stop,0
        Stop0,   
        Execute,
        GetIO,//
        SetIO,//
        GetIOByte0,//
        GetIOByte,//
        SetIOByte,//
        GetIOMemWord,//
        GetIOWord,//
        SetIOWord,//
        GetMemIO,//
        SetMemIO,//
        GetMemIOByte,//
        SetMemIOByte,//
        GetStatus,//
        GetVariable,//
        SetVariable,//
    }
}
