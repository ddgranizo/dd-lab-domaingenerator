using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models
{
    public class Entity
    {
        public string LogicalName { get; set; }
        public string DisplayName { get; set; }
        public List<Attribute> Attributes { get; set; }

        public Entity()
        {
            Attributes = new List<Attribute>();
        }

        public Entity(string logicalName, string displayName)
        {
            if (string.IsNullOrEmpty(logicalName))
            {
                throw new ArgumentException("message", nameof(logicalName));
            }

            if (string.IsNullOrEmpty(displayName))
            {
                throw new ArgumentException("message", nameof(displayName));
            }

            Attributes = new List<Attribute>();
            LogicalName = logicalName;
            DisplayName = displayName;
        }


        public static T DictionartyToEntity<T>(Dictionary<string, object> values) where T: new()
        {
            T instance = new T();
            var dic = new Dictionary<string, object>();
            foreach (var item in typeof(T).GetProperties())
            {
                var value = dic.ContainsKey(item.Name)
                        ? dic[item.Name]
                        : null;
                SetPropValue(instance, item.Name, value);
            }
            return instance;
        }

        public static Dictionary<string, object> EntityToDictionary<T>(T instance)
        {
            var dic = new Dictionary<string, object>();
            foreach (var item in instance.GetType().GetProperties())
            {
                var value = GetPropValue(instance, item.Name);
                dic.Add(item.Name, value);
            }
            return dic;
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private static void SetPropValue(object src, string propName, object value)
        {
            src.GetType(). GetProperty(propName).SetValue(src, value);
        }
    }
}
