using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.Models;
using UIClient.ViewModels;

namespace UIClient.Commands
{

    public class RemoveUseCaseEditorParameterCommand : RelayCommand
    {
        public enum ParameterDirection
        {
            Input,
            Output
        }

        public RemoveUseCaseEditorParameterCommand(UseCaseEditorControlViewModel vm, ParameterDirection direction)
        {
            Initialize((input) =>
            {
                try
                {
                    var useCaseItem = direction == ParameterDirection.Input
                        ? vm.SelectedInputUseCaseParameter
                        : vm.SelectedOutputUseCaseParameter;

                    if (direction == ParameterDirection.Input)
                    {
                        var parameters = vm.UseCase.InputParameters;
                        parameters.Remove((DataParameterModel)input);
                        vm.UseCase.InputParameters = parameters;
                    }
                    else if (direction == ParameterDirection.Output)
                    {
                        var parameters = vm.UseCase.OutputParameters;
                        parameters.Remove((DataParameterModel)input);
                        vm.UseCase.OutputParameters = parameters;
                    }
                    vm.RaiseCanExecuteCommandChanged();
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            }, data =>
            {
                return direction == ParameterDirection.Input && vm.SelectedInputUseCaseParameter != null
                        || direction == ParameterDirection.Output && vm.SelectedOutputUseCaseParameter != null;
            });
        }

    }

}
