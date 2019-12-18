
using DD.Lab.Wpf.Inputs.Events;
using DD.Lab.Wpf.Models.Inputs;
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


        private void OptionSetInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as OptionSetValueChangedEventArgs;
            RaiseValueChangedEvent(InputModel, myEvent.Value);
        }

        private void EntityReferenceInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as EntityReferenceValueChangedEventArgs;
            RaiseValueChangedEvent(InputModel, myEvent.Value);
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

        private void DoubleInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as DoubleValueChangedEventArgs;
            RaiseValueChangedEvent(InputModel, myEvent.Value);
        }

        private void PasswordInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as PasswordValueChangedEventArgs;
            RaiseValueChangedEvent(InputModel, myEvent.Value);
        }


        private void DateTimeInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as DateTimeValueChangedEventArgs;
            RaiseValueChangedEvent(InputModel, myEvent.Value);
        }


        public void ClearControl()
        {
            TheControlGrid.Children.Clear();
        }


        public void AddStringControl(string defaultValue, List<string> suggestions)
        {
            var control = new StringInputControlView
            {
                DefaultValue = defaultValue,
                Sugestions = suggestions,
                IsReadOnly = false,
                IsMultiline = false
            };
            control.ValueChanged += StringInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }

        public void AddMultilineStringControl(string defaultValue)
        {
            var control = new StringInputControlView
            {
                DefaultValue = defaultValue,
                IsReadOnly = false,
                IsMultiline = true
            };
            control.ValueChanged += StringInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }

        public void AddGuidControl(string defaultValue)
        {
            var control = new StringInputControlView
            {
                DefaultValue = defaultValue,
                IsReadOnly = true,
                IsMultiline = false
            };
            control.ValueChanged += StringInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }

        public void AddBooleanControl(bool defaultValue)
        {
            var control = new BooleanInputControlView
            {
                DefaultValue = defaultValue,
            };
            control.ValueChanged += BooleanInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }

        public void AddIntegerControl(int defaultValue)
        {
            var control = new IntegerInputControlView
            {
                DefaultValue = defaultValue,
            };
            control.ValueChanged += IntegerInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }

        public void AddDecimalControl(decimal defaultValue)
        {
            var control = new DecimalInputControlView
            {
                DefaultValue = defaultValue,
            };
            control.ValueChanged += DecimalInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }

        public void AddDoubleControl(double defaultValue)
        {
            var control = new DoubleInputControlView
            {
                DefaultValue = defaultValue,
            };
            control.ValueChanged += DoubleInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }

        public void AddPasswordControl()
        {
            var control = new PasswordInputControlView
            {
            };
            control.ValueChanged += PasswordInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }

        public void AddDateTimeControl(DateTime defaultValue)
        {
            var control = new DateTimeInputControlView
            {
                DefaultValue = defaultValue,
            };
            control.ValueChanged += DateTimeInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }

        public void AddEntityReferenceControl(EntityReferenceValue defaultValue)
        {
            var control = new EntityReferenceInputControlView
            {
                DefaultValue = defaultValue,
                InputModel = _viewModel.InputModel,
            };
            control.ValueChanged += EntityReferenceInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }

        public void AddOptionSetControl(OptionSetValue defaultValue, List<OptionSetValue> options)
        {
            var control = new OptionSetInputControlView
            {
                DefaultValue = defaultValue,
                Options = options,
            };
            control.ValueChanged += OptionSetInputControlView_ValueChanged;
            TheControlGrid.Children.Add(control);
        }


    }
}
