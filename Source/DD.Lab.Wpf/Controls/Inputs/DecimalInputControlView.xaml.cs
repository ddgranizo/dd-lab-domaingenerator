
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

    public partial class DecimalInputControlView : UserControl
    {

		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(DecimalInputControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent(decimal data)
        {
            RoutedEventArgs args = new DecimalValueChangedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        public decimal DefaultValue
        {
            get
            {
                return (decimal)GetValue(DefaultValueProperty);
            }
            set
            {
                SetValue(DefaultValueProperty, value);
            }
        }

		public static readonly DependencyProperty DefaultValueProperty =
                      DependencyProperty.Register(
                          nameof(DefaultValue),
                          typeof(decimal),
                          typeof(DecimalInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly DecimalInputControlViewModel _viewModel = null;

        public DecimalInputControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as DecimalInputControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			DecimalInputControlView v = d as DecimalInputControlView;
			if (e.Property.Name == nameof(DefaultValue))
            {
                v.SetDefaultValue((decimal)e.NewValue);
            }
        }

		private void SetDefaultValue(decimal data)
        {
            _viewModel.DefaultValue = data;
        }
    }
}
