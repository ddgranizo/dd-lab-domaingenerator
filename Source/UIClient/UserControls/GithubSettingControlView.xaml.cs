
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
using UIClient.Models;
using UIClient.ViewModels;

namespace UIClient.UserControls
{
    public partial class GithubSettingControlView : UserControl
    {

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
                          typeof(GithubSettingControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public GithubSettingModel GithubSetting
        {
            get
            {
                return (GithubSettingModel)GetValue(GithubSettingProperty);
            }
            set
            {
                SetValue(GithubSettingProperty, value);
            }
        }

		public static readonly DependencyProperty GithubSettingProperty =
                      DependencyProperty.Register(
                          nameof(GithubSetting),
                          typeof(GithubSettingModel),
                          typeof(GithubSettingControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly GithubSettingControlViewModel _viewModel = null;

        public GithubSettingControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as GithubSettingControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			GithubSettingControlView v = d as GithubSettingControlView;
			if (e.Property.Name == nameof(GithubSetting))
            {
                v.SetGithubSetting((GithubSettingModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(EventManager))
            {
                v.SetEventManager((DomainEventManager)e.NewValue);
            }
        }

		private void SetGithubSetting(GithubSettingModel data)
        {
            _viewModel.GithubSetting = data;
        }

        private void SetEventManager(DomainEventManager data)
        {
            _viewModel.EventManager = data;
        }

        private void General_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }
    }
}
