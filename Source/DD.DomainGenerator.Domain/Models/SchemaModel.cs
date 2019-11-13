using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.Models
{
    public class SchemaModel
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
        public List<SchemaModelProperty> Properties { get; set; }
        public List<SchemaView> Views { get; set; }

        public SchemaModel(string name)
        {
            UseCases = new List<UseCase>();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            Properties = new List<SchemaModelProperty>();
            Views = new List<SchemaView>();
            Name = name;
            AddView(new SchemaView(DefaultViewNames.All, false).AddColumnSet());
        }

        public void AddProperty(SchemaModelProperty property)
        {
            if (Properties.FirstOrDefault(k => k.Name == property.Name) != null)
            {
                throw new Exception("Name repeated in schema");
            }
            Properties.Add(property);
        }

        public void AddView(SchemaView view)
        {
            if (Views.FirstOrDefault(k => k.Name == view.Name) != null)
            {
                throw new Exception("View name repeated in schema");
            }
            Views.Add(view);
        }

        public void DeleteUseCase(UseCase.UseCaseTypes type, SchemaModel intersectionDomain = null)
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
                && (useCase.Type == UseCase.UseCaseTypes.Authorise
                || useCase.Type == UseCase.UseCaseTypes.Create
                || useCase.Type == UseCase.UseCaseTypes.DeleteByPk
                || useCase.Type == UseCase.UseCaseTypes.DeleteByUn
                || useCase.Type == UseCase.UseCaseTypes.RetrieveByPk
                || useCase.Type == UseCase.UseCaseTypes.RetrieveByUn
                || useCase.Type == UseCase.UseCaseTypes.RetrieveMultiple
                || useCase.Type == UseCase.UseCaseTypes.Update))
            {
                throw new Exception("Repeated use case");
            }

            if (useCase.Schema != null)
            {
                var repeatedIntersection = repeatedCrud
                    .Where(k => k.Schema?.Name == useCase.Schema.Name);
                if (repeatedIntersection.Count() > 0
                    && (useCase.Type == UseCase.UseCaseTypes.RetrieveMultipleIntersection))
                {
                    throw new Exception("Repeated use case");
                }
            }

            UseCases.Add(useCase);
        }

    }
}
