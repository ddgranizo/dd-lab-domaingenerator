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
        public ActionParameterDefinition HasStateParameter { get; set; }
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

          

            HasOwnerParameter = new ActionParameterDefinition(
              "hasowner", ActionParameterDefinition.TypeValue.Boolean, "The new schema has owner. It will add 'Owner'. Default value = false", "o", false);



            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(DomainParameter);
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
            var hasUserRelationship = GetBoolParameterValue(parameters, HasUserRelationshipParameter);
            var hasOwner = GetBoolParameterValue(parameters, HasOwnerParameter);

            var domainName = GetStringParameterValue(parameters, DomainParameter);

            var schema = new Schema(name);

            //if (hasId)
            //{
            schema.HasId = true;
            schema.AddProperty(new SchemaProperty(Definitions.DefaultAttributesSchemaNames.Id, SchemaProperty.PropertyTypes.PrimaryKey, false)
            { IsPrimaryKey = true });
            //}

            if (hasState)
            {
                schema.HasState = true;
                schema.AddProperty(new SchemaProperty(Definitions.DefaultAttributesSchemaNames.State, SchemaProperty.PropertyTypes.State, false));
                schema.AddProperty(new SchemaProperty(Definitions.DefaultAttributesSchemaNames.Status, SchemaProperty.PropertyTypes.Status, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.Active, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.Inactive, false));
            }

            //if (hasDates)
            //{
                schema.HasDates = true;
                schema.AddProperty(new SchemaProperty(Definitions.DefaultAttributesSchemaNames.CreatedOn, SchemaProperty.PropertyTypes.DateTime, false));
                schema.AddProperty(new SchemaProperty(Definitions.DefaultAttributesSchemaNames.ModifiedOn, SchemaProperty.PropertyTypes.DateTime, false));

                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.CreatedOnAtYear, false)
                    .AddProperty(ViewParameter.ParameterType.Integer, "Year", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.CreatedOnAtMonth, false)
                    .AddProperty(ViewParameter.ParameterType.Integer, "Year", 1, false)
                    .AddProperty(ViewParameter.ParameterType.Integer, "Month", 2, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.CreatedOnAtDay, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.CreatedOnAtDayAndHour, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.CreatedOnAtDayAndHourAndMinute, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.CreatedOnAtDayAndHourAndMinuteSecond, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.CreatedOnBetween, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTimeFrom", 1, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTimeTo", 2, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.CreatedOnBefore, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.CreatedOnAfter, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.ModifiedOnAtYear, false)
                    .AddProperty(ViewParameter.ParameterType.Integer, "Year", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.ModifiedOnAtMonth, false)
                    .AddProperty(ViewParameter.ParameterType.Integer, "Year", 1, false)
                    .AddProperty(ViewParameter.ParameterType.Integer, "Month", 2, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.ModifiedOnAtDay, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.ModifiedOnAtDayAndHour, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.ModifiedOnAtDayAndHourAndMinute, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.ModifiedOnAtDayAndHourAndMinuteSecond, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.ModifiedOnBetween, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTimeFrom", 1, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTimeTo", 2, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.ModifiedOnBefore, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.ModifiedOnAfter, false)
                    .AddProperty(ViewParameter.ParameterType.DateTime, "DateTime", 1, false));
            //}

            if (hasOwner)
            {
                var userSchema = project.GetSchema(Definitions.DefaultBasicDomainNames.User);
                if (userSchema == null)
                {
                    throw new Exception("Can't add user relationship because 'User' domain doesn't exists");
                }
                schema.HasOwner = true;
                schema.AddProperty(new SchemaProperty(Definitions.DefaultAttributesSchemaNames.Owner, SchemaProperty.PropertyTypes.ForeingKey, false)
                { ForeingSchema = userSchema });

                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.Owner, false)
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
                schema.AddProperty(new SchemaProperty(Definitions.DefaultAttributesSchemaNames.CreatedBy, SchemaProperty.PropertyTypes.ForeingKey, false)
                { ForeingSchema = userSchema });
                schema.AddProperty(new SchemaProperty(Definitions.DefaultAttributesSchemaNames.ModifiedBy, SchemaProperty.PropertyTypes.ForeingKey, false)
                { ForeingSchema = userSchema });

                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.CreatedBy, false)
                    .AddProperty(ViewParameter.ParameterType.Guid, "Id", 1, false));
                schema.GetDefaultRepository().AddView(new View(Definitions.DefaultViewNames.ModifiedBy, false)
                    .AddProperty(ViewParameter.ParameterType.Guid, "Id", 1, false));
            }

            schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.Create));
            schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.DeleteByPk));
            schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveByPk));
            //schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveMultiple));
            schema.AddUseCase(new UseCase(UseCase.UseCaseTypes.Update));


            var domain = project.Domains.FirstOrDefault(k => k.Name == domainName)
                ?? throw new Exception($"Can't find domain named '{domainName}'");
            domain.AddSchema(schema);

            OverrideOutputParameter(NameParameter.Name, name);

        }
    }
}
