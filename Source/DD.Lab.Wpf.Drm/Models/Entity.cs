using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models
{
    public class Entity
    {
        public string LogicalName { get; set; }
        public string DisplayName { get; set; }
        public List<Attribute> Attributes { get; set; }

        public Entity()
        {
        }

        public Entity(string logicalName, string displayName)
        {
            if (string.IsNullOrEmpty(logicalName))
            {
                throw new ArgumentException("message", nameof(logicalName));
            }

            if (string.IsNullOrEmpty(displayName))
            {
                throw new ArgumentException("message", nameof(displayName));
            }

            Attributes = new List<Attribute>();
            LogicalName = logicalName;
            DisplayName = displayName;
        }
    }
}
