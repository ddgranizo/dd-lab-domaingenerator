using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class DataParameterValueModel : BaseModel
    {
        public DataParameterModel DataParameter { get { return GetValue<DataParameterModel>(); } set { SetValue(value); } }
        public object Value { get { return GetValue<object>(); } set { SetValue(value); } }
    }
}
