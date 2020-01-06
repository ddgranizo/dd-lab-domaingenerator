using DomainGeneratorUI.Events;
using DomainGeneratorUI.Interfaces;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Models.RepositoryMethods;
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

    public enum EditorWindowResponse
    {
        OK = 1,
        KO = 2,
    }

    public partial class EditRepositoryMethodWindow : Window, IContentEditor<RepositoryMethodContent>
    {

        private readonly EditRepositoryMethodWindowViewmodel _viewModel = null;

        public RepositoryMethodContent ResponseContent { get; set; }
        public EditorWindowResponse Response { get; set; }

        public EditRepositoryMethodWindow()
        {
            InitializeComponent();

            _viewModel = Resources["ViewModel"] as EditRepositoryMethodWindowViewmodel;
            _viewModel.Initialize(this);
        }

        public void SetContent(RepositoryMethodContent instance)
        {
            _viewModel.Content = instance;
        }

        public RepositoryMethodContent GetContent()
        {
            return ResponseContent;
        }

        public EditorWindowResponse GetResponse()
        {
            return Response;
        }

        private void InputParametersManagerControlView_OnModifiedList(object sender, RoutedEventArgs e)
        {
            var myEvent = e as OnModifiedMethodParameterListEventArgs;
            var currentInputs = _viewModel.ContentView.Parameteters.Where(k => k.Direction == Models.Methods.MethodParameter.ParameterDirection.Input).ToList();
            foreach (var item in currentInputs)
            {
                _viewModel.ContentView.Parameteters.Remove(item);
            }
            _viewModel.ContentView.Parameteters.AddRange(myEvent.Data);
        }

        private void OutputParametersManagerControlView_OnModifiedList(object sender, RoutedEventArgs e)
        {
            var myEvent = e as OnModifiedMethodParameterListEventArgs;
            var currentInputs = _viewModel.ContentView.Parameteters.Where(k => k.Direction == Models.Methods.MethodParameter.ParameterDirection.Output).ToList();
            foreach (var item in currentInputs)
            {
                _viewModel.ContentView.Parameteters.Remove(item);
            }
            _viewModel.ContentView.Parameteters.AddRange(myEvent.Data);

        }
    }
}
