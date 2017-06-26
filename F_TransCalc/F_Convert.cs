#region // ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace FACC
{
    using System.Runtime.InteropServices;
    public class F_Convert
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = true)]
        static extern void CopyMemory(IntPtr Destination, IntPtr Source, uint Length);

        // 小端模式值：{ 1, 2, 3, 4 }= x1=67305985=0x04030201 Little-Endian
        static string _doBytesToStr1(byte[] vBuf)
        {
            //string _res = BitConverter.ToString(vBuf);
            //_res = _res.Replace("-", "");
            // BitConverter.ToInt32({d1,89,fd,0a}, 0); ==> d189fd0a
            return BitConverter.ToString(vBuf).Replace("-", "");
        }
        // 大端模式值： { 1, 2, 3, 4 }= x2=16909060=0x01020304 Big-Endian
        static int _doBytesToStr2(byte[] vBuf)
        {
            //string _res = BitConverter.ToString(vBuf);
            //_res = _res.Replace("-", "");

            int _val = BitConverter.ToInt32(vBuf, 0); // ==> d189fd0a
            return _val;// IPAddress.NetworkToHostOrder(_val);
        }
        //public static byte[] doDecToBytes(int source, uint Length)
        //{
        //    byte[] Destination = new byte[Length];
        //    IntPtr hStr1 = Marshal.AllocHGlobal(source);
        //    IntPtr pArray = Marshal.UnsafeAddrOfPinnedArrayElement(Destination, 0);
        //    CopyMemory(pArray, hStr1, Length);
        //    return Destination;
        //}
        // 将字节码转为整数  [0]=01 [1]=00 ==> 256   [0]=00 [1]=FF ==> 255
        public Byte[] Reverse(Byte[] data)
        {
            Array.Reverse(data);
            return data;
        }  
        /*
         字节码转数值
         */
        public static int doBytesToDec(byte[] source, int Length = 0)
        {
            int Destination = 0;
            int _val = BitConverter.ToInt32(source, Length);
            //IntPtr hStr1 = Marshal.AllocHGlobal(Destination);
            //IntPtr pArray = Marshal.UnsafeAddrOfPinnedArrayElement(source, 0);
            //CopyMemory(hStr1, pArray, Length);
            return Destination;
        }
        public virtual Byte[] GetBytes(short value)
        {
            return BitConverter.GetBytes(value);
        }

        public virtual Byte[] GetBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }

        public virtual Byte[] GetBytes(float value)
        {
            return BitConverter.GetBytes(value);
        }

        public virtual Byte[] GetBytes(double value)
        {
            return BitConverter.GetBytes(value);
        }
        //
        public virtual short GetShort(byte[] data)
        {
            return BitConverter.ToInt16(data, 0);
        }

        public virtual int GetInt(byte[] data)
        {
            return BitConverter.ToInt32(data, 0);
        }

        public virtual float GetFloat(byte[] data)
        {
            return BitConverter.ToSingle(data, 0);
        }

        public virtual double GetDouble(byte[] data)
        {
            return BitConverter.ToDouble(data, 0);
        }  
    }
}
