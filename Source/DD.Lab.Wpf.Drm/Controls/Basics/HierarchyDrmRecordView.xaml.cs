
using DD.Lab.Wpf.Drm.Events;
using DD.Lab.Wpf.Drm.Inputs;
using DD.Lab.Wpf.Drm.Models.Data;
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

    public partial class HierarchyDrmRecordView : UserControl
    {
        public static readonly RoutedEvent SelectedDataRowEvent =
                   EventManager.RegisterRoutedEvent(nameof(SelectedDataRow), RoutingStrategy.Bubble,
                   typeof(RoutedEventHandler), typeof(HierarchyDrmRecordView));

        public event RoutedEventHandler SelectedDataRow
        {
            add { AddHandler(SelectedDataRowEvent, value); }
            remove { RemoveHandler(SelectedDataRowEvent, value); }
        }

        public void RaiseSelectedDataRowEvent(DataRecord data, string logicalName)
        {
            RoutedEventArgs args = new SelectedDataRowEventArgs()
            {
                Data = data,
                LogicalName = logicalName
            };
            args.RoutedEvent = SelectedDataRowEvent;
            RaiseEvent(args);
        }


        public HierarchyDrmRecordInputData HierarchyRecordInputData
        {
            get
            {
                return (HierarchyDrmRecordInputData)GetValue(HierarchyRecordInputDataProperty);
            }
            set
            {
                SetValue(HierarchyRecordInputDataProperty, value);
            }
        }

		public static readonly DependencyProperty HierarchyRecordInputDataProperty =
                      DependencyProperty.Register(
                          nameof(HierarchyRecordInputData),
                          typeof(HierarchyDrmRecordInputData),
                          typeof(HierarchyDrmRecordView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

		private readonly HierarchyDrmRecordViewModel _viewModel = null;

        public HierarchyDrmRecordView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as HierarchyDrmRecordViewModel;
			_viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			HierarchyDrmRecordView v = d as HierarchyDrmRecordView;
			if (e.Property.Name == nameof(HierarchyRecordInputData))
            {
                v.SetHierarchyRecordInputData((HierarchyDrmRecordInputData)e.NewValue);
            }
        }

		private void SetHierarchyRecordInputData(HierarchyDrmRecordInputData data)
        {
            _viewModel.HierarchyRecordInputData = data;
        }

        private void HierarchyDrmRecordRelationshipView_SelectedDataRow(object sender, RoutedEventArgs e)
        {
            var data = e as SelectedDataRowEventArgs;
            if (data.LogicalName == _viewModel.ContextEntity)
            {
                RaiseSelectedDataRowEvent(data.Data, data.LogicalName);
            }
        }
    }
}
