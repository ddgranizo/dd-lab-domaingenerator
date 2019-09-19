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
    public class MoveDomain : ActionBase
    {
        public const string ActionName = "MoveDomain";
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition NewParentParameter { get; set; }
        public MoveDomain() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Domain name. Must be unique. Is mandatory to use PascalCase for the name. Otherwise the name will be converterd", "n")
            { IsDomainSuggestion = true };
            NewParentParameter = new ActionParameterDefinition(
                "newparent", ActionParameterDefinition.TypeValue.String, "New parent domain name", "p")
            { IsDomainSuggestion = true };

            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(NewParentParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter) && IsParamOk(parameters, NewParentParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter, string.Empty).ToWordPascalCase();
            var currentDomain = Domain.FindChildDomain(project.Domain, name)
                ?? throw new Exception($"Cant find domain named '{name}'");
            if (currentDomain.ParentDomain == null)
            {
                throw new Exception($"Root node cannot be moved");
            }
            var newParent = GetStringParameterValue(parameters, NewParentParameter, string.Empty).ToWordPascalCase();
            var newParentDomain = Domain.FindChildDomain(project.Domain, newParent);
            if (newParentDomain.HasModel)
            {
                throw new Exception($"New parent domain has already defined schema model. The schema model must be in the lowests levels, so domains with schema models cannot contain orhter childs domains");
            }
            currentDomain.ParentDomain = newParentDomain ?? throw new Exception($"Cant find new parent domain named '{newParent}'");
            newParentDomain.AddDomain(currentDomain);
            currentDomain.ParentDomain.Domains.Remove(currentDomain);
        }
    }
}
