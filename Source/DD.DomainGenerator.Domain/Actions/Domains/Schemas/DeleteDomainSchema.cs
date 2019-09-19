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
    public class DeleteDomainSchema : ActionBase
    {
        public const string ActionName = "DeleteDomainSchema";
        public ActionParameterDefinition DomainNameParameter { get; set; }
        public DeleteDomainSchema() : base(ActionName)
        {
            DomainNameParameter = new ActionParameterDefinition(
                "domainname", ActionParameterDefinition.TypeValue.String, "Domain name", "d")
            { IsDomainSuggestion = true };
            ActionParametersDefinition.Add(DomainNameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, DomainNameParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, DomainNameParameter, string.Empty).ToWordPascalCase();
            var domain = Domain.FindChildDomain(project.Domain, name);
            if (domain == null)
            {
                throw new Exception($"Can't find any domain named '{name}'");
            }
            domain.Schema = null;
        }

    }
}
