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
        public ActionParameterDefinition DomainParameter { get; set; }
        public DeleteSchema() : base(ActionName)
        {
            DomainParameter = new ActionParameterDefinition(
                "domain", ActionParameterDefinition.TypeValue.String, "Domain", "d", string.Empty)
            { IsDomainSuggestion = true };

            SchemaNameParameter = new ActionParameterDefinition(
                "schemaname", ActionParameterDefinition.TypeValue.String, "Schema name", "s", string.Empty)
            { InputSuggestionsHandler = GetDomainSchemasSuggestionHandler };

            ActionParametersDefinition.Add(DomainParameter);
            ActionParametersDefinition.Add(SchemaNameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, SchemaNameParameter)
                && IsParamOk(parameters, DomainParameter);
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, SchemaNameParameter);
            var domainName = GetStringParameterValue(parameters, DomainParameter);
            var domain = project.GetDomain(domainName);
            domain.DeleteSchema(name);
        }

    }
}
