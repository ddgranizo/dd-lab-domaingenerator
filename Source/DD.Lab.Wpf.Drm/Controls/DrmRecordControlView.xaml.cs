
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Viewmodels;
using DD.Lab.Wpf.Inputs.Events;
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
using static DD.Lab.Wpf.Drm.Viewmodels.DrmControlViewModel;

namespace DD.Lab.Wpf.Drm.Controls
{

    public partial class DrmRecordControlView : UserControl
    {

        public DrmRecordInputData DrmRecordInputData
        {
            get
            {
                return (DrmRecordInputData)GetValue(DrmRecordInputDataProperty);
            }
            set
            {
                SetValue(DrmRecordInputDataProperty, value);
            }
        }

        public static readonly DependencyProperty DrmRecordInputDataProperty =
                      DependencyProperty.Register(
                          nameof(DrmRecordInputData),
                          typeof(DrmRecordInputData),
                          typeof(DrmRecordControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        private readonly DrmRecordViewModel _viewModel = null;

        public DrmRecordControlView()
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as DrmRecordViewModel;
            _viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DrmRecordControlView v = d as DrmRecordControlView;
            if (e.Property.Name == nameof(DrmRecordInputData))
            {
                v.SetDrmRecordInputData((DrmRecordInputData)e.NewValue);
            }
        }

        private void SetDrmRecordInputData(DrmRecordInputData data)
        {
            _viewModel.DrmRecordInputData = data;
        }

        private void GenericFormControlView_ValueSetChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as ValueSetChangedEventArgs;
            _viewModel.UpdatedValuesInForm(myEvent.Data, myEvent.IsDataCompleted);
        }
    }
}
