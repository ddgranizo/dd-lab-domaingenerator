using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class Domain
    {
        public bool IsRootDomain { get; set; }
        public string Namespace { get; set; }
        public string Name { get; set; }

        public Domain()
        {
        }

        public Domain(string name) : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name cannot be null", nameof(name));
            }
            Name = name;
        }

        public Domain(string nameSpace, string name) : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name cannot be null", nameof(name));
            }
            if (string.IsNullOrEmpty(nameSpace))
            {
                throw new ArgumentException("namesSpace cannot be null", nameof(nameSpace));
            }
            IsRootDomain = true;
            Namespace = nameSpace;
            Name = name;
        }
    }
}
