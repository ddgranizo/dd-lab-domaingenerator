
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
using UIClient.Models;
using UIClient.ViewModels;

namespace UIClient.UserControls.Editors.UseCases
{

    public partial class UseCaseEditorControlView : UserControl
    {

        public static readonly RoutedEvent ValueChangedEvent =
                    System.Windows.EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(UseCaseEditorControlView));

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public void RaiseValueChangedEvent(UseCaseModel data)
        {
            RoutedEventArgs args = new UseCaseChangedRoutedEventArgs()
            {
                Value = data
            };
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        public DomainEventManager EventManager
        {
            get
            {
                return (DomainEventManager)GetValue(EventManagerProperty);
            }
            set
            {
                SetValue(EventManagerProperty, value);
            }
        }

        public static readonly DependencyProperty EventManagerProperty =
                      DependencyProperty.Register(
                          nameof(EventManager),
                          typeof(DomainEventManager),
                          typeof(UseCaseEditorControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public UseCaseModel UseCase
        {
            get
            {
                return (UseCaseModel)GetValue(UseCaseProperty);
            }
            set
            {
                SetValue(UseCaseProperty, value);
            }
        }

		public static readonly DependencyProperty UseCaseProperty =
                      DependencyProperty.Register(
                          nameof(UseCase),
                          typeof(UseCaseModel),
                          typeof(UseCaseEditorControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly UseCaseEditorControlViewModel _viewModel = null;

        public UseCaseEditorControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as UseCaseEditorControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			UseCaseEditorControlView v = d as UseCaseEditorControlView;
			if (e.Property.Name == nameof(UseCase))
            {
                v.SetUseCase((UseCaseModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(EventManager))
            {
                v.SetEventManager((DomainEventManager)e.NewValue);
            }
        }

        private void SetEventManager(DomainEventManager data)
        {
            _viewModel.EventManager = data;
        }

        private void SetUseCase(UseCaseModel data)
        {
            _viewModel.UseCase = data;
        }
		
    }
}
