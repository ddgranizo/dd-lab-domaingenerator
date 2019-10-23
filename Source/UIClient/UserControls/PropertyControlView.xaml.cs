
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
    public partial class PropertyControlView : UserControl
    {
        public SchemaModelPropertyModel Property
        {
            get
            {
                return (SchemaModelPropertyModel)GetValue(PropertyProperty);
            }
            set
            {
                SetValue(PropertyProperty, value);
            }
        }

		public static readonly DependencyProperty PropertyProperty =
                      DependencyProperty.Register(
                          nameof(Property),
                          typeof(SchemaModelPropertyModel),
                          typeof(PropertyControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly PropertyControlViewModel _viewModel = null;

        public PropertyControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as PropertyControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			PropertyControlView v = d as PropertyControlView;
			if (e.Property.Name == nameof(Property))
            {
                v.SetProperty((SchemaModelPropertyModel)e.NewValue);
            }
        }

		private void SetProperty(SchemaModelPropertyModel data)
        {
            _viewModel.Property = data;
        }
    }
}
