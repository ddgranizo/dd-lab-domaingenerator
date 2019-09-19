using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class SchemaModel
    {
        public string Name { get; set; }

        public bool HasId { get; set; }
        public bool HasDates { get; set; }
        public bool HasUserRelationship { get; set; }
        public bool HasState { get; set; }
        public bool HasOwner { get; set; }

        public List<SchemaModelProperty> Properties { get; set; }

        public SchemaModel(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            Properties = new List<SchemaModelProperty>();
            Name = name;
        }

        public void AddProperty(SchemaModelProperty property)
        {
            if (Properties.FirstOrDefault(k => k.Name == property.Name) != null)
            {
                throw new Exception("Name repeated in schema");
            }
            Properties.Add(property);
        }
    }
}
