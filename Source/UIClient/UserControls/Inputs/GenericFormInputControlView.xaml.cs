
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
using UIClient.Models.Inputs;
using UIClient.ViewModels;

namespace UIClient.UserControls.Inputs
{

    public partial class GenericFormInputControlView : UserControl
    {

		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(GenericFormInputControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent(GenericFormInputModel model, object data)
        {
            RoutedEventArgs args = new ValueChangedEventArgs()
            {
                Data = data,
                Model = model,
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        public GenericFormInputModel InputModel
        {
            get
            {
                return (GenericFormInputModel)GetValue(InputModelProperty);
            }
            set
            {
                SetValue(InputModelProperty, value);
            }
        }


		public static readonly DependencyProperty InputModelProperty =
                      DependencyProperty.Register(
                          nameof(InputModel),
                          typeof(GenericFormInputModel),
                          typeof(GenericFormInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });



		private readonly GenericFormInputControlViewModel _viewModel = null;

        public GenericFormInputControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as GenericFormInputControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			GenericFormInputControlView v = d as GenericFormInputControlView;
			if (e.Property.Name == nameof(InputModel))
            {
                v.SetInputModel((GenericFormInputModel)e.NewValue);
            }
			
        }

		private void SetInputModel(GenericFormInputModel data)
        {
            _viewModel.InputModel = data;
        }


        private void StringInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as StringValueChangedEventArgs;
            RaiseValueChangedEvent(InputModel, myEvent.Value);
        }

        private void BooleanInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as BooleanValueChangedEventArgs;
            RaiseValueChangedEvent(InputModel, myEvent.Value);
        }

        private void IntegerInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as IntValueChangedEventArgs;
            RaiseValueChangedEvent(InputModel, myEvent.Value);
        }

        private void DecimalInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as DecimalValueChangedEventArgs;
            RaiseValueChangedEvent(InputModel, myEvent.Value);
        }

        private void PasswordInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as PasswordValueChangedEventArgs;
            RaiseValueChangedEvent(InputModel, myEvent.Value);
        }
    }
}
