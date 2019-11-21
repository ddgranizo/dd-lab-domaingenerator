
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

    public partial class ProjectControlView : UserControl
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
                          typeof(DomainControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public ProjectStateModel ProjectState
        {
            get
            {
                return (ProjectStateModel)GetValue(ProjectStateProperty);
            }
            set
            {
                SetValue(ProjectStateProperty, value);
            }
        }

		public static readonly DependencyProperty ProjectStateProperty =
                      DependencyProperty.Register(
                          nameof(ProjectState),
                          typeof(ProjectStateModel),
                          typeof(ProjectControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


		private readonly ProjectControlViewModel _viewModel = null;

        public ProjectControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as ProjectControlViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			ProjectControlView v = d as ProjectControlView;
			if (e.Property.Name == nameof(ProjectState))
            {
                v.SetProjectState((ProjectStateModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(EventManager))
            {
                v.SetEventManager((DomainEventManager)e.NewValue);
            }
        }

		private void SetProjectState(ProjectStateModel data)
        {
            _viewModel.ProjectState = data;
        }

        private void SetEventManager(DomainEventManager data)
        {
            _viewModel.EventManager = data;
        }

        private void HierarchyItemControlView_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsGeneralOpen = (e as CollapsedChangedEventArgs).Data;
            }
            
        }

        private void Domain_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsDomainsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void AzurePipelineSettings_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsAzurePipelineSettingsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void Settings_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsSettingsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void GithubSettings_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsGithubSettingsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }

        private void Environments_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsEnvironmentsOpen = (e as CollapsedChangedEventArgs).Data;
            }
        }
    }
}
