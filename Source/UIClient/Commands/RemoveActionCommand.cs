using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class RemoveActionCommand : RelayCommand
    {
        public RemoveActionCommand(MainViewModel vm)
        {
            Initialize(data =>
            {
                if (vm.SelectedAction.State != ActionExecution.ActionExecutionState.NoQueued)
                {
                    vm.RaiseError("This action cannot be deleted. Only 'queued' actions can be deleted");
                    return;
                }
                vm.RaiseOkCancelDialog("Do you want to delete this action?", "Delete action", () =>
                {
                    try
                    {
                        vm.CleanErrors();
                        vm.ProjectManager.RemoveQueuedAction(vm.Mapper.Map<ActionExecution>(vm.SelectedAction));
                    }
                    catch (Exception ex)
                    {
                        vm.RaiseError(ex.Message);
                    }
                    vm.RaiseStateChanged();
                });
            }, 
            data => { return vm.SelectedAction != null; });
        }
    }
}
