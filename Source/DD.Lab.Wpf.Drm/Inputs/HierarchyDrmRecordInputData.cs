using DD.Lab.Wpf.Drm.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Inputs
{
    public class HierarchyDrmRecordInputData
    {
        public GenericManager GenericManager { get; set; }
        public DataRecord Record { get; set; }
        public string ParentContextEntity { get; set; }
        public Guid ParentContextEntityId { get; set; }
        public string TargetEntityLogicalName { get; set; }
        public string ContextEntity { get; set; }
        public Guid ContextEntityId { get; set; }
    }
}
