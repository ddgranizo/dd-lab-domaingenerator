
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
    public partial class ViewControlView : UserControl
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
                          typeof(ViewControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public ViewModel SchemaView
        {
            get
            {
                return (ViewModel)GetValue(SchemaViewProperty);
            }
            set
            {
                SetValue(SchemaViewProperty, value);
            }
        }

        public static readonly DependencyProperty SchemaViewProperty =
                      DependencyProperty.Register(
                          nameof(SchemaView),
                          typeof(ViewModel),
                          typeof(ViewControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        private readonly ViewControlViewModel _viewModel = null;

        public ViewControlView()
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as ViewControlViewModel;
            _viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ViewControlView v = d as ViewControlView;
            if (e.Property.Name == nameof(SchemaView))
            {
                v.SetSchemaView((ViewModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(EventManager))
            {
                v.SetEventManager((DomainEventManager)e.NewValue);
            }
        }


        private void SetEventManager(DomainEventManager data)
        {
            _viewModel.EventManager = data;
        }

        private void SetSchemaView(ViewModel data)
        {
            _viewModel.View = data;
        }

        private void General_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsGeneralOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void Attributes_CollapsedChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
