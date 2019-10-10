using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class ModifyActionConfirmCommand : RelayCommand
    {
        public ModifyActionConfirmCommand(MainViewModel vm)
        {
            Initialize((input) => {
                try
                {
                    vm.ProjectManager.UpdateQueuedAction(
                        vm.Mapper.Map<ActionExecution>(vm.SelectedActionForModify), 
                        vm.SelectedActionForModifyParametersDefinitionsValues);
                    vm.SelectedActionForModify = null;
                    vm.RaiseStateChanged();
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }
    }
}
