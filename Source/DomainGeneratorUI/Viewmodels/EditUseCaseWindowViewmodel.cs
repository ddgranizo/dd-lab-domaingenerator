using DD.Lab.Services.System.Implementations;
using DD.Lab.Services.System.Interfaces;
using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using DomainGeneratorUI.Extensions;
using DomainGeneratorUI.Windows;
using DD.Lab.Wpf.ViewModels.Base;
using DD.Lab.Wpf.Drm.Models;
using DomainGeneratorUI.Models;
using DomainGeneratorUI.Models.UseCases;
using System.Windows.Input;
using AutoMapper;
using DD.Lab.Wpf.Commands.Base;
using DomainGeneratorUI.Viewmodels.UseCases;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Viewmodels.Methods;
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using DD.Lab.Wpf.Commands;
using System.Linq;
using DomainGeneratorUI.Viewmodels.RepositoryMethods;
using DomainGeneratorUI.Models.RepositoryMethods;

namespace DomainGeneratorUI.Viewmodels
{
    public class EditUseCaseWindowViewModel : BaseViewModel
    {
        private UseCaseContent _content = null;
        public UseCaseContent Content
        {
            get { return _content; }
            set
            {
                _content = value;
                ContentView = Mapper.Map<UseCaseContentViewModel>(_content);
            }
        }

        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }
        public UseCaseContentViewModel ContentView { get { return GetValue<UseCaseContentViewModel>(); } set { SetValue(value); } }
        public UseCaseSentenceCollectionManagerInputData SentenceCollectionInputData { get { return GetValue<UseCaseSentenceCollectionManagerInputData>(); } set { SetValue(value); } }
        public UseCaseContext UseCaseContext { get { return GetValue<UseCaseContext>(); } set { SetValue(value); } }

        public IMapper Mapper { get; private set; }
        public EditUseCaseWindowViewModel()
        {
            UseCaseContext = new UseCaseContext();
            InitializeMapper();
            InitializeCommands();

            AddSetterPropertiesTrigger(new DD.Lab.Wpf.Models.PropertiesTrigger(() =>
            {
                SentenceCollectionInputData = new UseCaseSentenceCollectionManagerInputData()
                {
                    ParentInputParameters = ContentView.Parameters
                        .Where(k => k.Direction == MethodParameter.ParameterDirection.Input)
                        .Select(k => new MethodParameterReferenceViewModel(k))
                        .ToList(),
                    ParentOutputParameters = ContentView.Parameters
                        .Where(k => k.Direction == MethodParameter.ParameterDirection.Output)
                        .Select(k => new MethodParameterReferenceViewModel(k))
                        .ToList(),
                    SentenceCollection = ContentView.SentenceCollection,
                    GenericManager = GenericManager,
                    UseCaseContext = UseCaseContext,
                    Mapper = Mapper,
                };
            }, nameof(ContentView), nameof(GenericManager)));
        }

        private EditUseCaseWindow _view;

        public void Initialize(EditUseCaseWindow view)
        {
            _view = view;
        }

        private void InitializeMapper()
        {
            Mapper = new Mapper(ConfigureMappingProfiles());
        }

        public ICommand SaveCommand { get; set; }
        private void InitializeCommands()
        {
            SaveCommand = new RelayCommandHandled((input) =>
            {
                _view.ResponseContent = Mapper.Map<UseCaseContent>(ContentView);
                _view.Response = EditorWindowResponse.OK;
                _view.Close();
            });

            RegisterCommand(SaveCommand);
        }

        private MapperConfiguration ConfigureMappingProfiles()
        {
            return new MapperConfiguration(mc =>
            {
                mc.CreateReversiveMap<UseCaseContent, UseCaseContentViewModel>();
                mc.CreateReversiveMap<MethodParameter, MethodParameterViewModel>();
                mc.CreateReversiveMap<UseCaseSentenceCollection, UseCaseSentenceCollectionViewModel>();
                mc.CreateReversiveMap<UseCaseSentence, UseCaseSentenceViewModel>();
                mc.CreateReversiveMap<SentenceInputReferencedParameter, SentenceInputReferencedParameterViewModel>();
                mc.CreateReversiveMap<SentenceOutputReferencedParameter, SentenceOutputReferencedParameterViewModel>();
                mc.CreateReversiveMap<MethodParameterReferenceValue, MethodParameterReferenceValueViewModel>();
                mc.CreateReversiveMap<MethodParameterReference, MethodParameterReferenceViewModel>();

            });
        }

        public void UpdatedUseCaseParameters(UseCaseContentViewModel newValue)
        {
            UpdateUseCaseContent();
        }

        public void UpdatedUseCaseSentence()
        {
            UpdateUseCaseContent();
        }

        //public void UpdatedUseCaseInputParameters(UseCaseSentenceViewModel source, List<MethodParameterReferenceValueViewModel> parameters)
        //{
        //    var sentence = ContentView.SentenceCollection.Sentences.First(k => k == source);
        //    sentence.ReferencedInputParametersValues = parameters;
        //    UpdateUseCaseContent();
        //}

        private void UpdateUseCaseContent()
        {
            var data = Mapper.Map<UseCaseContent>(ContentView);
            Content = data;
        }

        public void CopiedUseCase(UseCaseSentenceViewModel useCase)
        {
            UseCaseContext.CopiedSentence = useCase;
            UpdateUseCaseContent();
        }

        public void PastedUseCase(UseCaseSentenceViewModel useCase)
        {
            UpdateUseCaseContent();
        }

        public void DeletedUseCase(UseCaseSentenceViewModel useCase)
        {
            UpdateUseCaseContent();
        }

        public void MovedUpUseCase(UseCaseSentenceViewModel useCase)
        {
            UpdateUseCaseContent();
        }

        public void MovedDownUseCase(UseCaseSentenceViewModel useCase)
        {
            UpdateUseCaseContent();
        }
    }
}
