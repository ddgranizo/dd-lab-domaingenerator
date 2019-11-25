
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
using UIClient.ViewModels;

namespace UIClient.UserControls.Inputs
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
        }


		private void SetDefaultValue(string data)
        {
            _viewModel.DefaultValue = data;
        }


		private void SetSugestions(List<string> data)
        {
            _viewModel.Sugestions = data;
        }


		
    }
}
