
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

namespace UIClient.UserControls
{

    public partial class GuidInputControlView : UserControl
    {

		public static readonly RoutedEvent ValueChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(GuidInputControlView));
        
		public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

		public void RaiseValueChangedEvent(Guid data)
        {
            RoutedEventArgs args = new GuidValueChangedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        public Guid DefaultValue
        {
            get
            {
                return (Guid)GetValue(DefaultValueProperty);
            }
            set
            {
                SetValue(DefaultValueProperty, value);
            }
        }

        public static readonly DependencyProperty DefaultValueProperty =
                            DependencyProperty.Register(
                                nameof(DefaultValue),
                                typeof(Guid),
                                typeof(GuidInputControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                                {
                                    BindsTwoWayByDefault = true,
                                });


        private readonly GuidInputControlViewModel _viewModel = null;

        public GuidInputControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as GuidInputControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			GuidInputControlView v = d as GuidInputControlView;
			if (e.Property.Name == nameof(DefaultValue))
            {
                v.SetDefaultValue((Guid)e.NewValue);
            }
        }

		private void SetDefaultValue(Guid data)
        {
            _viewModel.DefaultValue = data;
        }
		
    }
}
