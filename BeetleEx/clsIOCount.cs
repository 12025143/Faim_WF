using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetleEx
{
    public class clsIOCount
    {
        public int Idx { get; set; }
        public int Length { get; set; }
        public int TotalLen { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}/{2}", Idx, Length, TotalLen);
            //return base.ToString();
        }
        public void Clear()
        {
            Idx = 0;
            Length = 0;
            TotalLen = 0;
        }
    }
}
