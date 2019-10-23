
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
    public partial class ErrorControlView : UserControl
    {

        public ErrorExecutionActionModel Error
        {
            get
            {
                return (ErrorExecutionActionModel)GetValue(ErrorProperty);
            }
            set
            {
                SetValue(ErrorProperty, value);
            }
        }

		public static readonly DependencyProperty ErrorProperty =
                      DependencyProperty.Register(
                          nameof(Error),
                          typeof(ErrorExecutionActionModel),
                          typeof(ErrorControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly ErrorControlViewModel _viewModel = null;

        public ErrorControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as ErrorControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			ErrorControlView v = d as ErrorControlView;
			if (e.Property.Name == nameof(Error))
            {
                v.SetError((ErrorExecutionActionModel)e.NewValue);
            }
        }

		private void SetError(ErrorExecutionActionModel data)
        {
            _viewModel.Error = data;
        }

    }
}
