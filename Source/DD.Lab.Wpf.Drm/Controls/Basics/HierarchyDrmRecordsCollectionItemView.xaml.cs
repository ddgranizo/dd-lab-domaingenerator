
using DD.Lab.Wpf.Drm.Events;
using DD.Lab.Wpf.Drm.Inputs;
using DD.Lab.Wpf.Drm.Models.Data;
using HierarchyDrmRecordsCollectionItem;
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

    public partial class HierarchyDrmRecordsCollectionItemView : UserControl
    {

        public static readonly RoutedEvent SelectedDataRowEvent =
                   EventManager.RegisterRoutedEvent(nameof(SelectedDataRow), RoutingStrategy.Bubble,
                   typeof(RoutedEventHandler), typeof(HierarchyDrmRecordsCollectionItemView));

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


        public DataRecord Record
        {
            get
            {
                return (DataRecord)GetValue(RecordProperty);
            }
            set
            {
                SetValue(RecordProperty, value);
            }
        }

        public GenericManager GenericManager
        {
            get
            {
                return (GenericManager)GetValue(GenericManagerProperty);
            }
            set
            {
                SetValue(GenericManagerProperty, value);
            }
        }


        public string ContextEntity
        {
            get
            {
                return (string)GetValue(ContextEntityProperty);
            }
            set
            {
                SetValue(ContextEntityProperty, value);
            }
        }

        public string ParentContextEntity
        {
            get
            {
                return (string)GetValue(ParentContextEntityProperty);
            }
            set
            {
                SetValue(ParentContextEntityProperty, value);
            }
        }


        public Guid ParentContextEntityId
        {
            get
            {
                return (Guid)GetValue(ParentContextEntityIdProperty);
            }
            set
            {
                SetValue(ParentContextEntityIdProperty, value);
            }
        }

        public string TargetEntityLogicalName
        {
            get
            {
                return (string)GetValue(TargetEntityLogicalNameProperty);
            }
            set
            {
                SetValue(TargetEntityLogicalNameProperty, value);
            }
        }

        public static readonly DependencyProperty RecordProperty =
                      DependencyProperty.Register(
                          nameof(Record),
                          typeof(DataRecord),
                          typeof(HierarchyDrmRecordsCollectionItemView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty GenericManagerProperty =
                      DependencyProperty.Register(
                          nameof(GenericManager),
                          typeof(GenericManager),
                          typeof(HierarchyDrmRecordsCollectionItemView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


        public static readonly DependencyProperty ContextEntityProperty =
                      DependencyProperty.Register(
                          nameof(ContextEntity),
                          typeof(string),
                          typeof(HierarchyDrmRecordsCollectionItemView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty ParentContextEntityProperty =
                      DependencyProperty.Register(
                          nameof(ParentContextEntity),
                          typeof(string),
                          typeof(HierarchyDrmRecordsCollectionItemView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty ParentContextEntityIdProperty =
                      DependencyProperty.Register(
                          nameof(ParentContextEntityId),
                          typeof(Guid),
                          typeof(HierarchyDrmRecordsCollectionItemView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty TargetEntityLogicalNameProperty =
                      DependencyProperty.Register(
                          nameof(TargetEntityLogicalName),
                          typeof(string),
                          typeof(HierarchyDrmRecordsCollectionItemView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        private readonly HierarchyDrmRecordsCollectionItemViewModel _viewModel = null;

        public HierarchyDrmRecordsCollectionItemView()
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as HierarchyDrmRecordsCollectionItemViewModel;
            _viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HierarchyDrmRecordsCollectionItemView v = d as HierarchyDrmRecordsCollectionItemView;
            if (e.Property.Name == nameof(Record))
            {
                v.SetRecord((DataRecord)e.NewValue);
            }
            else if (e.Property.Name == nameof(GenericManager))
            {
                v.SetGenericManager((GenericManager)e.NewValue);
            }
            else if (e.Property.Name == nameof(ContextEntity))
            {
                v.SetContextEntity((string)e.NewValue);
            }
            else if (e.Property.Name == nameof(ParentContextEntity))
            {
                v.SetParentContextEntity((string)e.NewValue);
            }
            else if (e.Property.Name == nameof(ParentContextEntityId))
            {
                v.SetParentContextEntityId((Guid)e.NewValue);
            }
            else if (e.Property.Name == nameof(TargetEntityLogicalName))
            {
                v.SetTargetEntityLogicalName((string)e.NewValue);
            }

        }

        private void SetRecord(DataRecord data)
        {
            _viewModel.Record = data;
        }

        private void SetGenericManager(GenericManager data)
        {
            _viewModel.GenericManager = data;
        }

        private void SetContextEntity(string data)
        {
            _viewModel.ContextEntity = data;
        }

        private void SetParentContextEntity(string data)
        {
            _viewModel.ParentContextEntity = data;
        }

        private void SetParentContextEntityId(Guid data)
        {
            _viewModel.ParentContextEntityId = data;
        }

        private void SetTargetEntityLogicalName(string data)
        {
            _viewModel.TargetEntityLogicalName = data;
        }



        private void Record_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _viewModel.IsOpen = !_viewModel.IsOpen;
            }
        }

        public void SetViewRecordData(HierarchyDrmRecordInputData data)
        {
            RecordGrid.Children.Clear();
            var view = new HierarchyDrmRecordView()
            {
                HierarchyRecordInputData = data,
            };
            view.SelectedDataRow += View_SelectedDataRow;
            RecordGrid.Children.Add(view);
        }

        private void View_SelectedDataRow(object sender, RoutedEventArgs e)
        {
            var data = e as SelectedDataRowEventArgs;
            RaiseSelectedDataRowEvent(data.Data, data.LogicalName);
        }

        private void TargetRecord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed
                && e.ClickCount == 2)
            {
                RaiseSelectedDataRowEvent(_viewModel.Record, _viewModel.ContextEntity);
            }
        }
    }
}
