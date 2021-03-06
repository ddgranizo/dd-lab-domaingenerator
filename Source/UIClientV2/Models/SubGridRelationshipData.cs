﻿using DD.Lab.GenericUI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UIClientV2.Models
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
    }
}
