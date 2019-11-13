using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class ViewParameter
    {
        public enum ParameterType
        {
            Boolean = 1,
            Integer = 2,
            Decimal = 3,
            Double = 4,
            String = 5,
            Guid = 6,
            DateTime = 7,

            CurrentEntity = 90,
            ColumnSet = 99,
        }

        public int Order { get; set; }
        public ParameterType Type { get; set; }
        public string Name { get; set; }
        public bool IsCustom { get; set; }
        public ViewParameter(ParameterType type, string name, int order, bool isCustom = true)
        {
            Type = type;
            Name = name;
            Order = order;
            IsCustom = isCustom;
        }
    }
}
