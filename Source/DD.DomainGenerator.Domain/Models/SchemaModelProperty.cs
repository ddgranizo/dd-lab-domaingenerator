using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{

    public class SchemaModelProperty
    {
        public enum PropertyTypes
        {
            PrimaryKey = 1,
            State = 2,
            Status = 3,
            ForeingKey = 4,
            Boolean = 10,
            Integer = 11,
            Decimal = 12,
            Float = 13,
            Time = 30,
            DateTime = 31,
            String = 40,
            LongString = 41,
            Password = 99,
        }

        public PropertyTypes Type { get; set; }
        public int Length { get; set; }
        public string Name { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsNullable { get; set; }
        public bool IsUnique { get; set; }
        public bool IsAutoIncremental { get; set; }
        public Schema ForeingSchema { get; set; }

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

        public static List<string> GetUseCaseTypesList()
        {
            return Enum.GetNames(typeof(PropertyTypes)).ToList();
        }
    }
}
