
using DD.DomainGenerator.Actions.Base;
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

    public partial class ActionControlView : UserControl
    {

        public ActionExecutionModel Action
        {
            get
            {
                return (ActionExecutionModel)GetValue(ActionProperty);
            }
            set
            {
                SetValue(ActionProperty, value);
            }
        }

		public static readonly DependencyProperty ActionProperty =
                      DependencyProperty.Register(
                          nameof(Action),
                          typeof(ActionExecutionModel),
                          typeof(ActionControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly ActionControlViewModel _viewModel = null;

        public ActionControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as ActionControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			ActionControlView v = d as ActionControlView;
			if (e.Property.Name == nameof(Action))
            {
                v.SetAction((ActionExecutionModel)e.NewValue);
            }
        }


		private void SetAction(ActionExecutionModel data)
        {
            _viewModel.Action = data;
        }
    }
}
