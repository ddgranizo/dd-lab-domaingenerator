
using DD.Lab.Wpf.Drm.Events;
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Drm.Viewmodels;
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

namespace DD.Lab.Wpf.Drm.Controls
{

    public partial class DataSetGridControlView : UserControl
    {

        public static readonly RoutedEvent SelectedDataRowEvent =
                    EventManager.RegisterRoutedEvent(nameof(SelectedDataRow), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(DataSetGridControlView));

        public event RoutedEventHandler SelectedDataRow
        {
            add { AddHandler(SelectedDataRowEvent, value); }
            remove { RemoveHandler(SelectedDataRowEvent, value); }
        }

        public void RaiseSelectedDataRowEvent(DataRowModel data)
        {
            RoutedEventArgs args = new SelectedDataRowEventArgs()
            {
                Data = data
            };
            args.RoutedEvent = SelectedDataRowEvent;
            RaiseEvent(args);
        }

        public Entity Entity
        {
            get
            {
                return (Entity)GetValue(EntityProperty);
            }
            set
            {
                SetValue(EntityProperty, value);
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

        public GenericEventManager GenericEventManager
        {
            get
            {
                return (GenericEventManager)GetValue(GenericEventManagerProperty);
            }
            set
            {
                SetValue(GenericEventManagerProperty, value);
            }
        }

        public List<Relationship> Relationships
        {
            get
            {
                return (List<Relationship>)GetValue(RelationshipsProperty);
            }
            set
            {
                SetValue(RelationshipsProperty, value);
            }
        }


        public Relationship FilterRelationship
        {
            get
            {
                return (Relationship)GetValue(FilterRelationshipProperty);
            }
            set
            {
                SetValue(FilterRelationshipProperty, value);
            }
        }



        public Guid FilterRelationshipId
        {
            get
            {
                return (Guid)GetValue(FilterRelationshipIdProperty);
            }
            set
            {
                SetValue(FilterRelationshipIdProperty, value);
            }
        }


        public static readonly DependencyProperty FilterRelationshipIdProperty =
                      DependencyProperty.Register(
                          nameof(FilterRelationshipId),
                          typeof(Guid),
                          typeof(DataSetGridControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public static readonly DependencyProperty FilterRelationshipProperty =
                      DependencyProperty.Register(
                          nameof(FilterRelationship),
                          typeof(Relationship),
                          typeof(DataSetGridControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public static readonly DependencyProperty RelationshipsProperty =
                      DependencyProperty.Register(
                          nameof(Relationships),
                          typeof(List<Relationship>),
                          typeof(DataSetGridControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        public static readonly DependencyProperty EntityProperty =
                      DependencyProperty.Register(
                          nameof(Entity),
                          typeof(Entity),
                          typeof(DataSetGridControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty GenericManagerProperty =
                      DependencyProperty.Register(
                          nameof(GenericManager),
                          typeof(GenericManager),
                          typeof(DataSetGridControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty GenericEventManagerProperty =
                      DependencyProperty.Register(
                          nameof(GenericEventManager),
                          typeof(GenericEventManager),
                          typeof(DataSetGridControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });

        private readonly DataSetGridControlViewModel _viewModel = null;

        public DataSetGridControlView()
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as DataSetGridControlViewModel;
            _viewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSetGridControlView v = d as DataSetGridControlView;
            if (e.Property.Name == nameof(Entity))
            {
                v.SetEntity((Entity)e.NewValue);
            }
            else if (e.Property.Name == nameof(GenericManager))
            {
                v.SetGenericManager((GenericManager)e.NewValue);
            }
            else if (e.Property.Name == nameof(GenericEventManager))
            {
                v.SetGenericEventManager((GenericEventManager)e.NewValue);
            }
            else if (e.Property.Name == nameof(Relationships))
            {
                v.SetRelationships((List<Relationship>)e.NewValue);
            }
            else if (e.Property.Name == nameof(FilterRelationship))
            {
                v.SetFilterRelationship((Relationship)e.NewValue);
            }
            else if (e.Property.Name == nameof(FilterRelationshipId))
            {
                v.SetFilterRelationshipId((Guid)e.NewValue);
            }
        }

        private void SetEntity(Entity data)
        {
            _viewModel.Entity = data;
        }

        private void SetGenericManager(GenericManager data)
        {
            _viewModel.GenericManager = data;
        }

        private void SetGenericEventManager(GenericEventManager data)
        {
            _viewModel.GenericEventManager = data;
        }

        private void SetRelationships(List<Relationship> relationships)
        {
            _viewModel.Relationships = relationships;
        }

        private void SetFilterRelationship(Relationship relationship)
        {
            _viewModel.FilterRelationsip = relationship;
        }

        private void SetFilterRelationshipId(Guid relationshipId)
        {
            _viewModel.FilterRelationsipId = relationshipId;
        }

        public void UpdateColumns(List<Models.Attribute> attributes)
        {
            List<DataGridColumn> columnsForRemove = new List<DataGridColumn>();
            foreach (var item in this.DataMainGrid.Columns)
            {
                if (item.Header is string && (string)item.Header != "Id" && (string)item.Header != "#")
                {
                    columnsForRemove.Add(item);
                }
            }
            foreach (var item in columnsForRemove)
            {
                this.DataMainGrid.Columns.Remove(item);
            }
            foreach (var item in attributes
                        .Where(k => k.LogicalName != "Id")
                        .OrderBy(k => k.DisplayName))
            {
                Binding dataBinding = new Binding();
                dataBinding.Path = new PropertyPath($"Values[{item.LogicalName}]");
                dataBinding.Mode = BindingMode.OneWay;
                this.DataMainGrid.Columns.Add(new DataGridTextColumn() { Header = item.DisplayName, Binding = dataBinding, IsReadOnly = true });
            }
        }

        private void DataMainGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataMainGrid.SelectedItem == null) return;
            var selectedDataRow = DataMainGrid.SelectedItem as DataRowModel;
            _viewModel.SelectedDataRow(selectedDataRow);
        }
    }
}
