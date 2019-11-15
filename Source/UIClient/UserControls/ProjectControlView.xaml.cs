
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
        }

		private void SetProjectState(ProjectStateModel data)
        {
            _viewModel.ProjectState = data;
        }

        private void HierarchyItemControlView_CollapsedChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsOpen = (e as CollapsedChangedEventArgs).Data;
            }
            
        }
    }
}
