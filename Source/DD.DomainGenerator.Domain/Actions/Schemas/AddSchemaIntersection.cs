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
    public class AddSchemaIntersection : ActionBase
    {
        public const string ActionName = "AddSchemaIntersection";
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition FirstSchemaNameParameter { get; set; }
        public ActionParameterDefinition SecondSchemaNameParameter { get; set; }
        public AddSchemaIntersection() : base(ActionName)
        {

            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Schema name. Recomended to use same domain name but not in plural. The name will be converted to PascalCase", "n", string.Empty);

            FirstSchemaNameParameter = new ActionParameterDefinition(
               "firstdomain", ActionParameterDefinition.TypeValue.String, "First domain name intersection", "f", string.Empty)
            { IsSchemaSuggestion = true };

            SecondSchemaNameParameter = new ActionParameterDefinition(
               "seconddomain", ActionParameterDefinition.TypeValue.String, "Second domain name intersection", "s", string.Empty)
            { IsSchemaSuggestion = true };

            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(FirstSchemaNameParameter);
            ActionParametersDefinition.Add(SecondSchemaNameParameter);

        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter)
                && IsParamOk(parameters, FirstSchemaNameParameter)
                && IsParamOk(parameters, SecondSchemaNameParameter);
        }
        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var schemaName = GetStringParameterValue(parameters, NameParameter).ToWordPascalCase();
            var firstSchemaName = GetStringParameterValue(parameters, FirstSchemaNameParameter);
            var secondSchemaName = GetStringParameterValue(parameters, SecondSchemaNameParameter);

            var firstSchema = project.GetSchema(firstSchemaName);
            if (firstSchema == null)
            {
                throw new Exception($"Can't find any schema named '{firstSchemaName}'");
            }
            
            var secondSchema = project.GetSchema( secondSchemaName);
            if (secondSchema == null)
            {
                throw new Exception($"Can't find any schema named '{secondSchemaName}'");
            }
           
            var firstAttributeName = firstSchema.Name == secondSchema.Name
                ? $"{firstSchema.Name}OneId"
                : $"{firstSchema.Name}Id";
            var secondAttributeName = firstSchema.Name == secondSchema.Name
                ? $"{secondSchema.Name}TwoId"
                : $"{secondSchema.Name}Id";

            var newSchema = new Schema(schemaName) { HasId = true, IsIntersection = true };
            newSchema.AddProperty(
                new SchemaProperty(firstAttributeName, SchemaProperty.PropertyTypes.ForeingKey, false));
            newSchema.AddProperty(
                new SchemaProperty(secondAttributeName, SchemaProperty.PropertyTypes.ForeingKey, false));

            //firstSchema.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveMultipleIntersection, newSchema));
            //secondSchema.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveMultipleIntersection, newSchema));
            OverrideOutputParameter(NameParameter.Name, schemaName);
        }
    }
}
