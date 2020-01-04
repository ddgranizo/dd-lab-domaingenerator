using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Models.Workflows;
using DD.Lab.Wpf.Drm.Services;
using DD.Lab.Wpf.Models.Inputs;
using DomainGeneratorUI.Extensions;
using DomainGeneratorUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Workflows.Schemas
{
    public class CreateMainPropertiesWorkflow : IWorkflowAction
    {

        public EntityReferenceValue SchemaEntityReference { get; set; }
        public EntityReferenceValue RepositoryEntityReference { get; set; }
        public EntityReferenceValue UserEntityReference { get; set; }

        public CreateMainPropertiesWorkflow()
        {
        }

        public void Execute(GenericManager manager, WorkflowInputParameter inputParameter)
        {

            var id = (Guid)inputParameter.Values["Id"];
            var record = manager.Retrieve("Schema", id);
            var schemaObject = Entity.DictionartyToEntity<Schema>(record.Values);
            var schemaName = schemaObject.Name;
            SchemaEntityReference = new EntityReferenceValue(id, Schema.LogicalName, schemaName);


            var repositoryName = $"I{schemaObject.Name}MainRepository";
            var repositoryId = CreateMainRepository(manager, repositoryName);
            RepositoryEntityReference = new EntityReferenceValue(repositoryId, Repository.LogicalName, repositoryName);

            CreateIdAttribute(manager);
            CreateNameAttribute(manager);
            CreateDateAttributes(manager);

            if (schemaObject.HasState)
            {
                CreateStateAttributes(manager);
            }

            if (schemaObject.HasOwner)
            {
                CreateOwnerAttribute(manager);
            }

            if (schemaObject.HasUserRelationship)
            {
                CreateUserAttributes(manager);
            }

            CreateMainModel(manager, schemaName);

            CreateCrudServices(manager);
        }

        private void CreateMainModel(GenericManager manager, string name)
        {
            var model = new Model()
            {
                AllProperties = true,
                SchemaId = SchemaEntityReference,
                IsMainModel = true,
                Name = name,
            };
            manager.Create(Model.LogicalName, Entity.EntityToDictionary(model));
        }

        private void CreateCrudServices(GenericManager manager)
        {
            var createMehtodId = CreateRepositoryMethod(manager, "Create",
                RepositoryMethod.RepositoryMethodType.Create,
                new MethodParameter[] {
                    new MethodParameter(){
                        Name = "Entity",
                        Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Entity),
                    }
                },
                new MethodParameter()
                {
                    Name = "Id",
                    Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Guid),
                });

            var updateMehtodId = CreateRepositoryMethod(manager, "Update",
                RepositoryMethod.RepositoryMethodType.Update,
                new MethodParameter[] {
                    new MethodParameter(){
                        Name = "Entity",
                        Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Entity),
                    }
                }, null);

            var deleteMethodId = CreateRepositoryMethod(manager, "DeleteByPk",
                RepositoryMethod.RepositoryMethodType.DeleteByPk,
                new MethodParameter[] {
                    new MethodParameter(){
                        Name = "Id",
                        Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Guid),
                    }
                }, null);

            var retrieveMehtodId = CreateRepositoryMethod(manager, "RetrieveByPk",
                RepositoryMethod.RepositoryMethodType.RetrieveByPk,
                new MethodParameter[] {
                    new MethodParameter()
                {
                    Name = "Id",
                    Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Guid),
                }},
                new MethodParameter()
                {
                    Name = "Entity",
                    Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Entity),
                });


        }


        private void CreateUserAttributes(GenericManager manager)
        {
            var createdByProperty = new Property()
            {
                Name = "CreatedBy",
                Type = new OptionSetValue((int)Property.PropertyType.EntityReference),
                //EntityReferenceSchemaId = UserEntityReference,
                SchemaId = SchemaEntityReference
            };
            manager.Create(Property.LogicalName, Entity.EntityToDictionary(createdByProperty));
            var modifiedByProperty = new Property()
            {
                Name = "ModifiedBy",
                Type = new OptionSetValue((int)Property.PropertyType.EntityReference),
                //EntityReferenceSchemaId = UserEntityReference,
                SchemaId = SchemaEntityReference
            };
            manager.Create(Property.LogicalName, Entity.EntityToDictionary(createdByProperty));
            manager.Create(Property.LogicalName, Entity.EntityToDictionary(modifiedByProperty));

            CreateDefaultViewRepositoryMethod(manager, "GetByCreatedBy", new MethodParameter[] {
                new MethodParameter(){
                    Name = "Id",
                    Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Guid),
                }
            });

            CreateDefaultViewRepositoryMethod(manager, "GetByModifiedBy", new MethodParameter[] {
                new MethodParameter(){
                    Name = "Id",
                    Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Guid),
                }
            });
        }

        private void CreateOwnerAttribute(GenericManager manager)
        {
            var ownerProperty = new Property()
            {
                Name = "Owner",
                Type = new OptionSetValue((int)Property.PropertyType.EntityReference),
                SchemaId = SchemaEntityReference,
                //EntityReferenceSchemaId = UserEntityReference,
            };
            manager.Create(Property.LogicalName, Entity.EntityToDictionary(ownerProperty));

            CreateDefaultViewRepositoryMethod(manager, "GetByOwner", new MethodParameter[] {
                new MethodParameter(){
                    Name = "Id",
                    Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Guid),
                }
            });
        }

        private Guid CreateMainRepository(GenericManager genericManager, string repositoryName)
        {
            var repo = new Repository()
            {
                IsMainRepository = true,
                Name = repositoryName,
                SchemaId = SchemaEntityReference
            };
            return genericManager.Create(Repository.LogicalName, Entity.EntityToDictionary(repo));
        }

        private void CreateNameAttribute(GenericManager genericManager)
        {
            var nameProperty = new Property()
            {
                Length = 200,
                Name = "Name",
                Type = new OptionSetValue((int)Property.PropertyType.String),
                SchemaId = SchemaEntityReference
            };
            genericManager.Create(Property.LogicalName, Entity.EntityToDictionary(nameProperty));
        }

        private void CreateIdAttribute(GenericManager genericManager)
        {
            var idProperty = new Property()
            {
                IsPrimaryKey = true,
                Name = "Id",
                Type = new OptionSetValue((int)Property.PropertyType.PrimaryKey),
                SchemaId = SchemaEntityReference
            };
            genericManager.Create(Property.LogicalName, Entity.EntityToDictionary(idProperty));
        }



        private void CreateStateAttributes(GenericManager genericManager)
        {
            var stateProperty = new Property()
            {
                Name = "State",
                Type = new OptionSetValue((int)Property.PropertyType.State),
                SchemaId = SchemaEntityReference
            };

            var statusProperty = new Property()
            {
                Name = "Status",
                Type = new OptionSetValue((int)Property.PropertyType.Status),
                SchemaId = SchemaEntityReference
            };

            genericManager.Create(Property.LogicalName, Entity.EntityToDictionary(stateProperty));
            genericManager.Create(Property.LogicalName, Entity.EntityToDictionary(statusProperty));

            _ = CreateDefaultZeroInputsViewRepositoryMethod(genericManager, "GetActive");
            _ = CreateDefaultZeroInputsViewRepositoryMethod(genericManager, "GetInactive");
        }


        private void CreateDateAttributes(GenericManager genericManager)
        {
            var createdOnProperty = new Property()
            {
                Name = "CreatedOn",
                Type = new OptionSetValue((int)Property.PropertyType.DateTime),
                SchemaId = SchemaEntityReference
            };
            var modifiedOnProperty = new Property()
            {
                Name = "ModifiedOn",
                Type = new OptionSetValue((int)Property.PropertyType.DateTime),
                SchemaId = SchemaEntityReference
            };
            genericManager.Create(Property.LogicalName, Entity.EntityToDictionary(createdOnProperty));
            genericManager.Create(Property.LogicalName, Entity.EntityToDictionary(modifiedOnProperty));

            CreateDefaultViewRepositoryMethod(genericManager, "GetByCreatedOnAtYear", new MethodParameter[] {
                new MethodParameter(){
                    Name = "Year",
                    Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Integer),
                }
            });
            //TODO other methods
        }

        private Guid CreateRepositoryMethod(
            GenericManager genericManager,
            string methodName,
            RepositoryMethod.RepositoryMethodType type,
            MethodParameter[] inputParameters,
            MethodParameter outputParameter)
        {
            var method = new RepositoryMethod()
            {
                Name = methodName,
                RepositoryId = RepositoryEntityReference,
                Type = new OptionSetValue((int)type)
            };
            var methodId = genericManager.Create(RepositoryMethod.LogicalName, Entity.EntityToDictionary(method));
            _ = CreateDefaultViewOutputMethodParameters(genericManager, methodId, methodName);

            foreach (var item in inputParameters)
            {
                item.RepositoryMethodId = new EntityReferenceValue(methodId, RepositoryMethod.LogicalName, methodName);
                item.Direction = new OptionSetValue((int)MethodParameter.ParameterDirection.Input);
                _ = genericManager.Create(MethodParameter.LogicalName, Entity.EntityToDictionary(item));
            }

            if (outputParameter != null)
            {
                outputParameter.RepositoryMethodId = new EntityReferenceValue(methodId, RepositoryMethod.LogicalName, methodName);
                outputParameter.Direction = new OptionSetValue((int)MethodParameter.ParameterDirection.Output);
                _ = genericManager.Create(MethodParameter.LogicalName, Entity.EntityToDictionary(outputParameter));
            }

            return methodId;
        }

        private Guid CreateDefaultViewRepositoryMethod(GenericManager genericManager, string methodName, MethodParameter[] inputParameters)
        {
            var method = new RepositoryMethod()
            {
                Name = methodName,
                RepositoryId = RepositoryEntityReference,
                Type = new OptionSetValue((int)RepositoryMethod.RepositoryMethodType.View),
            };
            var methodId = genericManager.Create(RepositoryMethod.LogicalName, Entity.EntityToDictionary(method));
            _ = CreateDefaultViewOutputMethodParameters(genericManager, methodId, methodName);

            foreach (var item in inputParameters)
            {
                item.RepositoryMethodId = new EntityReferenceValue(methodId, RepositoryMethod.LogicalName, methodName);
                item.Direction = new OptionSetValue((int)MethodParameter.ParameterDirection.Input);
                _ = genericManager.Create(MethodParameter.LogicalName, Entity.EntityToDictionary(item));
            }

            return methodId;
        }


        private Guid CreateDefaultZeroInputsViewRepositoryMethod(GenericManager genericManager, string methodName)
        {
            var method = new RepositoryMethod()
            {
                Name = methodName,
                Type = new OptionSetValue((int)RepositoryMethod.RepositoryMethodType.View),
                RepositoryId = RepositoryEntityReference,
            };
            var methodId = genericManager.Create(Repository.LogicalName, Entity.EntityToDictionary(method));
            _ = CreateDefaultViewOutputMethodParameters(genericManager, methodId, methodName);
            return methodId;
        }



        private Guid CreateDefaultViewOutputMethodParameters(GenericManager genericManager, Guid methodId, string methodName)
        {
            var parameter = new MethodParameter()
            {
                Type = new OptionSetValue((int)MethodParameter.ParameterInputType.Enumerable),
                EnumerableType = new OptionSetValue((int)MethodParameter.ParameterInputType.Entity),
                Direction = new OptionSetValue((int)MethodParameter.ParameterDirection.Output),
                Name = "Collection",
                RepositoryMethodId = new EntityReferenceValue(methodId, RepositoryMethod.LogicalName, methodName),
            };
            return genericManager.Create(MethodParameter.LogicalName, Entity.EntityToDictionary(parameter));
        }
    }
}
