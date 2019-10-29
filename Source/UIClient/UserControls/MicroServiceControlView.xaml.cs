
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
    public partial class MicroServiceControlView : UserControl
    {

        public MicroServiceModel MicroService
        {
            get
            {
                return (MicroServiceModel)GetValue(MicroServiceProperty);
            }
            set
            {
                SetValue(MicroServiceProperty, value);
            }
        }

		public static readonly DependencyProperty MicroServiceProperty =
                      DependencyProperty.Register(
                          nameof(MicroService),
                          typeof(MicroServiceModel),
                          typeof(MicroServiceControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly MicroServiceControlViewModel _viewModel = null;

        public MicroServiceControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as MicroServiceControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			MicroServiceControlView v = d as MicroServiceControlView;
			if (e.Property.Name == nameof(MicroService))
            {
                v.SetMicroService((MicroServiceModel)e.NewValue);
            }
        }

		private void SetMicroService(MicroServiceModel data)
        {
            _viewModel.MicroService = data;
        }
		
    }
}
