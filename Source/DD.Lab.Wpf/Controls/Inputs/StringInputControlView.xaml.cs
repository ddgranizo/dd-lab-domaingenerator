
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

    public partial class StringInputControlView : UserControl
    {

		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(StringInputControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent(string data)
        {
            RoutedEventArgs args = new StringValueChangedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        public string DefaultValue
        {
            get
            {
                return (string)GetValue(DefaultValueProperty);
            }
            set
            {
                SetValue(DefaultValueProperty, value);
            }
        }

        public List<string> Sugestions
        {
            get
            {
                return (List<string>)GetValue(SugestionsProperty);
            }
            set
            {
                SetValue(SugestionsProperty, value);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }


        public bool IsMultiline
        {
            get
            {
                return (bool)GetValue(IsMultilineProperty);
            }
            set
            {
                SetValue(IsMultilineProperty, value);
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
                          typeof(StringInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty IsMultilineProperty =
                      DependencyProperty.Register(
                          nameof(IsMultiline),
                          typeof(bool),
                          typeof(StringInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty IsReadOnlyProperty =
                      DependencyProperty.Register(
                          nameof(IsReadOnly),
                          typeof(bool),
                          typeof(StringInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty DefaultValueProperty =
                      DependencyProperty.Register(
                          nameof(DefaultValue),
                          typeof(string),
                          typeof(StringInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


		public static readonly DependencyProperty SugestionsProperty =
                      DependencyProperty.Register(
                          nameof(Sugestions),
                          typeof(List<string>),
                          typeof(StringInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly StringInputControlViewModel _viewModel = null;

        public StringInputControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as StringInputControlViewModel;
            _viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			StringInputControlView v = d as StringInputControlView;
			if (e.Property.Name == nameof(DefaultValue))
            {
                v.SetDefaultValue((string)e.NewValue);
            }
			else if (e.Property.Name == nameof(Sugestions))
            {
                v.SetSugestions((List<string>)e.NewValue);
            }
            else if (e.Property.Name == nameof(IsMultiline))
            {
                v.SetIsMultiline((bool)e.NewValue);
            }
            else if (e.Property.Name == nameof(IsReadOnly))
            {
                v.SetIsReadOnly((bool)e.NewValue);
            }
            else if (e.Property.Name == nameof(WpfEventManager))
            {
                v.SetWpfEventManager((WpfEventManager)e.NewValue);
            }
        }

		private void SetDefaultValue(string data)
        {
            _viewModel.DefaultValue = data;
        }

		private void SetSugestions(List<string> data)
        {
            _viewModel.Sugestions = data;
        }

        private void SetIsReadOnly(bool data)
        {
            _viewModel.IsReadOnly = data;
        }

        private void SetIsMultiline(bool data)
        {
            _viewModel.IsMultiline = data;
        }

        private void SetWpfEventManager(WpfEventManager data)
        {
            _viewModel.WpfEventManager = data;
        }

    }
}
