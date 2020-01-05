using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models
{
    public class DrmGridInputData
    {
        public GenericManager GenericManager { get; set; }
        public GenericEventManager GenericEventManager { get; set; }
        public Entity Entity { get; set; }
        public WpfEventManager WpfEventManager { get; set; }
        public List<Relationship> Relationships { get; set; }
        public Relationship FilterRelationship { get; set; }
        public Guid FilterRelationshipId { get; set; }
        public string FilterRelationshipRecordDisplayName { get; set; }
    }
}
