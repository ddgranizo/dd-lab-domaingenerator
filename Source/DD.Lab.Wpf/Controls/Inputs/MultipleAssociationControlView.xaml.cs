
using DD.Lab.Wpf.Inputs.Events;
using DD.Lab.Wpf.Models.Inputs;
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

    public partial class MultipleAssociationControlView : UserControl
    {

		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(MultipleAssociationControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent(List<EntityReferenceValue> data)
        {
            RoutedEventArgs args = new MultipleAssociationValueChangedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        public List<EntityReferenceValue> AvailableValues
        {
            get
            {
                return (List<EntityReferenceValue>)GetValue(AvailableValuesProperty);
            }
            set
            {
                SetValue(AvailableValuesProperty, value);
            }
        }

        public List<EntityReferenceValue> InitialValues
        {
            get
            {
                return (List<EntityReferenceValue>)GetValue(InitialValuesProperty);
            }
            set
            {
                SetValue(InitialValuesProperty, value);
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
                          typeof(MultipleAssociationControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty AvailableValuesProperty =
                      DependencyProperty.Register(
                          nameof(AvailableValues),
                          typeof(List<EntityReferenceValue>),
                          typeof(MultipleAssociationControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


		public static readonly DependencyProperty InitialValuesProperty =
                      DependencyProperty.Register(
                          nameof(InitialValues),
                          typeof(List<EntityReferenceValue>),
                          typeof(MultipleAssociationControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


		private readonly MultipleAssociationControlViewModel _viewModel = null;

        public MultipleAssociationControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as MultipleAssociationControlViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			MultipleAssociationControlView v = d as MultipleAssociationControlView;
			if (e.Property.Name == nameof(AvailableValues))
            {
                v.SetAvailableValues((List<EntityReferenceValue>)e.NewValue);
            }
			else if (e.Property.Name == nameof(InitialValues))
            {
                v.SetInitialValues((List<EntityReferenceValue>)e.NewValue);
            }
            else if (e.Property.Name == nameof(WpfEventManager))
            {
                v.SetWpfEventManager((WpfEventManager)e.NewValue);
            }
        }

		private void SetAvailableValues(List<EntityReferenceValue> data)
        {
            _viewModel.AvailableValues = data;
        }

		private void SetInitialValues(List<EntityReferenceValue> data)
        {
            _viewModel.InitialValues = data;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdatedChecked();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdatedChecked();
        }

        private void SetWpfEventManager(WpfEventManager data)
        {
            _viewModel.WpfEventManager = data;
        }
    }
}
