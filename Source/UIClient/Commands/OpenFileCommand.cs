using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class OpenFileCommand : RelayCommand
    {
        public OpenFileCommand(MainViewModel vm)
        {
            Initialize(data =>
            {
                try
                {
                    vm.CleanErrors();
                    vm.OpenFile((string)data);
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            },
            data => { return true; });
        }
    }
}
