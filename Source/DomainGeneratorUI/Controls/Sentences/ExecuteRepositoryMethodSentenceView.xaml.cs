
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Viewmodels.Sentences;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DomainGeneratorUI.Controls.Sentences
{


    public partial class ExecuteRepositoryMethodSentenceView : UserControl
    {


        public ExecuteRepositoryMethodSentenceInputData ExecuteRepositoryMethodSentenceInputData
        {
            get
            {
                return (ExecuteRepositoryMethodSentenceInputData)GetValue(ExecuteRepositoryMethodSentenceInputDataProperty);
            }
            set
            {
                SetValue(ExecuteRepositoryMethodSentenceInputDataProperty, value);
            }
        }

		public static readonly DependencyProperty ExecuteRepositoryMethodSentenceInputDataProperty =
                      DependencyProperty.Register(
                          nameof(ExecuteRepositoryMethodSentenceInputData),
                          typeof(ExecuteRepositoryMethodSentenceInputData),
                          typeof(ExecuteRepositoryMethodSentenceView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


		private readonly ExecuteRepositoryMethodSentenceViewModel _viewModel = null;

        public ExecuteRepositoryMethodSentenceView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as ExecuteRepositoryMethodSentenceViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			ExecuteRepositoryMethodSentenceView v = d as ExecuteRepositoryMethodSentenceView;
            if (e.Property.Name == nameof(ExecuteRepositoryMethodSentenceInputData))
            {
                v.SetExecuteRepositoryMethodSentenceInputData((ExecuteRepositoryMethodSentenceInputData)e.NewValue);
            }
        }

        private void SetExecuteRepositoryMethodSentenceInputData(ExecuteRepositoryMethodSentenceInputData data)
        {
            _viewModel.ExecuteRepositoryMethodSentenceInputData = data;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
