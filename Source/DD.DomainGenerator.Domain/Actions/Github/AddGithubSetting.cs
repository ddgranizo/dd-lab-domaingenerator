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
        public ActionParameterDefinition UriParameter { get; set; }
        public ICryptoService CryptoService { get; }

        public AddGithubSetting(ICryptoService cryptoService) : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Name", "n", string.Empty);
            OAuthTokenKeyParameter = new ActionParameterDefinition(
                "oauthtokenkey", ActionParameterDefinition.TypeValue.Password, "Oauth token key for access", "t", string.Empty);
            UriParameter = new ActionParameterDefinition(
                "uri", ActionParameterDefinition.TypeValue.String, "Github URI. Use https://github.com/", "u", string.Empty);

            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(OAuthTokenKeyParameter);
            ActionParametersDefinition.Add(UriParameter);
            CryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter)
                && IsParamOk(parameters, OAuthTokenKeyParameter)
                && IsParamOk(parameters, UriParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter);
            var token = GetStringParameterValue(parameters, OAuthTokenKeyParameter);
            var uri = GetStringParameterValue(parameters, UriParameter);
            var repeated = project.GithubSettings
                .FirstOrDefault(k => k.OauthToken == token);
            if (repeated != null)
            {
                throw new Exception("Repeated Github setting");
            }

            var standardUri = StringFormats.ParseStringUri(uri)
                ?? throw new Exception("Invalid uri");

            var decriptedToken = CryptoService.Decrypt(token);

            project.GithubSettings.Add(new GithubSetting(name, standardUri.ToString(), decriptedToken));
        }
    }
}
