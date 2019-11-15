using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class MicroService
    {
        public List<Domain> Domains { get; set; }
        public string Name { get; }

        public MicroService(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            Name = name;
            Domains = new List<Domain>();
        }
        

        public void AddDomain(Domain domain)
        {
            var exists = Domains.FirstOrDefault(k => k.Name == domain.Name);
            if (exists != null)
            {
                throw new Exception("Domain with name arelady repated");
            }
            Domains.Add(domain);
        }

    }
}
