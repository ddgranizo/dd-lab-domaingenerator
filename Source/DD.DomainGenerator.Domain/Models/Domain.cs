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
        public bool HasModel { get { return Schema != null; } }

        public List<Domain> Domains { get; set; }

        public Domain ParentDomain { get; set; }
        public SchemaModel Schema { get; set; }

        public List<UseCase> UseCases { get; set; }
        public bool NeedsAuthorization { get; set; }

        public Domain()
        {
            UseCases = new List<UseCase>();
            Domains = new List<Domain>();
        }

        public Domain(Domain parentDomain, string name) : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            ParentDomain = parentDomain ?? throw new ArgumentNullException(nameof(parentDomain));
            Name = name;
        }

        public Domain(string nameSpace, string name) : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }
            if (string.IsNullOrEmpty(nameSpace))
            {
                throw new ArgumentException("message", nameof(nameSpace));
            }
            IsRootDomain = true;
            Namespace = nameSpace;
            Name = name;
        }

        public void DeleteUseCase(UseCase.UseCaseTypes type, SchemaModel intersectionDomain = null)
        {
            if (!HasModel)
            {
                throw new Exception("Current domain doesn't contain any use case becouse it hasn't schema yet");
            }
            var item = UseCases.FirstOrDefault(k => k.Type == type && k.Schema?.Name == intersectionDomain?.Name);
            if (item == null)
            {
                throw new Exception("Can't find use case");
            }
            UseCases.Remove(item);
        }

        public void AddUseCase(UseCase useCase)
        {
            if (!HasModel)
            {
                throw new Exception("Use case can only be added to domains with schema");
            }
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

        public static bool IsDomainNameRepeated(Domain domain, string name)
        {
            return FindChildDomain(domain, name) != null;
        }

        public static Domain FindChildDomain(Domain domain, string name)
        {
            if (domain.Name == name)
            {
                return domain;
            }

            foreach (var item in domain.Domains)
            {
                var returnedDomain = FindChildDomain(item, name);
                if (returnedDomain != null)
                {
                    return returnedDomain;
                }
            }
            return null;
        }


        public List<Domain> GetDomainsBelow()
        {
            List<Domain> domainsBelow = new List<Domain>();
            domainsBelow.Add(this);
            foreach (var item in Domains)
            {
                domainsBelow.AddRange(item.GetDomainsBelow());
            }
            return domainsBelow;
        }



        public void AddDomain(Domain childDomain)
        {
            if (HasModel)
            {
                throw new Exception("Child domain nodes can be only added to non-modeled domains");
            }
            Domains.Add(childDomain);
        }


        public void DeleteDomain(string childDomainName)
        {
            var domain = FindChildDomain(this, childDomainName);
            if (domain == null)
            {
                throw new Exception($"Can't find child domain with name '{childDomainName}'");
            }
            if (domain.Domains.Count > 0)
            {
                throw new Exception($"Can't delete domain with childs domains. Delete first the childs");
            }
            if (domain.ParentDomain == null)
            {
                throw new Exception($"Can't delete root domain");
            }
            var childDomain = domain.ParentDomain.Domains.FirstOrDefault(k => k.Name == childDomainName);
            if (childDomain == null)
            {
                throw new Exception("Cannot find child node. This exception shouldn't appear never due to the logic. Check why is this happening");
            }
            domain.ParentDomain.Domains.Remove(childDomain);
        }

    }
}
