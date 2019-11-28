using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Models.ActionParameterDefinition;

namespace UIClient.Models.Inputs
{
    public class GenericFormInputModel
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public TypeValue Type { get; set; }
        public string[] Options { get; set; }
        public IGenericFormSuggestionHandler SuggestionHandler { get; set; }
        public object DefaultValue { get; set; }
    }
}
