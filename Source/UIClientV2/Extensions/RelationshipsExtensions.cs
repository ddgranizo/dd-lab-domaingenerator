using DD.Lab.GenericUI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClientV2.Models;

namespace UIClientV2.Extensions
{
    public static class RelationshipsExtensions
    {
        public static SubGridRelationshipData ToSubGridRelationshipData(this Relationship relationship, Entity contextEntity,  List<Entity> entities, Guid mainEntityId)
        {
            var data = new SubGridRelationshipData();
            data.Relationship = relationship;
            data.MainEntityId = mainEntityId;

            var otherContextEntityLogicalName = contextEntity.LogicalName == relationship.MainEntity
                    ? relationship.RelatedEntity
                    : relationship.MainEntity;

            data.RelatedEntity = entities.First(k=>k.LogicalName == otherContextEntityLogicalName);
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
