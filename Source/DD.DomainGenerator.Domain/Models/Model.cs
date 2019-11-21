using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class Model
    {
        public string Name { get; set; }
        public bool IsCustom { get; set; }
        public bool AllProperties { get; set; }
        public bool IsMainModel { get; set; }
        public List<string> Properties { get; set; }

        public Model(string name, bool isCustom, bool allProperties = false, bool isMainModel = false)
        {
            Name = name;
            IsCustom = isCustom;
            AllProperties = allProperties;
            IsMainModel = isMainModel;
            Properties = new List<string>();
        }
    }
}
