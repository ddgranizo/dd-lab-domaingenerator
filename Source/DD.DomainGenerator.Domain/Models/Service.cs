using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class Service
    {
        public List<Domain> Domains { get; set; }
        public Service()
        {
            Domains = new List<Domain>();
        }


        public void AddDomain(Domain domain)
        {
            var repeated = Domains.FirstOrDefault(k => k.Name == domain.Name);
            if (repeated != null)
            {
                throw new Exception("Domain already added as a service");
            }
            Domains.Add(domain);
        }

        public void AddDomains(List<Domain> domains)
        {
            foreach (var item in domains)
            {
                AddDomain(item);
            }
        }
    }
}
