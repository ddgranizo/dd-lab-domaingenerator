using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.Lab.Wpf.Drm.Extensions
{
    public static class DataSetModelExtensions
    {
        public static DataSet ToDisplayableDataSet(this DataSet dataSetModel, Entity entity)
        {
            var set = new DataSet(entity.LogicalName);
            foreach (var row in dataSetModel.Values)
            {
                var rowModel = new DataRecord(row.Id, new Dictionary<string, object>());
                foreach (var item in row.Values)
                {
                    var attributeDefinition = GetAttribute(entity, item.Key);
                    if (attributeDefinition != null)
                    {
                        rowModel.Values.Add(item.Key, GenericObjectToDisplayable(item.Value, attributeDefinition));
                    }
                }
                set.Values.Add(rowModel);
            }
            return set;
        }

        private static Models.Attribute GetAttribute(Entity entity, string logicalName)
        {
            return entity.Attributes.FirstOrDefault(k => k.LogicalName == logicalName);
        }

        private static string GenericObjectToDisplayable(object data, Models.Attribute attribute)
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
                var option = attribute.Options.FirstOrDefault(k => k.Value == ((OptionSetValue)data).Value);
                return option?.DisplayName ?? string.Empty;
            }
            else if (data is bool)
            {
                return (bool)data ? "Yes" : "No";
            }
            else
            {
                return attribute.IsCustomAttribute
                    ? string.Empty
                    : data.ToString();
            }
        }
    }
}
