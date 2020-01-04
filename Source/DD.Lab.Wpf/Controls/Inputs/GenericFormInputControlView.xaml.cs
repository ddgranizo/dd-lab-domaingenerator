
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

        public StringInputControlView StringInputControl { get; private set; }
        public BooleanInputControlView BooleanInputControl { get; private set; }
        public IntegerInputControlView IntegerInputControl { get; private set; }
        public DecimalInputControlView DecimalInputControl { get; private set; }
        public DoubleInputControlView DoubleInputControl { get; private set; }
        public PasswordInputControlView PasswordInputControl { get; private set; }
        public DateTimeInputControlView DateTimeInputControl { get; private set; }
        public EntityReferenceInputControlView EntityReferenceInputControl { get; private set; }
        public OptionSetInputControlView OptionSetInputControl { get; private set; }

        public static readonly DependencyProperty WpfEventManagerProperty =
                      DependencyProperty.Register(
                          nameof(WpfEventManager),
                          typeof(WpfEventManager),
                          typeof(GenericFormInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

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
            else if (e.Property.Name == nameof(WpfEventManager))
            {
                v.SetWpfEventManager((WpfEventManager)e.NewValue);
            }
        }

		private void SetInputModel(GenericFormInputModel data)
        {
            _viewModel.InputModel = data;
        }

        private void SetWpfEventManager(WpfEventManager data)
        {
            _viewModel.WpfEventManager = data;
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


        public void AddStringControl(WpfEventManager wpfEventManager, string defaultValue, List<string> suggestions)
        {
            StringInputControl = new StringInputControlView
            {
                DefaultValue = defaultValue,
                Sugestions = suggestions,
                IsReadOnly = false,
                IsMultiline = false,
                WpfEventManager = wpfEventManager,
            };
            StringInputControl.ValueChanged += StringInputControlView_ValueChanged;
            TheControlGrid.Children.Add(StringInputControl);
        }

        public void AddMultilineStringControl(WpfEventManager wpfEventManager, string defaultValue)
        {
            StringInputControl = new StringInputControlView
            {
                DefaultValue = defaultValue,
                IsReadOnly = false,
                IsMultiline = true,
                WpfEventManager = wpfEventManager,
            };
            StringInputControl.ValueChanged += StringInputControlView_ValueChanged;
            TheControlGrid.Children.Add(StringInputControl);
        }

        public void AddGuidControl(WpfEventManager wpfEventManager, string defaultValue)
        {
            var StringInputControl = new StringInputControlView
            {
                DefaultValue = defaultValue,
                IsReadOnly = true,
                IsMultiline = false,
                WpfEventManager = wpfEventManager,
            };
            StringInputControl.ValueChanged += StringInputControlView_ValueChanged;
            TheControlGrid.Children.Add(StringInputControl);
        }

        public void AddBooleanControl(WpfEventManager wpfEventManager, bool defaultValue)
        {
            BooleanInputControl = new BooleanInputControlView
            {
                DefaultValue = defaultValue,
                WpfEventManager = wpfEventManager,
            };
            BooleanInputControl.ValueChanged += BooleanInputControlView_ValueChanged;
            TheControlGrid.Children.Add(BooleanInputControl);
        }

        public void AddIntegerControl(WpfEventManager wpfEventManager, int defaultValue)
        {
            IntegerInputControl = new IntegerInputControlView
            {
                DefaultValue = defaultValue,
                WpfEventManager = wpfEventManager,
            };
            IntegerInputControl.ValueChanged += IntegerInputControlView_ValueChanged;
            TheControlGrid.Children.Add(IntegerInputControl);
        }

        public void AddDecimalControl(WpfEventManager wpfEventManager, decimal defaultValue)
        {
            DecimalInputControl = new DecimalInputControlView
            {
                DefaultValue = defaultValue,
                WpfEventManager = wpfEventManager,
            };
            DecimalInputControl.ValueChanged += DecimalInputControlView_ValueChanged;
            TheControlGrid.Children.Add(DecimalInputControl);
        }

        public void AddDoubleControl(WpfEventManager wpfEventManager, double defaultValue)
        {
            DoubleInputControl = new DoubleInputControlView
            {
                DefaultValue = defaultValue,
                WpfEventManager = wpfEventManager,
            };
            DoubleInputControl.ValueChanged += DoubleInputControlView_ValueChanged;
            TheControlGrid.Children.Add(DoubleInputControl);
        }

        public void AddPasswordControl(WpfEventManager wpfEventManager)
        {
            PasswordInputControl = new PasswordInputControlView
            {
                WpfEventManager = wpfEventManager,
            };
            PasswordInputControl.ValueChanged += PasswordInputControlView_ValueChanged;
            TheControlGrid.Children.Add(PasswordInputControl);
        }

        public void AddDateTimeControl(WpfEventManager wpfEventManager, DateTime defaultValue)
        {
            DateTimeInputControl = new DateTimeInputControlView
            {
                DefaultValue = defaultValue,
                WpfEventManager = wpfEventManager,
            };
            DateTimeInputControl.ValueChanged += DateTimeInputControlView_ValueChanged;
            TheControlGrid.Children.Add(DateTimeInputControl);
        }

        public void AddEntityReferenceControl(WpfEventManager wpfEventManager, EntityReferenceValue defaultValue)
        {
            EntityReferenceInputControl = new EntityReferenceInputControlView
            {
                DefaultValue = defaultValue,
                InputModel = _viewModel.InputModel,
                WpfEventManager = wpfEventManager,
            };
            EntityReferenceInputControl.ValueChanged += EntityReferenceInputControlView_ValueChanged;
            TheControlGrid.Children.Add(EntityReferenceInputControl);
        }

        public void AddOptionSetControl(WpfEventManager wpfEventManager, OptionSetValue defaultValue, List<OptionSetValue> options)
        {
            OptionSetInputControl = new OptionSetInputControlView
            {
                DefaultValue = defaultValue,
                Options = options,
                WpfEventManager = wpfEventManager,
            };
            OptionSetInputControl.ValueChanged += OptionSetInputControlView_ValueChanged;
            TheControlGrid.Children.Add(OptionSetInputControl);
        }


    }
}
