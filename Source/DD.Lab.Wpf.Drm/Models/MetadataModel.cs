using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models
{
    public class MetadataModel
    {

        public string MainEntity { get; set; }

        public MetadataModel()
        {
            Entities = new List<Entity>();
            Relationships = new List<Relationship>();
        }

        public List<Entity> Entities { get; set; }
        public List<Relationship> Relationships { get; set; }
    }
}
