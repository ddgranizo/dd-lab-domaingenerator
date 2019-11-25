using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.ViewModels;

namespace UIClient.Commands
{
    
    public class AddUseCaseEditorParameterCommand : RelayCommand
    {
        public enum ParameterDirection
        {
            Input,
            Output
        }

        public AddUseCaseEditorParameterCommand(UseCaseEditorControlViewModel vm, ParameterDirection direction)
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
