using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Definitions;
using static DD.DomainGenerator.Models.RepositoryMethodParameter;
using static DD.DomainGenerator.Models.UseCase;

namespace DD.DomainGenerator.Models
{
    public class RepositoryMethod
    {
        public string Name { get; set; }
        public bool IsCustom { get; set; }
        public List<DataParameter> InputParameters { get; set; }
        public List<DataParameter> OutputParameters { get; set; }
        public UseCaseTypes Type { get; set; }
        public RepositoryMethod(UseCaseTypes type, string name, bool isCustom)
        {
            Type = type;
            Name = name;
            IsCustom = isCustom;
            InputParameters = new List<DataParameter>();
            OutputParameters = new List<DataParameter>();
        }

        public RepositoryMethod AddInputParameter(DomainInputType type, string name)
        {
            InputParameters.Add(new DataParameter(type, name));
            return this;
        }

        public RepositoryMethod AddInputParameter(DomainInputType type, string name, DomainInputType enumerableType)
        {
            InputParameters.Add(new DataParameter(type, name) { EnumerableType = enumerableType });
            return this;
        }

        public RepositoryMethod AddInputParameter(DomainInputType type, string name, DomainInputType dictionaryKeyType, DomainInputType dictionaryValueType)
        {
            InputParameters.Add(new DataParameter(type, name) { DictionaryKeyType = dictionaryKeyType, DictionaryValueType = dictionaryValueType });
            return this;
        }

        public RepositoryMethod AddOutputParameter(DomainInputType type, string name)
        {
            OutputParameters.Add(new DataParameter(type, name));
            return this;
        }

        public RepositoryMethod AddDefaultOutputViewParameter()
        {
            OutputParameters.Add(new DataParameter(DomainInputType.Enumerable, "Collection") { EnumerableType = DomainInputType.DomainEntity });
            return this;
        }
    }
}
