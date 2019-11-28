using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.Models.Inputs;
using UIClient.ViewModels;

namespace UIClient.Commands
{

    public class AddUseCaseEditorExecutionSentenceCommand : RelayCommand
    {
        public enum ParameterDirection
        {
            Input,
            Output
        }

        public AddUseCaseEditorExecutionSentenceCommand(UseCaseEditorControlViewModel vm, ParameterDirection direction)
        {
            Initialize((input) =>
            {
                try
                {
                    var requestModel = new GenericFormModel("Add new parameter");
                    requestModel.AddAttribute(ActionParameterDefinition.TypeValue.String,
                        Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.Type,
                        "Type",
                        "Type of the parameter",
                        DataParameter.GetUseCaseParameterTypesList().ToArray());
                    requestModel.AddAttribute(
                        ActionParameterDefinition.TypeValue.String,
                        Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.Name,
                        "Name",
                        "Name of the parameter");
                    requestModel.AddAttribute(ActionParameterDefinition.TypeValue.String,
                        Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.EnumerableType,
                        "Enumerable type",
                        "If type=Enumerable, this is the type of the enumerable",
                        DataParameter.GetUseCaseParameterTypesList().ToArray());
                    requestModel.AddAttribute(
                        ActionParameterDefinition.TypeValue.String,
                        Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.DictionaryKeyType,
                        "Dictionary key type",
                        "If type=Dictionary, this is the type of the key",
                        DataParameter.GetUseCaseParameterTypesList().ToArray());
                    requestModel.AddAttribute(ActionParameterDefinition.TypeValue.String,
                        Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.DictionaryValueType,
                        "Dictionary value type",
                        "If type=Dictionary, this is the type of the value",
                        DataParameter.GetUseCaseParameterTypesList().ToArray());
                    vm.GenericFormRequestId = Guid.NewGuid();
                    if (direction == ParameterDirection.Input)
                    {
                        vm.CurrentFormInputActionType = UseCaseEditorControlViewModel.ActionType.AddInputParameter;
                    }
                    else if (direction == ParameterDirection.Output)
                    {
                        vm.CurrentFormInputActionType = UseCaseEditorControlViewModel.ActionType.AddOutputParameter;
                    }
                    vm.EventManager.RaiseOnGenericFormInputRequestedEvent(vm.GenericFormRequestId, requestModel);
                    vm.RaiseCanExecuteCommandChanged();
                }
                catch (Exception ex)
                {
                    vm.RaiseError(ex.Message);
                }
            }, data =>
            {
                return direction == ParameterDirection.Input || 
                    (direction == ParameterDirection.Output && vm.UseCase?.OutputParameters.Count == 0);
            });
        }

    }
}
