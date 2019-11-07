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
    public class AddDomain : ActionBase
    {
        public const string ActionName = "AddDomain";
        public ActionParameterDefinition NameParameter { get; set; }

        public AddDomain() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Domain name. Must be unique. Is mandatory to use PascalCase for the name. Otherwise the name will be converterd", "n", string.Empty);
            
            ActionParametersDefinition.Add(NameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter) ;
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter).ToWordPascalCase();
            bool isRepeated = project.Domains.FirstOrDefault(k => k.Name == name) != null;
            if (isRepeated)
            {
                throw new Exception("Found repeated domain name. Domain name must be unique");
            }
            Domain domain = new Domain(name);
            project.Domains.Add(domain);
            OverrideOutputParameter(NameParameter.Name, name);
        }
    }
}
