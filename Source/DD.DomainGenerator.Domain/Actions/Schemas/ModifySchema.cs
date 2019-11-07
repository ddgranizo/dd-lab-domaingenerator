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
    public class ModifySchema : ActionBase
    {
        public const string ActionName = "ModifySchema";
        public ActionParameterDefinition SchemaNameParameter { get; set; }
        public ActionParameterDefinition AddIdParameter { get; set; }
        public ActionParameterDefinition AddStateParameter { get; set; }
        public ActionParameterDefinition AddDatesParameter { get; set; }
        public ActionParameterDefinition AddUserRelationshipParameter { get; set; }
        public ActionParameterDefinition AddOwnerParameter { get; set; }
        public ModifySchema() : base(ActionName)
        {
            SchemaNameParameter = new ActionParameterDefinition(
                "schemaname", ActionParameterDefinition.TypeValue.String, "Domain name", "s", string.Empty)
            { IsSchemaSuggestion = true };

            AddIdParameter = new ActionParameterDefinition(
               "addid", ActionParameterDefinition.TypeValue.Boolean, "Add id to the schema. It will add 'Id' attribute with GUID and primary key. Default value = false", "i", false);

            AddStateParameter = new ActionParameterDefinition(
               "addstate", ActionParameterDefinition.TypeValue.Boolean, "Add state to the schema. It will add 'State' attribute with values 1 enbled, 0 disabled. Default value = false", "st", false);

            AddUserRelationshipParameter = new ActionParameterDefinition(
              "adduserrelationship", ActionParameterDefinition.TypeValue.Boolean, "Add user relationships to schema. It will add 'CreatedBy' and 'ModifiedBy' forening keys to the schema, related with 'User' schema (must be created first). Default value = false", "u", false);

            AddDatesParameter = new ActionParameterDefinition(
               "adddates", ActionParameterDefinition.TypeValue.Boolean, "Add dates to schema. It will add 'CreatedOn' and 'ModifiedOn' of DateTime type. Default value = false", "d", false);

            AddOwnerParameter = new ActionParameterDefinition(
               "addowner", ActionParameterDefinition.TypeValue.Boolean, "Add owner to schema. It will add 'Owner'. Default value = false", "o", false);

            ActionParametersDefinition.Add(SchemaNameParameter);
            ActionParametersDefinition.Add(AddIdParameter);
            ActionParametersDefinition.Add(AddDatesParameter);
            ActionParametersDefinition.Add(AddStateParameter);
            ActionParametersDefinition.Add(AddOwnerParameter);
            ActionParametersDefinition.Add(AddUserRelationshipParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, SchemaNameParameter);
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var schemaName = GetStringParameterValue(parameters, SchemaNameParameter).ToWordPascalCase();

            var addId = GetBoolParameterValue(parameters, AddIdParameter);
            var addState = GetBoolParameterValue(parameters, AddStateParameter);
            var addDates = GetBoolParameterValue(parameters, AddDatesParameter);
            var addUserRelationship = GetBoolParameterValue(parameters, AddUserRelationshipParameter);
            var addOwner = GetBoolParameterValue(parameters, AddOwnerParameter);
            var schema = project.GetSchema(schemaName);
            if (schema == null)
            {
                throw new Exception($"Can't find any schema named '{schemaName}'");
            }
           
            if (addId && !schema.HasId)
            {
                schema.HasId = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Id, SchemaModelProperty.PropertyTypes.PrimaryKey)
                { IsPrimaryKey = true });
            }
            if (addState && !schema.HasState)
            {
                schema.HasState = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.State, SchemaModelProperty.PropertyTypes.State));
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Status, SchemaModelProperty.PropertyTypes.Status));
            }
            if (addDates && !schema.HasDates)
            {
                schema.HasDates = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.CreatedOn, SchemaModelProperty.PropertyTypes.DateTime));
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.ModifiedOn, SchemaModelProperty.PropertyTypes.DateTime));
            }
            if (addOwner && !schema.HasOwner)
            {
                var userSchema = project.GetSchema(Definitions.DefaultBasicDomainNames.User);
                if (userSchema == null)
                {
                    throw new Exception("Can't add user relationship because 'User' domain doesn't exists");
                }
                schema.HasOwner = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Owner, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userSchema });
            }
            if (addUserRelationship && !schema.HasUserRelationship)
            {
                var userSchema = project.GetSchema(Definitions.DefaultBasicDomainNames.User);
                if (userSchema == null)
                {
                    throw new Exception("Can't add user relationship because 'User' domain doesn't exists");
                }
                schema.HasUserRelationship = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.CreatedBy, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userSchema });
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.ModifiedOn, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userSchema });
            }

            OverrideOutputParameter(SchemaNameParameter.Name, schemaName);
        }

    }
}
