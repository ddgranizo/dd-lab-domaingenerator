using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Models.UseCaseParameter;

namespace UIClient.Models
{
    public class UseCaseParameterModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public InputType Type { get { return GetValue<InputType>(); } set { SetValue(value); } }
        public InputType EnumerableType { get { return GetValue<InputType>(); } set { SetValue(value); } }
        public InputType DictionaryKeyType { get { return GetValue<InputType>(); } set { SetValue(value); } }
        public InputType DictionaryKeyValue { get { return GetValue<InputType>(); } set { SetValue(value); } }

    }
}
