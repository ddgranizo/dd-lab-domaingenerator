using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Actions.Schemas.UseCases
{
    public class AddUseCase : ActionBase
    {
        public const string ActionName = "AddUseCase";
        public ActionParameterDefinition SchemaNameParameter { get; set; }
        public ActionParameterDefinition UseCaseParameter { get; set; }
        public ActionParameterDefinition IntersectionDomainParameter { get; set; }
        public ActionParameterDefinition NeedsAthorizationParameter { get; set; }


        public AddUseCase() : base(ActionName)
        {
            SchemaNameParameter = new ActionParameterDefinition(
              "schema", ActionParameterDefinition.TypeValue.String, "Schema name", "s", string.Empty)
            { IsSchemaSuggestion = true };

            UseCaseParameter = new ActionParameterDefinition(
              "usecase", ActionParameterDefinition.TypeValue.String, "Use case", "u", string.Empty)
            { InputSuggestions = UseCase.GetUseCaseTypesList() };

            IntersectionDomainParameter = new ActionParameterDefinition(
             "intersectionschema", ActionParameterDefinition.TypeValue.String, "If use case = RetrieveMultipleIntersection, indicate the intersection schema", "i", string.Empty)
            { IsSchemaSuggestion = true };

            NeedsAthorizationParameter = new ActionParameterDefinition(
                "authorization", ActionParameterDefinition.TypeValue.Boolean, "Access to this use case needs user-authorization. Default value = false", "a", false);

            ActionParametersDefinition.Add(NeedsAthorizationParameter);
            ActionParametersDefinition.Add(UseCaseParameter);
            ActionParametersDefinition.Add(SchemaNameParameter);
            ActionParametersDefinition.Add(IntersectionDomainParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, UseCaseParameter)
                && IsParamOk(parameters, SchemaNameParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var schemaName = GetStringParameterValue(parameters, SchemaNameParameter).ToWordPascalCase();
            var schema = project.GetSchema(schemaName)
                ?? throw new Exception($"Can't find any schema named '{schemaName}'");
            var useCaseTypeName = GetStringParameterValue(parameters, UseCaseParameter);
            var type = UseCase.StringToType(useCaseTypeName);
            SchemaModel intersectionSchemaModel = null;
            if (type == UseCase.UseCaseTypes.RetrieveMultipleIntersection)
            {
                var intersectionSchemaName = GetStringParameterValue(parameters, IntersectionDomainParameter).ToWordPascalCase();
                var intersectionSchema = project.GetSchema(intersectionSchemaName)
                    ?? throw new Exception($"Can't find any intersection schema named '{intersectionSchemaName}'");
                intersectionSchemaModel = intersectionSchema;
            }
            var caseUse = new UseCase(type, intersectionSchemaModel);
            var authorization = GetBoolParameterValue(parameters, NeedsAthorizationParameter);
            if (authorization)
            {
                caseUse.NeedsAuthorization = true;
            }
            schema.AddUseCase(caseUse);
        }
    }
}
