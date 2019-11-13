
using DD.DomainGenerator.Models;
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

    public partial class SchemaControlView : UserControl
    {

        public SchemaModelModel Schema
        {
            get
            {
                return (SchemaModelModel)GetValue(SchemaProperty);
            }
            set
            {
                SetValue(SchemaProperty, value);
            }
        }

		public static readonly DependencyProperty SchemaProperty =
                      DependencyProperty.Register(
                          nameof(Schema),
                          typeof(SchemaModelModel),
                          typeof(SchemaControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

		private readonly SchemaControlViewModel _viewModel = null;

        public SchemaControlView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as SchemaControlViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			SchemaControlView v = d as SchemaControlView;
			if (e.Property.Name == nameof(Schema))
            {
                v.SetSchema((SchemaModelModel)e.NewValue);
            }
        }

		private void SetSchema(SchemaModelModel data)
        {
            _viewModel.Schema = data;
        }
    

        private void ShowProperties_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.ShowProperties = true;
        }

        private void HideProperties_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.ShowProperties = false;
        }

        private void ShowUseCases_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.ShowUseCases = true;
        }

        private void HideUseCases_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.ShowUseCases = false;
        }

        private void ShowViews_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.ShowViews = true;
        }

        private void HideViews_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.ShowViews = false;
        }
    }
}
