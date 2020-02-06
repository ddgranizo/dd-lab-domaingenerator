
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

        private void GenericInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var data = e as ValueChangedEventArgs;
            RaiseValueChangedEvent(data.Model, data.Data);
        }
    }
}
