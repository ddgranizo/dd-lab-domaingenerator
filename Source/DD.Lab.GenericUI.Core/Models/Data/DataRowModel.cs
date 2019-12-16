using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.GenericUI.Core.Models.Data
{
    public class DataRowModel
    {
        public Guid Id { get; set; }
        public Dictionary<string, object> Values { get; set; }

        public DataRowModel(Guid id, Dictionary<string, object> values)
        {
            Id = id;
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }
    }
}
