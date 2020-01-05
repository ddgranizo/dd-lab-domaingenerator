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
       
        private readonly EditUseCaseWindowViewmodel _viewModel = null;

        public UseCaseContent ResponseContent { get; set; }
        public EditorWindowResponse Response { get; set; }

        public EditUseCaseWindow()
        {
            InitializeComponent();

            _viewModel = Resources["ViewModel"] as EditUseCaseWindowViewmodel;
            _viewModel.Initialize(this);
        }

        public void SetContent(UseCaseContent instance)
        {
            _viewModel.Content = instance;
        }

        public UseCaseContent GetContent()
        {
            return ResponseContent;
        }

        public EditorWindowResponse GetResponse()
        {
            return Response;
        }
    }
}
