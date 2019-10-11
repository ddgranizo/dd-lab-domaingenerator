﻿using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class AddActionCommand : RelayCommand
    {
        public AddActionCommand(MainViewModel vm)
        {
            Initialize(data =>
            {
                try
                {
                    vm.ProjectManager.AddQueueAction(vm.SelectedNewAction.Name, vm.NewActionParametersDefinitionsValues);
                    vm.SelectedNewAction = null;
                    vm.RaiseStateChanged();
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
                
            }, 
            data => { return vm.SelectedNewAction != null; });
        }
    }
}