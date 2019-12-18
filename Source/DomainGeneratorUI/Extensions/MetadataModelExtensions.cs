using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Extensions
{

    public static class MetadataModelExtensions
    {
        public static void ValidateModel(this MetadataModel model)
        {

        }

        public static MetadataModel CompleteModel(this MetadataModel model)
        {
            foreach (var entity in model.Entities)
            {
                AddIdAttribute(entity);
                AddNameAttribute(entity);
            }
            return model;
        }

        private static void AddIdAttribute(Entity entity)
        {
            var idAttribute = entity.Attributes.FirstOrDefault(k => k.LogicalName == "Id");
            if (idAttribute == null)
            {
                entity.Attributes.Insert(0, new DD.Lab.Wpf.Drm.Models.Attribute()
                {
                    Description = "Id",
                    DisplayName = "Id",
                    LogicalName = "Id",
                    IsMandatory = false,
                    Type = DD.Lab.Wpf.Drm.Models.Attribute.AttributeType.Guid
                });
            }
        }

        private static void AddNameAttribute(Entity entity)
        {
            var nameAttribute = entity.Attributes.FirstOrDefault(k => k.LogicalName == "Name");
            if (nameAttribute == null)
            {
                entity.Attributes.Insert(1, new DD.Lab.Wpf.Drm.Models.Attribute()
                {
                    Description = "Name",
                    DisplayName = "Name",
                    LogicalName = "Name",
                    IsMandatory = true,
                    Type = DD.Lab.Wpf.Drm.Models.Attribute.AttributeType.String
                });
            }
        }
    }
}
