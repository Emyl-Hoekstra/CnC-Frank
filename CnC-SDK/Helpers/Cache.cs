using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnC.Helpers
{
    public static class Cache
    {

        public static T LoadObject<T>(string path)
        {
            if (!path.EndsWith("/")) path = path += "/";
            string sFile = path + typeof(T).Name + ".json";
            if (File.Exists(sFile))
            {
                string json = File.ReadAllText(sFile);

                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }

        public static T LoadObject<T>()
        {
            string sFile = AppDomain.CurrentDomain.BaseDirectory + typeof(T).Name + ".json";
            if (File.Exists(sFile))
            {
                string json = File.ReadAllText(sFile);

                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }

        public static void SaveObject<T>(string path, object objectToSave)
        {
            if (!path.EndsWith("/"))path = path += "/";

            File.WriteAllText(path + typeof(T).Name + ".json", JsonConvert.SerializeObject(objectToSave));
        }

        public static void SaveObject<T>(object objectToSave)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + typeof(T).Name + ".json", JsonConvert.SerializeObject(objectToSave));
        }
    }
}
