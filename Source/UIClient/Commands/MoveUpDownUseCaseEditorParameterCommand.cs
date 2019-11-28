using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.Commands.Base;
using UIClient.Models;
using UIClient.ViewModels;

namespace UIClient.Commands
{

    public class MoveUpDownUseCaseEditorParameterCommand : RelayCommand
    {
        public enum ParameterDirection
        {
            Input,
            Output
        }

        public enum MovementDirection
        {
            Up,
            Down
        }

        public MoveUpDownUseCaseEditorParameterCommand(UseCaseEditorControlViewModel vm, ParameterDirection parameterDirection, MovementDirection movementDirection)
        {
            Initialize((input) =>
            {
                try
                {
                    var useCaseItem = vm.SelectedInputUseCaseParameter;
                    var list = vm.UseCase.InputParameters;
                    var item = vm.SelectedInputUseCaseParameter;

                    var originalIndex = list.IndexOf(item);
                    list.RemoveAt(originalIndex);
                    if (movementDirection == MovementDirection.Up)
                    {
                        list.Insert(originalIndex - 1, item);
                    }
                    else if (movementDirection == MovementDirection.Down)
                    {
                        list.Insert(originalIndex + 1, item);
                    }
                    vm.UseCase.InputParameters = list;
                    vm.SelectedInputUseCaseParameter = vm.UseCase.InputParameters.First(k => k.Name == item.Name);
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            }, data =>
            {
                return (parameterDirection == ParameterDirection.Input
                            && vm.SelectedInputUseCaseParameter != null
                            && vm.UseCase?.InputParameters.Count > 0
                            && (movementDirection == MovementDirection.Down
                                    && vm.UseCase?.InputParameters.IndexOf(vm.SelectedInputUseCaseParameter) < vm.UseCase?.InputParameters.Count - 1)
                                || (movementDirection == MovementDirection.Up
                                    && vm.UseCase?.InputParameters.IndexOf(vm.SelectedInputUseCaseParameter) > 0));
            });
        }


    }

}
