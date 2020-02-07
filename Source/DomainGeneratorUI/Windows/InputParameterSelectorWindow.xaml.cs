using AutoMapper;
using DD.Lab.Services.System.Interfaces;
using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Events;
using DD.Lab.Wpf.Windows;
using DomainGeneratorUI.Controls;
using DomainGeneratorUI.Events;
using DomainGeneratorUI.Extensions;
using DomainGeneratorUI.Interfaces;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Models.RepositoryMethods;
using DomainGeneratorUI.Models.UseCases;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels;
using DomainGeneratorUI.Viewmodels.Methods;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DomainGeneratorUI.Windows
{
    public partial class InputParameterSelectorWindow : Window
    {
        public InputParameterSelectorWindowViewmodel ViewModel { get; set; }

        public WindowResponse Response { get; set; }
        public IJsonParserService JsonParserService { get; }
        public List<MethodParameterReferenceValueViewModel> SourceMethodInputParametersReferenceValues { get; }
        public List<MethodParameterReferenceValueViewModel> OutputMethodInputParametersReferenceValues { get; set; }
        public IMapper Mapper { get; private set; }

        public InputParameterSelectorWindow(
            GenericManager manager,
            List<MethodParameterViewModel> methodInputParameters,
            List<MethodParameterReferenceViewModel> availableInputParameterReferences,
            List<MethodParameterReferenceValueViewModel> methodInputParametersReferenceValues)
        {
            InitializeComponent();
            ViewModel = Resources["ViewModel"] as InputParameterSelectorWindowViewmodel;
            ViewModel.Initialize(this, manager, methodInputParameters, availableInputParameterReferences, methodInputParametersReferenceValues);
            SourceMethodInputParametersReferenceValues = methodInputParametersReferenceValues 
                    ?? throw new ArgumentNullException(nameof(methodInputParametersReferenceValues));
        }

        public void SaveAndClose(List<MethodParameterReferenceValueViewModel> data)
        {
            OutputMethodInputParametersReferenceValues = data;
            Response = WindowResponse.OK;
            Close();
        }
        
        private void InputParameterSelectorControlView_UpdatedValue(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedMethodParameterReferenceValueEventArgs;
            ViewModel.UpdatedParameterReferenceValue(data.OldValue, data.NewValue);
        }
    }
}
