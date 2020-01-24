
using DD.Lab.Wpf.Drm.Inputs;
using DD.Lab.Wpf.Drm.Models;
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

    public partial class HierarchyDrmRecordRelationshipView : UserControl
    {
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

        public Relationship Relationship
        {
            get
            {
                return (Relationship)GetValue(RelationshipProperty);
            }
            set
            {
                SetValue(RelationshipProperty, value);
            }
        }


        public static readonly DependencyProperty RecordProperty =
                      DependencyProperty.Register(
                          nameof(Record),
                          typeof(DataRecord),
                          typeof(HierarchyDrmRecordRelationshipView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty GenericManagerProperty =
                      DependencyProperty.Register(
                          nameof(GenericManager),
                          typeof(GenericManager),
                          typeof(HierarchyDrmRecordRelationshipView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty ContextEntityProperty =
                      DependencyProperty.Register(
                          nameof(ContextEntity),
                          typeof(string),
                          typeof(HierarchyDrmRecordRelationshipView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty ParentContextEntityProperty =
                      DependencyProperty.Register(
                          nameof(ParentContextEntity),
                          typeof(string),
                          typeof(HierarchyDrmRecordRelationshipView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty ParentContextEntityIdProperty =
                      DependencyProperty.Register(
                          nameof(ParentContextEntityId),
                          typeof(Guid),
                          typeof(HierarchyDrmRecordRelationshipView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty TargetEntityLogicalNameProperty =
                      DependencyProperty.Register(
                          nameof(TargetEntityLogicalName),
                          typeof(string),
                          typeof(HierarchyDrmRecordRelationshipView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty RelationshipProperty =
                      DependencyProperty.Register(
                          nameof(Relationship),
                          typeof(Relationship),
                          typeof(HierarchyDrmRecordRelationshipView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


        private readonly HierarchyDrmRecordRelationshipViewModel _viewModel = null;

        public HierarchyDrmRecordRelationshipView()
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as HierarchyDrmRecordRelationshipViewModel;
            _viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HierarchyDrmRecordRelationshipView v = d as HierarchyDrmRecordRelationshipView;
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
            else if (e.Property.Name == nameof(Relationship))
            {
                v.SetRelationship((Relationship)e.NewValue);
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

        private void SetRelationship(Relationship data)
        {
            _viewModel.Relationship = data;
        }


        private void Relationship_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _viewModel.IsOpen = !_viewModel.IsOpen;
            }
        }

        public void SetRecords(HierarchyDrmRecordCollectionInputData data)
        {
            RecordsGrid.Children.Clear();
            RecordsGrid.Children.Add(new HierarchyDrmRecordsCollectionView() { 
                HierarchyDrmEntityCollectionInputData = data,
            });
        }
    }
}
