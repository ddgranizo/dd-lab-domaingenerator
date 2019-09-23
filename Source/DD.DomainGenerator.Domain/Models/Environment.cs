using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class Environment
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Order { get; set; }

        public Environment()
        {
        }

        public Environment(string name, string shortName, int order)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrEmpty(shortName))
            {
                throw new ArgumentException("message", nameof(shortName));
            }

            Name = name;
            ShortName = shortName;
            Order = order;
        }
    }
}
