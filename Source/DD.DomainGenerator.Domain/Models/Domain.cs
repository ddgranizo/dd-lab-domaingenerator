using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class Domain
    {
        public bool IsRootDomain { get; set; }
        public string Namespace { get; set; }
        public string Name { get; set; }
        public List<Schema> Schemas { get; set; }


        public Domain()
        {
            Schemas = new List<Schema>();
        }



        public Domain(string name) : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name cannot be null", nameof(name));
            }
            Name = name;
        }

        public Domain(string nameSpace, string name) : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name cannot be null", nameof(name));
            }
            if (string.IsNullOrEmpty(nameSpace))
            {
                throw new ArgumentException("namesSpace cannot be null", nameof(nameSpace));
            }
            IsRootDomain = true;
            Namespace = nameSpace;
            Name = name;
        }


        public void AddSchema(Schema schema)
        {
            var existing = Schemas.FirstOrDefault(k => k.Name == schema.Name);
            if (existing != null)
            {
                throw new Exception("Schema with name arelady repated");
            }
            Schemas.Add(schema);
        }

        public void DeleteSchema(string schemaName)
        {
            var existing = Schemas.FirstOrDefault(k => k.Name == schemaName)
                ?? throw new Exception($"This domain doesn't contain schema with name 'schemaName'");
            Schemas.Remove(existing);
        }
    }
}
