using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.Models
{
    public class DataParameter
    {
        public DomainInputType Type { get; set; }
        public DomainInputType EnumerableType { get; set; }
        public DomainInputType DictionaryKeyType { get; set; }
        public DomainInputType DictionaryValueType { get; set; }

        public string Name { get; set; }

        public DataParameter(DomainInputType type, string name)
        {
            Type = type;
            Name = name;
        }

        public static List<string> GetUseCaseParameterTypesList()
        {
            return Enum.GetNames(typeof(DomainInputType)).ToList();
        }

        public static DomainInputType ParseInputType(string inputTypeString)
        {
            if (Enum.TryParse(inputTypeString, out DomainInputType type))
            {
                return type;
            }
            throw new InvalidCastException();
        }
    }
}
