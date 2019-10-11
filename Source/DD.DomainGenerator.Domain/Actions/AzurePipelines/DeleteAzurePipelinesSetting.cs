using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;

namespace DD.DomainGenerator.Actions.AzurePipelines
{
    public class DeleteAzurePipelinesSetting : ActionBase
    {
        public const string ActionName = "DeleteAzurePipelinesSetting";
        public ActionParameterDefinition NameParameter { get; set; }
        public DeleteAzurePipelinesSetting() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Name", "o", string.Empty);
            ActionParametersDefinition.Add(NameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter);
            var setting = project.AzurePipelineSettings
                .FirstOrDefault(k => k.Name == name)
                ?? throw new Exception($"Can't find any setting named '{name}'");
            project.AzurePipelineSettings.Remove(setting);
        }
    }
}
