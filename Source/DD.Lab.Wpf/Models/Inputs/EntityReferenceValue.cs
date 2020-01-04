using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Models.Inputs
{
    public class EntityReferenceValue
    {

        public EntityReferenceValue(Guid id, string logicalName, string displayName)
        {
            Id = id;
            LogicalName = logicalName ?? throw new ArgumentNullException(nameof(logicalName));
            DisplayName = displayName;
        }

        public EntityReferenceValue(Guid id)
        {
            Id = id;
        }

        public EntityReferenceValue()
        {
        }

        public Guid Id { get; set; }
        public string LogicalName { get; set; }
        public string DisplayName { get; set; }
    }
}
