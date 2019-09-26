using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Models.UseCase;

namespace UIClient.Models
{
    public class UseCaseModel : BaseModel
    {
        public UseCaseTypes Type { get { return GetValue<UseCaseTypes>(); } set { SetValue(value); } }
        public SchemaModelPropertyModel Attribute { get { return GetValue<SchemaModelPropertyModel>(); } set { SetValue(value); } }
        public SchemaModelModel Schema { get { return GetValue<SchemaModelModel>(); } set { SetValue(value); } }
    }
}
