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
                    if (input != null)
                    {
                        if (direction == ParameterDirection.Input)
                        {
                            var parameters = vm.UseCase.InputParameters;
                            parameters.Remove((UseCaseParameterModel)input);
                            vm.UseCase.InputParameters = parameters;
                        }
                        else if (direction == ParameterDirection.Output)
                        {
                            var parameters = vm.UseCase.OutputParameters;
                            parameters.Remove((UseCaseParameterModel)input);
                            vm.UseCase.OutputParameters = parameters;
                        }
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
