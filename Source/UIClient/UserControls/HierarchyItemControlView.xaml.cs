
using MaterialDesignThemes.Wpf;
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


    public partial class HierarchyItemControlView : UserControl
    {

		public static readonly RoutedEvent CollapsedChangedEvent =
                    EventManager.RegisterRoutedEvent(nameof(CollapsedChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(HierarchyItemControlView));
        
		public event RoutedEventHandler CollapsedChanged
        {
            add { AddHandler(CollapsedChangedEvent, value); }
            remove { RemoveHandler(CollapsedChangedEvent, value); }
        }

		public void RaiseCollapsedChangedEvent(bool data)
        {
            RoutedEventArgs args = new CollapsedChangedEventArgs()
            {
                Data = data
            };
            args.RoutedEvent = CollapsedChangedEvent;
            RaiseEvent(args);
        }





        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public PackIconKind Icon
        {
            get
            {
                return (PackIconKind)GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }

        public bool IsCollapsible
        {
            get
            {
                return (bool)GetValue(IsCollapsibleProperty);
            }
            set
            {
                SetValue(IsCollapsibleProperty, value);
            }
        }

		public static readonly DependencyProperty TextProperty =
                      DependencyProperty.Register(
                          nameof(Text),
                          typeof(string),
                          typeof(HierarchyItemControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		public static readonly DependencyProperty IconProperty =
                      DependencyProperty.Register(
                          nameof(Icon),
                          typeof(PackIconKind),
                          typeof(HierarchyItemControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


		public static readonly DependencyProperty IsCollapsibleProperty =
                      DependencyProperty.Register(
                          nameof(IsCollapsible),
                          typeof(bool),
                          typeof(HierarchyItemControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });



		private readonly HierarchyItemControlViewModel _viewModel = null;

        public HierarchyItemControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as HierarchyItemControlViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			HierarchyItemControlView v = d as HierarchyItemControlView;
			if (e.Property.Name == nameof(Text))
            {
                v.SetText((string)e.NewValue);
            }
			else if (e.Property.Name == nameof(Icon))
            {
                v.SetIcon((PackIconKind)e.NewValue);
            }
			else if (e.Property.Name == nameof(IsCollapsible))
            {
                v.SetIsCollapsible((bool)e.NewValue);
            }
        }

		private void SetText(string data)
        {
            _viewModel.Text = data;
        }

		private void SetIcon(PackIconKind data)
        {
            _viewModel.Icon = data;
        }

		private void SetIsCollapsible(bool data)
        {
            _viewModel.IsCollapsible = data;
        }
    
     
        private void Collapser_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.IsCollapsed = !_viewModel.IsCollapsed;
        }
    }
}
