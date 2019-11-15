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
            var firstSchemaName = GetStringParameterValue(parameters, FirstSchemaNameParameter).ToWordPascalCase();
            var secondSchemaName = GetStringParameterValue(parameters, SecondSchemaNameParameter).ToWordPascalCase();

            var firstSchema = project.GetSchema(firstSchemaName);
            if (firstSchema == null)
            {
                throw new Exception($"Can't find any schema named '{firstSchemaName}'");
            }
            
            var secondDomain = project.GetSchema( secondSchemaName);
            if (secondDomain == null)
            {
                throw new Exception($"Can't find any schema named '{secondSchemaName}'");
            }
           
            var firstAttributeName = firstSchema.Name == secondDomain.Name
                ? $"{firstSchema.Name}1Id"
                : $"{firstSchema.Name}Id";
            var secondAttributeName = firstSchema.Name == secondDomain.Name
                ? $"{secondDomain.Name}2Id"
                : $"{secondDomain.Name}Id";

            var newSchema = new Schema(schemaName) { HasId = true, IsIntersection = true };
            newSchema.AddProperty(
                new SchemaModelProperty(firstAttributeName, SchemaModelProperty.PropertyTypes.ForeingKey));
            newSchema.AddProperty(
                new SchemaModelProperty(secondAttributeName, SchemaModelProperty.PropertyTypes.ForeingKey));

            firstSchema.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveMultipleIntersection, newSchema));
            secondDomain.AddUseCase(new UseCase(UseCase.UseCaseTypes.RetrieveMultipleIntersection, newSchema));
            OverrideOutputParameter(NameParameter.Name, schemaName);
            OverrideOutputParameter(FirstSchemaNameParameter.Name, firstSchemaName);
            OverrideOutputParameter(SecondSchemaNameParameter.Name, secondSchemaName);
        }
    }
}
