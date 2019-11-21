using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Models.SchemaProperty;

namespace UIClient.Models
{
    public class SchemaPropertyModel : BaseModel
    {
        public PropertyTypes Type { get { return GetValue<PropertyTypes>(); } set { SetValue(value); } }
        public bool IsCustom { get { return GetValue<bool>(); } set { SetValue(value); } }
        public int Length { get { return GetValue<int>(); } set { SetValue(value); } }
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool IsPrimaryKey { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsNullable { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsUnique { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsAutoIncremental { get { return GetValue<bool>(); } set { SetValue(value); } }
        public SchemaModel ForeingSchema { get { return GetValue<SchemaModel>(); } set { SetValue(value); } }
    }
}
