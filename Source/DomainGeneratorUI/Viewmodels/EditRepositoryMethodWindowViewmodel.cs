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
using DomainGeneratorUI.Models.RepositoryMethods;
using AutoMapper;
using DomainGeneratorUI.Viewmodels.RepositoryMethods;
using DomainGeneratorUI.Viewmodels.Methods;
using DomainGeneratorUI.Models.Methods;
using System.Windows.Input;
using DD.Lab.Wpf.Commands.Base;

namespace DomainGeneratorUI.Viewmodels
{
    public class EditRepositoryMethodWindowViewmodel : BaseViewModel
    {
        private RepositoryMethodContent _content = null;
        public RepositoryMethodContent Content { get { return _content; } set { _content = value; ContentView = Mapper.Map<RepositoryMethodContentViewmodel>(value); } }
        public RepositoryMethodContentViewmodel ContentView { get { return GetValue<RepositoryMethodContentViewmodel>(); } set { SetValue(value); } }


        public IMapper Mapper { get; private set; }

        public EditRepositoryMethodWindowViewmodel()
        {
            InitializeMapper();
            InitializeCommands();
        }

        private EditRepositoryMethodWindow _view;

        public void Initialize(EditRepositoryMethodWindow view)
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
                _view.ResponseContent = Mapper.Map<RepositoryMethodContent>(ContentView);
                _view.Response = EditorWindowResponse.OK;
                _view.Close();
            });

            RegisterCommand(SaveCommand);
        }


        private MapperConfiguration ConfigureMappingProfiles()
        {
            return new MapperConfiguration(mc =>
            {
                mc.CreateMap<RepositoryMethodContent, RepositoryMethodContentViewmodel>();
                mc.CreateMap<RepositoryMethodContentViewmodel, RepositoryMethodContent>();

                mc.CreateMap<MethodParameter, MethodParameterViewmodel>();
                mc.CreateMap<MethodParameterViewmodel, MethodParameter>();

            });
        }
    }
}
