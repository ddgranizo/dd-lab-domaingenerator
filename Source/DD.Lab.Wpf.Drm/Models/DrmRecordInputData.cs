using System;
using System.Collections.Generic;
using System.Text;
using static DD.Lab.Wpf.Drm.Viewmodels.DrmControlViewModel;

namespace DD.Lab.Wpf.Drm.Models
{
    public class DrmRecordInputData
    {
        public GenericManager GenericManager { get; set; }
        public GenericEventManager GenericEventManager { get; set; }
        public Entity Entity { get; set; }
        public WpfEventManager WpfEventManager { get; set; }
        public DetailMode Mode { get; set; }
        public Dictionary<string, object> InitialValues { get; set; }
        public List<Entity> Entities { get; set; }
        public List<Relationship> Relationships { get; set; }
        
    }
}
