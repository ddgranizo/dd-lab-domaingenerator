
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

    public partial class EnvironmentControlView : UserControl
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
                          typeof(EnvironmentControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public EnvironmentModel Environment
        {
            get
            {
                return (EnvironmentModel)GetValue(EnvironmentProperty);
            }
            set
            {
                SetValue(EnvironmentProperty, value);
            }
        }

		public static readonly DependencyProperty EnvironmentProperty =
                      DependencyProperty.Register(
                          nameof(Environment),
                          typeof(EnvironmentModel),
                          typeof(EnvironmentControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly EnvironmentControlViewModel _viewModel = null;

        public EnvironmentControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as EnvironmentControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			EnvironmentControlView v = d as EnvironmentControlView;
			if (e.Property.Name == nameof(Environment))
            {
                v.SetEnvironment((EnvironmentModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(EventManager))
            {
                v.SetEventManager((DomainEventManager)e.NewValue);
            }
        }

		private void SetEnvironment(EnvironmentModel data)
        {
            _viewModel.Environment = data;
        }

        private void SetEventManager(DomainEventManager data)
        {
            _viewModel.EventManager = data;
        }

        private void General_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }
    }
}
