using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class ModifyActionRequestCommand : RelayCommand
    {
        public ModifyActionRequestCommand(MainViewModel vm)
        {
            Initialize((input) => {
                try
                {
                    vm.SelectedActionForModify = vm.SelectedAction;
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }
    }
}
