
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

        public DetailMode Mode
        {
            get
            {
                return (DetailMode)GetValue(ModeProperty);
            }
            set
            {
                SetValue(ModeProperty, value);
            }
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

        public Dictionary<string, object> InitialValues
        {
            get
            {
                return (Dictionary<string, object>)GetValue(InitialValuesProperty);
            }
            set
            {
                SetValue(InitialValuesProperty, value);
            }
        }


        public List<Entity> Entities
        {
            get
            {
                return (List<Entity>)GetValue(EntitiesProperty);
            }
            set
            {
                SetValue(EntitiesProperty, value);
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


        public static readonly DependencyProperty EntitiesProperty =
                      DependencyProperty.Register(
                          nameof(Entities),
                          typeof(List<Entity>),
                          typeof(DrmRecordControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty RelationshipsProperty =
                      DependencyProperty.Register(
                          nameof(Relationships),
                          typeof(List<Relationship>),
                          typeof(DrmRecordControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty ModeProperty =
                      DependencyProperty.Register(
                          nameof(Mode),
                          typeof(DetailMode),
                          typeof(DrmRecordControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty EntityProperty =
                      DependencyProperty.Register(
                          nameof(Entity),
                          typeof(Entity),
                          typeof(DrmRecordControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty GenericManagerProperty =
                      DependencyProperty.Register(
                          nameof(GenericManager),
                          typeof(GenericManager),
                          typeof(DrmRecordControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty GenericEventManagerProperty =
                      DependencyProperty.Register(
                          nameof(GenericEventManager),
                          typeof(GenericEventManager),
                          typeof(DrmRecordControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });


        public static readonly DependencyProperty InitialValuesProperty =
                      DependencyProperty.Register(
                          nameof(InitialValues),
                          typeof(Dictionary<string, object>),
                          typeof(DrmRecordControlView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler))
                          {
                              BindsTwoWayByDefault = true,
                          });



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
            else if (e.Property.Name == nameof(InitialValues))
            {
                v.SetInitialValues((Dictionary<string, object>)e.NewValue);
            }
            else if (e.Property.Name == nameof(Mode))
            {
                v.SetMode((DetailMode)e.NewValue);
            }
            else if (e.Property.Name == nameof(Relationships))
            {
                v.SetRelationships((List<Relationship>)e.NewValue);
            }
            else if (e.Property.Name == nameof(Entities))
            {
                v.SetEntities((List<Entity>)e.NewValue);
            }

        }

        private void SetMode(DetailMode data)
        {
            _viewModel.Mode = data;
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

        private void SetInitialValues(Dictionary<string, object> data)
        {
            _viewModel.InitialValues = data;
        }

        private void SetRelationships(List<Relationship> relationships)
        {
            _viewModel.Relationships = relationships;
        }

        private void SetEntities(List<Entity> entities)
        {
            _viewModel.Entities = entities;
        }

        private void GenericFormControlView_ValueSetChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as ValueSetChangedEventArgs;
            _viewModel.UpdatedValuesInForm(myEvent.Data, myEvent.IsDataCompleted);
        }
    }
}
