using DomainGeneratorUI.Interfaces;
using DomainGeneratorUI.Models.Methods;
using System;
using System.Collections.Generic;
using System.Text;
using static DomainGeneratorUI.Models.Methods.MethodParameter;

namespace DomainGeneratorUI.Models.RepositoryMethods
{
    public class RepositoryMethodContent : IInitializable<RepositoryMethodContent>
    {
        public List<MethodParameter> Parameteters { get; set; }

        public RepositoryMethodContent()
        {
            Parameteters = new List<MethodParameter>();

        }

        public RepositoryMethodContent AddInputParameter(string name, ParameterInputType type)
        {
            Parameteters.Add(new MethodParameter()
            {
                Direction = ParameterDirection.Input,
                Name = name,
                Type = type,
            });
            return this;
        }

        public RepositoryMethodContent AddOutputParameter(string name, ParameterInputType type)
        {
            Parameteters.Add(new MethodParameter()
            {
                Direction = ParameterDirection.Output,
                Name = name,
                Type = type,
            });
            return this;
        }

        public RepositoryMethodContent AddDefaultOutputViewParameter()
        {
            Parameteters.Add(new MethodParameter()
            {
                Direction = ParameterDirection.Output,
                Name = "Collection",
                Type =ParameterInputType.Enumerable,
                EnumerableType = ParameterInputType.Entity
            });
            return this;
        }

        public RepositoryMethodContent GetInitialInstance()
        {
            return new RepositoryMethodContent();
        }
    }
}
