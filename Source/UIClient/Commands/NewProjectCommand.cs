﻿using DD.DomainGenerator.Actions.Project;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class NewProjectCommand: RelayCommand
    {
        public NewProjectCommand(MainViewModel vm)
        {
            Initialize((input) => {
                try
                {
                    vm.NewProject();
                    vm.OpenMenuForAction(InitializeProject.ActionName);
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }
    }
}
