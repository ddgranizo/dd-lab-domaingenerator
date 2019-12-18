using DD.Lab.GenericUI.Core;
using DD.Lab.GenericUI.Core.Models;
using DD.Lab.Services.System.Implementations;
using DD.Lab.Services.System.Interfaces;
using DD.Lab.Wpf.Commands.Base;
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
using UIClientV2.Controls;
using UIClientV2.Extensions;
using UIClientV2.Services;

namespace UIClientV2.Viewmodels
{
    public class MainControlViewModel : BaseViewModel
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

        public IFileService FileService { get; }
        public IJsonParserService JsonParserService { get; }
        public StoredMetadataSchemaService StoredMetadataModel { get; }
        public StoredGenericValuesService StoredDataModel { get; }
        public GenericManager GenericManager { get; }
        public BusinessEventManager BusinessEventManager { get; }
        public BusinessWorkflowManager BusinessWorkflowManager { get; set; }
        private MainControlView _view;

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


        public MainControlViewModel()
        {
            GenericManager = new GenericManager();
            BusinessWorkflowManager = new BusinessWorkflowManager(GenericManager);
            FileService = new FileService();
            JsonParserService = new JsonParserService();
            StoredMetadataModel = new StoredMetadataSchemaService(JsonParserService, FileService);
            StoredDataModel = new StoredGenericValuesService(JsonParserService, FileService);
            BusinessEventManager = new BusinessEventManager();
            var currentModel = StoredMetadataModel
                .GetStoredData()
                .CompleteModel();
            
            GenericManager.InitializeModel(currentModel);

            GenericManager.CreateHandler = new CreateService(StoredDataModel);
            GenericManager.UpdateHandler = new UpdateService(StoredDataModel);
            GenericManager.DeleteHandler = new DeleteService(StoredDataModel);
            GenericManager.RetrieveHandler = new RetrieveService(StoredDataModel);
            GenericManager.RetrieveAllHandler = new RetrieveAllService(StoredDataModel);
            GenericManager.AssociateHandler = new AssociateService(StoredDataModel);
            GenericManager.DisassociateHandler = new DisassociateService(StoredDataModel);
            GenericManager.RetrieveAllAssociatedHandler = new RetrieveAllAssociatedService(StoredDataModel);

            Entities = GenericManager.Model.Entities.OrderBy(k => k.DisplayName).ToList();
            Relationships = GenericManager.Model.Relationships;

            CurrentEntity = !string.IsNullOrEmpty(currentModel.MainEntity)
                 ? Entities.First(k => k.LogicalName == currentModel.MainEntity)
                 : Entities.First();
            CurrentViewType = ViewType.List;

            BusinessEventManager.OnCreateRequested += BusinessEventManager_OnCreateRequested;
            BusinessEventManager.OnUpdatedEntity += BusinessEventManager_OnUpdatedEntity;
            BusinessEventManager.OnSelectedEntity += BusinessEventManager_OnSelectedEntity;
            BusinessEventManager.OnDeletedEntity += BusinessEventManager_OnDeletedEntity;
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


        public void Initialize(MainControlView v)
        {
            _view = v;
        }

    }
}
