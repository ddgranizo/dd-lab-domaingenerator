using DD.Lab.GenericUI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIClientV2.Extensions
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
                entity.Attributes.Insert(0, new DD.Lab.GenericUI.Core.Models.Attribute()
                {
                    Description = "Id",
                    DisplayName = "Id",
                    LogicalName = "Id",
                    IsMandatory = false,
                    Type = DD.Lab.GenericUI.Core.Models.Attribute.AttributeType.Guid
                });
            }
        }

        private static void AddNameAttribute(Entity entity)
        {
            var nameAttribute = entity.Attributes.FirstOrDefault(k => k.LogicalName == "Name");
            if (nameAttribute == null)
            {
                entity.Attributes.Insert(1, new DD.Lab.GenericUI.Core.Models.Attribute()
                {
                    Description = "Name",
                    DisplayName = "Name",
                    LogicalName = "Name",
                    IsMandatory = true,
                    Type = DD.Lab.GenericUI.Core.Models.Attribute.AttributeType.String
                });
            }
        }
    }
}
