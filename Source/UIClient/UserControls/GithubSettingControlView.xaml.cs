
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
    public partial class GithubSettingControlView : UserControl
    {

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
        }

		private void SetGithubSetting(GithubSettingModel data)
        {
            _viewModel.GithubSetting = data;
        }
    }
}
