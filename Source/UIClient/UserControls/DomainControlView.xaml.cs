
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
    public partial class DomainControlView : UserControl
    {

        public DomainModel Domain
        {
            get
            {
                return (DomainModel)GetValue(DomainProperty);
            }
            set
            {
                SetValue(DomainProperty, value);
            }
        }

		public static readonly DependencyProperty DomainProperty =
                      DependencyProperty.Register(
                          nameof(Domain),
                          typeof(DomainModel),
                          typeof(DomainControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly DomainControlViewModel _viewModel = null;

        public DomainControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as DomainControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			DomainControlView v = d as DomainControlView;
			
            if (e.Property.Name == nameof(Domain))
            {
                v.SetDomain((DomainModel)e.NewValue);
            }
        }

		private void SetDomain(DomainModel data)
        {
            _viewModel.Domain = data;
        }
    }
}
