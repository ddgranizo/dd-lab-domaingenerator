using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models.Data
{
    public class DataSetModel
    {
        public List<DataRowModel> Values { get; set; }

        public DataSetModel()
        {
            Values = new List<DataRowModel>();
        }
    }
}
