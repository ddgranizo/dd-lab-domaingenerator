using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class ExecuteActionCommand : RelayCommand
    {
        public ExecuteActionCommand(MainViewModel vm)
        {
            Initialize((input) =>
            {
                try
                {
                    //vm.CleanErrors();
                    vm.SelectedNewAction.PrepareExecution(vm.ProjectState, vm.NewActionParametersDefinitionsValues.ToActionParametersList());
                    vm.RaiseStateChanged();
                    vm.UnsetDialog();
                    //vm.ProjectManager.AddActionForExecute(vm.SelectedNewAction, vm.NewActionParametersDefinitionsValues);
                    //vm.SelectedNewAction = null;
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            }, k => vm.SelectedNewAction != null);
        }
    }
}
