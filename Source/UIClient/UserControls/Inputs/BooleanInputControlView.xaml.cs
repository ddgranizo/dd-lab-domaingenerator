
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


    public partial class BooleanInputControlView : UserControl
    {

		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(BooleanInputControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent(bool data)
        {
            RoutedEventArgs args = new BooleanValueChangedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }





        public bool DefaultValue
        {
            get
            {
                return (bool)GetValue(DefaultValueProperty);
            }
            set
            {
                SetValue(DefaultValueProperty, value);
            }
        }





		public static readonly DependencyProperty DefaultValueProperty =
                      DependencyProperty.Register(
                          nameof(DefaultValue),
                          typeof(bool),
                          typeof(BooleanInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });



		private readonly BooleanInputControlViewModel _viewModel = null;

        public BooleanInputControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as BooleanInputControlViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			BooleanInputControlView v = d as BooleanInputControlView;
			if(false)
			{
			}

			else if (e.Property.Name == nameof(DefaultValue))
            {
                v.SetDefaultValue((bool)e.NewValue);
            }


        }


		private void SetDefaultValue(bool data)
        {
            _viewModel.DefaultValue = data;
        }


		
    }
}
