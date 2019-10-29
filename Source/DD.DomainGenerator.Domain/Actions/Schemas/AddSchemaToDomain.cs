using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Actions.Schemas
{
   
    public class AddSchemaToDomain : ActionBase
    {
        public const string ActionName = "AddSchemaToDomain";
        public ActionParameterDefinition SchemaNameParameter { get; set; }
        public ActionParameterDefinition DomainNameParameter { get; set; }
        public AddSchemaToDomain() : base(ActionName)
        {
            SchemaNameParameter = new ActionParameterDefinition(
                "schemaname", ActionParameterDefinition.TypeValue.String, "Schema name", "s", string.Empty)
            { IsSchemaSuggestion = true };
            DomainNameParameter = new ActionParameterDefinition(
               "domainname", ActionParameterDefinition.TypeValue.String, "Domain name", "d", string.Empty)
            { IsDomainSuggestion = true };

            ActionParametersDefinition.Add(SchemaNameParameter);
            ActionParametersDefinition.Add(DomainNameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, SchemaNameParameter) && IsParamOk(parameters, DomainNameParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var schemaName = GetStringParameterValue(parameters, SchemaNameParameter).ToWordPascalCase();
            var schema = project.GetSchema(schemaName);
            if (schema == null)
            {
                throw new Exception($"Can't find any schema named '{schemaName}'");
            }
            var domainName = GetStringParameterValue(parameters, DomainNameParameter).ToWordPascalCase();
            var domain = project.GetDomain(domainName);
            if (domain == null)
            {
                throw new Exception($"Can't find any domain named '{domainName}'");
            }
            project.SchemaInDomains.Add(new SchemaInDomain(domain, schema));
        }

    }
}
