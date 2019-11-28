using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Commands.Base;
using UIClient.Models;
using UIClient.Models.Inputs;
using UIClient.ViewModels;

namespace UIClient.Commands
{

    public class ModifyUseCaseEditorParameterCommand : RelayCommand
    {
        public enum ParameterDirection
        {
            Input,
            Output
        }

        public ModifyUseCaseEditorParameterCommand(UseCaseEditorControlViewModel vm, ParameterDirection direction)
        {
            Initialize((input) =>
            {
                try
                {
                    var useCaseItem = direction == ParameterDirection.Input
                        ? vm.SelectedInputUseCaseParameter
                        : vm.SelectedOutputUseCaseParameter;
                    var requestModel = new GenericFormModel("Modify parameter");
                    requestModel.AddAttribute(
                        ActionParameterDefinition.TypeValue.String,
                        Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.Type,
                        "Type",
                        "Type of the parameter",
                        DataParameter.GetUseCaseParameterTypesList().ToArray(),
                        useCaseItem.Type.ToString());
                    requestModel.AddAttribute(ActionParameterDefinition.TypeValue.String,
                        Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.Name,
                        "Name",
                        "Name of the parameter",
                         useCaseItem.Name);
                    requestModel.AddAttribute(
                        ActionParameterDefinition.TypeValue.String,
                        Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.EnumerableType,
                        "Enumerable type",
                        "If type=Enumerable, this is the type of the enumerable",
                        DataParameter.GetUseCaseParameterTypesList().ToArray(),
                       useCaseItem.EnumerableType.ToString());
                    requestModel.AddAttribute(ActionParameterDefinition.TypeValue.String,
                        Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.DictionaryKeyType,
                        "Dictionary key type",
                        "If type=Dictionary, this is the type of the key",
                        DataParameter.GetUseCaseParameterTypesList().ToArray(),
                        useCaseItem.DictionaryKeyType.ToString());
                    requestModel.AddAttribute(
                        ActionParameterDefinition.TypeValue.String,
                        Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.DictionaryValueType,
                        "Dictionary value type",
                        "If type=Dictionary, this is the type of the value",
                        DataParameter.GetUseCaseParameterTypesList().ToArray(),
                        useCaseItem.DictionaryValueType.ToString());
                    vm.GenericFormRequestId = Guid.NewGuid();

                    if (direction == ParameterDirection.Input)
                    {
                        vm.CurrentFormInputActionType = UseCaseEditorControlViewModel.ActionType.ModifyInputParameter;
                    }
                    else if (direction == ParameterDirection.Output)
                    {
                        vm.CurrentFormInputActionType = UseCaseEditorControlViewModel.ActionType.ModifyOutputParameter;
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
                return direction == ParameterDirection.Input && vm.SelectedInputUseCaseParameter != null
                        || direction == ParameterDirection.Output && vm.SelectedOutputUseCaseParameter != null;
            });
        }

    }

}
