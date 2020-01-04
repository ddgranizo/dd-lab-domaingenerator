
using DD.Lab.Wpf.Inputs.Events;
using DD.Lab.Wpf.Models.Inputs;
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

    public partial class OptionSetInputControlView : UserControl
    {

        public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(OptionSetInputControlView));

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public void RaiseValueChangedEvent(OptionSetValue data)
        {
            RoutedEventArgs args = new OptionSetValueChangedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        public OptionSetValue DefaultValue
        {
            get
            {
                return (OptionSetValue)GetValue(DefaultValueProperty);
            }
            set
            {
                SetValue(DefaultValueProperty, value);
            }
        }

        public List<OptionSetValue> Options
        {
            get
            {
                return (List<OptionSetValue>)GetValue(OptionsProperty);
            }
            set
            {
                SetValue(OptionsProperty, value);
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
                          typeof(OptionSetInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty DefaultValueProperty =
                      DependencyProperty.Register(
                          nameof(DefaultValue),
                          typeof(OptionSetValue),
                          typeof(OptionSetInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty OptionsProperty =
                      DependencyProperty.Register(
                          nameof(Options),
                          typeof(List<OptionSetValue>),
                          typeof(OptionSetInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        private readonly OptionSetInputControlViewModel _viewModel = null;

        public OptionSetInputControlView()
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as OptionSetInputControlViewModel;
            _viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OptionSetInputControlView v = d as OptionSetInputControlView;
            if (e.Property.Name == nameof(DefaultValue))
            {
                v.SetDefaultValue((OptionSetValue)e.NewValue);
            }
            else if (e.Property.Name == nameof(Options))
            {
                v.SetOptions((List<OptionSetValue>)e.NewValue);
            }
            else if (e.Property.Name == nameof(WpfEventManager))
            {
                v.SetWpfEventManager((WpfEventManager)e.NewValue);
            }
        }

        private void SetDefaultValue(OptionSetValue data)
        {
            _viewModel.DefaultValue = data;
        }

        private void SetOptions(List<OptionSetValue> data)
        {
            _viewModel.Options = data;
        }

        private void SetWpfEventManager(WpfEventManager data)
        {
            _viewModel.WpfEventManager = data;
        }

    }
}
