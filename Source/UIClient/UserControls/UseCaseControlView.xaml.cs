
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

    public partial class UseCaseControlView : UserControl
    {

        public UseCaseModel UseCase
        {
            get
            {
                return (UseCaseModel)GetValue(UseCaseProperty);
            }
            set
            {
                SetValue(UseCaseProperty, value);
            }
        }

		public static readonly DependencyProperty UseCaseProperty =
                      DependencyProperty.Register(
                          nameof(UseCase),
                          typeof(UseCaseModel),
                          typeof(UseCaseControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly UseCaseControlViewModel _viewModel = null;

        public UseCaseControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as UseCaseControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			UseCaseControlView v = d as UseCaseControlView;
			if (e.Property.Name == nameof(UseCase))
            {
                v.SetUseCase((UseCaseModel)e.NewValue);
            }
        }

		private void SetUseCase(UseCaseModel data)
        {
            _viewModel.UseCase = data;
        }
    }
}
