
using DomainGeneratorUI.Events;
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

namespace DomainGeneratorUI.Controls
{


    public partial class ParametersManagerControlView : UserControl
    {

        public static readonly RoutedEvent OnModifiedListEvent =
                    EventManager.RegisterRoutedEvent(nameof(OnModifiedList), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(ParametersManagerControlView));

        public event RoutedEventHandler OnModifiedList
        {
            add { AddHandler(OnModifiedListEvent, value); }
            remove { RemoveHandler(OnModifiedListEvent, value); }
        }

        public void RaiseOnModifiedListEvent(List<MethodParameterViewmodel> data)
        {
            RoutedEventArgs args = new OnModifiedMethodParameterListEventArgs()
            {
                Data = data
            };
            args.RoutedEvent = OnModifiedListEvent;
            RaiseEvent(args);
        }

        public IEnumerable<MethodParameterViewmodel> Parameters
        {
            get
            {
                return (IEnumerable<MethodParameterViewmodel>)GetValue(ParametersProperty);
            }
            set
            {
                SetValue(ParametersProperty, value);
            }
        }

        public static readonly DependencyProperty ParametersProperty =
                      DependencyProperty.Register(
                          nameof(Parameters),
                          typeof(IEnumerable<MethodParameterViewmodel>),
                          typeof(ParametersManagerControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)) { BindsTwoWayByDefault = true });

		private readonly ParametersManagerControlViewModel _viewModel = null;

        public ParametersManagerControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as ParametersManagerControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			ParametersManagerControlView v = d as ParametersManagerControlView;
			if (e.Property.Name == nameof(Parameters))
            {
                v.SetParameters((IEnumerable<MethodParameterViewmodel>)e.NewValue);
            }
        }

		private void SetParameters(IEnumerable<MethodParameterViewmodel> data)
        {
            if (data != null)
            {
                _viewModel.Parameters = data.ToList();
            }
            else
            {
                _viewModel.Parameters = null;
            }
            
        }
		
    }
}
