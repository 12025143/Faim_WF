using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_Protocols
{
    using System.Reflection;
    using F_Entitys;
    using F_Enums;
    public class clsRobot_AZ
    {
        protected string v00_Command { get; set; }
        public eMode_AZ v01_Mode { get; set; }
        public List<int> v02_List { get; set; }
        clsFaim3 _faim3 = null;
        clsRobot_AZ() { }
        public clsRobot_AZ(clsFaim3 v_faim3)
        {
            _faim3 = v_faim3;
            v02_List = new List<int>();
            v01_Mode = eMode_AZ.Free;
        }
        public override string ToString()
        {
            //return base.ToString();
            string _res = "";
            if (v02_List.Count < 1) return "";
            if (eMode_AZ.Free == v01_Mode)
            {
                _res = string.Format("{0} {1}",
                                    v00_Command,  v02_List);
            }
            else
            {
                string _01 = v01_Mode.ToString().Replace("_","");
                string _02 = v02_List[0].ToString();
                for (int i = 0; i < v02_List.Count; i++)
                {
                    _02 = _02 + " " + v02_List[i].ToString();
                }
                _res = string.Format("{0} {1} {2}",
                                    v00_Command, _01, _02);
            }
            return _res;
        }
        string Out_AZ()
        {
            clsRobot_AZ _en = new clsRobot_AZ();
            _en.v00_Command = "M";
            _en.v01_Mode = eMode_AZ._1;
            _en.v02_List.Add(2);
            return this.ToString(); // "M 1 2"
        }
    }
}
