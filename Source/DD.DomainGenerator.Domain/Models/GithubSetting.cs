using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class GithubSetting
    {
        public GithubSetting(string name, string token)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("message", nameof(token));
            }

            Name = name;
            OauthToken = token;
        }

        public string Name { get; set; }
        public string OauthToken { get; set; }
    }
}
