using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;

namespace DD.DomainGenerator.Actions.Schemas
{
    public class AddSchema : ActionBase
    {
        public const string ActionName = "AddSchema";
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition HasIdParameter { get; set; }
        public ActionParameterDefinition HasStateParameter { get; set; }
        public ActionParameterDefinition HasDatesParameter { get; set; }
        public ActionParameterDefinition HasUserRelationshipParameter { get; set; }
        public ActionParameterDefinition HasOwnerParameter { get; set; }
        public ActionParameterDefinition AddCRUDUseCasesParameter { get; set; }

        public AddSchema() : base(ActionName)
        {

            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Schema name. Recomended to use same domain name but not in plural. The name will be converted to PascalCase", "n", string.Empty);

            HasIdParameter = new ActionParameterDefinition(
               "hasid", ActionParameterDefinition.TypeValue.Boolean, "The new schema has id. It will add 'Id' attribute with GUID and primary key. Default value = true", "i", true);

            HasStateParameter = new ActionParameterDefinition(
               "hasstate", ActionParameterDefinition.TypeValue.Boolean, "The new schema has state. It will add 'State' attribute with values 1 enbled, 0 disabled. Default value = false", "s", false);

            HasUserRelationshipParameter = new ActionParameterDefinition(
              "hasuserrelationship", ActionParameterDefinition.TypeValue.Boolean, "The new schema has user relationship. It will add 'CreatedBy' and 'ModifiedBy' forening keys to the schema, related with 'User' schema (must be created first). Default value = false", "u", false);

            HasDatesParameter = new ActionParameterDefinition(
               "hasdates", ActionParameterDefinition.TypeValue.Boolean, "The new schema has dates. It will add 'CreatedOn' and 'ModifiedOn' of DateTime type. Default value = false", "d", false);

            HasOwnerParameter = new ActionParameterDefinition(
              "hasowner", ActionParameterDefinition.TypeValue.Boolean, "The new schema has owner. It will add 'Owner'. Default value = false", "o", false);

            AddCRUDUseCasesParameter = new ActionParameterDefinition(
              "addcrudusecases", ActionParameterDefinition.TypeValue.Boolean, "Add CRUD use cases. Default value = yes", "a", true);

            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(HasIdParameter);
            ActionParametersDefinition.Add(HasDatesParameter);
            ActionParametersDefinition.Add(HasStateParameter);
            ActionParametersDefinition.Add(HasOwnerParameter);
            ActionParametersDefinition.Add(HasUserRelationshipParameter);
            ActionParametersDefinition.Add(AddCRUDUseCasesParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return  IsParamOk(parameters, NameParameter);
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter).ToWordPascalCase();
            var hasId = GetBoolParameterValue(parameters, HasIdParameter);
            var hasState = GetBoolParameterValue(parameters, HasStateParameter);
            var hasDates = GetBoolParameterValue(parameters, HasDatesParameter);
            var hasUserRelationship = GetBoolParameterValue(parameters, HasUserRelationshipParameter);
            var hasOwner = GetBoolParameterValue(parameters, HasOwnerParameter);
            var addCrudUseCases = GetBoolParameterValue(parameters, AddCRUDUseCasesParameter);
            var schema = new SchemaModel(name);
            if (hasId)
            {
                schema.HasId = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Id, SchemaModelProperty.PropertyTypes.PrimaryKey)
                { IsPrimaryKey = true });
            }

            if (hasState)
            {
                schema.HasState = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.State, SchemaModelProperty.PropertyTypes.State));
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Status, SchemaModelProperty.PropertyTypes.Status));
            }
            
            if (hasDates)
            {
                schema.HasDates = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.CreatedOn, SchemaModelProperty.PropertyTypes.DateTime));
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.ModifiedOn, SchemaModelProperty.PropertyTypes.DateTime));
            }
            if (hasOwner)
            {
                var userSchema =  project.Schemas.FirstOrDefault(k=>k.Name == Definitions.DefaultBasicDomainNames.User);
                if (userSchema == null)
                {
                    throw new Exception("Can't add user relationship because 'User' domain doesn't exists");
                }
                schema.HasOwner = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Owner, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userSchema });
            }
            if (hasUserRelationship)
            {
                var userSchema = project.Schemas.FirstOrDefault(k => k.Name == Definitions.DefaultBasicDomainNames.User);
                if (userSchema == null)
                {
                    throw new Exception("Can't add user relationship because 'User' domain doesn't exists");
                }
                schema.HasUserRelationship = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.CreatedBy, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userSchema });
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.ModifiedBy, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userSchema });
            }
            if (addCrudUseCases)
            {
                schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.Create));
                schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.DeleteByPk));
                schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveByPk));
                schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveMultiple));
                schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.Update));
            }
            project.Schemas.Add(schema);
            OverrideOutputParameter(NameParameter.Name, name);

        }
    }
}
