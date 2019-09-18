using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{

    public class SchemaModelProperty
    {
        public enum PropertyTypes
        {
            Guid = 1,
            Boolean = 2,
            Integer = 3,
            Decimal = 4,
            Float = 5,
            Time = 6,
            DateTime = 7,
            String = 8,
            LongString = 9,
            Password = 99,
        }

        public PropertyTypes Type { get; set; }
        public int Length { get; set; }
        public string Name { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsNullable { get; set; }
        public bool IsUnique { get; set; }
        public bool IsAutoIncremental { get; set; }

        public SchemaModelProperty()
        {
        }

        public SchemaModelProperty(string name, PropertyTypes type)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            Type = type;
            Name = name;
        }

        public static PropertyTypes StringToType(string type)
        {
            foreach (var item in Enum.GetValues(typeof(PropertyTypes)))
            {
                var name = Enum.GetName(typeof(PropertyTypes), item);
                if (name == type)
                {
                    return (PropertyTypes)item;
                }
            }
            throw new Exception($"Can't find type named {type}");
        }
    }
}
