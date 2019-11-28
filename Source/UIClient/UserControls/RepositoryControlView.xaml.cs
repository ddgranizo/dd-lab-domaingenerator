
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
    public partial class RepositoryControlView : UserControl
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
                          typeof(RepositoryControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public RepositoryModel Repository
        {
            get
            {
                return (RepositoryModel)GetValue(RepositoryProperty);
            }
            set
            {
                SetValue(RepositoryProperty, value);
            }
        }

		public static readonly DependencyProperty RepositoryProperty =
                      DependencyProperty.Register(
                          nameof(Repository),
                          typeof(RepositoryModel),
                          typeof(RepositoryControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly RepositoryControlViewModel _viewModel = null;

        public RepositoryControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as RepositoryControlViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			RepositoryControlView v = d as RepositoryControlView;
			if (e.Property.Name == nameof(Repository))
            {
                v.SetRepository((RepositoryModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(EventManager))
            {
                v.SetEventManager((DomainEventManager)e.NewValue);
            }
        }

		private void SetRepository(RepositoryModel data)
        {
            _viewModel.Repository = data;
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

        private void Views_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void Methods_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsMethodsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }
    }
}
