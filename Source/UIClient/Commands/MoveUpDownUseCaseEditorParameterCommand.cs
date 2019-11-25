using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
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
                    
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }


    }

}
