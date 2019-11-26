
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

    public partial class GenericFormControlView : UserControl
    {

		public static readonly RoutedEvent OnConfirmedValuesEvent =
                    EventManager.RegisterRoutedEvent(nameof(OnConfirmedValues), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(GenericFormControlView));
        
		public event RoutedEventHandler OnConfirmedValues
        {
            add { AddHandler(OnConfirmedValuesEvent, value); }
            remove { RemoveHandler(OnConfirmedValuesEvent, value); }
        }

		public void RaiseOnConfirmedValuesEvent(Dictionary<string, object> data)
        {
            RoutedEventArgs args = new OnGenericFormConfirmedValuesEventArgs()
            {
                Data = data
            };
            args.RoutedEvent = OnConfirmedValuesEvent;
            RaiseEvent(args);
        }

        public static readonly RoutedEvent OnCanceledValuesEvent =
                    EventManager.RegisterRoutedEvent(nameof(OnCanceledValues), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(GenericFormControlView));

        public event RoutedEventHandler OnCanceledValues
        {
            add { AddHandler(OnCanceledValuesEvent, value); }
            remove { RemoveHandler(OnCanceledValuesEvent, value); }
        }

        public void RaiseOnCancelledValuesEvent()
        {
            RoutedEventArgs args = new OnGenericFormCanceledValuesEventArgs()
            {
            };
            args.RoutedEvent = OnCanceledValuesEvent;
            RaiseEvent(args);
        }

        public GenericFormModel FormModel
        {
            get
            {
                return (GenericFormModel)GetValue(FormModelProperty);
            }
            set
            {
                SetValue(FormModelProperty, value);
            }
        }

        public Dictionary<string, object> InitialValues
        {
            get
            {
                return (Dictionary<string, object>)GetValue(InitialValuesProperty);
            }
            set
            {
                SetValue(InitialValuesProperty, value);
            }
        }

		public static readonly DependencyProperty FormModelProperty =
                      DependencyProperty.Register(
                          nameof(FormModel),
                          typeof(GenericFormModel),
                          typeof(GenericFormControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		public static readonly DependencyProperty InitialValuesProperty =
                      DependencyProperty.Register(
                          nameof(InitialValues),
                          typeof(Dictionary<string, object>),
                          typeof(GenericFormControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly GenericFormControlViewModel _viewModel = null;

        public GenericFormControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as GenericFormControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			GenericFormControlView v = d as GenericFormControlView;
			if (e.Property.Name == nameof(FormModel))
            {
                v.SetFormModel((GenericFormModel)e.NewValue);
            }
			else if (e.Property.Name == nameof(InitialValues))
            {
                v.SetInitialValues((Dictionary<string, object>)e.NewValue);
            }
        }

		private void SetFormModel(GenericFormModel data)
        {
            _viewModel.FormModel = data;
        }

		private void SetInitialValues(Dictionary<string, object> data)
        {
            _viewModel.InitialValues = data;
        }
    }
}
