
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

    public partial class DateTimeInputControlView : UserControl
    {

		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(DateTimeInputControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent(DateTime data)
        {
            RoutedEventArgs args = new DateTimeValueChangedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }


        public DateTime DefaultValue
        {
            get
            {
                return (DateTime)GetValue(DefaultValueProperty);
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
                          typeof(DateTimeInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty DefaultValueProperty =
                      DependencyProperty.Register(
                          nameof(DefaultValue),
                          typeof(DateTime),
                          typeof(DateTimeInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly DateTimeInputControlViewModel _viewModel = null;

        public DateTimeInputControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as DateTimeInputControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			DateTimeInputControlView v = d as DateTimeInputControlView;
			if (e.Property.Name == nameof(DefaultValue))
            {
                v.SetDefaultValue((DateTime)e.NewValue);
            }
            else if (e.Property.Name == nameof(WpfEventManager))
            {
                v.SetWpfEventManager((WpfEventManager)e.NewValue);
            }
        }

		private void SetDefaultValue(DateTime data)
        {
            _viewModel.DefaultValue = data;
        }

        private void SetWpfEventManager(WpfEventManager data)
        {
            _viewModel.WpfEventManager = data;
        }
    }
}
