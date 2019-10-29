using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class MicroService
    {
        
        public MicroService(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            Name = name;
        }

        public string Name { get; }
    }
}
