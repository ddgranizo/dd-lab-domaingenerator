
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
    public partial class SchemaInDomainControlView : UserControl
    {

        public SchemaInDomainModel SchemaInDomain
        {
            get
            {
                return (SchemaInDomainModel)GetValue(SchemaInDomainProperty);
            }
            set
            {
                SetValue(SchemaInDomainProperty, value);
            }
        }


        public static readonly DependencyProperty SchemaInDomainProperty =
                      DependencyProperty.Register(
                          nameof(SchemaInDomain),
                          typeof(SchemaInDomainModel),
                          typeof(SchemaInDomainControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        private readonly SchemaInDomainViewModel _viewModel = null;

        public SchemaInDomainControlView()
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as SchemaInDomainViewModel;
            _viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SchemaInDomainControlView v = d as SchemaInDomainControlView;
            if (e.Property.Name == nameof(SchemaInDomain))
            {
                v.SetSchemaInDomain((SchemaInDomainModel)e.NewValue);
            }
        }

        private void SetSchemaInDomain(SchemaInDomainModel data)
        {
            _viewModel.SchemaInDomain = data;
        }
    }
}
