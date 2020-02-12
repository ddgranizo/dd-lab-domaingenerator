using AutoMapper;
using DD.Lab.Wpf.Commands;
using DD.Lab.Wpf.Commands.Base;
using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Services;
using DD.Lab.Wpf.Drm.Services.Implementations;
using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Controls.Sentences;
using DomainGeneratorUI.Extensions;
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Models;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Models.RepositoryMethods;
using DomainGeneratorUI.Models.UseCases;
using DomainGeneratorUI.Models.UseCases.Sentences;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels.Methods;
using DomainGeneratorUI.Viewmodels.UseCases;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using DomainGeneratorUI.Windows;
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


namespace DomainGeneratorUI.Viewmodels.Sentences
{
    public class ExecuteUseCaseSentenceViewModel : BaseViewModel
    {

        public ExecuteUseCaseSentenceInputData ExecuteUseCseSentenceInputData { get { return GetValue<ExecuteUseCaseSentenceInputData>(); } set { SetValue(value, UpdatedExecuteUseCseSentenceInputData); } }
        public UseCaseSentenceViewModel BasicSentence { get { return GetValue<UseCaseSentenceViewModel>(); } set { SetValue(value, UpdatedBasicSentence); } }
        public GenericManager GenericManager { get; set; }
        public ExecuteUseCaseSentence Sentence { get; set; }
        public List<MethodParameterReferenceViewModel> ParentInputParameters { get; set; }
        public List<MethodParameterReferenceViewModel> ParentOutputParameters { get; set; }

