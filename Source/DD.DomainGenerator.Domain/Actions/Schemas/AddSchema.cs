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
        public ActionParameterDefinition DomainParameter { get; set; }
        public ActionParameterDefinition HasIdParameter { get; set; }
        public ActionParameterDefinition HasStateParameter { get; set; }
        public ActionParameterDefinition HasDatesParameter { get; set; }
        public ActionParameterDefinition HasUserRelationshipParameter { get; set; }
        public ActionParameterDefinition HasOwnerParameter { get; set; }

        public AddSchema() : base(ActionName)
        {

            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Schema name. Recomended to use same domain name but not in plural. The name will be converted to PascalCase", "n", string.Empty);

            DomainParameter = new ActionParameterDefinition(
                "domain", ActionParameterDefinition.TypeValue.String, "Domain name", "d", string.Empty)
            {
                IsDomainSuggestion = true,
            };

            HasStateParameter = new ActionParameterDefinition(
               "hasstate", ActionParameterDefinition.TypeValue.Boolean, "The new schema has state. It will add 'State' attribute with values 1 enbled, 0 disabled. Default value = false", "s", false);

            HasUserRelationshipParameter = new ActionParameterDefinition(
              "hasuserrelationship", ActionParameterDefinition.TypeValue.Boolean, "The new schema has user relationship. It will add 'CreatedBy' and 'ModifiedBy' forening keys to the schema, related with 'User' schema (must be created first). Default value = false", "u", false);

            HasDatesParameter = new ActionParameterDefinition(
               "hasdates", ActionParameterDefinition.TypeValue.Boolean, "The new schema has dates. It will add 'CreatedOn' and 'ModifiedOn' of DateTime type. Default value = false", "hd", false);

            HasOwnerParameter = new ActionParameterDefinition(
              "hasowner", ActionParameterDefinition.TypeValue.Boolean, "The new schema has owner. It will add 'Owner'. Default value = false", "o", false);



            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(DomainParameter);
            ActionParametersDefinition.Add(HasDatesParameter);
            ActionParametersDefinition.Add(HasStateParameter);
            ActionParametersDefinition.Add(HasOwnerParameter);
            ActionParametersDefinition.Add(HasUserRelationshipParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter);
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter).ToWordPascalCase();
            //var hasId = GetBoolParameterValue(parameters, HasIdParameter);
            var hasState = GetBoolParameterValue(parameters, HasStateParameter);
            var hasDates = GetBoolParameterValue(parameters, HasDatesParameter);
            var hasUserRelationship = GetBoolParameterValue(parameters, HasUserRelationshipParameter);
            var hasOwner = GetBoolParameterValue(parameters, HasOwnerParameter);

            var domainName = GetStringParameterValue(parameters, DomainParameter);

            var schema = new Schema(name);

            //if (hasId)
            //{
            schema.HasId = true;
            schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Id, SchemaModelProperty.PropertyTypes.PrimaryKey)
            { IsPrimaryKey = true });
            //}

            if (hasState)
            {
                schema.HasState = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.State, SchemaModelProperty.PropertyTypes.State));
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Status, SchemaModelProperty.PropertyTypes.Status));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.Active, false)
                    .AddColumnSet());
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.Inactive, false)
                     .AddColumnSet());
            }

            if (hasDates)
            {
                schema.HasDates = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.CreatedOn, SchemaModelProperty.PropertyTypes.DateTime));
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.ModifiedOn, SchemaModelProperty.PropertyTypes.DateTime));

                schema.AddView(new SchemaView(Definitions.DefaultViewNames.CreatedOnAtYear, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.Integer, "Year", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.CreatedOnAtMonth, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.Integer, "Year", 1, false)
                    .AddProperty(ViewParameter.ParameterType.Integer, "Month", 2, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.CreatedOnAtDay, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.CreatedOnAtDayAndHour, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.CreatedOnAtDayAndHourAndMinute, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.CreatedOnAtDayAndHourAndMinuteSecond, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.CreatedOnBetween, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTimeFrom", 1, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTimeTo", 2, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.CreatedOnBefore, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.CreatedOnAfter, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.ModifiedOnAtYear, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.Integer, "Year", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.ModifiedOnAtMonth, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.Integer, "Year", 1, false)
                    .AddProperty(ViewParameter.ParameterType.Integer, "Month", 2, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.ModifiedOnAtDay, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.ModifiedOnAtDayAndHour, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.ModifiedOnAtDayAndHourAndMinute, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.ModifiedOnAtDayAndHourAndMinuteSecond, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.ModifiedOnBetween, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTimeFrom", 1, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTimeTo", 2, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.ModifiedOnBefore, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.ModifiedOnAfter, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
            }

            if (hasOwner)
            {
                var userSchema = project.GetSchema(Definitions.DefaultBasicDomainNames.User);
                if (userSchema == null)
                {
                    throw new Exception("Can't add user relationship because 'User' domain doesn't exists");
                }
                schema.HasOwner = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Owner, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userSchema });

                schema.AddView(new SchemaView(Definitions.DefaultViewNames.Owner, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.Guid, "Id", 1, false));
            }

            if (hasUserRelationship)
            {
                var userSchema = project.GetSchema(Definitions.DefaultBasicDomainNames.User);
                if (userSchema == null)
                {
                    throw new Exception("Can't add user relationship because 'User' domain doesn't exists");
                }
                schema.HasUserRelationship = true;
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.CreatedBy, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userSchema });
                schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.ModifiedBy, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userSchema });

                schema.AddView(new SchemaView(Definitions.DefaultViewNames.CreatedBy, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.Guid, "Id", 1, false));
                schema.AddView(new SchemaView(Definitions.DefaultViewNames.ModifiedBy, false)
                    .AddColumnSet()
                    .AddProperty(ViewParameter.ParameterType.Guid, "Id", 1, false));
            }

            schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.Create));
            schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.DeleteByPk));
            schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveByPk));
            schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveMultiple));
            schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.Update));


            var domain = project.Domains.FirstOrDefault(k => k.Name == domainName)
                ?? throw new Exception($"Can't find domain named '{domainName}'");
            domain.AddSchema(schema);

            OverrideOutputParameter(NameParameter.Name, name);

        }
    }
}
