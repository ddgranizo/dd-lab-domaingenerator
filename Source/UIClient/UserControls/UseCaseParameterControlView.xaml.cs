
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
    public partial class UseCaseParameterControlView : UserControl
    {
        public UseCaseParameterModel UseCaseParameter
        {
            get
            {
                return (UseCaseParameterModel)GetValue(UseCaseParameterProperty);
            }
            set
            {
                SetValue(UseCaseParameterProperty, value);
            }
        }

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

		public static readonly DependencyProperty UseCaseParameterProperty =
                      DependencyProperty.Register(
                          nameof(UseCaseParameter),
                          typeof(UseCaseParameterModel),
                          typeof(UseCaseParameterControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		public static readonly DependencyProperty EventManagerProperty =
                      DependencyProperty.Register(
                          nameof(EventManager),
                          typeof(DomainEventManager),
                          typeof(UseCaseParameterControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly UseCaseParameterControlViewModel _viewModel = null;

        public UseCaseParameterControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as UseCaseParameterControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			UseCaseParameterControlView v = d as UseCaseParameterControlView;
			if (e.Property.Name == nameof(UseCaseParameter))
            {
                v.SetUseCaseParameter((UseCaseParameterModel)e.NewValue);
            }
			else if (e.Property.Name == nameof(EventManager))
            {
                v.SetEventManager((DomainEventManager)e.NewValue);
            }
        }

		private void SetUseCaseParameter(UseCaseParameterModel data)
        {
            _viewModel.UseCaseParameter = data;
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
    }
}
