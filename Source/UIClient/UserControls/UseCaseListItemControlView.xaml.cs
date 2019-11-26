
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
using UIClient.Models;
using UIClient.ViewModels;

namespace UIClient.UserControls
{

    public partial class UseCaseListItemControlView : UserControl
    {

        public UseCaseListItemModel UseCaseItem
        {
            get
            {
                return (UseCaseListItemModel)GetValue(UseCaseItemProperty);
            }
            set
            {
                SetValue(UseCaseItemProperty, value);
            }
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

		public static readonly DependencyProperty UseCaseItemProperty =
                      DependencyProperty.Register(
                          nameof(UseCaseItem),
                          typeof(UseCaseListItemModel),
                          typeof(UseCaseListItemControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		public static readonly DependencyProperty EventManagerProperty =
                      DependencyProperty.Register(
                          nameof(EventManager),
                          typeof(DomainEventManager),
                          typeof(UseCaseListItemControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly UseCaseListItemControlViewModel _viewModel = null;

        public UseCaseListItemControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as UseCaseListItemControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			UseCaseListItemControlView v = d as UseCaseListItemControlView;
			if (e.Property.Name == nameof(UseCaseItem))
            {
                v.SetUseCaseItem((UseCaseListItemModel)e.NewValue);
            }
			else if (e.Property.Name == nameof(EventManager))
            {
                v.SetEventManager((DomainEventManager)e.NewValue);
            }
        }

		private void SetUseCaseItem(UseCaseListItemModel data)
        {
            _viewModel.UseCaseItem = data;
        }

		private void SetEventManager(DomainEventManager data)
        {
            _viewModel.EventManager = data;
        }


        private void UseCase_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.SelectedUseCse();
        }
    }
}
