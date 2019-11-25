using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class UseCaseParameter
    {
        public enum InputType
        {
            Boolean = 1,
            Integer = 2,
            Decimal = 3,
            Double = 4,
            Guid = 9, 
            String = 10,

            Enumerable = 80,
            Dictionary = 90,
            DomainEntity = 99,
        }

        public InputType Type { get; set; }
        public InputType EnumerableType { get; set; }
        public InputType DictionaryKeyType { get; set; }
        public InputType DictionaryKeyValue { get; set; }

        public string Name { get; set; }

        public UseCaseParameter(InputType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
