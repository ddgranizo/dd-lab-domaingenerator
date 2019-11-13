
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
    public partial class SchemaViewControlView : UserControl
    {

        public SchemaViewModel SchemaView
        {
            get
            {
                return (SchemaViewModel)GetValue(SchemaViewProperty);
            }
            set
            {
                SetValue(SchemaViewProperty, value);
            }
        }

		public static readonly DependencyProperty SchemaViewProperty =
                      DependencyProperty.Register(
                          nameof(SchemaView),
                          typeof(SchemaViewModel),
                          typeof(SchemaViewControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly SchemaViewControlViewModel _viewModel = null;

        public SchemaViewControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as SchemaViewControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			SchemaViewControlView v = d as SchemaViewControlView;
			if (e.Property.Name == nameof(SchemaView))
            {
                v.SetSchemaView((SchemaViewModel)e.NewValue);
            }

        }

		private void SetSchemaView(SchemaViewModel data)
        {
            _viewModel.SchemaView = data;
        }
    }
}
