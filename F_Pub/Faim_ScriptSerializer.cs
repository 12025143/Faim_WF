using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace FACC
{
    public class Faim_ScriptSerializer
    {
        public static string SerializeObject(object vObj)
        {
            JavaScriptSerializer _convert = new JavaScriptSerializer();
            string _json = _convert.Serialize(vObj);
            return _json;
        }
        public static string Serialize<T>(T vObj)
        {
            JavaScriptSerializer _convert = new JavaScriptSerializer();
            string _json = _convert.Serialize(vObj);
            return _json;
        }
        public static string Serialize_List<T>(List<T> vlst_obj)
        {
            if (vlst_obj == null)
            {
                vlst_obj = new List<T>();
                vlst_obj.Add((T)(Activator.CreateInstance(typeof(T))));
            }
            JavaScriptSerializer _convert = new JavaScriptSerializer();
            string _json = _convert.Serialize(vlst_obj);
            return _json;
        }
        public static T Deserialize<T>(string vJson)
        {
            JavaScriptSerializer _convert = new JavaScriptSerializer();
            T _obj;
            try
            {
                _obj = _convert.Deserialize<T>(vJson);
                return _obj;
            }
            catch (ArgumentException ex)
            {
                Console.Write(ex.Message);
                // FACC.Log.Error_4("Deserialize", ex.Message)
                return default(T);
            }
        }
        public static List<T> Deserialize_List<T>(string vJson)
        {
            JavaScriptSerializer _convert = new JavaScriptSerializer();
            List<T> _obj;
            try
            {
                _obj = _convert.Deserialize<List<T>>(vJson);
                return _obj;
            }
            catch (ArgumentException ex)
            {
                Console.Write(ex.Message);
                // FACC.Log.Error_4("Deserialize_List", ex.Message)
                return null;
            }
        }
        private void do_demo()
        {
            Faim_ScriptSerializer _convert = new Faim_ScriptSerializer();
            string _json = Faim_ScriptSerializer.Serialize("abc");
            object _obj = Faim_ScriptSerializer.Deserialize<String>(_json);
        }
    }
}
