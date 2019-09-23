using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;

namespace DD.DomainGenerator.Actions.Domains.Schemas
{
    public class ModifyDomainSchema : ActionBase
    {
        public const string ActionName = "ModifyDomainSchema";
        public ActionParameterDefinition DomainNameParameter { get; set; }
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition AddIdParameter { get; set; }
        public ActionParameterDefinition AddStateParameter { get; set; }
        public ActionParameterDefinition AddDatesParameter { get; set; }
        public ActionParameterDefinition AddUserRelationshipParameter { get; set; }
        public ActionParameterDefinition AddOwnerParameter { get; set; }
        public ModifyDomainSchema() : base(ActionName)
        {
            DomainNameParameter = new ActionParameterDefinition(
                "domainname", ActionParameterDefinition.TypeValue.String, "Domain name", "d")
            { IsDomainSuggestion = true };

            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Name", "n");

            AddIdParameter = new ActionParameterDefinition(
               "addid", ActionParameterDefinition.TypeValue.Boolean, "Add id to the schema. It will add 'Id' attribute with GUID and primary key. Default value = false", "i");

            AddStateParameter = new ActionParameterDefinition(
               "addstate", ActionParameterDefinition.TypeValue.Boolean, "Add state to the schema. It will add 'State' attribute with values 1 enbled, 0 disabled. Default value = false", "s");

            AddUserRelationshipParameter = new ActionParameterDefinition(
              "adduserrelationship", ActionParameterDefinition.TypeValue.Boolean, "Add user relationships to schema. It will add 'CreatedBy' and 'ModifiedBy' forening keys to the schema, related with 'User' schema (must be created first). Default value = false", "u");

            AddDatesParameter = new ActionParameterDefinition(
               "adddates", ActionParameterDefinition.TypeValue.Boolean, "Add dates to schema. It will add 'CreatedOn' and 'ModifiedOn' of DateTime type. Default value = false", "d");

            AddOwnerParameter = new ActionParameterDefinition(
               "addowner", ActionParameterDefinition.TypeValue.Boolean, "Add owner to schema. It will add 'Owner'. Default value = false", "o");

            ActionParametersDefinition.Add(DomainNameParameter);
            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(AddIdParameter);
            ActionParametersDefinition.Add(AddDatesParameter);
            ActionParametersDefinition.Add(AddStateParameter);
            ActionParametersDefinition.Add(AddOwnerParameter);
            ActionParametersDefinition.Add(AddUserRelationshipParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, DomainNameParameter);
        }
        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var domainName = GetStringParameterValue(parameters, DomainNameParameter, string.Empty).ToWordPascalCase();
            var name = GetStringParameterValue(parameters, NameParameter, string.Empty).ToWordPascalCase();

            var addId = GetBoolParameterValue(parameters, AddIdParameter, false);
            var addState = GetBoolParameterValue(parameters, AddStateParameter, false);
            var addDates = GetBoolParameterValue(parameters, AddDatesParameter, false);
            var addUserRelationship = GetBoolParameterValue(parameters, AddUserRelationshipParameter, false);
            var addOwner = GetBoolParameterValue(parameters, AddOwnerParameter, false);

            var domain = Domain.FindChildDomain(project.Domain, domainName);
            if (domain == null)
            {
                throw new Exception($"Can't find any domain named '{domainName}'");
            }
            if (!domain.HasModel)
            {
                throw new Exception($"Domain named '{domainName}' cannot be modified. Initialize the schema domain first");
            }
            if (!string.IsNullOrEmpty(name))
            {
                domain.Schema.Name = name;
            }
            if (addId && !domain.Schema.HasId)
            {
                domain.Schema.HasId = true;
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Id, SchemaModelProperty.PropertyTypes.PrimaryKey)
                { IsPrimaryKey = true });
            }
            if (addState && !domain.Schema.HasState)
            {
                domain.Schema.HasState = true;
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.State, SchemaModelProperty.PropertyTypes.State));
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Status, SchemaModelProperty.PropertyTypes.Status));
            }
            if (addDates && !domain.Schema.HasDates)
            {
                domain.Schema.HasDates = true;
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.CreatedOn, SchemaModelProperty.PropertyTypes.DateTime));
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.ModifiedOn, SchemaModelProperty.PropertyTypes.DateTime));
            }
            if (addOwner && !domain.Schema.HasOwner)
            {
                var userDomain = Domain.FindChildDomain(project.Domain, Definitions.DefaultBasicDomainNames.User);
                if (userDomain == null)
                {
                    throw new Exception("Can't add owner because 'User' domain doesn't exists");
                }
                if (!userDomain.HasModel)
                {
                    throw new Exception("Can't add user relationship because 'User' domain doesn't contain schema yet");
                }
                domain.Schema.HasOwner = true;
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Owner, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userDomain.Schema });
            }
            if (addUserRelationship && !domain.Schema.HasUserRelationship)
            {
                var userDomain = Domain.FindChildDomain(project.Domain, Definitions.DefaultBasicDomainNames.User);
                if (userDomain == null)
                {
                    throw new Exception("Can't add user relationship because 'User' domain doesn't exists");
                }
                if (!userDomain.HasModel)
                {
                    throw new Exception("Can't add user relationship because 'User' domain doesn't contain schema yet");
                }
                domain.Schema.HasUserRelationship = true;
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.CreatedBy, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userDomain.Schema });
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.ModifiedOn, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userDomain.Schema });
            }
        }

    }
}
