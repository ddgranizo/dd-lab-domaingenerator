using DD.Lab.Wpf.Drm.Services;
using DomainGeneratorUI.Models;
using DomainGeneratorUI.Models.UseCases.Sentences;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DomainGeneratorUI.Extensions
{
    public static class UseCaseSentenceViewModelExtensions
    {

        public static ExecuteRepositoryMethodSentence ToExecuteRepositoryMethod(
            this UseCaseSentenceViewModel sentence,
            IJsonParserService jsonParserService)
        {
            var composedTypes = new Dictionary<string, Type>();
            composedTypes.Add(nameof(ExecuteRepositoryMethodSentence.RepositoryMethod), typeof(RepositoryMethod));
            return ToGenericSentence<ExecuteRepositoryMethodSentence>(sentence, jsonParserService, composedTypes);
        }

        public static T ToGenericSentence<T>(
            this UseCaseSentenceViewModel sentence,
            IJsonParserService jsonParserService,
            Dictionary<string, Type> composedProperties) where T : UseCaseSentence, new()
        {
            var target = new T();
            SetPropertiesValues(target, sentence.Values, jsonParserService, composedProperties);
            return target;
        }


        private static void SetPropertiesValues<T>(
            T instance,
            Dictionary<string, object> values,
            IJsonParserService jsonParserService,
            Dictionary<string, Type> composedProperties)
        {
            var properties = instance.GetType().GetProperties();
            foreach (var property in properties)
            {
                var targetPropertyName = property.Name;
                if (values.ContainsKey(targetPropertyName) && !composedProperties.ContainsKey(targetPropertyName))
                {
                    instance.GetType().GetProperty(targetPropertyName).SetValue(instance, values[targetPropertyName]);
                }
                else if (composedProperties.ContainsKey(targetPropertyName))
                {
                    //var value = (Dictionary<string, object>)values[targetPropertyName];
                    var type = composedProperties[targetPropertyName];
                    MethodInfo method = typeof(IJsonParserService).GetMethod("Objectify");
                    MethodInfo generic = method.MakeGenericMethod(type);
                    object objectified = generic.Invoke(jsonParserService, new object[] { jsonParserService.Stringfy(values[targetPropertyName]) });
                    instance.GetType().GetProperty(targetPropertyName).SetValue(instance, objectified);
                }
            }
        }
    }
}
