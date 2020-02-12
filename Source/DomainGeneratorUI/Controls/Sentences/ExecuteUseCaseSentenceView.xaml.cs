
using DomainGeneratorUI.Events;
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels.Sentences;
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

namespace DomainGeneratorUI.Controls.Sentences
{

    public partial class ExecuteUseCaseSentenceView : UserControl
    {
        public static readonly RoutedEvent UpdatedUseCaseEvent =
                    EventManager.RegisterRoutedEvent(nameof(UpdatedUseCase), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(ExecuteUseCaseSentenceView));

        public event RoutedEventHandler UpdatedUseCase
        {
            add { AddHandler(UpdatedUseCaseEvent, value); }
            remove { RemoveHandler(UpdatedUseCaseEvent, value); }
        }

        public void RaiseUpdateUseCaseSentenceEvent(UseCaseSentence useCaseSentence, UseCaseSentenceViewModel viewmodel)
        {
            RoutedEventArgs args = new UpdatedUseCaseSentenceEventArgs()
            {
                UseCase = useCaseSentence,
                UseCaseViewModel = viewmodel,
                Type = UpdatedUseCaseSentenceEventArgs.UpdateType.Sentence,
            };
            args.RoutedEvent = UpdatedUseCaseEvent;
            RaiseEvent(args);
        }

        public void RaiseUpdateUseCaseSentenceEvent(List<MethodParameterReferenceValueViewModel> parameters, UseCaseSentenceViewModel viewmodel)
        {
            RoutedEventArgs args = new UpdatedUseCaseSentenceEventArgs()
            {
                Parameters = parameters,
                UseCaseViewModel = viewmodel,
                Type = UpdatedUseCaseSentenceEventArgs.UpdateType.InputParameters,
            };
            args.RoutedEvent = UpdatedUseCaseEvent;
            RaiseEvent(args);
        }

        public ExecuteUseCaseSentenceInputData ExecuteUseCaseSentenceInputData
        {
            get
            {
                return (ExecuteUseCaseSentenceInputData)GetValue(ExecuteUseCaseSentenceInputDataProperty);
            }
            set
            {
                SetValue(ExecuteUseCaseSentenceInputDataProperty, value);
            }
        }

		public static readonly DependencyProperty ExecuteUseCaseSentenceInputDataProperty =
                      DependencyProperty.Register(
                          nameof(ExecuteUseCaseSentenceInputData),
                          typeof(ExecuteUseCaseSentenceInputData),
                          typeof(ExecuteUseCaseSentenceView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


		private readonly ExecuteUseCaseSentenceViewModel _viewModel = null;

        public ExecuteUseCaseSentenceView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as ExecuteUseCaseSentenceViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExecuteUseCaseSentenceView v = d as ExecuteUseCaseSentenceView;
            if (e.Property.Name == nameof(ExecuteUseCaseSentenceInputData))
            {
                v.SetExecuteUseCaseSentenceInputData((ExecuteUseCaseSentenceInputData)e.NewValue);
            }
        }

        private void SetExecuteUseCaseSentenceInputData(ExecuteUseCaseSentenceInputData data)
        {
            _viewModel.ExecuteUseCseSentenceInputData = data;
        }

    
        private void Name_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var command = _viewModel.EditCommand;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        private void Inputs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var command = _viewModel.ManageInputsCommand;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
    }
}
