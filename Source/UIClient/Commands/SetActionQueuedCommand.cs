using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class SetActionQueuedCommand : RelayCommand
    {
        public SetActionQueuedCommand(MainViewModel vm)
        {
            Initialize((input) =>
            {
                try
                {
                    var currentAction = vm.Mapper.Map<ActionExecution>( vm.SelectedAction);
                    vm.ProjectManager.QueueAction(currentAction);
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            }, k => !vm.IsActiveVirtualState && vm.SelectedAction!=null);
        }
    }
}
