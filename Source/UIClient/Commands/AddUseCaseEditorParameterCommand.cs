using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.Models.Inputs;
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
                    var requestModel = new GenericFormModel("Add new parameter");
                    requestModel.AddAttribute(ActionParameterDefinition.TypeValue.String, "type", "Type", "Type of the parameter", UseCaseParameter.GetUseCaseParameterTypesList().ToArray());
                    requestModel.AddAttribute(ActionParameterDefinition.TypeValue.String, "name", "Name", "Name of the parameter");
                    requestModel.AddAttribute(ActionParameterDefinition.TypeValue.String, "enumerabletype", "Enumerable type", "If type=Enumerable, this is the type of the enumerable", UseCaseParameter.GetUseCaseParameterTypesList().ToArray());
                    requestModel.AddAttribute(ActionParameterDefinition.TypeValue.String, "dictionarykeytype", "Dictionary key type", "If type=Dictionary, this is the type of the key", UseCaseParameter.GetUseCaseParameterTypesList().ToArray());
                    requestModel.AddAttribute(ActionParameterDefinition.TypeValue.String, "dictionaryvaluetype", "Dictionary value type", "If type=Dictionary, this is the type of the value", UseCaseParameter.GetUseCaseParameterTypesList().ToArray());
                    vm.GenericFormRequestId = Guid.NewGuid();
                    vm.EventManager.RaiseOnGenericFormInputRequestedEvent(vm.GenericFormRequestId, requestModel);
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            });
        }


    }

}
