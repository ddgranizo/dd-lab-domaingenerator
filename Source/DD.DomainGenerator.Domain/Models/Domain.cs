using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class Domain
    {
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


        public List<UseCase> GetAllUseCases()
        {
            return Schemas
                .SelectMany(k => k.UseCases)
                .ToList();
        }

        public List<RepositoryMethod> GetAllRepositoriesMethods()
        {
            return Schemas
                .SelectMany(k => k.Repositories)
                .SelectMany(k => k.RepositoryMethods)
                .ToList();
        }


        public List<Repository> GetAllRepositories()
        {
            return  Schemas
                .SelectMany(k => k.Repositories)
                .ToList();
        }


        public List<Schema> GetAllSchemas()
        {
            return Schemas
                .ToList();
        }

      
    }
}
