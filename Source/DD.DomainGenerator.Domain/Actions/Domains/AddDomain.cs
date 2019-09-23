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
        public ActionParameterDefinition ParentParameter { get; set; }
        public ActionParameterDefinition NeedsAthorizationParameter { get; set; }

        public AddDomain() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Domain name. Must be unique. Is mandatory to use PascalCase for the name. Otherwise the name will be converterd", "n");
            ParentParameter = new ActionParameterDefinition(
                "parent", ActionParameterDefinition.TypeValue.String, "Parent domain name", "p")
            { IsDomainSuggestion = true };
            NeedsAthorizationParameter = new ActionParameterDefinition(
                "authorization", ActionParameterDefinition.TypeValue.Boolean, "Access to this domain needs user-authorization. Default value = false", "a");
            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(ParentParameter);
            ActionParametersDefinition.Add(NeedsAthorizationParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter) && IsParamOk(parameters, ParentParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter, string.Empty).ToWordPascalCase();
            if (Domain.IsDomainNameRepeated(project.Domain, name))
            {
                throw new Exception("Found repeated domain name. Domain name must be unique");
            }

            var parent = GetStringParameterValue(parameters, ParentParameter, string.Empty).ToWordPascalCase();
            var parentDomain = Domain.FindChildDomain(project.Domain, parent);
            Domain domain = new Domain(parentDomain, name);
            if (parentDomain != null)
            {
                if (parentDomain.HasModel)
                {
                    throw new Exception($"Domain named '{name}' has already schema defined. Domains with schema cannot contain child domains");
                }
                parentDomain.AddDomain(domain);
            }
            else
            {
                throw new Exception($"Cant find parent node named '{name}'");
            }

            var authorization = GetBoolParameterValue(parameters, NeedsAthorizationParameter, false);
            if (authorization)
            {
                domain.NeedsAuthorization = true;
            }
        }

    }
}
