using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Actions.Architecture
{
    public class InitializeArchitectureSetup : ActionBase
    {
        public const string ActionName = "InitializeArchitectureSetup";

        public ActionParameterDefinition EnvironmentsParameter { get; set; }
        public ActionParameterDefinition NodesParameter { get; set; }


        public InitializeArchitectureSetup() : base(ActionName)
        {
            EnvironmentsParameter = new ActionParameterDefinition(
                "environments", ActionParameterDefinition.TypeValue.String, "Distribution of the environments", "e", string.Empty)
            {
                InputSuggestions = ArchitectureSetup.GetArchitectureEnvironmentsSetupTypesList(),
            };

            NodesParameter = new ActionParameterDefinition(
                "nodes", ActionParameterDefinition.TypeValue.String, "Distribution of the nodes", "n", string.Empty)
            {
                InputSuggestions = ArchitectureSetup.GetArchitectureNodesSetupTypesList(),
            };

            ActionParametersDefinition.Add(EnvironmentsParameter);
            ActionParametersDefinition.Add(NodesParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return true;
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var environmentsName = GetStringParameterValue(parameters, EnvironmentsParameter);
            var nodesName = GetStringParameterValue(parameters, NodesParameter);
            project.Architecture = new ArchitectureSetup();
            if (!string.IsNullOrEmpty(environmentsName))
            {
                var environments = ArchitectureSetup.StringToArchitectureEnvironmentsSetupType(environmentsName);
                project.Architecture.InitializeEnvironmentsSetup(environments);
            }
            if (!string.IsNullOrEmpty(nodesName))
            {
                var nodes = ArchitectureSetup.StringToArchitectureNodesSetupType(nodesName);
                project.Architecture.InitializeNodesSetup(project, nodes);
            }
        }
    }
}
