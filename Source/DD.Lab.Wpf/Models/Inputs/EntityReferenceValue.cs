using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Models.Inputs
{
    public class EntityReferenceValue
    {
        public EntityReferenceValue(Guid id, string displayName)
        {
            Id = id;
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
        public string DisplayName { get; set; }


    }
}
