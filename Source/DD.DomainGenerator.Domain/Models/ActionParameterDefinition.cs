using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class ActionParameterDefinition
    {
        public enum TypeValue
        {
            Boolean = 1,
            Integer = 2,
            Decimal = 3,
            Guid = 9,
            String = 10,
            Password = 99,
        }


        public List<string> InputSuggestions { get; set; }

        public TypeValue Type { get; set; }
        public string Description { get; }
        public string ShortCut { get; }
        public string Name { get; }

        public bool IsDomainSuggestion { get; set; }

        public ActionParameterDefinition(string name, TypeValue type, string description, string shortCut = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("command parameter name cannot be null or empty", nameof(name));
            }
            Name = name;
            Type = type;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ShortCut = shortCut;
        }


        public string GetInvokeName()
        {
            return $"--{this.Name.ToLowerInvariant()}";
        }

    }
}
