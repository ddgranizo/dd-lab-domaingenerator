
using DD.DomainGenerator.Models;
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
using static DD.DomainGenerator.Models.ActionParameterDefinition;

namespace UIClient.UserControls.Inputs
{
    public partial class GenericInputControlView : UserControl
    {
		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(GenericInputControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent (ActionParameterDefinition parameterDefinition, object data)
        {
            RoutedEventArgs args = new ValueChangedEventArgs()
            {
                Data = data,
                ParameterDefinition = parameterDefinition,
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        public Dictionary<string, List<string>> SugestionsDictionary
        {
            get
            {
                return (Dictionary<string, List<string>>)GetValue(SugestionsDictionaryProperty);
            }
            set
            {
                SetValue(SugestionsDictionaryProperty, value);
            }
        }


        public ActionParameterDefinition ParameterDefinition
        {
            get
            {
                return (ActionParameterDefinition)GetValue(ParameterDefinitionProperty);
            }
            set
            {
                SetValue(ParameterDefinitionProperty, value);
            }
        }

        public Dictionary<string, object>  DefaultValues
        {
            get
            {
                return (Dictionary<string, object>)GetValue(DefaultValuesProperty);
            }
            set
            {
                SetValue(DefaultValuesProperty, value);
            }
        }

        public static readonly DependencyProperty DefaultValuesProperty =
                      DependencyProperty.Register(
                          nameof(DefaultValues),
                          typeof(Dictionary<string, object>),
                          typeof(GenericInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public static readonly DependencyProperty ParameterDefinitionProperty =
                      DependencyProperty.Register(
                          nameof(ParameterDefinition),
                          typeof(ActionParameterDefinition),
                          typeof(GenericInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public static readonly DependencyProperty SugestionsDictionaryProperty =
                      DependencyProperty.Register(
                          nameof(SugestionsDictionary),
                          typeof(Dictionary<string, List<string>>),
                          typeof(GenericInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        private readonly GenericInputControlViewModel _viewModel = null;

        public GenericInputControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as GenericInputControlViewModel;
            _viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			GenericInputControlView v = d as GenericInputControlView;
			if (e.Property.Name == nameof(ParameterDefinition))
            {
                v.SetParameterDefinition((ActionParameterDefinition)e.NewValue);
            }
            else if (e.Property.Name == nameof(DefaultValues))
            {
                v.SetDefaultValues((Dictionary<string, object>)e.NewValue);
            }
            else if (e.Property.Name == nameof(SugestionsDictionary))
            {
                v.SetSugestionsDictionary((Dictionary<string, List<string>>)e.NewValue);
            }
        }

		private void SetParameterDefinition(ActionParameterDefinition data)
        {
            _viewModel.ParameterDefinition = data;
        }

        private void SetDefaultValues(Dictionary<string, object> data)
        {
            _viewModel.DefaultValues = data;
        }

        private void SetSugestionsDictionary(Dictionary<string, List<string>> data)
        {
            _viewModel.SugestionsDictionary = data;
        }

        private void StringInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as StringValueChangedEventArgs;
            RaiseValueChangedEvent(ParameterDefinition, myEvent.Value);
        }

        private void BooleanInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as BooleanValueChangedEventArgs;
            RaiseValueChangedEvent(ParameterDefinition, myEvent.Value);
        }

        private void IntegerInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as IntValueChangedEventArgs;
            RaiseValueChangedEvent(ParameterDefinition, myEvent.Value);
        }

        private void DecimalInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as DecimalValueChangedEventArgs;
            RaiseValueChangedEvent(ParameterDefinition, myEvent.Value);
        }


        private void PasswordInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as PasswordValueChangedEventArgs;
            RaiseValueChangedEvent(ParameterDefinition, myEvent.Value);
        }
    }
}
