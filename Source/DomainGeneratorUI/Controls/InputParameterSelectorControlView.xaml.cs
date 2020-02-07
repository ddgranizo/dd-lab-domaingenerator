
using DD.Lab.Wpf.Controls.Inputs;
using DD.Lab.Wpf.Events;
using DD.Lab.Wpf.Inputs.Events;
using DD.Lab.Wpf.Models.Inputs;
using DomainGeneratorUI.Events;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Viewmodels;
using DomainGeneratorUI.Viewmodels.Methods;
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
using static DD.Lab.Wpf.Models.Inputs.GenericFormInputModel;

namespace DomainGeneratorUI.Controls
{

    public partial class InputParameterSelectorControlView : UserControl
    {

        public static readonly RoutedEvent UpdatedValueEvent =
                    EventManager.RegisterRoutedEvent(nameof(UpdatedValue), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(InputParameterSelectorControlView));

        public event RoutedEventHandler UpdatedValue
        {
            add { AddHandler(UpdatedValueEvent, value); }
            remove { RemoveHandler(UpdatedValueEvent, value); }
        }

        public void RaiseUpdatedValueEvent(MethodParameterReferenceValueViewModel oldValue, 
            MethodParameterReferenceValueViewModel newValue)
        {
            RoutedEventArgs args = new UpdatedMethodParameterReferenceValueEventArgs()
            {
                OldValue = oldValue,
                NewValue = newValue
            };
            args.RoutedEvent = UpdatedValueEvent;
            RaiseEvent(args);
        }

        public MethodParameterViewModel Parameter
        {
            get
            {
                return (MethodParameterViewModel)GetValue(ParameterProperty);
            }
            set
            {
                SetValue(ParameterProperty, value);
            }
        }

        public List<MethodParameterReferenceViewModel> AvailableParameterReferences
        {
            get
            {
                return (List<MethodParameterReferenceViewModel>)GetValue(AvailableParameterReferencesProperty);
            }
            set
            {
                SetValue(AvailableParameterReferencesProperty, value);
            }
        }

        public MethodParameterReferenceValueViewModel CurrentReferenceValue
        {
            get
            {
                return (MethodParameterReferenceValueViewModel)GetValue(CurrentReferenceValueProperty);
            }
            set
            {
                SetValue(CurrentReferenceValueProperty, value);
            }
        }

        public static readonly DependencyProperty ParameterProperty =
                      DependencyProperty.Register(
                          nameof(Parameter),
                          typeof(MethodParameterViewModel),
                          typeof(InputParameterSelectorControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty AvailableParameterReferencesProperty =
                      DependencyProperty.Register(
                          nameof(AvailableParameterReferences),
                          typeof(List<MethodParameterReferenceViewModel>),
                          typeof(InputParameterSelectorControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty CurrentReferenceValueProperty =
                      DependencyProperty.Register(
                          nameof(CurrentReferenceValue),
                          typeof(MethodParameterReferenceValueViewModel),
                          typeof(InputParameterSelectorControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)) { DefaultValue = null });

        public InputParameterSelectorControlViewModel ViewModel { get; }


        private GenericInputControlView _currentConsantInputControl = null;

        public InputParameterSelectorControlView()
        {
            InitializeComponent();
            ViewModel = Resources["ViewModel"] as InputParameterSelectorControlViewModel;
            ViewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InputParameterSelectorControlView v = d as InputParameterSelectorControlView;

            if (e.Property.Name == nameof(Parameter))
            {
                v.SetParameter((MethodParameterViewModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(AvailableParameterReferences))
            {
                v.SetAvailableParameterReferences((List<MethodParameterReferenceViewModel>)e.NewValue);
            }
            else if (e.Property.Name == nameof(CurrentReferenceValue))
            {
                v.SetCurrentReferenceValue((MethodParameterReferenceValueViewModel)e.NewValue);
            }
        }

        private void SetParameter(MethodParameterViewModel data)
        {
            ViewModel.Parameter = data;
        }

        private void SetAvailableParameterReferences(List<MethodParameterReferenceViewModel> data)
        {
            ViewModel.AvailableParameterReferences = data;
        }

        private void SetCurrentReferenceValue(MethodParameterReferenceValueViewModel data)
        {
            ViewModel.CurrentReferenceValue = data;
        }


        public void UpdateGenericConstantInput(string name, TypeValue type, object defaultValue)
        {
            SetControlViewWithValue(name, type, defaultValue);
        }

        public void SetGenericConstantInput(string name, TypeValue type)
        {
            SetControlViewWithValue(name, type);
        }

        private void SetControlViewWithValue(string name, TypeValue type, object defaultValue = null)
        {
            if (_currentConsantInputControl != null)
            {
                _currentConsantInputControl.ValueChanged -= ConstantControlView_ValueChanged;
            }
            ConstantInputGrid.Children.Clear();
            if ((int)type == 0)
            {
                ConstantInputGrid.Children.Add(new TextBlock() { Text = "Type doesn't allow constant input" });
            }
            else
            {
                var formModel = new GenericFormInputModel()
                {
                    Description = string.Empty,
                    Type = type,
                    Key = name,
                    DisplayName = string.Empty,
                };
                if (defaultValue != null)
                {
                    formModel.DefaultValue = defaultValue;
                }
                _currentConsantInputControl = new GenericInputControlView()
                {
                    InputModel = formModel,
                };
                _currentConsantInputControl.ValueChanged += ConstantControlView_ValueChanged;
                ConstantInputGrid.Children.Add(_currentConsantInputControl);
            }
        }

        private void ConstantControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var data = e as ValueChangedEventArgs;
            ViewModel.UpdatedConstantValue(data.Data);
        }
    }
}
