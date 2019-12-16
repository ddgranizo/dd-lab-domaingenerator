using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.GenericUI.Core.Models
{
    public class Attribute
    {
        public enum AttributeType
        {
            Guid = 1,
            Bool = 10,
            Int = 11,
            Decimal = 13,
            Double = 14,
            Datetime = 15,
            String = 20,
            OptionSet = 40,

            State = 50,

            EntityReference = 90,
        }

        public Attribute()
        {
        }


        public bool IsMandatory { get; set; }
        public AttributeType Type { get; set; }
        public string LogicalName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<OptionSetValue> Options { get; set; }

        public string ReferencedEntity { get; set; }

        public Attribute(AttributeType type, string logicalName, string displayName, string description, bool isMandatory = false)
        {
            if (string.IsNullOrEmpty(logicalName))
            {
                throw new ArgumentException("message", nameof(logicalName));
            }

            if (string.IsNullOrEmpty(displayName))
            {
                throw new ArgumentException("message", nameof(displayName));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("message", nameof(description));
            }

            Type = type;
            LogicalName = logicalName;
            DisplayName = displayName;
            Description = description;
            IsMandatory = isMandatory;
        }
    }
}
