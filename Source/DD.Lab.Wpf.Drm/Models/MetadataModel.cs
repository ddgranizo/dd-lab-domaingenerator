using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models
{
    public class MetadataModel
    {

        public string MainEntity { get; set; }

        public MetadataModel()
        {
            Entities = new List<Entity>();
            Relationships = new List<Relationship>();
        }

        public List<Entity> Entities { get; set; }
        public List<Relationship> Relationships { get; set; }

        public void AddEntity<T>(T instance)
        {
            var entity = new Entity();
            entity.DisplayName = instance.GetType().Name;
            entity.LogicalName = instance.GetType().Name;
            foreach (var item in instance.GetType().GetProperties())
            {
                var type = Attribute.GetAttributeTypeFromPropertyInfo(item);
                var isMandatory = Attribute.GetMandatoryFromPropertyInfo(item);
                var description = Attribute.GetDescriptionFromPropertyInfo(item);
                var attribute = new Attribute(type, item.Name, item.Name, description ?? item.Name, isMandatory);
                if (type == Attribute.AttributeType.OptionSet)
                {
                    attribute.Options = Attribute.GetOptionsFromPropertyInfo(instance.GetType(), item);
                }
                entity.Attributes.Add(attribute);
            }
            Entities.Add(entity);
            if (string.IsNullOrEmpty(MainEntity))
            {
                MainEntity = entity.LogicalName;
            }
        }


        public void AddRelationship<U, V>(U mainInstance, V referencedInstance, string referencedAttribute = null)
        {

            var mainEntity = mainInstance.GetType().Name;
            var mainEntityObject = Entities.FirstOrDefault(l => l.LogicalName == mainEntity);
            if (mainEntityObject == null)
            {
                throw new Exception("Add both entities first");
            }
            var realReferencedAttribute = referencedAttribute ?? $"{mainInstance.GetType().Name}Id";
            var referencedEntity = referencedInstance.GetType().Name;
            var referencedEntityObject = Entities.FirstOrDefault(l => l.LogicalName == referencedEntity);
            if (referencedEntityObject == null)
            {
                throw new Exception("Add both entities first");
            }
            var attribute = referencedEntityObject.Attributes.FirstOrDefault(k => k.LogicalName == realReferencedAttribute);
            Relationships.Add(new Relationship(mainEntity, referencedEntity, realReferencedAttribute));
        }

        public void AddManyTwoManyRelationship<U, V>(U firstInstance, V secondInstance)
        {
            var firstEntity = firstInstance.GetType().Name;
            var firstEntityObject = Entities.FirstOrDefault(l => l.LogicalName == firstEntity);
            if (firstEntityObject == null)
            {
                throw new Exception("Add both entities first");
            }
            var secondEntity = secondInstance.GetType().Name;
            var secondEntityObject = Entities.FirstOrDefault(l => l.LogicalName == secondEntity);
            if (secondEntityObject == null)
            {
                throw new Exception("Add both entities first");
            }
            var intersectionName = $"{firstEntity}{secondEntity}";
            Relationships.Add(new Relationship(firstEntity, secondEntity, null)
                {
                    IsManyToMany = true,
                    IntersectionName = intersectionName
            });
        }
    }
}
