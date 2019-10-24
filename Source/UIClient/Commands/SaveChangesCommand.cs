using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    public class SaveChangesCommand : RelayCommand
    {
        public SaveChangesCommand(MainViewModel vm)
        {
            Initialize((input) =>
            {
                try
                {
                    var path =
                        string.IsNullOrEmpty(vm.LastFileLoaded)
                        ? vm.GetInputText("Path for save the file", "Save the file")
                        : vm.LastFileLoaded;
                    vm.ProjectManager.SaveChanges(path);
                    vm.AddNewRecentFile(path);
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }

       
    }
}
