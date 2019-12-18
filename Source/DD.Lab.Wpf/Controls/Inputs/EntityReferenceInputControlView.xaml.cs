
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



    public partial class EntityReferenceInputControlView : UserControl
    {

		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(EntityReferenceInputControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent(EntityReferenceValue data)
        {
            RoutedEventArgs args = new EntityReferenceValueChangedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        public EntityReferenceValue DefaultValue
        {
            get
            {
                return (EntityReferenceValue)GetValue(EntityReferenceProperty);
            }
            set
            {
                SetValue(EntityReferenceProperty, value);
            }
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

		public static readonly DependencyProperty EntityReferenceProperty =
                      DependencyProperty.Register(
                          nameof(DefaultValue),
                          typeof(EntityReferenceValue),
                          typeof(EntityReferenceInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


		public static readonly DependencyProperty InputModelProperty =
                      DependencyProperty.Register(
                          nameof(InputModel),
                          typeof(GenericFormInputModel),
                          typeof(EntityReferenceInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly EntityReferenceInputControlViewModel _viewModel = null;

        public EntityReferenceInputControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as EntityReferenceInputControlViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			EntityReferenceInputControlView v = d as EntityReferenceInputControlView;
			if (e.Property.Name == nameof(DefaultValue))
            {
                v.SetEntityReference((EntityReferenceValue)e.NewValue);
            }
			else if (e.Property.Name == nameof(InputModel))
            {
                v.SetInputModel((GenericFormInputModel)e.NewValue);
            }
        }

		private void SetEntityReference(EntityReferenceValue data)
        {
            _viewModel.EntityReference = data;
        }

		private void SetInputModel(GenericFormInputModel data)
        {
            _viewModel.InputModel = data;
        }
    }
}
