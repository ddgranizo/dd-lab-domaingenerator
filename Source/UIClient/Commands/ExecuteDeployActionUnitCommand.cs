using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.Commands.Base;
using UIClient.Models;
using UIClient.ViewModels;

namespace UIClient.Commands
{

    public class ExecuteDeployActionUnitCommand : RelayCommand
    {
        public ExecuteDeployActionUnitCommand(MainViewModel vm)
        {
            Initialize(data =>
            {
                try
                {
                    var deployActionModel = (DeployActionUnitModel)data;
                    var deployAction = vm.Mapper.Map<DeployActionUnit>(deployActionModel);
                    var actionExecution = vm.ProjectManager.ProjectState.Actions.First(k => k.Id == deployActionModel.ActionId);
                    var deploySourceAction = vm.ProjectManager.ActionManager.Actions.First(k => k.Name == actionExecution.ActionName)
                        .DeployActions.First(k => k.Name == deployActionModel.Name);

                    deploySourceAction.State = DeployActionUnit.DeployState.Queued;
                    vm.ProjectManager.DeployManager.ExecuteDeployAction(
                        deploySourceAction, actionExecution, vm.ProjectManager.ProjectState);
                    var saveCommand = vm.SaveChangesCommand;
                    if (saveCommand.CanExecute(null))
                    {
                        saveCommand.Execute(null);
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
