using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.Commands.Base;
using UIClient.Models;
using UIClient.ViewModels;

namespace UIClient.Commands
{

    public class CheckDeployActionUnitCommand : RelayCommand
    {
        public CheckDeployActionUnitCommand(MainViewModel vm)
        {
            Initialize(data =>
            {
                try
                {
                    var deployActionModel = (DeployActionUnitModel)data;
                    vm.ProjectManager.ExecuteDeployActionUnitCheck(vm.Mapper.Map<DeployActionUnit>(deployActionModel));
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }

            });
        }
    }
}
