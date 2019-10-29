
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
using static DD.DomainGenerator.Models.DeployActionUnit;

namespace UIClient.UserControls
{

    public partial class DeployActionUnitControlView : UserControl
    {

        public static readonly RoutedEvent DeployActionUnitStateChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(DeployActionUnitStateChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(DeployActionUnitControlView));

        public event RoutedEventHandler DeployActionUnitStateChanged
        {
            add { AddHandler(DeployActionUnitStateChangedEvent, value); }
            remove { RemoveHandler(DeployActionUnitStateChangedEvent, value); }
        }

        public void RaiseDeployActionUnitStateChangedEvent(DeployState data)
        {
            RoutedEventArgs args = new DeployActionUnitStateChangedEventArgs()
            {
                Data = data
            };
            args.RoutedEvent = DeployActionUnitStateChangedEvent;
            RaiseEvent(args);
        }

        public DeployActionUnitModel DeployActionUnit
        {
            get
            {
                return (DeployActionUnitModel)GetValue(DeployActionUnitProperty);
            }
            set
            {
                SetValue(DeployActionUnitProperty, value);
            }
        }

        public static readonly DependencyProperty DeployActionUnitProperty =
                      DependencyProperty.Register(
                          nameof(DeployActionUnit),
                          typeof(DeployActionUnitModel),
                          typeof(DeployActionUnitControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        private readonly DeployActionUnitControlViewModel _viewModel = null;

        public DeployActionUnitControlView()
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as DeployActionUnitControlViewModel;
            _viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DeployActionUnitControlView v = d as DeployActionUnitControlView;
            if (e.Property.Name == nameof(DeployActionUnit))
            {
                v.SetDeployActionUnit((DeployActionUnitModel)e.NewValue);
            }
        }

        private void SetDeployActionUnit(DeployActionUnitModel data)
        {
            _viewModel.DeployActionUnit = data;
        }

    }
}
