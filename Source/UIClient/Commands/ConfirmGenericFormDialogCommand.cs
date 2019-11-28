using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
   
    public class ConfirmGenericFormDialogCommand : RelayCommand
    {
        public ConfirmGenericFormDialogCommand(GenericFormControlViewModel vm)
        {
            Initialize((input) =>
            {
                try
                {
                    vm.ConfirmValues();
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }


    }
}
