using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Models.ActionParameterDefinition;

namespace UIClient.Models.Inputs
{
    public class GenericFormModel
    {
        public List<GenericFormInputModel> Attributes { get; set; }
        public string Description { get; }
        public GenericFormModel(string description)
        {
            Attributes = new List<GenericFormInputModel>();
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public void AddAttribute(TypeValue type, string key, string displayName, string description, object defaultValue = null)
        {
            AddAttribute(type, key, displayName, description, new string[] { }, null, defaultValue);
        }

        public void AddAttribute(TypeValue type, string key, string displayName, string description, string[] options, object defaultValue = null)
        {
            AddAttribute(type, key, displayName, description, options, null, defaultValue);
        }

        public void AddAttribute(TypeValue type, string key, string displayName, string description, IGenericFormSuggestionHandler suggestionHandler, object defaultValue = null)
        {
            AddAttribute(type, key, displayName, description, new string[] { }, suggestionHandler, defaultValue);
        }

        public void AddAttribute(TypeValue type, string key, string displayName, string description, string[] options, IGenericFormSuggestionHandler suggestionHandler, object defaultValue)
        {
            if (Attributes.Any(k => k.Key == key))
            {
                throw new Exception($"Parameter repeated: {key}");
            }
            Attributes.Add(new GenericFormInputModel()
            {
                Key = key,
                Description = description,
                DisplayName = displayName,
                Type = type,
                Options = options,
                SuggestionHandler = suggestionHandler,
                DefaultValue = defaultValue,
            });
        }




    }
}
