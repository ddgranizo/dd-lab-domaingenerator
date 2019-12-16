using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Models.Inputs
{
    public class GenericFormInputModel
    {
        //public enum TypeValue
        //{
        //    Boolean = 1,
        //    Integer = 2,
        //    Decimal = 3,
        //    Double = 4,
        //    Datetime = 5,
        //    //Guid = 9, --> deprecated, cannot parse json guid value into generic <string, object> and then deserialize into a guid --> it become string
        //    String = 10,
        //    MultilineString = 11,
        //    Password = 99,
        //}

        public enum TypeValue
        {
            Guid = 1,
            Bool = 10,
            Int = 11,
            Decimal = 13,
            Double = 14,
            DateTime = 15,
            String = 20,
            MultilineString = 21,
            OptionSet = 40,

            State = 50,
            EntityReference = 90,
            Password = 99,
        }

        public string Key { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public TypeValue Type { get; set; }
        public bool IsMandatory { get; set; }
        public string[] StringSuggestionOptions { get; set; }
        public IGenericFormSuggestionHandler SuggestionHandler { get; set; }
        public IEntityReferenceSuggestionHandler EntityReferenceSuggestionHandler { get; set; }
        public OptionSetValue[] OptionSetValueOptions { get; set; }
        public object DefaultValue { get; set; }
    }
}
