using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class SchemaModel
    {
        public List<SchemaModelProperty> Properties { get; set; }

        public SchemaModel()
        {
            Properties = new List<SchemaModelProperty>();
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
