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
    public class InitializeDomainSchemaIntersection : ActionBase
    {
        public const string ActionName = "InitializeDomainSchemaIntersection";
        public ActionParameterDefinition DomainNameParameter { get; set; }
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition FirstDomainNameParameter { get; set; }
        public ActionParameterDefinition SecondDomainNameParameter { get; set; }
        public InitializeDomainSchemaIntersection() : base(ActionName)
        {
            DomainNameParameter = new ActionParameterDefinition(
               "domain", ActionParameterDefinition.TypeValue.String, "Domain name", "d")
            { IsDomainSuggestion = true };

            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Schema name. Recomended to use same domain name but not in plural. The name will be converted to PascalCase", "n");

            FirstDomainNameParameter = new ActionParameterDefinition(
               "firstdomain", ActionParameterDefinition.TypeValue.String, "First domain name intersection", "f")
            { IsDomainSuggestion = true };

            SecondDomainNameParameter = new ActionParameterDefinition(
               "seconddomain", ActionParameterDefinition.TypeValue.String, "Second domain name intersection", "s")
            { IsDomainSuggestion = true };

            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(DomainNameParameter);
            ActionParametersDefinition.Add(FirstDomainNameParameter);
            ActionParametersDefinition.Add(SecondDomainNameParameter);

        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter)
                && IsParamOk(parameters, DomainNameParameter)
                && IsParamOk(parameters, FirstDomainNameParameter)
                && IsParamOk(parameters, SecondDomainNameParameter);
        }
        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var domainName = GetStringParameterValue(parameters, NameParameter, string.Empty).ToWordPascalCase();
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

            var name = GetStringParameterValue(parameters, NameParameter, string.Empty).ToWordPascalCase();
            var firstDomainName = GetStringParameterValue(parameters, FirstDomainNameParameter, string.Empty).ToWordPascalCase();
            var secondDomainName = GetStringParameterValue(parameters, SecondDomainNameParameter, string.Empty).ToWordPascalCase();

            var firstDomain = Domain.FindChildDomain(project.Domain, firstDomainName);
            if (firstDomain == null)
            {
                throw new Exception($"Can't find any domain named '{firstDomainName}'");
            }
            if (!firstDomain.HasModel)
            {
                throw new Exception($"Domain '{firstDomainName}' has not model yet. Initialize first");
            }
            var secondDomain = Domain.FindChildDomain(project.Domain, secondDomainName);
            if (secondDomain == null)
            {
                throw new Exception($"Can't find any domain named '{secondDomainName}'");
            }
            if (!secondDomain.HasModel)
            {
                throw new Exception($"Domain '{secondDomainName}' has not model yet. Initialize first");
            }

            var firstAttributeName = firstDomain.Name == secondDomain.Name
                ? $"{firstDomain.Schema.Name}1Id"
                : $"{firstDomain.Schema.Name}Id";
            var secondAttributeName = firstDomain.Name == secondDomain.Name
                ? $"{secondDomain.Schema.Name}2Id"
                : $"{secondDomain.Schema.Name}Id";

            domain.Schema = new SchemaModel(name) { HasId = true, IsIntersection = true};
            domain.Schema.AddProperty(
                new SchemaModelProperty(firstAttributeName, SchemaModelProperty.PropertyTypes.ForeingKey));
            domain.Schema.AddProperty(
                new SchemaModelProperty(secondAttributeName, SchemaModelProperty.PropertyTypes.ForeingKey));

            firstDomain.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveMultipleIntersection, domain.Schema));
            secondDomain.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveMultipleIntersection, domain.Schema));
        }
    }
}
