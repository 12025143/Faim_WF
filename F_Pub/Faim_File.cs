using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace FACC
{
    public class Faim_File
    {
        public static string WriteAllText(string vFn, string _json)
        {
            if (File.Exists(vFn))
            {
                RemoveFileReadOnly(vFn);
            }
            File.WriteAllText(vFn, _json, Encoding.Default);
            return _json;
        }
        public static string ReadAllText(string vFn)
        {
            if (!File.Exists(vFn))
            {
                return null;
            }
            string _json = File.ReadAllText(vFn, Encoding.Default);
            return _json;
        }
        public static void RemoveFileReadOnly(string vFn)
        {
            if (!File.Exists(vFn))
                return;
            FileInfo fi = new FileInfo(vFn);
            if (((fi.Attributes | FileAttributes.ReadOnly) == FileAttributes.ReadOnly))
            {
                fi.Attributes = fi.Attributes & ~FileAttributes.ReadOnly;
            }
        }
    }
}
