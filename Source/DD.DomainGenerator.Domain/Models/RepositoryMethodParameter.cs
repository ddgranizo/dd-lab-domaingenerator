using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.Models
{
    public class RepositoryMethodParameter
    {
        public DomainInputType Type { get; set; }
        public string Name { get; set; }
        public bool IsCustom { get; set; }
        public RepositoryMethodParameter(DomainInputType type, string name, bool isCustom = true)
        {
            Type = type;
            Name = name;
            IsCustom = isCustom;
        }
    }
}
