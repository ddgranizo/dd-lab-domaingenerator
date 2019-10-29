using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{

    public class ChangeCurrentRealVirtualStateCommand : RelayCommand
    {
        public ChangeCurrentRealVirtualStateCommand(MainViewModel vm)
        {
            Initialize(data =>
            {
                try
                {
                    if (vm.IsActiveVirtualState == true)
                    {
                        vm.CurrentState = vm.State;
                        vm.IsActiveVirtualState = false;
                    }
                    else
                    {
                        vm.CurrentState = vm.VirtualState;
                        vm.IsActiveVirtualState = true;
                    }
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }

            });
        }
    }
}
