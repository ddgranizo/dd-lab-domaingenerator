using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
   
    public class OpenAddActionDialogCommand : RelayCommand
    {
        public OpenAddActionDialogCommand(MainViewModel vm)
        {
            Initialize((input) =>
            {
                try
                {
                    vm.SetActionDialog();
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }


    }
}
