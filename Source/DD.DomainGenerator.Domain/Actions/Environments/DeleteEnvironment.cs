using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;

namespace DD.DomainGenerator.Actions.Environments
{
    public class DeleteEnvironment : ActionBase
    {
        public const string ActionName = "DeleteEnvironment";
        
        public ActionParameterDefinition NameParameter { get; set; }

        public DeleteEnvironment() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                   "name", ActionParameterDefinition.TypeValue.String, "Evironment name", "n", string.Empty)
            { IsEnvironmentSuggestion = true};
            ActionParametersDefinition.Add(NameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter);
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter);
            var exists = project.Environments
                .FirstOrDefault(k => k.Name == name);
            if (exists == null)
            {
                throw new Exception($"Can't find any environment named '{name}'");
            }
            project.Environments.Remove(exists);
        }
    }
}
