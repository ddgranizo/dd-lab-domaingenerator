using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class SchemaInDomain
    {
        public Domain Domain { get; set; }
        public SchemaModel Schema { get; set; }

        public SchemaInDomain(Domain domain, SchemaModel schema)
        {
            Domain = domain ?? throw new ArgumentNullException(nameof(domain));
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));
        }
    }
}
