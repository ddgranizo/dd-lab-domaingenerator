using DD.Lab.Wpf.Drm.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Inputs
{
    public class HierarchyDrmRecordCollectionInputData
    {
        public GenericManager GenericManager { get; set; }
        public List<DataRecord> Records { get; set; }
        public string ParentContextEntity { get; set; }
        public Guid ParentContextEntityId { get; set; }
        public string TargetEntityLogicalName { get; set; }
        public string ContextEntity { get; set; }
    }
}
