
using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Controls.Sentences;
using DomainGeneratorUI.Events;
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels;
using DomainGeneratorUI.Viewmodels.Methods;
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

    public partial class UseCaseSentenceContainerView : UserControl
    {
        public static readonly RoutedEvent UpdatedUseCaseEvent =
                    EventManager.RegisterRoutedEvent(nameof(UpdatedUseCase), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(UseCaseSentenceContainerView));

        public event RoutedEventHandler UpdatedUseCase
        {
            add { AddHandler(UpdatedUseCaseEvent, value); }
            remove { RemoveHandler(UpdatedUseCaseEvent, value); }
        }

        public void RaiseUpdateUseCaseSentenceEvent(UseCaseSentence useCaseSentence, UseCaseSentenceViewModel useCaseSentenceViewModel)
        {
            RoutedEventArgs args = new UpdatedUseCaseSentenceEventArgs()
            {
                UseCase = useCaseSentence,
                UseCaseViewModel = useCaseSentenceViewModel,
            };
            args.RoutedEvent = UpdatedUseCaseEvent;
            RaiseEvent(args);
        }

        public void RaiseUpdateUseCaseSentenceEvent(UpdatedUseCaseSentenceEventArgs args)
        {
            args.RoutedEvent = UpdatedUseCaseEvent;
            RaiseEvent(args);
        }


        public List<MethodParameterReferenceViewModel> ParentInputParameters
        {
            get
            {
                return (List<MethodParameterReferenceViewModel>)GetValue(ParentInputParametersProperty);
            }
            set
            {
                SetValue(ParentInputParametersProperty, value);
            }
        }

        public List<MethodParameterReferenceViewModel> ParentOutputParameters
        {
            get
            {
                return (List<MethodParameterReferenceViewModel>)GetValue(ParentOutputParametersProperty);
            }
            set
            {
                SetValue(ParentOutputParametersProperty, value);
            }
        }

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


        public static readonly DependencyProperty ParentInputParametersProperty =
                      DependencyProperty.Register(
                          nameof(ParentInputParameters),
                          typeof(List<MethodParameterReferenceViewModel>),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty ParentOutputParametersProperty =
                      DependencyProperty.Register(
                          nameof(ParentOutputParameters),
                          typeof(List<MethodParameterReferenceViewModel>),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


        public static readonly DependencyProperty GenericManagerProperty =
                      DependencyProperty.Register(
                          nameof(GenericManager),
                          typeof(GenericManager),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty SentenceProperty =
                      DependencyProperty.Register(
                          nameof(Sentence),
                          typeof(UseCaseSentenceViewModel),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        private readonly UseCaseSentenceContainerViewModel _viewModel = null;

        public UseCaseSentenceContainerView()
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as UseCaseSentenceContainerViewModel;
            _viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UseCaseSentenceContainerView v = d as UseCaseSentenceContainerView;
            if (e.Property.Name == nameof(Sentence))
            {
                v.SetSentence((UseCaseSentenceViewModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(GenericManager))
            {
                v.SetGenericManager((GenericManager)e.NewValue);
            }
            else if (e.Property.Name == nameof(ParentInputParameters))
            {
                v.SetParentInputParameters((List<MethodParameterReferenceViewModel>)e.NewValue);
            }
            else if (e.Property.Name == nameof(ParentOutputParameters))
            {
                v.SetParentOutputParameters((List<MethodParameterReferenceViewModel>)e.NewValue);
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

        private void SetParentInputParameters(List<MethodParameterReferenceViewModel> data)
        {
            _viewModel.ParentInputParameters = data;
        }

        private void SetParentOutputParameters(List<MethodParameterReferenceViewModel> data)
        {
            _viewModel.ParentOutputParameters = data;
        }

        public void AddExecuteRepositoryMethodSentence(
            GenericManager manager,
            UseCaseSentenceViewModel sentence,
            List<MethodParameterReferenceViewModel> parentInputParameters,
            List<MethodParameterReferenceViewModel> parentOutputParameters)
        {
            foreach (var item in SentenceGrid.Children.OfType<ExecuteRepositoryMethodSentenceView>())
            {
                item.UpdatedUseCase -= Instance_UpdatedUseCase;
            }
            SentenceGrid.Children.Clear();
            var instance = new ExecuteRepositoryMethodSentenceView()
            {
                ExecuteRepositoryMethodSentenceInputData = new ExecuteRepositoryMethodSentenceInputData()
                {
                    Sentence = sentence,
                    GenericManager = manager,
                    ParentInputParameters = parentInputParameters,
                    ParentOutputParameters = parentOutputParameters,
                }
            };
            instance.UpdatedUseCase += Instance_UpdatedUseCase;
            SentenceGrid.Children.Add(instance);
        }

        private void Instance_UpdatedUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;

            if (data.UseCaseViewModel == Sentence)
            {
                RaiseUpdateUseCaseSentenceEvent(data);
            }
        }
    }
}
