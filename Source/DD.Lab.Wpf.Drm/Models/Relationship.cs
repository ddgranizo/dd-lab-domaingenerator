using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models
{
    public class Relationship
    {
        public string MainEntity { get; set; }
        public string RelatedEntity { get; set; }
        public string RelatedAttribute { get; set; }
        public bool IsManyToMany { get; set; }
        public string IntersectionName { get; set; }
        public Relationship()
        {
        }

        public Relationship(string mainEntity, string relatedEntity, string relatedAttribute)
        {
            MainEntity = mainEntity;
            RelatedEntity = relatedEntity;
            RelatedAttribute = relatedAttribute;
        }
    }
}
