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
    public class InitializeDomainSchema : ActionBase
    {
        public const string ActionName = "InitializeDomainSchema";
        public ActionParameterDefinition DomainNameParameter { get; set; }
        public InitializeDomainSchema() : base(ActionName)
        {
            DomainNameParameter = new ActionParameterDefinition(
                "domainname", ActionParameterDefinition.TypeValue.String, "Domain name", "d");
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
            if (domain.Domains.Count>0)
            {
                throw new Exception($"Domain named '{name}' already has child domains. Only childest domains can contain schema definitions");
            }
            domain.Schema = new SchemaModel();
        }

    }
}
