using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Actions.Domains.UseCases
{
    public class DeleteUseCase : ActionBase
    {
        public const string ActionName = "DeleteUseCase";
        public ActionParameterDefinition DomainNameParameter { get; set; }
        public ActionParameterDefinition UseCaseParameter { get; set; }
        public ActionParameterDefinition IntersectionDomainParameter { get; set; }
        public DeleteUseCase() : base(ActionName)
        {
            DomainNameParameter = new ActionParameterDefinition(
              "domain", ActionParameterDefinition.TypeValue.String, "Domain name", "d")
            { IsDomainSuggestion = true };

            UseCaseParameter = new ActionParameterDefinition(
              "usecase", ActionParameterDefinition.TypeValue.String, "Use case", "u")
            { InputSuggestions = UseCase.GetUseCaseTypesList() };

            IntersectionDomainParameter = new ActionParameterDefinition(
             "intersectiondomain", ActionParameterDefinition.TypeValue.String, "If use case = RetrieveMultipleIntersection, indicate the intersection domain", "i")
            { IsDomainSuggestion = true };

            ActionParametersDefinition.Add(UseCaseParameter);
            ActionParametersDefinition.Add(DomainNameParameter);
            ActionParametersDefinition.Add(IntersectionDomainParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, UseCaseParameter)
                && IsParamOk(parameters, DomainNameParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var domainName = GetStringParameterValue(parameters, DomainNameParameter, string.Empty).ToWordPascalCase();
            var domain = Domain.FindChildDomain(project.Domain, domainName)
                ?? throw new Exception($"Can't find any domain named '{domainName}'");
            
            var useCaseTypeName = GetStringParameterValue(parameters, UseCaseParameter, string.Empty);
            var type = UseCase.StringToType(useCaseTypeName);
            SchemaModel intersectionSchemaModel = null;
            if (type == UseCase.UseCaseTypes.RetrieveMultipleIntersection)
            {
                var intersectionDomainName = GetStringParameterValue(parameters, IntersectionDomainParameter, string.Empty).ToWordPascalCase();
                var intersectionDomain = Domain.FindChildDomain(project.Domain, intersectionDomainName)
                    ?? throw new Exception($"Can't find any intersection domain named '{domainName}'");
                intersectionSchemaModel = intersectionDomain.Schema;
            }
            domain.DeleteUseCase(type, intersectionSchemaModel);
        }
    }
}
