using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using UIClient.Commands;
using UIClient.Models;
using UIClient.UserControls.Editors.UseCases;
using UIClient.ViewModels.Base;
using DD.DomainGenerator;

namespace UIClient.ViewModels
{
    public class UseCaseEditorControlViewModel : BaseViewModel
    {
        public enum ActionType
        {
            AddInputParameter,
            AddOutputParameter,
            ModifyInputParameter,
            ModifyOutputParameter,
            AddingNewExecutionSentence,
        }

        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value, UpdatedDomainEventManager); } }
        public UseCaseModel UseCase { get { return GetValue<UseCaseModel>(); } set { SetValue(value); } }
        public DataParameterModel SelectedInputUseCaseParameter { get { return GetValue<DataParameterModel>(); } set { SetValue(value); } }
        public DataParameterModel SelectedOutputUseCaseParameter { get { return GetValue<DataParameterModel>(); } set { SetValue(value); } }

        public ActionType CurrentFormInputActionType { get { return GetValue<ActionType>(); } set { SetValue(value); } }

        public bool RegisteredEventManager { get; set; } = false;

        private UseCaseEditorControlView _view;

        public ICommand AddInputParameterCommand { get; set; }
        public ICommand AddOutputParameterCommand { get; set; }
        public ICommand RemoveInputParameterCommand { get; set; }
        public ICommand RemoveOutputParameterCommand { get; set; }
        public ICommand ModifyInputParameterCommand { get; set; }
        public ICommand ModifyOutputParameterCommand { get; set; }
        public ICommand MoveUpInputParameterCommand { get; set; }
        public ICommand MoveUpOutputParameterCommand { get; set; }
        public ICommand MoveDownInputParameterCommand { get; set; }
        public ICommand MoveDownOutputParameterCommand { get; set; }

        public Guid GenericFormRequestId { get; set; }

        public UseCaseEditorControlViewModel()
        {
            AddInputParameterCommand = new AddUseCaseEditorParameterCommand(this, AddUseCaseEditorParameterCommand.ParameterDirection.Input);
            AddOutputParameterCommand = new AddUseCaseEditorParameterCommand(this, AddUseCaseEditorParameterCommand.ParameterDirection.Output);
            RemoveInputParameterCommand = new RemoveUseCaseEditorParameterCommand(this, RemoveUseCaseEditorParameterCommand.ParameterDirection.Input);
            RemoveOutputParameterCommand = new RemoveUseCaseEditorParameterCommand(this, RemoveUseCaseEditorParameterCommand.ParameterDirection.Output);
            ModifyInputParameterCommand = new ModifyUseCaseEditorParameterCommand(this, ModifyUseCaseEditorParameterCommand.ParameterDirection.Input);
            ModifyOutputParameterCommand = new ModifyUseCaseEditorParameterCommand(this, ModifyUseCaseEditorParameterCommand.ParameterDirection.Output);
            MoveUpInputParameterCommand = new MoveUpDownUseCaseEditorParameterCommand(this, MoveUpDownUseCaseEditorParameterCommand.ParameterDirection.Input, MoveUpDownUseCaseEditorParameterCommand.MovementDirection.Up);
            MoveUpOutputParameterCommand = new MoveUpDownUseCaseEditorParameterCommand(this, MoveUpDownUseCaseEditorParameterCommand.ParameterDirection.Output, MoveUpDownUseCaseEditorParameterCommand.MovementDirection.Up);
            MoveDownInputParameterCommand = new MoveUpDownUseCaseEditorParameterCommand(this, MoveUpDownUseCaseEditorParameterCommand.ParameterDirection.Input, MoveUpDownUseCaseEditorParameterCommand.MovementDirection.Down);
            MoveDownOutputParameterCommand = new MoveUpDownUseCaseEditorParameterCommand(this, MoveUpDownUseCaseEditorParameterCommand.ParameterDirection.Output, MoveUpDownUseCaseEditorParameterCommand.MovementDirection.Down);





            RegisterCommand(AddInputParameterCommand);
            RegisterCommand(AddOutputParameterCommand);
            RegisterCommand(RemoveInputParameterCommand);
            RegisterCommand(RemoveOutputParameterCommand);
            RegisterCommand(ModifyInputParameterCommand);
            RegisterCommand(ModifyOutputParameterCommand);
            RegisterCommand(MoveUpInputParameterCommand);
            RegisterCommand(MoveUpOutputParameterCommand);
            RegisterCommand(MoveDownInputParameterCommand);
            RegisterCommand(MoveDownOutputParameterCommand);
        }

        public void Initialize(UseCaseEditorControlView v)
        {
			_view = v;
        }


        private void UpdatedDomainEventManager(DomainEventManager manager)
        {
            if (!RegisteredEventManager)
            {
                RegisteredEventManager = true;
                manager.OnGenericFormInputResponsed += Manager_OnGenericFormInputResponsed;
            }
        }

        private void Manager_OnGenericFormInputResponsed(object sender, Events.GenericFormResponseEventArgs args)
        {
            if (args.RequestId == GenericFormRequestId && args.Completed)
            {
                AddIncomingParameterToUseCase(args.Values);
            }
        }

        private void AddIncomingParameterToUseCase(Dictionary<string, object> values)
        {
            var useCaseParameter = DictionaryToUseCaseParameterModel(values);
            if (CurrentFormInputActionType == ActionType.AddInputParameter)
            {
                var parameters = UseCase.InputParameters;
                parameters.Add(useCaseParameter);
                UseCase.InputParameters = parameters;
            }
            else if (CurrentFormInputActionType == ActionType.AddOutputParameter)
            {
                var parameters = UseCase.OutputParameters;
                parameters.Add(useCaseParameter);
                UseCase.OutputParameters = parameters;
            }
            else if (CurrentFormInputActionType == ActionType.ModifyInputParameter)
            {
                var parameters = UseCase.InputParameters;
                var item = parameters.First(k => k.Name == SelectedInputUseCaseParameter.Name);
                var index = parameters.IndexOf(item);
                parameters[index] = useCaseParameter;
                UseCase.InputParameters = parameters;
            }
            else if (CurrentFormInputActionType == ActionType.ModifyOutputParameter)
            {
                var parameters = UseCase.OutputParameters;
                var item = parameters.First(k => k.Name == SelectedOutputUseCaseParameter.Name);
                var index = parameters.IndexOf(item);
                parameters[index] = useCaseParameter;
                UseCase.OutputParameters = parameters;
            }
        }

        private static DataParameterModel DictionaryToUseCaseParameterModel(Dictionary<string, object> values)
        {
            DataParameterModel parameter = new DataParameterModel();
            if (CheckDictionaryContainsKey(values, Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.Type))
            {
                parameter.Type = DataParameter.ParseInputType(
                    (string)values[Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.Type]);
            }
            if (CheckDictionaryContainsKey(values, Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.Name))
            {
                parameter.Name = (string)values[Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.Name];
            }
            if (CheckDictionaryContainsKey(values, Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.DictionaryKeyType))
            {
                parameter.DictionaryKeyType = DataParameter.ParseInputType(
                    (string)values[Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.DictionaryKeyType]);
            }
            if (CheckDictionaryContainsKey(values, Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.DictionaryValueType))
            {
                parameter.DictionaryValueType = DataParameter.ParseInputType(
                    (string)values[Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.DictionaryValueType]);
            }
            if (CheckDictionaryContainsKey(values, Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.EnumerableType))
            {
                parameter.EnumerableType = DataParameter.ParseInputType(
                    (string)values[Definitions.UseCaseEditorDefinitions.UseCaseModelAttibutes.EnumerableType]);
            }


            if ((int)parameter.Type<=0)
            {
                throw new InvalidCastException($"UseCaseParameterModel requires at least Type paramater");
            }
            if (string.IsNullOrEmpty(parameter.Name))
            {
                throw new InvalidCastException($"UseCaseParameterModel requires at least Name paramater");
            }

            if (parameter.Type == DD.DomainGenerator.Definitions.DomainInputType.Enumerable)
            {
                if ((int)parameter.EnumerableType <= 0)
                {
                    throw new InvalidCastException($"UseCaseParameterModel with Type=Enumerable requires EnumerableType paramater");
                }
            }

            if (parameter.Type == DD.DomainGenerator.Definitions.DomainInputType.Dictionary)
            {
                if ((int)parameter.DictionaryKeyType <= 0)
                {
                    throw new InvalidCastException($"UseCaseParameterModel with Type=Dictionary requires DictionaryKeyType paramater");
                }
                else if ((int)parameter.DictionaryValueType <= 0)
                {
                    throw new InvalidCastException($"UseCaseParameterModel with Type=Dictionary requires DictionaryValueType paramater");
                }
            }

            if (parameter.Type == DD.DomainGenerator.Definitions.DomainInputType.Enumerable)
            {
                parameter.DictionaryKeyType = 0;
                parameter.DictionaryValueType = 0;
            }
            else if (parameter.Type == DD.DomainGenerator.Definitions.DomainInputType.Dictionary)
            {
                parameter.EnumerableType = 0;
            }


            return parameter;
        }

        private static bool CheckDictionaryContainsKey(Dictionary<string, object> values, string key)
        {
            return values.ContainsKey(key) && values[key] != null;
        }
    }
}
