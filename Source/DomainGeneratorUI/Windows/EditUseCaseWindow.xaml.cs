using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Events;
using DomainGeneratorUI.Interfaces;
using DomainGeneratorUI.Models.UseCases;
using DomainGeneratorUI.Viewmodels;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditUseCaseWindow : Window, IContentEditor<UseCaseContent>
    {
       
        private readonly EditUseCaseWindowViewModel _viewModel = null;

        public UseCaseContent ResponseContent { get; set; }
        public EditorWindowResponse Response { get; set; }

        public EditUseCaseWindow()
        {
            InitializeComponent();

            _viewModel = Resources["ViewModel"] as EditUseCaseWindowViewModel;
            _viewModel.Initialize(this);
        }

        public UseCaseContent GetContent()
        {
            return ResponseContent;
        }

        public EditorWindowResponse GetResponse()
        {
            return Response;
        }

        public void SetContext(GenericManager manager, UseCaseContent instance)
        {
            _viewModel.GenericManager = manager;
            _viewModel.Content = instance;
        }

        private void InputParametersManagerControlView_OnModifiedList(object sender, RoutedEventArgs e)
        {
            var myEvent = e as OnModifiedMethodParameterListEventArgs;
            var useCaseViewmodel = _viewModel.ContentView;
            var currentInputs = useCaseViewmodel.Parameters.Where(k => k.Direction == Models.Methods.MethodParameter.ParameterDirection.Input).ToList();
            foreach (var item in currentInputs)
            {
                useCaseViewmodel.Parameters.Remove(item);
            }
            useCaseViewmodel.Parameters.AddRange(myEvent.Data);
            _viewModel.UpdatedUseCaseParameters(useCaseViewmodel);
        }

        private void OutputParametersManagerControlView_OnModifiedList(object sender, RoutedEventArgs e)
        {
            var myEvent = e as OnModifiedMethodParameterListEventArgs;
            var useCaseViewmodel = _viewModel.ContentView;
            var currentInputs = useCaseViewmodel.Parameters.Where(k => k.Direction == Models.Methods.MethodParameter.ParameterDirection.Output).ToList();
            foreach (var item in currentInputs)
            {
                useCaseViewmodel.Parameters.Remove(item);
            }
            useCaseViewmodel.Parameters.AddRange(myEvent.Data);
            _viewModel.UpdatedUseCaseParameters(useCaseViewmodel);
        }

        private void UseCaseSentenceCollectionManagerView_UpdatedUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            if (data.Type == UpdatedUseCaseSentenceEventArgs.UpdateType.Sentence)
            {
                _viewModel.UpdatedUseCaseSentence(data.UseCaseViewModel, data.UseCase);
            }
            else
            {
                _viewModel.UpdatedUseCaseInputParameters(data.UseCaseViewModel, data.Parameters);
            }
            
        }

        private void UseCaseSentenceCollectionManagerView_CopiedUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            _viewModel.CopiedUseCase(data.UseCaseViewModel);
        }

        private void UseCaseSentenceCollectionManagerView_PastedUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            _viewModel.PastedUseCase(data.UseCaseViewModel);
        }

        private void UseCaseSentenceCollectionManagerView_DeletedUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            _viewModel.DeletedUseCase(data.UseCaseViewModel);
        }

        private void UseCaseSentenceCollectionManagerView_MovedDownUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            _viewModel.MovedDownUseCase(data.UseCaseViewModel);
        }

        private void UseCaseSentenceCollectionManagerView_MovedUpUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            _viewModel.MovedUpUseCase(data.UseCaseViewModel);
        }
    }
}
