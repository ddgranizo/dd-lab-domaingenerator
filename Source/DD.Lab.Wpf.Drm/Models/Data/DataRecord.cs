using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models.Data
{
    public class DataRecord
    {
        public Guid Id { get; set; }
        public Dictionary<string, object> Values { get; set; }

        public DataRecord(Guid id, Dictionary<string, object> values)
        {
            Id = id;
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }
    }
}
