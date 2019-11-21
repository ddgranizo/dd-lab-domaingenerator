using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Models.ViewParameter;

namespace DD.DomainGenerator.Models
{
    public class View
    {
        private const string ColumnSetParameter = "ColumnSet";

        public string Name { get; set; }
        public bool IsCustom { get; set; }
        public List<ViewParameter> Parameters { get; set; }

        public View(string name, bool isCustom)
        {
            Name = name;
            IsCustom = isCustom;
            Parameters = new List<ViewParameter>();
            
        }

        //public View AddColumnSet()
        //{
        //    Parameters.Add(new ViewParameter(ParameterType.ColumnSet, ColumnSetParameter, 99, false));
        //    return this;
        //}

        public View AddProperty(ParameterType type, string name, int order, bool isCustom)
        {
            Parameters.Add(new ViewParameter(type, name, order, isCustom));
            return this;
        }
    }
}
