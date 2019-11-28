using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Definitions;
using static DD.DomainGenerator.Models.RepositoryMethodParameter;

namespace UIClient.Models
{
    public class RepositoryMethodParameterModel: BaseModel
    {
        public DataParameterModel InputParameter { get { return GetValue<DataParameterModel>(); } set { SetValue(value); } }
        public bool IsCustom { get { return GetValue<bool>(); } set { SetValue(value); } }
    }
}
