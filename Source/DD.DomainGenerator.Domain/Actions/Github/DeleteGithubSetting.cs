using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Actions.Github
{
    public class DeleteGithubSetting : ActionBase
    {
        public const string ActionName = "DeleteGithubSetting";
        public ActionParameterDefinition NameParameter { get; set; }
        public DeleteGithubSetting() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Name", "n");

            ActionParametersDefinition.Add(NameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter);

            var setting = project.GithubSettings
                .FirstOrDefault(k => k.Name == name)
                ?? throw new Exception($"Can't find any setting named '{name}'");
            project.GithubSettings.Remove(setting);
        }
    }
}
