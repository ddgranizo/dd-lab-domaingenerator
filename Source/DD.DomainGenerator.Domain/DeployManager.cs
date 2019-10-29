using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.DomainGenerator
{
    public class DeployManager
    {
        public enum Phases
        {
            EmptyProject = 1,
            AvailableGithubRepositories = 10,
            AvailableApis = 20,
            AvailableAzurePipeline = 30,
        }

        public Phases CurrentPhase { get; set; }

        public DeployManager()
        {

        }


        public void ExecuteDeployAction(DeployActionUnit action, ActionExecution actionExecution, ProjectState projectState)
        {
            if (action.State == DeployActionUnit.DeployState.Queued)
            {
                action.State = DeployActionUnit.DeployState.Executing;
                action.SetResponseException(null);
                action.SetResponseParameters(null);

                var response = action.Action.Invoke(actionExecution, projectState);
                if (!response.IsError)
                {
                    
                    action.SetResponseParameters(response.Parameters);
                    action.State = DeployActionUnit.DeployState.Completed;
                }
                else
                {
                    action.SetResponseException(response.Exception);
                    action.State = DeployActionUnit.DeployState.Error;
                    throw response.Exception;
                }
            }
            
        }
    }
}
