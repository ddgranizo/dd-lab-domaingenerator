using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Models.ViewParameter;

namespace DD.DomainGenerator.Models
{
    public class SchemaView
    {
        private const string ColumnSetParameter = "ColumnSet";

        public string Name { get; set; }
        public bool IsCustom { get; set; }
        public List<ViewParameter> Parameters { get; set; }

        public SchemaView(string name, bool isCustom)
        {
            Name = name;
            IsCustom = isCustom;
            Parameters = new List<ViewParameter>();
            
        }

        public SchemaView AddColumnSet()
        {
            Parameters.Add(new ViewParameter(ParameterType.ColumnSet, ColumnSetParameter, 99, false));
            return this;
        }

        public SchemaView AddProperty(ParameterType type, string name, int order, bool isCustom)
        {
            Parameters.Add(new ViewParameter(type, name, order, isCustom));
            return this;
        }
    }
}
