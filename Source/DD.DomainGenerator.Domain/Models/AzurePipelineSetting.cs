using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class AzurePipelineSetting
    {
        public AzurePipelineSetting(string name, string organizationUri, string token)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrEmpty(organizationUri))
            {
                throw new ArgumentException("message", nameof(organizationUri));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("message", nameof(token));
            }

            Name = name;
            OrganizationUri = organizationUri;
            Token = token;
        }

        public string Name { get; set; }
        public string OrganizationUri { get; set; }
        public string Token { get; set; }
    }
}
