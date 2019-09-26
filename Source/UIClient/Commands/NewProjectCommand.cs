using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.Utilities;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class NewProjectCommand : RelayCommand
    {
        public NewProjectCommand(MainViewModel vm)
        {
            Initialize((object parameter) =>
            {
                vm.ProjectManager.NewProject();
            }, (object parameter) =>
            {
                return true;
            });
        }
    }
}
