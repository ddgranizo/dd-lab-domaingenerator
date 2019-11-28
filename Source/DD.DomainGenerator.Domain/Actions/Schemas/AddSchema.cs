using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Sentences;
using DD.DomainGenerator.Utilities;
using static DD.DomainGenerator.Definitions;

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
            var hasState = GetBoolParameterValue(parameters, HasStateParameter);
            var hasUserRelationship = GetBoolParameterValue(parameters, HasUserRelationshipParameter);
            var hasOwner = GetBoolParameterValue(parameters, HasOwnerParameter);

            var domainName = GetStringParameterValue(parameters, DomainParameter);
            var domain = project.Domains.FirstOrDefault(k => k.Name == domainName)
               ?? throw new Exception($"Can't find domain named '{domainName}'");
            var schema = new Schema(name);

            schema.HasId = true;
            schema.AddProperty(new SchemaProperty(DefaultAttributesSchemaNames.Id, SchemaProperty.PropertyTypes.PrimaryKey, false)
            { IsPrimaryKey = true });

            if (hasState)
            {
                schema.HasState = true;
                schema.AddProperty(new SchemaProperty(DefaultAttributesSchemaNames.State, SchemaProperty.PropertyTypes.State, false));
                schema.AddProperty(new SchemaProperty(DefaultAttributesSchemaNames.Status, SchemaProperty.PropertyTypes.Status, false));
                schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.Active, false)
                .AddDefaultOutputViewParameter());
                schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.Inactive, false)
                .AddDefaultOutputViewParameter());
            }

            schema.HasDates = true;
            schema.AddProperty(new SchemaProperty(DefaultAttributesSchemaNames.CreatedOn, SchemaProperty.PropertyTypes.DateTime, false));
            schema.AddProperty(new SchemaProperty(DefaultAttributesSchemaNames.ModifiedOn, SchemaProperty.PropertyTypes.DateTime, false));

            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.CreatedOnAtYear, false)
                .AddInputParameter(DomainInputType.Integer, "Year")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.CreatedOnAtMonth, false)
                .AddInputParameter(DomainInputType.Integer, "Year")
                .AddInputParameter(DomainInputType.Integer, "Month")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.CreatedOnAtDay, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.CreatedOnAtDayAndHour, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.CreatedOnAtDayAndHourAndMinute, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.CreatedOnAtDayAndHourAndMinuteSecond, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.CreatedOnBetween, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTimeFrom")
                .AddInputParameter(DomainInputType.Datetime, "DateTimeTo")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.CreatedOnBefore, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.CreatedOnAfter, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.ModifiedOnAtYear, false)
                .AddInputParameter(DomainInputType.Integer, "Year")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.ModifiedOnAtMonth, false)
                .AddInputParameter(DomainInputType.Integer, "Year")
                .AddInputParameter(DomainInputType.Integer, "Month")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.ModifiedOnAtDay, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.ModifiedOnAtDayAndHour, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.ModifiedOnAtDayAndHourAndMinute, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.ModifiedOnAtDayAndHourAndMinuteSecond, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.ModifiedOnBetween, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTimeFrom")
                .AddInputParameter(DomainInputType.Datetime, "DateTimeTo")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.ModifiedOnBefore, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());
            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.ModifiedOnAfter, false)
                .AddInputParameter(DomainInputType.Datetime, "DateTime")
                .AddDefaultOutputViewParameter());

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

                schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.Owner, false)
                    .AddInputParameter(DomainInputType.Guid, "Id")
                    .AddDefaultOutputViewParameter());
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

                schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.CreatedBy, false)
                    .AddInputParameter(DomainInputType.Guid, "Id")
                    .AddDefaultOutputViewParameter());
                schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.ModifiedBy, false)
                    .AddInputParameter(DomainInputType.Guid, "Id")
                    .AddDefaultOutputViewParameter());
            }

            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.Create, DefaultRepositoryMethodNames.Create, false)
                    .AddInputParameter(DomainInputType.DomainEntity, "Entity")
                    .AddOutputParameter(DomainInputType.Guid, "Id"));

            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.DeleteByPk, DefaultRepositoryMethodNames.DeleteByPk, false)
                    .AddInputParameter(DomainInputType.Guid, "Id")); ;

            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.Update, DefaultRepositoryMethodNames.Update, false)
                    .AddInputParameter(DomainInputType.DomainEntity, "Entity"));

            schema.GetDefaultRepository().AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.RetrieveByPk, DefaultRepositoryMethodNames.RetrieveByPk, false)
                    .AddInputParameter(DomainInputType.Guid, "Id")
                    .AddOutputParameter(DomainInputType.DomainEntity, "Entity"));

            var useCaseCreate = GetCreateUseCase(domain, schema);
            var useCaseUpdate = GetUpdateUseCase(domain, schema);
            var useCaseDeleteByPk = GetDeleteByPkUseCase(domain, schema);
            var useCaseRetrieveByPk = GetRetrieveByPkUseCase(domain, schema);
            schema.AddUseCase(useCaseCreate);
            schema.AddUseCase(useCaseDeleteByPk);
            schema.AddUseCase(useCaseRetrieveByPk);
            schema.AddUseCase(useCaseUpdate);

            domain.AddSchema(schema);

            OverrideOutputParameter(NameParameter.Name, name);
        }


        private static UseCase GetRetrieveByPkUseCase(Domain domain, Schema schema)
        {
            var useCaseCreate = new UseCase(UseCase.UseCaseTypes.RetrieveByPk);
            var method = new ExecuteRepositoryMethodSentence(
                domain,
                schema,
                schema.GetDefaultRepository(),
                schema.GetDefaultRepository().GetDefaultCreateRepositoryMethod());

            var inputParameters = new List<UseCaseLinkInputExecutionParameter>();
            inputParameters.Add(new UseCaseLinkInputExecutionParameter(
                useCaseCreate.InputParameters.First(),
                method.InputContextParameters.First()));

            var outputParameter = new UseCaseLinkOutputExecutionParameter(
                method.OutputContextParameters.First(),
                useCaseCreate.OutputParameters.First());

            var executionSentence = new UseCaseExecutionSentence(method, inputParameters, new List<UseCaseLinkExecutionParameter>());
            useCaseCreate.Execution.AddExecutionSentence(executionSentence);
            useCaseCreate.Execution.SetExecutionOutputParameter(outputParameter);
            return useCaseCreate;
        }


        private static UseCase GetDeleteByPkUseCase(Domain domain, Schema schema)
        {
            var useCaseCreate = new UseCase(UseCase.UseCaseTypes.DeleteByPk);
            var method = new ExecuteRepositoryMethodSentence(
                domain,
                schema,
                schema.GetDefaultRepository(),
                schema.GetDefaultRepository().GetDefaultCreateRepositoryMethod());

            var inputParameters = new List<UseCaseLinkInputExecutionParameter>();
            inputParameters.Add(new UseCaseLinkInputExecutionParameter(
                useCaseCreate.InputParameters.First(),
                method.InputContextParameters.First()));

            var executionSentence = new UseCaseExecutionSentence(method, inputParameters, new List<UseCaseLinkExecutionParameter>());
            useCaseCreate.Execution.AddExecutionSentence(executionSentence);
            return useCaseCreate;
        }


        private static UseCase GetUpdateUseCase(Domain domain, Schema schema)
        {
            var useCaseCreate = new UseCase(UseCase.UseCaseTypes.Update);
            var method = new ExecuteRepositoryMethodSentence(
                domain,
                schema,
                schema.GetDefaultRepository(),
                schema.GetDefaultRepository().GetDefaultCreateRepositoryMethod());

            var inputParameters = new List<UseCaseLinkInputExecutionParameter>();
            inputParameters.Add(new UseCaseLinkInputExecutionParameter(
                useCaseCreate.InputParameters.First(),
                method.InputContextParameters.First()));

            var executionSentence = new UseCaseExecutionSentence(method, inputParameters, new List<UseCaseLinkExecutionParameter>());
            useCaseCreate.Execution.AddExecutionSentence(executionSentence);

            return useCaseCreate;
        }


        private static UseCase GetCreateUseCase(Domain domain, Schema schema)
        {
            var useCaseCreate = new UseCase(UseCase.UseCaseTypes.Create);
            var method = new ExecuteRepositoryMethodSentence(
                domain,
                schema,
                schema.GetDefaultRepository(),
                schema.GetDefaultRepository().GetDefaultCreateRepositoryMethod());

            var inputParameters = new List<UseCaseLinkInputExecutionParameter>();
            inputParameters.Add(new UseCaseLinkInputExecutionParameter(
                useCaseCreate.InputParameters.First(),
                method.InputContextParameters.First()));

            var outputParameter = new UseCaseLinkOutputExecutionParameter(
                method.OutputContextParameters.First(),
                useCaseCreate.OutputParameters.First());

            var executionSentence = new UseCaseExecutionSentence(method, inputParameters, new List<UseCaseLinkExecutionParameter>());
            useCaseCreate.Execution.AddExecutionSentence(executionSentence);
            useCaseCreate.Execution.SetExecutionOutputParameter(outputParameter);
            return useCaseCreate;
        }
    }
}
