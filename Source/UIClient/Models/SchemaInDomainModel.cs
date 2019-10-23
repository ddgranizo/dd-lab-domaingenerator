using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class SchemaInDomainModel : BaseModel
    {
        public DomainModel Domain { get { return GetValue<DomainModel>(); } set { SetValue(value); } }
        public SchemaModelModel Schema { get { return GetValue<SchemaModelModel>(); } set { SetValue(value); } }
    }
}
