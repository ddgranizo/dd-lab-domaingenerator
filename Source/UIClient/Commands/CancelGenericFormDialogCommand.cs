using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
   
    public class CancelGenericFormDialogCommand : RelayCommand
    {
        public CancelGenericFormDialogCommand(GenericFormControlViewModel vm)
        {
            Initialize((input) =>
            {
                try
                {
                    vm.Cancel();
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }


    }
}
