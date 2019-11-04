
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
    public partial class SettingControlView : UserControl
    {

        public SettingModel Setting
        {
            get
            {
                return (SettingModel)GetValue(SettingProperty);
            }
            set
            {
                SetValue(SettingProperty, value);
            }
        }

		public static readonly DependencyProperty SettingProperty =
                      DependencyProperty.Register(
                          nameof(Setting),
                          typeof(SettingModel),
                          typeof(SettingControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });



		private readonly SettingControlViewModel _viewModel = null;

        public SettingControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as SettingControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			SettingControlView v = d as SettingControlView;
			if (e.Property.Name == nameof(Setting))
            {
                v.SetSetting((SettingModel)e.NewValue);
            }
        }

		private void SetSetting(SettingModel data)
        {
            _viewModel.Setting = data;
        }
		
    }
}
