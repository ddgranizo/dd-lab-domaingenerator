
using DD.DomainGenerator.Models;
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
using UIClient.Events;
using UIClient.Models;
using UIClient.ViewModels;

namespace UIClient.UserControls
{

    public partial class UseCaseControlView : UserControl
    {

        public DomainEventManager EventManager
        {
            get
            {
                return (DomainEventManager)GetValue(EventManagerProperty);
            }
            set
            {
                SetValue(EventManagerProperty, value);
            }
        }
        public static readonly DependencyProperty EventManagerProperty =
                      DependencyProperty.Register(
                          nameof(EventManager),
                          typeof(DomainEventManager),
                          typeof(UseCaseControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public UseCaseModel UseCase
        {
            get
            {
                return (UseCaseModel)GetValue(UseCaseProperty);
            }
            set
            {
                SetValue(UseCaseProperty, value);
            }
        }

		public static readonly DependencyProperty UseCaseProperty =
                      DependencyProperty.Register(
                          nameof(UseCase),
                          typeof(UseCaseModel),
                          typeof(UseCaseControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly UseCaseControlViewModel _viewModel = null;

        public UseCaseControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as UseCaseControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			UseCaseControlView v = d as UseCaseControlView;
			if (e.Property.Name == nameof(UseCase))
            {
                v.SetUseCase((UseCaseModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(EventManager))
            {
                v.SetEventManager((DomainEventManager)e.NewValue);
            }
        }

		private void SetUseCase(UseCaseModel data)
        {
            _viewModel.UseCase = data;
        }

        private void SetEventManager(DomainEventManager data)
        {
            _viewModel.EventManager = data;
        }

        private void General_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsGeneralOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void Input_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsInputsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void Output_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsOutputsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void UseCase_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.SelectedUseCse();
        }
    }
}
