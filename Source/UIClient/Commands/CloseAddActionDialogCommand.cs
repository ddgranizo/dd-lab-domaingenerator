using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
   
    public class CloseAddActionDialogCommand : RelayCommand
    {
        public CloseAddActionDialogCommand(MainViewModel vm)
        {
            Initialize((input) =>
            {
                try
                {
                    vm.UnsetDialog();
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }


    }
}
