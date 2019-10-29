
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
    public partial class DomainInMicroServiceControlView : UserControl
    {

        public DomainInMicroServiceModel DomainInMicroService
        {
            get
            {
                return (DomainInMicroServiceModel)GetValue(DomainInMicroServiceProperty);
            }
            set
            {
                SetValue(DomainInMicroServiceProperty, value);
            }
        }

		public static readonly DependencyProperty DomainInMicroServiceProperty =
                      DependencyProperty.Register(
                          nameof(DomainInMicroService),
                          typeof(DomainInMicroServiceModel),
                          typeof(DomainInMicroServiceControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly DomainInMicroServiceControlViewModel _viewModel = null;

        public DomainInMicroServiceControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as DomainInMicroServiceControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			DomainInMicroServiceControlView v = d as DomainInMicroServiceControlView;
			if (e.Property.Name == nameof(DomainInMicroService))
            {
                v.SetDomainInMicroService((DomainInMicroServiceModel)e.NewValue);
            }
        }

		private void SetDomainInMicroService(DomainInMicroServiceModel data)
        {
            _viewModel.DomainInMicroService = data;
        }
    }
}
