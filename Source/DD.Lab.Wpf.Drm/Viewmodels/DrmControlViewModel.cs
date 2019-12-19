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

        public GenericEventManager GenericEventManager { get { return GetValue<GenericEventManager>(); } set { SetValue(value); } }


        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }
        
        private DD.Lab.Wpf.Drm.Controls.DrmControlView _view;

        public ViewType CurrentViewType { get { return GetValue<ViewType>(); } set { SetValue(value); } }
        public DetailMode CurrentDetailMode { get { return GetValue<DetailMode>(); } set { SetValue(value); } }

        public Entity CurrentEntity { get { return GetValue<Entity>(); } set { SetValue(value, UpdatedCurrentEntity); } }
        public Dictionary<string, object> InitialValues { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }

        public List<Dictionary<string, object>> Values { get { return GetValue<List<Dictionary<string, object>>>(); } set { SetValue(value); UpdateListToCollection(value, ValuesCollection); } }
        public ObservableCollection<Dictionary<string, object>> ValuesCollection { get; set; } = new ObservableCollection<Dictionary<string, object>>();

        public List<Entity> Entities { get { return GetValue<List<Entity>>(); } set { SetValue(value); UpdateListToCollection(value, EntitiesCollection); } }
        public ObservableCollection<Entity> EntitiesCollection { get; set; } = new ObservableCollection<Entity>();

        public List<Relationship> Relationships { get { return GetValue<List<Relationship>>(); } set { SetValue(value); UpdateListToCollection(value, RelationshipsCollection); } }
        public ObservableCollection<Relationship> RelationshipsCollection { get; set; } = new ObservableCollection<Relationship>();


        public DrmControlViewModel()
        {
            AddSetterPropertiesTrigger(new PropertiesTrigger(() =>
            {
                var currentModel = GenericManager.Model;
                Entities = GenericManager.Model.Entities.OrderBy(k => k.DisplayName).ToList();
                Relationships = GenericManager.Model.Relationships;

                CurrentEntity = !string.IsNullOrEmpty(currentModel.MainEntity)
                     ? Entities.First(k => k.LogicalName == currentModel.MainEntity)
                     : Entities.First();
                CurrentViewType = ViewType.List;
            }, nameof(GenericManager)));

            GenericEventManager = new GenericEventManager();
            GenericEventManager.OnCreateRequested += BusinessEventManager_OnCreateRequested;
            GenericEventManager.OnCreatedEntity += GenericEventManager_OnCreatedEntity;
            GenericEventManager.OnUpdatedEntity += BusinessEventManager_OnUpdatedEntity;
            GenericEventManager.OnSelectedEntity += BusinessEventManager_OnSelectedEntity;
            GenericEventManager.OnDeletedEntity += BusinessEventManager_OnDeletedEntity;

            InitializeCommands();
        }

   

        private void BusinessEventManager_OnDeletedEntity(object sender, Events.EntityEventArgs eventArgs)
        {
            SetContextEntity(eventArgs.Entity);
        }


        private void UpdatedCurrentEntity(Entity entity)
        {
            CurrentViewType = ViewType.List;
        }

        private void SetContextEntity(Entity entity)
        {
            CurrentEntity = null;
            CurrentEntity = entity;
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
                var currentEntity = CurrentEntity;
                SetContextEntity(currentEntity);
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
            SetContextEntity(entity);
            CurrentViewType = ViewType.Detail;
            CurrentDetailMode = DetailMode.Creating;
            InitialValues = null;
            InitialValues = initialValues;
        }

        private void SetUpdateEntityMode(Entity entity, Dictionary<string, object> values)
        {
            SetContextEntity(entity);
            CurrentViewType = ViewType.Detail;
            CurrentDetailMode = DetailMode.Updating;
            InitialValues = null;
            InitialValues = values;
        }


        public void Initialize(DD.Lab.Wpf.Drm.Controls.DrmControlView v)
        {
            _view = v;
        }

    }
}
