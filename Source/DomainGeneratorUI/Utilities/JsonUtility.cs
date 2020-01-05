using DomainGeneratorUI.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Utilities
{
    public static class JsonUtility
    {
        public static bool IsValidJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return true;
            }
            try
            {
                var content = JsonConvert.DeserializeObject<T>(json);
                return content != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static T GetInstance<T>(string json) where T: IInitializable<T>, new()
        {
            if (string.IsNullOrEmpty(json))
            {
                var instance = new T();
                return instance.GetInitialInstance();
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Stringfy<T>(T content)
        {
            return JsonConvert.SerializeObject(content);
        }


        public static T GetInitialInstance<T>() where T : IInitializable<T>, new()
        {
            var instance = new T();
            return instance.GetInitialInstance();
        }
    }
}
