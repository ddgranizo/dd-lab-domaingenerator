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

        public static T ObjectifyWithTypes<T>(string json) where T : IInitializable<T>, new()
        {
            if (string.IsNullOrEmpty(json))
            {
                var instance = new T();
                return instance.GetInitialInstance();
            }
            var settings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Objects
            };

            return JsonConvert.DeserializeObject<T>(json, settings);
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

        public static string StringfyWithTypes<T>(T content)
        {
            var settings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Objects
            };
            return JsonConvert.SerializeObject(content, settings);
        }


        public static T GetInitialInstance<T>() where T : IInitializable<T>, new()
        {
            var instance = new T();
            return instance.GetInitialInstance();
        }
    }
}
