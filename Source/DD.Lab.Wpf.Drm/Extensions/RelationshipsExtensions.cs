using DD.Lab.Wpf.Drm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.Lab.Wpf.Drm.Extensions
{
    public static class RelationshipsExtensions
    {
        public static SubGridRelationshipData ToSubGridRelationshipData(
            this Relationship relationship,
            Entity contextEntity,
            List<Entity> entities,
            Guid mainEntityId,
            string mainEntityRecordDisplayName)
        {
            var data = new SubGridRelationshipData();
            data.Relationship = relationship;
            data.MainEntityId = mainEntityId;
            data.MainEntityRecordDisplayName = mainEntityRecordDisplayName;
            var otherContextEntityLogicalName = contextEntity.LogicalName == relationship.MainEntity
                    ? relationship.RelatedEntity
                    : relationship.MainEntity;

            data.RelatedEntity = entities.First(k => k.LogicalName == otherContextEntityLogicalName);
            data.MainEntityDisplayName = GetEntityDisplayName(relationship.MainEntity, entities);
            data.RelatedEntityDisplayName = GetEntityDisplayName(relationship.RelatedEntity, entities);
            if (!relationship.IsManyToMany)
            {
                data.RelatedAttributeDisplayName =
                    entities.First(k => k.LogicalName == relationship.RelatedEntity)
                        .Attributes.First(k => k.LogicalName == relationship.RelatedAttribute).DisplayName;
            }
            else
            {
                data.IntersectionDisplayableEntity = relationship.MainEntity == contextEntity.LogicalName
                    ? GetEntityDisplayName(relationship.RelatedEntity, entities)
                    : GetEntityDisplayName(relationship.MainEntity, entities);
            }
            return data;
        }

        private static string GetEntityDisplayName(string logicalName, List<Entity> entities)
        {
            return entities.First(k => k.LogicalName == logicalName).DisplayName;
        }

    }
}
