using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class RemoveSelectedNewActionCommand : RelayCommand
    {
        public RemoveSelectedNewActionCommand(MainViewModel vm)
        {
            Initialize(data =>
            {
                vm.SelectedNewAction = null;
                vm.SelectedActionForModify = null;
            }, 
            data => { 
                return vm.SelectedNewAction != null
                || vm.SelectedActionForModify != null; });
        }
    }
}
