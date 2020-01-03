
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models
{
    public class SubGridRelationshipData
    {
        public Relationship Relationship { get; set; }
        public Entity RelatedEntity { get; set; }
        public Guid MainEntityId { get; set; }
        public string RelatedEntityDisplayName { get; set; }
        public string RelatedAttributeDisplayName { get; set; }
        public string MainEntityDisplayName { get; set; }
        public string IntersectionDisplayableEntity { get; set; }

        public string GetDisplayableRelationshipName()
        {
            if (Relationship == null)
            {
                return null;
            }
            if (Relationship.IsManyToMany)
            {
                return $"[N:M] Related {IntersectionDisplayableEntity}(s) by attr {RelatedAttributeDisplayName}";
            }
            else
            {
                return $"[1:N] Related {RelatedEntityDisplayName}(s) by intersection entity {Relationship.IntersectionName}";
            }
        }
      
    }
}
