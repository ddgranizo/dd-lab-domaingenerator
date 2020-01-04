
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

    public partial class PasswordInputControlView : UserControl
    {

		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(PasswordInputControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent(string data)
        {
            RoutedEventArgs args = new PasswordValueChangedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
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
                          typeof(PasswordInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


        private readonly PasswordInputControlViewModel _viewModel = null;

        public PasswordInputControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as PasswordInputControlViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordInputControlView v = d as PasswordInputControlView;
            if (e.Property.Name == nameof(WpfEventManager))
            {
                v.SetWpfEventManager((WpfEventManager)e.NewValue);
            }
        }

        private void SetWpfEventManager(WpfEventManager data)
        {
            _viewModel.WpfEventManager = data;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Value = passwordBox.Password;
        }
    }
}
