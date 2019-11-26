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

        public void AddAttribute(TypeValue type, string key, string displayName, string description)
        {
            AddAttribute(type, key, displayName, description, new string[] { });
        }


        public void AddAttribute(TypeValue type, string key, string displayName, string description, string[] options)
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
                SuggestionHandler = null,
            }); 
        }

        public void AddAttribute(TypeValue type, string key, string displayName, string description, IGenericFormSuggestionHandler suggestionHandler)
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
                Options = new string[] { },
                SuggestionHandler = suggestionHandler,
            });
        }

    }
}
