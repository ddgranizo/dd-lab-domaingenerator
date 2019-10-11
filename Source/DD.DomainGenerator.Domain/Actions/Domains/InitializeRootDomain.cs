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
    public class InitializeRootDomain : ActionBase
    {
        public const string ActionName = "InitializeRootDomain";
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition NamespaceParameter { get; set; }
        public InitializeRootDomain() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Domain name. Must be unique. Is mandatory to use PascalCase for the name. Otherwise the name will be converterd", "n", string.Empty);
            NamespaceParameter = new ActionParameterDefinition(
                "namespace", ActionParameterDefinition.TypeValue.String, "Namespace. Is mandatory to use My.Domain.Project.Convention for your namespace. Otherwise the namespace will be converterd", "s", string.Empty);

            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(NamespaceParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter) && IsParamOk(parameters, NamespaceParameter);
        }
        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter).ToWordPascalCase();
            var nameSpace = GetStringParameterValue(parameters, NamespaceParameter).ToNamespacePascalCase();
            project.Domains.Add(new Domain() { Namespace = nameSpace, Name = name, IsRootDomain = true });
        }

    }
}
