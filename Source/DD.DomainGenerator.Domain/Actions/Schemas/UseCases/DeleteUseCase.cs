using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Actions.Schemas.UseCases
{
    public class DeleteUseCase : ActionBase
    {
        public const string ActionName = "DeleteUseCase";
        public ActionParameterDefinition SchemaNameParameter { get; set; }
        public ActionParameterDefinition UseCaseParameter { get; set; }
        public ActionParameterDefinition IntersectionSchemaParameter { get; set; }
        public DeleteUseCase() : base(ActionName)
        {
            SchemaNameParameter = new ActionParameterDefinition(
              "schema", ActionParameterDefinition.TypeValue.String, "Schema name", "s", string.Empty)
            { IsSchemaSuggestion = true };

            UseCaseParameter = new ActionParameterDefinition(
              "usecase", ActionParameterDefinition.TypeValue.String, "Use case", "u", string.Empty)
            { InputSuggestions = UseCase.GetUseCaseTypesList() };

            IntersectionSchemaParameter = new ActionParameterDefinition(
             "intersectiondomain", ActionParameterDefinition.TypeValue.String, "If use case = RetrieveMultipleIntersection, indicate the intersection domain", "i", string.Empty)
            { IsSchemaSuggestion = true };

            ActionParametersDefinition.Add(UseCaseParameter);
            ActionParametersDefinition.Add(SchemaNameParameter);
            ActionParametersDefinition.Add(IntersectionSchemaParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, UseCaseParameter)
                && IsParamOk(parameters, SchemaNameParameter);
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var schemaName = GetStringParameterValue(parameters, SchemaNameParameter).ToWordPascalCase();
            var schema = project.GetSchema(schemaName)
                ?? throw new Exception($"Can't find any schema named '{schemaName}'");
            
            var useCaseTypeName = GetStringParameterValue(parameters, UseCaseParameter);
            var type = UseCase.StringToType(useCaseTypeName);
            Schema intersectionSchemaModel = null;
            if (type == UseCase.UseCaseTypes.RetrieveMultipleIntersection)
            {
                var intersectionSchemaName = GetStringParameterValue(parameters, IntersectionSchemaParameter).ToWordPascalCase();
                var intersectionSchema = project.GetSchema(intersectionSchemaName)
                    ?? throw new Exception($"Can't find any intersection domain named '{schemaName}'");
                intersectionSchemaModel = intersectionSchema;
            }
            schema.DeleteUseCase(type, intersectionSchemaModel);
            OverrideOutputParameter(SchemaNameParameter.Name, schemaName);
        }
    }
}
