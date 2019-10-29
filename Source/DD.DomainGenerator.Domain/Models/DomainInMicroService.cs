using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class DomainInMicroService
    {
        public DomainInMicroService(Domain domain, MicroService microService)
        {
            Domain = domain ?? throw new ArgumentNullException(nameof(domain));
            MicroService = microService ?? throw new ArgumentNullException(nameof(microService));
        }

        public Domain Domain { get; }
        public MicroService MicroService { get; }
    }
}
