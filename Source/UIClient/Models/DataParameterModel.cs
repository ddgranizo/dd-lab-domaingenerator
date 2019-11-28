using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Definitions;
using static DD.DomainGenerator.Models.DataParameter;

namespace UIClient.Models
{
    public class DataParameterModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public DomainInputType Type { get { return GetValue<DomainInputType>(); } set { SetValue(value); } }
        public DomainInputType EnumerableType { get { return GetValue<DomainInputType>(); } set { SetValue(value); } }
        public DomainInputType DictionaryKeyType { get { return GetValue<DomainInputType>(); } set { SetValue(value); } }
        public DomainInputType DictionaryValueType { get { return GetValue<DomainInputType>(); } set { SetValue(value); } }


        

    }
}
