using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Actions.Github
{
    public class AddGithubSetting : ActionBase
    {
        public const string ActionName = "AddGithubSetting";
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition OAuthTokenKeyParameter { get; set; }
        public AddGithubSetting() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Name", "n");
            OAuthTokenKeyParameter = new ActionParameterDefinition(
                "oauthtokenkey", ActionParameterDefinition.TypeValue.String, "Oauth token key for access", "t");

            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(OAuthTokenKeyParameter);

        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter) && IsParamOk(parameters, OAuthTokenKeyParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter);
            var token = GetStringParameterValue(parameters, OAuthTokenKeyParameter);

            var repeated = project.GithubSettings
                .FirstOrDefault(k => k.OauthToken == token)
                ?? throw new Exception("Repeated Github setting");

            project.GithubSettings.Add(new GithubSetting(name, token));
        }
    }
}
