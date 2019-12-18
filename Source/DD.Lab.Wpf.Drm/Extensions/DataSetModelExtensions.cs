using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Extensions
{
    public static class DataSetModelExtensions
    {
        public static DataSetModel ToDisplayableDataSet(this DataSetModel dataSetModel)
        {
            var set = new DataSetModel();
            foreach (var row in dataSetModel.Values)
            {
                var rowModel = new DataRowModel(row.Id, new Dictionary<string, object>());
                foreach (var item in row.Values)
                {
                    rowModel.Values.Add(item.Key, GenericObjectToDisplayable(item.Value));
                }
                set.Values.Add(rowModel);
            }
            return set;
        }


        private static string GenericObjectToDisplayable(object data)
        {
            if (data == null)
            {
                return string.Empty;
            }
            if (data is EntityReferenceValue)
            {
                return ((EntityReferenceValue)data).DisplayName;
            }
            else if (data is OptionSetValue)
            {
                return ((OptionSetValue)data).DisplayName;
            }
            else if (data is bool)
            {
                return (bool)data ? "Yes" : "No";
            }
            else
            {
                return data.ToString();
            }
        }
    }
}
