using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace F_Protocols
{
    using System.Reflection;
    using F_Entitys;
    using F_Enums;
    public class clsRobot_J
    {
        protected string v00_Command { get; set; }
        public eMode_J v01_Mode { get; set; }
        public int v02_Move_Tool { get; set; }
        public string v13_Dir { get; set; }
        public string v14_Step_val { get; set; }
        public string v23_XStep_val { get; set; }
        public string v24_YStep_val { get; set; }
        public List<double> v33_iDistance { get; set; }
        public List<int> v34_iJX { get; set; }
        public string v35_Hand { get; set; }
        public clsRobot_J()
        {
            v00_Command = "J";
            v01_Mode = eMode_J.Free;
            v02_Move_Tool = 0;
            v13_Dir = "";
            v14_Step_val = "";
            v33_iDistance = new List<double>();
            v34_iJX = new List<int>();
            for (int i = 0; i < 6; i++) v33_iDistance.Add(0);
            for (int i = 0; i < 4; i++) v34_iJX.Add(0);
        }
        string Out_J()
        {
            clsRobot_J _en = new clsRobot_J();
            _en.v01_Mode = eMode_J.UA;
            return  _en.ToString();
        }
        public override string ToString()
        {
            //return base.ToString();
            string _res = "";
            if (eMode_J.Stop == v01_Mode)
            {
                _res = "J Stop 0 0 0";
            }
            else if (!string.IsNullOrEmpty(v13_Dir))
            {
                _res = string.Format("{0} {1} {2} {3}{4}",
                                    v00_Command, v01_Mode.ToString(), v02_Move_Tool,
                                    v13_Dir, v14_Step_val);
            }
            else if (!string.IsNullOrEmpty(v23_XStep_val))
            {
                _res = string.Format("{0} {1} {2} {3} {4}",
                                    v00_Command, v01_Mode.ToString(), v02_Move_Tool,
                                    v23_XStep_val, v24_YStep_val);
            }
            else if (!string.IsNullOrEmpty(v35_Hand))
            {
                if ((v33_iDistance.Count != 6) || (v34_iJX.Count != 4)) return "";
                string _33 = v33_iDistance[0].ToString();
                for (int i = 0; i < v33_iDistance.Count ; i++)
                {
                    _33 = _33 + " " + v33_iDistance[i].ToString();
                    
                }
                string _34 = v34_iJX[0].ToString();
                for (int i = 0; i < v34_iJX.Count; i++)
                {
                    _34 = _34 + " " + v34_iJX[i].ToString();
                }
                _res = string.Format("{0} {1} {2} {3} {4} {5}",
                                    v00_Command, v01_Mode.ToString(), v02_Move_Tool,
                                    _33, _34, v35_Hand);
            }
            else
            { }
            return _res;
        }
    }
}
