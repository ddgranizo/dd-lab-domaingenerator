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

namespace DomainGeneratorUI.Viewmodels
{
    public class EditUseCaseWindowViewmodel : BaseViewModel
    {
        private UseCaseContent _content = null;
        public UseCaseContent Content
        {
            get { return _content; }
            set
            {
                _content = value;
                ContentView = Mapper.Map<UseCaseContentViewmodel>(value);
            }
        }

        public UseCaseContentViewmodel ContentView { get { return GetValue<UseCaseContentViewmodel>(); } set { SetValue(value, UpdatedContentView); } }

        public UseCaseSentenceCollectionManagerInputData SentenceCollectionInputData { get { return GetValue<UseCaseSentenceCollectionManagerInputData>(); } set { SetValue(value); } }

        public IMapper Mapper { get; private set; }

        public EditUseCaseWindowViewmodel()
        {
            InitializeMapper();
            InitializeCommands();
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
            SaveCommand = new RelayCommand((input) =>
            {
                _view.ResponseContent = Mapper.Map<UseCaseContent>(ContentView);
                _view.Response = EditorWindowResponse.OK;
                _view.Close();
            });

            RegisterCommand(SaveCommand);
        }

        private void UpdatedContentView(UseCaseContentViewmodel data)
        {
            SentenceCollectionInputData = new UseCaseSentenceCollectionManagerInputData()
            {
                SentenceCollection = data.SentenceCollection,
            };
        }


        private MapperConfiguration ConfigureMappingProfiles()
        {
            return new MapperConfiguration(mc =>
            {
                mc.CreateReversiveMap<UseCaseContent, UseCaseContentViewmodel>();
                mc.CreateReversiveMap<MethodParameter, MethodParameterViewModel>();
                mc.CreateReversiveMap<UseCaseSentenceCollection, UseCaseSentenceCollectionViewmodel>();
                mc.CreateReversiveMap<UseCaseSentence, UseCaseSentenceViewModel>();
                mc.CreateReversiveMap<SentenceInputParameter, SentenceInputParameterViewModel>();
                mc.CreateReversiveMap<SentenceOutputParameter, SentenceOutputParameterViewModel>();

            });
        }
    }
}
