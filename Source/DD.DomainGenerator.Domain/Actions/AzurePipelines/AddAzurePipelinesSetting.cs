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

namespace DD.DomainGenerator.Actions.AzurePipelines
{
    public class AddAzurePipelinesSetting : ActionBase
    {
        public const string ActionName = "AddAzurePipelinesSetting";
        public ActionParameterDefinition OrganizationUriParameter { get; set; }
        public ActionParameterDefinition TokenParameter { get; set; }
        public ActionParameterDefinition ProjectIdParameter { get; set; }
        public ActionParameterDefinition NameParameter { get; set; }
        public ICryptoService CryptoService { get; }

        public AddAzurePipelinesSetting(ICryptoService cryptoService) : base(ActionName)
        {

            NameParameter = new ActionParameterDefinition(
                   "name", ActionParameterDefinition.TypeValue.String, "Name", "n");
            OrganizationUriParameter = new ActionParameterDefinition(
                "organizationuri", ActionParameterDefinition.TypeValue.String, "Organization URI. Use https://xxxxx.visualstudio.com/", "o");
            TokenParameter = new ActionParameterDefinition(
                "token", ActionParameterDefinition.TypeValue.String, "Token for access", "t");
            ProjectIdParameter = new ActionParameterDefinition(
                "projectid", ActionParameterDefinition.TypeValue.Guid, "Azure pipelines project id", "p");
            
            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(ProjectIdParameter);
            ActionParametersDefinition.Add(OrganizationUriParameter);
            ActionParametersDefinition.Add(TokenParameter);

            CryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter) && 
                IsParamOk(parameters, OrganizationUriParameter) && 
                IsParamOk(parameters, TokenParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter);
            var organizationUri = GetStringParameterValue(parameters, OrganizationUriParameter);
            var token = GetStringParameterValue(parameters, TokenParameter);
            var projectId = GetGuidParameterValue(parameters, ProjectIdParameter);
            var tokenCrypted = CryptoService.Encrypt(token);

            var repeated = project.AzurePipelineSettings
                .FirstOrDefault(k => k.OrganizationUri == organizationUri
                                    && k.Token == tokenCrypted
                                    && k.ProjectId == projectId) 
                ?? throw new Exception("Repeated Azure pipeline setting");

            var standardUri = StringFormats.ParseStringUri(organizationUri);
            
            project.AzurePipelineSettings.Add(new AzurePipelineSetting(name, standardUri.ToString(), tokenCrypted, projectId));
        }
    }
}
