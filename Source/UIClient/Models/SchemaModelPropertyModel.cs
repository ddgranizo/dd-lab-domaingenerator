using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Models.SchemaModelProperty;

namespace UIClient.Models
{
    public class SchemaModelPropertyModel : BaseModel
    {
        public PropertyTypes Type { get { return GetValue<PropertyTypes>(); } set { SetValue(value); } }
        public int Length { get { return GetValue<int>(); } set { SetValue(value); } }
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool IsPrimaryKey { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsNullable { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsUnique { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsAutoIncremental { get { return GetValue<bool>(); } set { SetValue(value); } }
        public SchemaModelModel ForeingSchema { get { return GetValue<SchemaModelModel>(); } set { SetValue(value); } }
    }
}
