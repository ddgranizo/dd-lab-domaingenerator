using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services.Implementations
{
    public class JsonParserService : IJsonParserService
    {
        public T Objectify<T>(string json)
        {
            var settings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public string Stringfy<T>(T instance)
        {
            var settings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };
            return JsonConvert.SerializeObject(instance, Formatting.Indented, settings);
        }
    }
}
