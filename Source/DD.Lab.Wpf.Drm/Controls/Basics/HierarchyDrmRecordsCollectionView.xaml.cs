
using DD.Lab.Wpf.Drm.Inputs;
using DD.Lab.Wpf.Drm.Viewmodels.Basics;
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

namespace DD.Lab.Wpf.Drm.Controls.Basics
{

    public partial class HierarchyDrmRecordsCollectionView : UserControl
    {

        public HierarchyDrmRecordCollectionInputData HierarchyDrmEntityCollectionInputData
        {
            get
            {
                return (HierarchyDrmRecordCollectionInputData)GetValue(HierarchyDrmEntityCollectionInputDataProperty);
            }
            set
            {
                SetValue(HierarchyDrmEntityCollectionInputDataProperty, value);
            }
        }

		public static readonly DependencyProperty HierarchyDrmEntityCollectionInputDataProperty =
                      DependencyProperty.Register(
                          nameof(HierarchyDrmEntityCollectionInputData),
                          typeof(HierarchyDrmRecordCollectionInputData),
                          typeof(HierarchyDrmRecordsCollectionView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

		private readonly HierarchyDrmRecordsCollectionViewModel _viewModel = null;

        public HierarchyDrmRecordsCollectionView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as HierarchyDrmRecordsCollectionViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			HierarchyDrmRecordsCollectionView v = d as HierarchyDrmRecordsCollectionView;
            if (e.Property.Name == nameof(HierarchyDrmEntityCollectionInputData))
            {
                v.SetHierarchyDrmEntityCollectionInputData((HierarchyDrmRecordCollectionInputData)e.NewValue);
            }
        }

        private void SetHierarchyDrmEntityCollectionInputData(HierarchyDrmRecordCollectionInputData data)
        {
            _viewModel.HierarchyDrmEntityCollectionInputData = data;
        }



    }
}
