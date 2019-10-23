
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
    public partial class AzurePipelineSettingControlView : UserControl
    {

        public AzurePipelineSettingModel AzurePipelineSetting
        {
            get
            {
                return (AzurePipelineSettingModel)GetValue(AzurePipelineSettingProperty);
            }
            set
            {
                SetValue(AzurePipelineSettingProperty, value);
            }
        }

		public static readonly DependencyProperty AzurePipelineSettingProperty =
                      DependencyProperty.Register(
                          nameof(AzurePipelineSetting),
                          typeof(AzurePipelineSettingModel),
                          typeof(AzurePipelineSettingControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly AzurePipelineSettingControlViewModel _viewModel = null;

        public AzurePipelineSettingControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as AzurePipelineSettingControlViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			AzurePipelineSettingControlView v = d as AzurePipelineSettingControlView;
			if (e.Property.Name == nameof(AzurePipelineSetting))
            {
                v.SetAzurePipelineSetting((AzurePipelineSettingModel)e.NewValue);
            }
        }

		private void SetAzurePipelineSetting(AzurePipelineSettingModel data)
        {
            _viewModel.AzurePipelineSetting = data;
        }
		
    }
}
