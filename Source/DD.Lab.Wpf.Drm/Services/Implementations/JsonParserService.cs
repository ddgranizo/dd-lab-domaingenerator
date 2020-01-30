using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DD.Lab.Wpf.Drm.Services.Implementations
{
    public class JsonParserService : IJsonParserService
    {
        public T Objectify<T>(string json)
        {
            var settings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                //TypeNameHandling = TypeNameHandling.None
            };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public string Stringfy<T>(T instance)
        {
            var settings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                //TypeNameHandling = TypeNameHandling.None
            };
            return JsonConvert.SerializeObject(instance, Formatting.Indented, settings);
        }

        public string StringfyWithTypes<T>(T instance)
        {
            var settings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Objects
            };
            return JsonConvert.SerializeObject(instance, Formatting.Indented, settings);
        }
    }
}