        private ExecuteUseCaseSentenceView _view;
        public string Description { get { return GetValue<string>(); } set { SetValue(value); } }
        public string DisplayName { get { return GetValue<string>(); } set { SetValue(value); } }
        public Guid UseCaseId { get { return GetValue<Guid>(); } set { SetValue(value); } }
        public bool AreInputsOk { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsUseCaseOk { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool ShowParametersMenu { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool UseCaseHasZeroParameters { get; set; }

        public IMapper Mapper { get; private set; }

        public ExecuteUseCaseSentenceViewModel()
        {
            InitializeCommands();
            InitializeMapper();
        }

        public void Initialize(ExecuteUseCaseSentenceView v)
        {
            _view = v;
        }

        private void InitializeMapper()
        {
            Mapper = new Mapper(ConfigureMappingProfiles());
        }

        public ICommand EditCommand { get; set; }
        public ICommand ManageInputsCommand { get; set; }
        private void InitializeCommands()
        {
            EditCommand = new RelayCommandHandled((input) =>
            {
                var finderRecord = new GenericRecordFinderWindow(GenericManager, Domain.LogicalName, Guid.Empty, UseCase.LogicalName);
                finderRecord.ShowDialog();
                if (finderRecord.Response == DD.Lab.Wpf.Windows.WindowResponse.OK)
                {
                    var record = finderRecord.ResponseValue;
                    var useCase = Entity.DictionartyToEntity<UseCase>(record.Values);
                    UpdatedExecuteUseCaseSentence(useCase);
                }
            });

            ManageInputsCommand = new RelayCommandHandled((input) =>
            {
                var parameters = GenericManager.ParserService.Clone<List<MethodParameterReferenceValueViewModel>>
                            (BasicSentence.ReferencedInputParametersValues);

                var window = new InputParameterSelectorWindow(
                    GenericManager,
                    BasicSentence.InputParameters,
                    ParentInputParameters,
                    parameters);
                window.ShowDialog();
                if (window.Response == DD.Lab.Wpf.Windows.WindowResponse.OK)
                {
                    var response = window.OutputMethodInputParametersReferenceValues;
                    UpdatedExecuteUseCaseSentenceInputParameters(response);
                }
            });

            RegisterCommand(ManageInputsCommand);
            RegisterCommand(EditCommand);
        }


        private void CheckIfSentenceIsOk()
        {
            CheckIfUseCaseIsOk();
            CheckIfInputsAreOk();
            ShowParametersMenu = IsUseCaseOk && !UseCaseHasZeroParameters;
        }

        private void CheckIfInputsAreOk()
        {
            if (IsUseCaseOk)
            {

                var useCase = Entity.DictionartyToEntity<UseCase>
                    (GenericManager.Retrieve(UseCase.LogicalName, Sentence.UseCaseId.Id).Values);
                var useCaseContentJson = useCase.Content;
                var useCaseContent = GenericManager
                    .ParserService
                    .ObjectifyWithTypes<UseCaseContent>(useCaseContentJson);

                var notFoundParameters = new List<MethodParameter>();
                foreach (var item in useCaseContent.Parameters
                                            .Where(k=>k.Direction == MethodParameter.ParameterDirection.Input))
                {
                    var setParameter = Sentence
                        .ReferencedInputParametersValues
                        .FirstOrDefault(k => k.RegardingMethodParameter != null
                            && k.RegardingMethodParameter.Name == item.Name
                            && k.RegardingMethodParameter.Type == item.Type);
                    if (setParameter == null)
                    {
                        notFoundParameters.Add(item);
                    }
                }
                if (notFoundParameters.Count > 0)
                {
                    Sentence
                        .ReferencedInputParametersValues = new List<MethodParameterReferenceValue>();
                }
                UseCaseHasZeroParameters = useCaseContent.Parameters
                                            .Where(k => k.Direction == MethodParameter.ParameterDirection.Input).Count() == 0;
                bool isUnsetParameter = Sentence.ReferencedInputParametersValues.Count == 0;
                bool areAllInputsOk = true;
                foreach (var item in Sentence
                        .ReferencedInputParametersValues)
                {
                    bool inputIsOk = true;
                    if (item.Type == MethodParameterReferenceValue.ValueType.Constant)
                    {
                        inputIsOk = item.ConstantValue != null;
                    }
                    else if (item.Type == MethodParameterReferenceValue.ValueType.SentenceOutput)
                    {
                        inputIsOk = item.RegardingMethodParameter != null 
                                    && item.RegardingReferenceMethodParameter != null;
                    }
                    else if (item.Type == MethodParameterReferenceValue.ValueType.UseCaseInput)
                    {
                        inputIsOk = item.RegardingMethodParameter != null;
                    }
                    areAllInputsOk = !areAllInputsOk ? areAllInputsOk : inputIsOk;
                }

                AreInputsOk = UseCaseHasZeroParameters || areAllInputsOk && !isUnsetParameter;

            }
        }

        private void CheckIfUseCaseIsOk()
        {
            IsUseCaseOk = Sentence.UseCaseId != null
                   && Sentence.UseCaseId.Id != Guid.Empty;
        }

        private void UpdatedExecuteUseCaseSentenceInputParameters(List<MethodParameterReferenceValueViewModel> parameters)
        {
            BasicSentence.ReferencedInputParametersValues = parameters;
            _view.RaiseUpdateUseCaseSentenceEvent(parameters, BasicSentence);
        }

        private void UpdatedExecuteUseCaseSentence(UseCase useCase)
        {
            var schemaId = useCase.SchemaId.Id;
            var schema = Entity.DictionartyToEntity<Schema>(GenericManager.Retrieve(Schema.LogicalName, schemaId).Values);
            var newSentence = new ExecuteUseCaseSentence(schema.Name,  useCase);
            _view.RaiseUpdateUseCaseSentenceEvent(newSentence, BasicSentence);
        }

        private void UpdatedExecuteUseCseSentenceInputData(ExecuteUseCaseSentenceInputData data)
        {
            ParentInputParameters = data.ParentInputParameters;
            ParentOutputParameters = data.ParentOutputParameters;
            GenericManager = data.GenericManager;
            BasicSentence = data.Sentence;
        }

        private void UpdatedBasicSentence(UseCaseSentenceViewModel data)
        {
            var sentence = Mapper.Map<UseCaseSentence>(BasicSentence);
            SetCurrentSentenceFromUseCase(sentence);
            CheckIfSentenceIsOk();
        }

        private void SetCurrentSentenceFromUseCase(UseCaseSentence sentence)
        {
            Sentence = new ExecuteUseCaseSentence(sentence);

            var description = Sentence.Description;
            if (description == null
                || description != null && description.Trim() == string.Empty)
            {
                description = "Add description";
            }
            Description = description;

            var displayName = Sentence.DisplayName;
            if (displayName == null
                || displayName != null && displayName.Trim() == string.Empty)
            {
                displayName = "Select Use case";
            }
            DisplayName = displayName;

        }

        private MapperConfiguration ConfigureMappingProfiles()
        {
            return new MapperConfiguration(mc =>
            {
                mc.CreateReversiveMap<UseCaseContent, UseCaseContentViewModel>();
                mc.CreateReversiveMap<MethodParameter, MethodParameterViewModel>();
                mc.CreateReversiveMap<UseCaseSentenceCollection, UseCaseSentenceCollectionViewModel>();
                mc.CreateReversiveMap<UseCaseSentence, UseCaseSentenceViewModel>();
                mc.CreateReversiveMap<MethodParameterReference, MethodParameterReferenceViewModel>();
                mc.CreateReversiveMap<MethodParameterReferenceValue, MethodParameterReferenceValueViewModel>();
                mc.CreateReversiveMap<SentenceInputReferencedParameter, SentenceInputReferencedParameterViewModel>();
                mc.CreateReversiveMap<SentenceOutputReferencedParameter, SentenceOutputReferencedParameterViewModel>();
            });
        }

    }
}
