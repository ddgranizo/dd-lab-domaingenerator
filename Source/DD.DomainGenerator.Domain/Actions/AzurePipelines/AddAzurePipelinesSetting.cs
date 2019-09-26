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

        public AddAzurePipelinesSetting() : base(ActionName)
        {

            NameParameter = new ActionParameterDefinition(
                   "name", ActionParameterDefinition.TypeValue.String, "Name", "n");
            OrganizationUriParameter = new ActionParameterDefinition(
                "organizationuri", ActionParameterDefinition.TypeValue.String, "Organization URI. Use https://xxxxx.visualstudio.com/", "o");
            TokenParameter = new ActionParameterDefinition(
                "token", ActionParameterDefinition.TypeValue.Password, "Token for access", "t");
            ProjectIdParameter = new ActionParameterDefinition(
                "projectid", ActionParameterDefinition.TypeValue.Guid, "Azure pipelines project id", "p");
            
            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(ProjectIdParameter);
            ActionParametersDefinition.Add(OrganizationUriParameter);
            ActionParametersDefinition.Add(TokenParameter);

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

            var repeated = project.AzurePipelineSettings
                .FirstOrDefault(k => k.Name == name) 
                ?? throw new Exception("Repeated Azure pipeline setting");

            var standardUri = StringFormats.ParseStringUri(organizationUri);
            
            project.AzurePipelineSettings.Add(new AzurePipelineSetting(name, standardUri.ToString(), token, projectId));
        }
    }
}
