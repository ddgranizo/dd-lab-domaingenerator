using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Models.Inputs;
using DomainGeneratorUI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static DD.Lab.Wpf.Drm.Models.Attribute;

namespace DomainGeneratorUI
{
    public class BusinessWorkflowManager
    {
        public GenericManager GenericManager { get; set; }

        public BusinessWorkflowManager(GenericManager genericManager)
        {
            GenericManager = genericManager ?? throw new ArgumentNullException(nameof(genericManager));
            Initialize();
        }

        private void Initialize()
        {
            InitializeSchemaWorkflows();
        }

        private void InitializeSchemaWorkflows()
        {
            GenericManager.RegisterNewCreateWorkflow("Schema", (genericManager, input) =>
            {
                var id = (Guid)input.Values["Id"];
                var record = genericManager.Retrieve("Schema", id);
                var schemaObject = Entity.DictionartyToEntity<Schema>(record.Values);

                var propertyEntityLogicalName = "Property";
                var idProperty = new Property()
                {
                    IsPrimaryKey = true,
                    Name = "Id",
                    Type = new OptionSetValue((int)Property.PropertyType.PrimaryKey),
                    SchemaId = new EntityReferenceValue(id, schemaObject.Name)
                };
                var nameProperty = new Property()
                {
                    Length = 200,
                    Name = "Name",
                    Type = new OptionSetValue((int)Property.PropertyType.String),
                    SchemaId = new EntityReferenceValue(id, schemaObject.Name)
                };
                var createdOnProperty = new Property()
                {
                    Name = "CreatedOn",
                    Type = new OptionSetValue((int)Property.PropertyType.DateTime),
                    SchemaId = new EntityReferenceValue(id, schemaObject.Name)
                };
                var modifiedOnProperty = new Property()
                {
                    Name = "ModifiedOn",
                    Type = new OptionSetValue((int)Property.PropertyType.DateTime),
                    SchemaId = new EntityReferenceValue(id, schemaObject.Name)
                };
                
                genericManager.Create(propertyEntityLogicalName, Entity.EntityToDictionary(idProperty));
                genericManager.Create(propertyEntityLogicalName, Entity.EntityToDictionary(nameProperty));
                genericManager.Create(propertyEntityLogicalName, Entity.EntityToDictionary(createdOnProperty));
                genericManager.Create(propertyEntityLogicalName, Entity.EntityToDictionary(modifiedOnProperty));

                if (schemaObject.HasState)
                {
                    var stateProperty = new Property()
                    {
                        Name = "State",
                        Type = new OptionSetValue((int)Property.PropertyType.State),
                        SchemaId = new EntityReferenceValue(id, schemaObject.Name)
                    };

                    var statusProperty = new Property()
                    {
                        Name = "Status",
                        Type = new OptionSetValue((int)Property.PropertyType.Status),
                        SchemaId = new EntityReferenceValue(id, schemaObject.Name)
                    };

                    genericManager.Create(propertyEntityLogicalName, Entity.EntityToDictionary(stateProperty));
                    genericManager.Create(propertyEntityLogicalName, Entity.EntityToDictionary(statusProperty));
                }

                if (schemaObject.HasOwner)
                {
                    var ownerProperty = new Property()
                    {
                        Name = "Owner",
                        Type = new OptionSetValue((int)Property.PropertyType.ForeingKey),
                        SchemaId = new EntityReferenceValue(id, schemaObject.Name)
                    };
                    genericManager.Create(propertyEntityLogicalName, Entity.EntityToDictionary(ownerProperty));
                }

                if (true)
                {

                }

            });
        }
    }
}
