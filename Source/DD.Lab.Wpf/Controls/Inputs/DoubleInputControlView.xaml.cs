
using DD.Lab.Wpf.Inputs.Events;
using DD.Lab.Wpf.Viewmodels;
using DD.Lab.Wpf.Viewmodels.Inputs;
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

namespace DD.Lab.Wpf.Controls.Inputs
{



    public partial class DoubleInputControlView : UserControl
    {

		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(DoubleInputControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent(double data)
        {
            RoutedEventArgs args = new DoubleValueChangedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }


        public double DefaultValue
        {
            get
            {
                return (double)GetValue(DefaultValueProperty);
            }
            set
            {
                SetValue(DefaultValueProperty, value);
            }
        }

        public WpfEventManager WpfEventManager
        {
            get
            {
                return (WpfEventManager)GetValue(WpfEventManagerProperty);
            }
            set
            {
                SetValue(WpfEventManagerProperty, value);
            }
        }

        public static readonly DependencyProperty WpfEventManagerProperty =
                      DependencyProperty.Register(
                          nameof(WpfEventManager),
                          typeof(WpfEventManager),
                          typeof(DoubleInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty DefaultValueProperty =
                      DependencyProperty.Register(
                          nameof(DefaultValue),
                          typeof(double),
                          typeof(DoubleInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


		private readonly DoubleInputControlViewModel _viewModel = null;

        public DoubleInputControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as DoubleInputControlViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			DoubleInputControlView v = d as DoubleInputControlView;
			if (e.Property.Name == nameof(DefaultValue))
            {
                v.SetDefaultValue((double)e.NewValue);
            }
            else if (e.Property.Name == nameof(WpfEventManager))
            {
                v.SetWpfEventManager((WpfEventManager)e.NewValue);
            }
        }

		private void SetDefaultValue(double data)
        {
            _viewModel.DefaultValue = data;
        }

        private void SetWpfEventManager(WpfEventManager data)
        {
            _viewModel.WpfEventManager = data;
        }
    }
}
