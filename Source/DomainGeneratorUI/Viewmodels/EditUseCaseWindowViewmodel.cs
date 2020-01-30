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

        public GenericManager GenericManager { get; set; }

        public UseCaseContentViewModel ContentView { get { return GetValue<UseCaseContentViewModel>(); } set { SetValue(value); } }

        public UseCaseSentenceCollectionManagerInputData SentenceCollectionInputData { get { return GetValue<UseCaseSentenceCollectionManagerInputData>(); } set { SetValue(value); } }

        public IMapper Mapper { get; private set; }

        public EditUseCaseWindowViewModel()
        {
            InitializeMapper();
            InitializeCommands();
            AddSetterPropertiesTrigger(new DD.Lab.Wpf.Models.PropertiesTrigger(() =>
            {
                SentenceCollectionInputData = new UseCaseSentenceCollectionManagerInputData()
                {
                    SentenceCollection = ContentView.SentenceCollection,
                    GenericManager = GenericManager,
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
                mc.CreateReversiveMap<SentenceInputParameter, SentenceInputParameterViewModel>();
                mc.CreateReversiveMap<SentenceOutputParameter, SentenceOutputParameterViewModel>();
            });
        }

        public void UpdatedUseCaseSentence(UseCaseSentenceViewModel source, UseCaseSentence newValue)
        {
            var index = ContentView.SentenceCollection.Sentences.IndexOf(source);
            var newViewModel = Mapper.Map<UseCaseSentenceViewModel>(newValue);
            ContentView.SentenceCollection.Sentences[index] = newViewModel;
            Content = Mapper.Map<UseCaseContent>(ContentView);
        }
    }
}
