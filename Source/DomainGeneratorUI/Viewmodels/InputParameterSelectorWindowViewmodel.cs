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
using DD.Lab.Wpf.Commands;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using System.Collections.ObjectModel;
using System.Linq;

namespace DomainGeneratorUI.Viewmodels
{
    public class InputParameterSelectorWindowViewmodel : BaseViewModel
    {
        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }

        public List<MethodParameterViewModel> MethodInputParameters { get { return GetValue<List<MethodParameterViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, MethodInputParametersCollection); } }
        public ObservableCollection<MethodParameterViewModel> MethodInputParametersCollection { get; set; } = new ObservableCollection<MethodParameterViewModel>();

        public List<MethodParameterReferenceViewModel> AvailableInputParameterReferences { get { return GetValue<List<MethodParameterReferenceViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, AvailableInputParameterReferencesCollection); } }
        public ObservableCollection<MethodParameterReferenceViewModel> AvailableInputParameterReferencesCollection { get; set; } = new ObservableCollection<MethodParameterReferenceViewModel>();

        public List<MethodParameterReferenceValueViewModel> MethodInputParametersReferenceValues { get { return GetValue<List<MethodParameterReferenceValueViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, MethodInputParametersReferenceValuesCollection); } }
        public ObservableCollection<MethodParameterReferenceValueViewModel> MethodInputParametersReferenceValuesCollection { get; set; } = new ObservableCollection<MethodParameterReferenceValueViewModel>();

        public InputParameterSelectorWindowViewmodel()
        {
            InitializeCommands();
        }

        private InputParameterSelectorWindow _view;

        public void Initialize(
            InputParameterSelectorWindow view,
            GenericManager manager,
            List<MethodParameterViewModel> methodInputParameters,
            List<MethodParameterReferenceViewModel> availableInputParameterReferences,
            List<MethodParameterReferenceValueViewModel> methodInputParametersReferenceValues)
        {
            _view = view 
                ?? throw new ArgumentNullException(nameof(view));
            MethodInputParameters = methodInputParameters   
                ?? throw new ArgumentNullException(nameof(methodInputParameters));
            AvailableInputParameterReferences = availableInputParameterReferences
                ?? throw new ArgumentNullException(nameof(availableInputParameterReferences)); ;
            MethodInputParametersReferenceValues = methodInputParametersReferenceValues
                ?? throw new ArgumentNullException(nameof(methodInputParametersReferenceValues));
            GenericManager = manager
                ?? throw new ArgumentNullException(nameof(manager));
        }

        public MethodParameterReferenceValueViewModel GetCurrentValueForParameter(MethodParameterViewModel parameter)
        {
            return MethodInputParametersReferenceValues.FirstOrDefault(k => k.RegardingMethodParameter == parameter);
        }


        public List<MethodParameterReferenceViewModel> GetAvailableParametersForMethodParmaeter(MethodParameterViewModel parameter)
        {
            return AvailableInputParameterReferences.Where(k => k.MethodParameter.Type == parameter.Type).ToList();
        }

        public ICommand SaveCommand { get; set; }
        private void InitializeCommands()
        {
            SaveCommand = new RelayCommandHandled((input) =>
            {
                
            });
            RegisterCommand(SaveCommand);
        }
    }
}
