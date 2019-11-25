
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

    public partial class SchemaControlView : UserControl
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
                          typeof(SchemaControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public SchemaModel Schema
        {
            get
            {
                return (SchemaModel)GetValue(SchemaProperty);
            }
            set
            {
                SetValue(SchemaProperty, value);
            }
        }

		public static readonly DependencyProperty SchemaProperty =
                      DependencyProperty.Register(
                          nameof(Schema),
                          typeof(SchemaModel),
                          typeof(SchemaControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly SchemaControlViewModel _viewModel = null;

        public SchemaControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as SchemaControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			SchemaControlView v = d as SchemaControlView;
			if (e.Property.Name == nameof(Schema))
            {
                v.SetSchema((SchemaModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(EventManager))
            {
                v.SetEventManager((DomainEventManager)e.NewValue);
            }
        }

		private void SetSchema(SchemaModel data)
        {
            _viewModel.Schema = data;
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

        private void Properties_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsPropertiesOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void Views_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsRepositoriesOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }


        private void UseCases_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsUseCasesOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void BusinessUseCases_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsBusinessUseCasesOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void BasicUseCases_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsBasicUseCasesOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void Repositories_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsRepositoriesOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void Models_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsModelsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }
    }
}
