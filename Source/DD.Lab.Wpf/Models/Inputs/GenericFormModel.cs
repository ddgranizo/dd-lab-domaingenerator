using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.Lab.Wpf.Models.Inputs.GenericFormInputModel;

namespace DD.Lab.Wpf.Models.Inputs
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

        public void AddAttribute(TypeValue type, string key, string displayName, string description, bool isMandatory,  object defaultValue = null)
        {
            AddAttribute(type, key, displayName, description, isMandatory, new string[] { }, null, defaultValue);
        }

        public void AddAttribute(TypeValue type, string key, string displayName, string description, bool isMandatory, string[] options, object defaultValue = null)
        {
            AddAttribute(type, key, displayName, description, isMandatory, options, null, defaultValue);
        }

        public void AddAttribute(TypeValue type, string key, string displayName, string description, bool isMandatory, IGenericFormSuggestionHandler suggestionHandler, object defaultValue = null)
        {
            AddAttribute(type, key, displayName, description, isMandatory, new string[] { }, suggestionHandler, defaultValue);
        }

        public void AddAttribute(TypeValue type, string key, string displayName, string description, bool isMandatory, string[] options, IGenericFormSuggestionHandler suggestionHandler, object defaultValue)
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
                StringSuggestionOptions = options,
                IsMandatory = isMandatory,
                SuggestionHandler = suggestionHandler,
                DefaultValue = defaultValue,
            });
        }




    }
}
