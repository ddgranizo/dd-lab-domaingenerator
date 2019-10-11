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
    public class DeleteSchema : ActionBase
    {
        public const string ActionName = "DeleteSchema";
        public ActionParameterDefinition SchemaNameParameter { get; set; }
        public DeleteSchema() : base(ActionName)
        {
            SchemaNameParameter = new ActionParameterDefinition(
                "domainname", ActionParameterDefinition.TypeValue.String, "Schema name", "d", string.Empty)
            { IsSchemaSuggestion = true };
            ActionParametersDefinition.Add(SchemaNameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, SchemaNameParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, SchemaNameParameter).ToWordPascalCase();
            var schema = project.GetSchema(name);
            if (schema == null)
            {
                throw new Exception($"Can't find any schema named '{name}'");
            }
            project.Schemas.Remove(schema);
        }

    }
}
