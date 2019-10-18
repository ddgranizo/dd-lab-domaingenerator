using System;
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
                    var path =
                        string.IsNullOrEmpty(vm.LastFileLoaded)
                        ? vm.GetInputText("Path for save the file", "Save the file")
                        : vm.LastFileLoaded;
                    vm.ProjectManager.SaveChanges(path);
                    var currentProjectsStored = vm.StoredRecentProjectsService.GetStoredData();
                    if (currentProjectsStored.Paths.IndexOf(vm.LastFileLoaded) == -1)
                    {
                        currentProjectsStored.Paths.Add(path);
                        vm.StoredRecentProjectsService.SaveStoredData(currentProjectsStored);
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
