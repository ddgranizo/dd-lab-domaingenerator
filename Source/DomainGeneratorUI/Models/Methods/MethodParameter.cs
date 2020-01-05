using DomainGeneratorUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.Methods
{
    public class MethodParameter : IInitializable<MethodParameter>
    {
        public enum ParameterDirection
        {
            Input = 1,
            Output = 2
        }

        public enum ParameterInputType
        {
            Boolean = 1,
            Integer = 2,
            Decimal = 3,
            Double = 4,
            Guid = 9,
            String = 10,
            Datetime = 20,

            Enumerable = 80,
            Dictionary = 90,

            Entity = 99,
        }

        public string Name { get; set; }

        public ParameterDirection Direction { get; set; }

        public ParameterInputType Type { get; set; }

        public ParameterInputType EnumerableType { get; set; }

        public ParameterInputType DictionaryKeyType { get; set; }

        public ParameterInputType DictionaryValueType { get; set; }


        public MethodParameter GetInitialInstance()
        {
            return new MethodParameter();
        }
    }
}
