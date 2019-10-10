﻿using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class SaveChangesCommand : RelayCommand
    {
        public SaveChangesCommand(MainViewModel vm)
        {
            Initialize((input) => {
                try
                {
                    vm.ProjectManager.SaveChanges(vm.LastFileLoaded);
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }
    }
}
