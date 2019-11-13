using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Actions.Schemas
{
    public class AddSchemaView : ActionBase
    {
        public const string ActionName = "AddSchemaView";
        public ActionParameterDefinition SchemaNameParameter { get; set; }
        public ActionParameterDefinition ViewNameParameter { get; set; }
        public AddSchemaView() : base(ActionName)
        {
            SchemaNameParameter = new ActionParameterDefinition(
                "schemaname", ActionParameterDefinition.TypeValue.String, "Schema name", "s", string.Empty)
            { IsSchemaSuggestion = true };
            SchemaNameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "View name. The name will be completed with format. The name will be coverted to CamelCase. Example of name: GetActiveEntities, GetXEntities", "s", string.Empty)
            { IsSchemaSuggestion = true };

            ActionParametersDefinition.Add(SchemaNameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, SchemaNameParameter) ;
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var schemaName = GetStringParameterValue(parameters, ViewNameParameter);
            var name = GetStringParameterValue(parameters, SchemaNameParameter).ToWordPascalCase();
            var schema = project.GetSchema(schemaName);
            if (schema == null)
            {
                throw new Exception($"Can't find any schema named '{schemaName}'");
            }
            schema.AddView(new SchemaView(name, true));
            OverrideOutputParameter(ViewNameParameter.Name, name);
        }
    }
}
