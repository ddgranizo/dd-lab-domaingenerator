using DD.Lab.Services.System.Implementations;
using DD.Lab.Services.System.Interfaces;
using DD.Lab.Wpf.Commands.Base;
using DD.Lab.Wpf.Drm.Controls;
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Models;
using DD.Lab.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace DD.Lab.Wpf.Drm.Viewmodels
{
    public class DrmControlViewModel : BaseViewModel
    {
        public enum ViewType
        {
            List = 10,
            Detail = 20,
        }

        public enum DetailMode
        {
            Creating = 10,
            Updating = 20,
        }



        private DD.Lab.Wpf.Drm.Controls.DrmControlView _view;

        public ViewType CurrentViewType { get { return GetValue<ViewType>(); } set { SetValue(value); RaisePropertyChange(nameof(IsVisibleList)); } }
        public DetailMode CurrentDetailMode { get { return GetValue<DetailMode>(); } set { SetValue(value); } }

        public Entity CurrentEntity { get { return GetValue<Entity>(); } set { SetValue(value, UpdatedCurrentEntity); } }

        public List<Entity> Entities { get { return GetValue<List<Entity>>(); } set { SetValue(value); UpdateListToCollection(value, EntitiesCollection); } }
        public ObservableCollection<Entity> EntitiesCollection { get; set; } = new ObservableCollection<Entity>();

        public List<Relationship> Relationships { get; set; }
        public GenericEventManager GenericEventManager { get; set; }
        public WpfEventManager WpfEventManager { get; set; }

        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }
        public DrmGridInputData DrmGridInputData { get { return GetValue<DrmGridInputData>(); } set { SetValue(value); } }
        public DrmRecordInputData DrmRecordInputData { get { return GetValue<DrmRecordInputData>(); } set { SetValue(value); } }


        public bool IsVisibleList
        {
            get
            {
                return CurrentViewType == ViewType.List;
            }
        }


        public DrmControlViewModel()
        {
            AddSetterPropertiesTrigger(new PropertiesTrigger(() =>
            {
                var currentModel = GenericManager.Model;
                Entities = GenericManager.Model.Entities.OrderBy(k => k.DisplayName).ToList();
                Relationships = GenericManager.Model.Relationships;

                CurrentViewType = ViewType.List;
                CurrentEntity = !string.IsNullOrEmpty(currentModel.MainEntity)
                     ? Entities.First(k => k.LogicalName == currentModel.MainEntity)
                     : Entities.First();
                
            }, nameof(GenericManager)));

            GenericEventManager = new GenericEventManager();
            GenericEventManager.OnCreateRequested += BusinessEventManager_OnCreateRequested;
            GenericEventManager.OnCreatedEntity += GenericEventManager_OnCreatedEntity;
            GenericEventManager.OnUpdatedEntity += BusinessEventManager_OnUpdatedEntity;
            GenericEventManager.OnSelectedEntity += BusinessEventManager_OnSelectedEntity;
            GenericEventManager.OnDeletedEntity += BusinessEventManager_OnDeletedEntity;

            WpfEventManager = new WpfEventManager();
            WpfEventManager.OnEntityReferenceInputLeftClicked += WpfEventManager_OnEntityReferenceInputLeftClicked;
            InitializeCommands();
        }

        private void WpfEventManager_OnEntityReferenceInputLeftClicked(object sender, Wpf.Events.WpfEntityReferenceClickEventArgs args)
        {
            GenericEventManager.RaiseOnSelectedEntity(Entities.First(k=>k.LogicalName == args.EntityLogicalName), args.Id);
        }

        private void BusinessEventManager_OnDeletedEntity(object sender, Events.EntityEventArgs eventArgs)
        {
            SetGridMode(eventArgs.Entity);
        }

        private void UpdatedCurrentEntity(Entity entity)
        {
            SetGridMode(entity);
        }

        private void SetGridMode(Entity entity)
        {
            CurrentViewType = ViewType.List;
            DrmGridInputData = null;
            DrmGridInputData = new DrmGridInputData()
            {
                Entity = entity,
                GenericEventManager = GenericEventManager,
                GenericManager = GenericManager,
                WpfEventManager = WpfEventManager,
                Relationships = Relationships
            };
        }



        private void SetRecordMode(Entity entity, DetailMode mode, Dictionary<string, object> initialValues)
        {
            CurrentEntity = entity;
            CurrentViewType = ViewType.Detail;
            DrmRecordInputData = null;
            DrmRecordInputData = new DrmRecordInputData()
            {
                Entities = Entities,
                Entity = entity,
                GenericEventManager = GenericEventManager,
                GenericManager = GenericManager,
                InitialValues = initialValues,
                Mode = mode,
                WpfEventManager = WpfEventManager,
                Relationships = Relationships
            };
            
        }

        private void BusinessEventManager_OnUpdatedEntity(object sender, Events.EntityEventArgs eventArgs)
        {
            var entity = eventArgs.Entity;
            var entityValues = GenericManager.Retrieve(entity.LogicalName, eventArgs.Id);
            SetUpdateEntityMode(eventArgs.Entity, entityValues.Values);
        }

        private void GenericEventManager_OnCreatedEntity(object sender, Events.EntityEventArgs eventArgs)
        {
            var entity = eventArgs.Entity;
            var entityValues = GenericManager.Retrieve(entity.LogicalName, eventArgs.Id);
            SetUpdateEntityMode(eventArgs.Entity, entityValues.Values);
        }

        private void BusinessEventManager_OnSelectedEntity(object sender, Events.EntityEventArgs eventArgs)
        {
            var entity = eventArgs.Entity;
            var entityValues = GenericManager.Retrieve(entity.LogicalName, eventArgs.Id);
            SetUpdateEntityMode(eventArgs.Entity, entityValues.Values);
        }

        private void InitializeCommands()
        {
            ListViewModeCommand = new RelayCommand((data) =>
            {
                CurrentViewType = ViewType.List;
                SetGridMode(CurrentEntity);
            },
            (data) =>
            {
                return CurrentViewType == ViewType.Detail;
            });
            RegisterCommand(ListViewModeCommand);
        }

        public ICommand ListViewModeCommand { get; set; }

        private void BusinessEventManager_OnCreateRequested(object sender, Events.CreateRequestEventArgs eventArgs)
        {
            SetCreateEntityMode(eventArgs.Entity, eventArgs.InitalValues);
        }

        private void SetCreateEntityMode(Entity entity, Dictionary<string, object> initialValues)
        {
            SetRecordMode(entity, DetailMode.Creating, initialValues);
        }

        private void SetUpdateEntityMode(Entity entity, Dictionary<string, object> values)
        {
            SetRecordMode(entity, DetailMode.Updating, values);
        }


        public void Initialize(DD.Lab.Wpf.Drm.Controls.DrmControlView v)
        {
            _view = v;
        }

    }
}
