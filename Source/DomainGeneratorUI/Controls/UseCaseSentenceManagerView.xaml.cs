
using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Controls.Sentences;
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Viewmodels;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
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

namespace DomainGeneratorUI.Controls
{

    public partial class UseCaseSentenceManagerView : UserControl
    {

        public UseCaseSentenceViewModel Sentence
        {
            get
            {
                return (UseCaseSentenceViewModel)GetValue(SentenceProperty);
            }
            set
            {
                SetValue(SentenceProperty, value);
            }
        }

        public GenericManager GenericManager
        {
            get
            {
                return (GenericManager)GetValue(GenericManagerProperty);
            }
            set
            {
                SetValue(GenericManagerProperty, value);
            }
        }

        public static readonly DependencyProperty GenericManagerProperty =
                      DependencyProperty.Register(
                          nameof(GenericManager),
                          typeof(GenericManager),
                          typeof(UseCaseSentenceManagerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


        public static readonly DependencyProperty SentenceProperty =
                      DependencyProperty.Register(
                          nameof(Sentence),
                          typeof(UseCaseSentenceViewModel),
                          typeof(UseCaseSentenceManagerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


		private readonly UseCaseSentenceManagerViewModel _viewModel = null;

        public UseCaseSentenceManagerView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as UseCaseSentenceManagerViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			UseCaseSentenceManagerView v = d as UseCaseSentenceManagerView;
            if (e.Property.Name == nameof(Sentence))
            {
                v.SetSentence((UseCaseSentenceViewModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(GenericManager))
            {
                v.SetGenericManager((GenericManager)e.NewValue);
            }
        }

        private void SetSentence(UseCaseSentenceViewModel data)
        {
            _viewModel.Sentence = data;
        }

        private void SetGenericManager(GenericManager data)
        {
            _viewModel.GenericManager = data;
        }

        public void AddExecuteRepositoryMethodSentence(GenericManager manager, UseCaseSentenceViewModel sentence)
        {
            SentenceGrid.Children.Clear();
            SentenceGrid.Children.Add(new ExecuteRepositoryMethodSentenceView()
            {
                ExecuteRepositoryMethodSentenceInputData = new ExecuteRepositoryMethodSentenceInputData()
                {
                    Sentence = sentence,
                    GenericManager = manager,
                }
            });
        }
    }
}
