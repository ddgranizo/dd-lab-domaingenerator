using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;

namespace DD.DomainGenerator.Actions.Domains
{
    public class DeleteDomain : ActionBase
    {
        public const string ActionName = "DeleteDomain";
        public ActionParameterDefinition NameParameter { get; set; }
        public DeleteDomain() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Domain name. Must be unique. Is mandatory to use PascalCase for the name. Otherwise the name will be converterd", "n", string.Empty)
            { IsDomainSuggestion = true };

            ActionParametersDefinition.Add(NameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter);
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter).ToWordPascalCase();
            var domain = project.Domains.FirstOrDefault(k => k.Name == name);
            if (domain == null)
            {
                throw new Exception($"Domain with name {name} not found");
            }
            project.Domains.Remove(domain);
            OverrideOutputParameter(NameParameter.Name, name);
        }
    }
}
