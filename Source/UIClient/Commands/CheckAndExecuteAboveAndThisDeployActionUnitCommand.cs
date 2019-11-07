using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UIClient.Commands.Base;
using UIClient.Models;
using UIClient.ViewModels;

namespace UIClient.Commands
{

    public class CheckAndExecuteAboveAndThisDeployActionUnitCommand : RelayCommand
    {
        public CheckAndExecuteAboveAndThisDeployActionUnitCommand(MainViewModel vm)
        {
            Initialize(data =>
            {
                try
                {
                    var deployActionModel = (DeployActionUnitModel)data;
                    var deployAction = vm.ProjectManager.SearchActionUnit
                        (vm.ProjectManager.DeployActions, vm.Mapper.Map<DeployActionUnit>(deployActionModel));

                    var index = vm.ProjectManager.DeployActions.IndexOf(deployAction);
                    for (int i = 0; i <= index; i++)
                    {
                        var action = vm.ProjectManager.DeployActions[i];
                        if (action.State == DeployActionUnit.DeployState.NotInitiated)
                        {
                            vm.ProjectManager.ExecuteDeployActionUnitCheck(action);
                        }
                        if (action.State == DeployActionUnit.DeployState.QueuedForExecution)
                        {
                            vm.ProjectManager.ExecuteDeployActionUnitExecution(action);
                        }
                        if (action.State == DeployActionUnit.DeployState.Error)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }
    }
}
