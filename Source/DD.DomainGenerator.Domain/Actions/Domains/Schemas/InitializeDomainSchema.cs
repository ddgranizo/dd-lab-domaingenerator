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
    public class InitializeDomainSchema : ActionBase
    {
        public const string ActionName = "InitializeDomainSchema";
        public ActionParameterDefinition DomainNameParameter { get; set; }
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition HasIdParameter { get; set; }
        public ActionParameterDefinition HasStateParameter { get; set; }
        public ActionParameterDefinition HasDatesParameter { get; set; }
        public ActionParameterDefinition HasUserRelationshipParameter { get; set; }
        public ActionParameterDefinition HasOwnerParameter { get; set; }
        public ActionParameterDefinition AddCRUDUseCasesParameter { get; set; }

        public InitializeDomainSchema() : base(ActionName)
        {
            DomainNameParameter = new ActionParameterDefinition(
               "domainname", ActionParameterDefinition.TypeValue.String, "Domain name", "d")
            { IsDomainSuggestion = true };

            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Schema name. Recomended to use same domain name but not in plural. The name will be converted to PascalCase", "n");

            HasIdParameter = new ActionParameterDefinition(
               "hasid", ActionParameterDefinition.TypeValue.Boolean, "The new schema has id. It will add 'Id' attribute with GUID and primary key. Default value = true", "i");

            HasStateParameter = new ActionParameterDefinition(
               "hasstate", ActionParameterDefinition.TypeValue.Boolean, "The new schema has state. It will add 'State' attribute with values 1 enbled, 0 disabled. Default value = false", "s");

            HasUserRelationshipParameter = new ActionParameterDefinition(
              "hasuserrelationship", ActionParameterDefinition.TypeValue.Boolean, "The new schema has user relationship. It will add 'CreatedBy' and 'ModifiedBy' forening keys to the schema, related with 'User' schema (must be created first). Default value = false", "u");

            HasDatesParameter = new ActionParameterDefinition(
               "hasdates", ActionParameterDefinition.TypeValue.Boolean, "The new schema has dates. It will add 'CreatedOn' and 'ModifiedOn' of DateTime type. Default value = false", "d");

            HasOwnerParameter = new ActionParameterDefinition(
              "hasowner", ActionParameterDefinition.TypeValue.Boolean, "The new schema has owner. It will add 'Owner'. Default value = false", "o");

            AddCRUDUseCasesParameter = new ActionParameterDefinition(
              "addcrudusecases", ActionParameterDefinition.TypeValue.Boolean, "Add CRUD use cases. Default value = yes", "a");

            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(DomainNameParameter);
            ActionParametersDefinition.Add(HasIdParameter);
            ActionParametersDefinition.Add(HasDatesParameter);
            ActionParametersDefinition.Add(HasStateParameter);
            ActionParametersDefinition.Add(HasOwnerParameter);
            ActionParametersDefinition.Add(HasUserRelationshipParameter);
            ActionParametersDefinition.Add(AddCRUDUseCasesParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, DomainNameParameter) && IsParamOk(parameters, NameParameter);
        }
        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var domainName = GetStringParameterValue(parameters, DomainNameParameter, string.Empty).ToWordPascalCase();
            var name = GetStringParameterValue(parameters, NameParameter, string.Empty).ToWordPascalCase();
            var hasId = GetBoolParameterValue(parameters, HasIdParameter, true);
            var hasState = GetBoolParameterValue(parameters, HasStateParameter, false);
            var hasDates = GetBoolParameterValue(parameters, HasDatesParameter, false);
            var hasUserRelationship = GetBoolParameterValue(parameters, HasUserRelationshipParameter, false);
            var hasOwner = GetBoolParameterValue(parameters, HasOwnerParameter, false);
            var addCrudUseCases = GetBoolParameterValue(parameters, AddCRUDUseCasesParameter, true);
            var domain = Domain.FindChildDomain(project.Domain, domainName);
            if (domain == null)
            {
                throw new Exception($"Can't find any domain named '{domainName}'");
            }
            if (domain.HasModel)
            {
                throw new Exception($"Domain '{domainName}' has already model. Use a non-initialized domain");
            }
            if (domain.Domains.Count > 0)
            {
                throw new Exception($"Domain named '{domainName}' already has child domains. Only childest domains can contain schema definitions");
            }
            domain.Schema = new SchemaModel(name);
            if (hasId)
            {
                domain.Schema.HasId = true;
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Id, SchemaModelProperty.PropertyTypes.PrimaryKey)
                { IsPrimaryKey = true });
            }
            if (hasState)
            {
                domain.Schema.HasState = true;
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.State, SchemaModelProperty.PropertyTypes.State));
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Status, SchemaModelProperty.PropertyTypes.Status));
            }
            if (hasDates)
            {
                domain.Schema.HasDates = true;
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.CreatedOn, SchemaModelProperty.PropertyTypes.DateTime));
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.ModifiedOn, SchemaModelProperty.PropertyTypes.DateTime));
            }
            if (hasOwner)
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
                domain.Schema.HasOwner = true;
                domain.Schema.AddProperty(new SchemaModelProperty(Definitions.DefaultAttributesSchemaNames.Owner, SchemaModelProperty.PropertyTypes.ForeingKey)
                { ForeingSchema = userDomain.Schema });
            }
            if (hasUserRelationship)
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
            if (addCrudUseCases)
            {
                domain.AddUseCase(new UseCase(UseCase.UseCaseTypes.Create));
                domain.AddUseCase(new UseCase(UseCase.UseCaseTypes.DeleteByPk));
                domain.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveByPk));
                domain.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveMultiple));
                domain.AddUseCase(new UseCase(UseCase.UseCaseTypes.Update));
            }
        }
    }
}
