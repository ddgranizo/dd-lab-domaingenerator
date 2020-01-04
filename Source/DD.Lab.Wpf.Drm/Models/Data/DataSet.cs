using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models.Data
{
    public class DataSet
    {
        public string EntityLogicalName { get; set; }
        public List<DataRecord> Values { get; set; }

        public DataSet(string entityLogicalName)
        {
            Values = new List<DataRecord>();
            EntityLogicalName = entityLogicalName ?? throw new ArgumentNullException(nameof(entityLogicalName));
        }

        public DataSet()
        {
            Values = new List<DataRecord>();
        }
    }
}
