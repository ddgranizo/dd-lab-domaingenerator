using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models.Data
{
    public class DataSet
    {
        public List<DataRecord> Values { get; set; }

        public DataSet()
        {
            Values = new List<DataRecord>();
        }
    }
}
