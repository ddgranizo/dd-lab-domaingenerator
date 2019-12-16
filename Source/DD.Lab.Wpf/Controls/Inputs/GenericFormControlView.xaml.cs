
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

    public partial class GenericFormControlView : UserControl
    {

        public static readonly RoutedEvent ValueSetChangedEvent =
                EventManager.RegisterRoutedEvent(nameof(ValueSetChanged), RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(GenericFormInputControlView));

        public event RoutedEventHandler ValueSetChanged
        {
            add { AddHandler(ValueSetChangedEvent, value); }
            remove { RemoveHandler(ValueSetChangedEvent, value); }
        }

        public void RaiseValueSetChangedEvent(Dictionary<string, object> data, bool isDataCompleted)
        {
            RoutedEventArgs args = new ValueSetChangedEventArgs()
            {
                Data = data,
                IsDataCompleted = isDataCompleted,
            };
            args.RoutedEvent = ValueSetChangedEvent;
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

        public static readonly DependencyProperty FormModelProperty =
                      DependencyProperty.Register(
                          nameof(FormModel),
                          typeof(GenericFormModel),
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
        }

        private void SetFormModel(GenericFormModel data)
        {
            _viewModel.FormModel = data;
        }

        private void GenericFormInputControl_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as ValueChangedEventArgs;
            _viewModel.UpdateValue(myEvent.Model.Key, myEvent.Data);
        }
    }
}
