using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Models.ViewParameter;

namespace UIClient.Models
{
    public class ViewParameterModel: BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public ParameterType Type { get { return GetValue<ParameterType>(); } set { SetValue(value); } }
        public bool IsCustom { get { return GetValue<bool>(); } set { SetValue(value); } }
        public int Order { get { return GetValue<int>(); } set { SetValue(value); } }
    }
}
