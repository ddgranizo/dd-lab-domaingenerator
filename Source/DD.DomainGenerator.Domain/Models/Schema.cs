using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.Models
{
    public class Schema
    {
        public string Name { get; set; }
        public List<UseCase> UseCases { get; set; }
        public bool HasId { get; set; }
        public bool HasDates { get; set; }
        public bool HasUserRelationship { get; set; }
        public bool HasState { get; set; }
        public bool HasOwner { get; set; }
        public bool IsIntersection { get; set; }
        public bool NeedsAuthorization { get; set; }
        public List<SchemaProperty> Properties { get; set; }
        public List<Repository> Repositories { get; set; }
        public List<Model> Models { get; set; }

        public Schema(string name)
        {
            UseCases = new List<UseCase>();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }
            Properties = new List<SchemaProperty>();
            Name = name;
            Repositories = new List<Repository>();
            Models = new List<Model>();
            AddRepository(new Repository($"{name}MainRepository", false, true));
            AddModel(new Model(name, false, true, true));
        }

        public Repository GetDefaultRepository()
        {
            return Repositories.First(k => k.IsMain);
        }

        public Repository GetRepository(string name)
        {
            return Repositories.First(k => k.Name == name);
        }

        public void AddModel(Model model)
        {
            if (Models.FirstOrDefault(k => k.Name == model.Name) != null)
            {
                throw new Exception("Model repeated in schema");
            }
            Models.Add(model);
        }

        public void AddRepository(Repository repository)
        {
            if (Repositories.FirstOrDefault(k => k.Name == repository.Name) != null)
            {
                throw new Exception("Repository repeated in schema");
            }
            Repositories.Add(repository);
        }

        public void AddProperty(SchemaProperty property)
        {
            if (Properties.FirstOrDefault(k => k.Name == property.Name) != null)
            {
                throw new Exception("Name repeated in schema");
            }
            Properties.Add(property);
        }

        

        public void DeleteUseCase(UseCase.UseCaseTypes type, Schema intersectionDomain = null)
        {
            var item = UseCases.FirstOrDefault(k => k.Type == type && k.Schema?.Name == intersectionDomain?.Name);
            if (item == null)
            {
                throw new Exception("Can't find use case");
            }
            UseCases.Remove(item);
        }

        public void AddUseCase(UseCase useCase)
        {
            var repeatedCrud = UseCases.Where(k => k.Type == useCase.Type);
            if (repeatedCrud.Count() > 0
                && (useCase.Type == UseCase.UseCaseTypes.Create
                || useCase.Type == UseCase.UseCaseTypes.DeleteByPk
                || useCase.Type == UseCase.UseCaseTypes.DeleteByUn
                || useCase.Type == UseCase.UseCaseTypes.RetrieveByPk
                || useCase.Type == UseCase.UseCaseTypes.RetrieveByUn
                || useCase.Type == UseCase.UseCaseTypes.Update))
            {
                throw new Exception("Repeated use case");
            }

            UseCases.Add(useCase);
        }



        public List<UseCase> GetAllUseCases()
        {
            return UseCases
                .ToList();
        }

        public List<RepositoryMethod> GetAllRepositoriesMethods()
        {
            return Repositories
                .SelectMany(k => k.RepositoryMethods)
                .ToList();
        }


        public List<Repository> GetAllRepositories()
        {
            return Repositories
                .ToList();
        }


    }
}
