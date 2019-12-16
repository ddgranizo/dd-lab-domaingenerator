using DD.Lab.Services.System.Interfaces;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DD.Lab.Services.System.Implementations
{
    public class JsonParserService : IJsonParserService
    {
        public T Objectify<T>(string json)
        {
            var settings = new JsonSerializerSettings()
            {
                //PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Objects
                
            };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public string Stringfy<T>(T instance)
        {
            var settings = new JsonSerializerSettings()
            {
                //PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Objects
            };
            return JsonConvert.SerializeObject(instance, Formatting.Indented, settings);
        }
    }
}
