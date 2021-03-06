
using DD.Lab.Wpf.Commands;
using DD.Lab.Wpf.Commands.Base;
using DD.Lab.Wpf.Drm.Controls;
using DD.Lab.Wpf.Drm.Extensions;
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Services.Implementations;
using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
using DD.Lab.Wpf.Windows;
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

using static DD.Lab.Wpf.Drm.Viewmodels.DrmControlViewModel;

namespace DD.Lab.Wpf.Drm.Viewmodels
{
    public class DrmRecordViewModel : BaseViewModel
    {

        public DrmRecordInputData DrmRecordInputData { get { return GetValue<DrmRecordInputData>(); } set { SetValue(value, UpdatedDrmRecordInputData); } }

        public WpfEventManager WpfEventManager { get { return GetValue<WpfEventManager>(); } set { SetValue(value); } }
        public DetailMode Mode { get { return GetValue<DetailMode>(); } set { SetValue(value); } }
        public Entity Entity { get { return GetValue<Entity>(); } set { SetValue(value); } }
        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }
        public GenericEventManager GenericEventManager { get { return GetValue<GenericEventManager>(); } set { SetValue(value); } }
        public Dictionary<string, object> InitialValues { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }
        public List<Relationship> Relationships { get { return GetValue<List<Relationship>>(); } set { SetValue(value); UpdateListToCollection(value, RelationshipsCollection); } }
        public ObservableCollection<Relationship> RelationshipsCollection { get; set; } = new ObservableCollection<Relationship>();
        public List<Entity> Entities { get { return GetValue<List<Entity>>(); } set { SetValue(value); UpdateListToCollection(value, EntitiesCollection); } }
        public ObservableCollection<Entity> EntitiesCollection { get; set; } = new ObservableCollection<Entity>();


        public GenericFormModel FormModel { get { return GetValue<GenericFormModel>(); } set { SetValue(value); } }
        public bool IsCompleted { get { return GetValue<bool>(); } set { SetValue(value); } }

        public List<SubGridRelationshipData> CurrentEntityRelationships { get { return GetValue<List<SubGridRelationshipData>>(); } set { SetValue(value); UpdateListToCollection(value, CurrentEntityRelationshipsCollection); } }
        public ObservableCollection<SubGridRelationshipData> CurrentEntityRelationshipsCollection { get; set; } = new ObservableCollection<SubGridRelationshipData>();

        public SubGridRelationshipData SelectedEntityRelationship { get { return GetValue<SubGridRelationshipData>(); } set { SetValue(value, UpdatedSelectedEntityRelationship); } }

        public Dictionary<string, object> Values { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value, UpdatedValues); } }

        public DrmGridInputData DrmGridInputData { get { return GetValue<DrmGridInputData>(); } set { SetValue(value); } }


        public Guid Id { get { return GetValue<Guid>(); } set { SetValue(value); } }

        private DrmRecordControlView _view;


        public DrmRecordViewModel()
        {
            Values = new Dictionary<string, object>();
            IsCompleted = false;
            InitializeCommands();
        }

        public void Initialize(DrmRecordControlView v)
        {
            _view = v;
        }

        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        private void InitializeCommands()
        {
            SaveCommand = new RelayCommandHandled((data) =>
            {
                if (Mode == DetailMode.Creating)
                {
                    var id = GenericManager.Create(Entity.LogicalName, Values);
                    //var newData = GenericManager.Retrieve(Entity.LogicalName, id);
                    //Id = id;
                    //Values = newData.Values;
                    //Mode = DetailMode.Updating;
                    GenericEventManager.RaiseOnCreatedEntity(Entity, id);
                }
                else if (Mode == DetailMode.Updating)
                {
                    GenericManager.Update(Entity.LogicalName, Id, Values);
                    GenericEventManager.RaiseOnUpdatedEntity(Entity, Id, Values);
                }
            },
            (data) =>
            {
                return IsCompleted;
            });

            DeleteCommand = new RelayCommandHandled((data) =>
            {
                var dialog = new OkCancelMessageBox("Confirm the delete? This operation cannot be undone", "Delete operation");
                dialog.ShowDialog();
                if (dialog.Response == OkCancelMessageBox.InputTextBoxResponse.OK)
                {
                    GenericManager.Delete(Entity.LogicalName, Id);
                    GenericEventManager.RaiseOnDeletedEntity(Entity, Id);
                }
            },
            (data) =>
            {
                return Mode == DetailMode.Updating;
            });

            RegisterCommand(SaveCommand);
            RegisterCommand(DeleteCommand);
        }

        public void UpdatedValuesInForm(Dictionary<string, object> values, bool isCompleted)
        {
            Values = values;
            IsCompleted = isCompleted;
        }


        public void UpdatedValues(Dictionary<string, object> values)
        {
            if (Mode == DetailMode.Updating)
            {
                if (values != null && values.ContainsKey("Id"))
                {
                    Id = (Guid)values["Id"];
                }
                else
                {
                    throw new Exception("When updating is necessary to have 'Id' in the values");
                }
            }
            //if (Relationships != null)
            //{
            //    SetRelationships();
            //}
        }

        private void UpdatedDrmRecordInputData(DrmRecordInputData data)
        {
            Entities = null;
            WpfEventManager = null;
            Mode = 0;
            Entity = null;
            GenericManager = null;
            GenericEventManager = null;
            InitialValues = null;
            Relationships = null;

            if (data != null)
            {
                Entities = data.Entities;
                WpfEventManager = data.WpfEventManager;
                Mode = data.Mode;
                Entity = data.Entity;
                GenericManager = data.GenericManager;
                GenericEventManager = data.GenericEventManager;
                InitialValues = data.InitialValues;
                Relationships = data.Relationships;
                SetInitialValues();
                SetModel();
                SetRelationships();
            }
        }



        private void UpdatedSelectedEntityRelationship(SubGridRelationshipData data)
        {
            if (data != null)
            {
                DrmGridInputData = null;
                DrmGridInputData = new DrmGridInputData()
                {
                    Entity = data.RelatedEntity,
                    FilterRelationship = data.Relationship,
                    FilterRelationshipId = data.MainEntityId,
                    FilterRelationshipRecordDisplayName = GetRecordDisplayName(),
                    GenericEventManager = GenericEventManager,
                    GenericManager = GenericManager,
                    Relationships = Relationships,
                    WpfEventManager = WpfEventManager,
                };
            }
        }

        private void SetRelationships()
        {
            CurrentEntityRelationships = Relationships
                            .Where(k => !k.IsManyToMany && k.MainEntity == Entity.LogicalName
                                        || k.IsManyToMany && (k.MainEntity == Entity.LogicalName || k.RelatedEntity == Entity.LogicalName))
                            .Select(k => k.ToSubGridRelationshipData(Entity, Entities, Id, GetRecordDisplayName()))
                            .OrderBy(k => k.GetDisplayableRelationshipName())
                            .ToList();
            if (CurrentEntityRelationships.Any())
            {
                SelectedEntityRelationship = CurrentEntityRelationshipsCollection.First();
            }
        }

        private string GetRecordDisplayName()
        {
            return Values.ContainsKey("Name")
                ? (string)Values["Name"]
                : string.Empty;
        }


        private void SetModel()
        {
            GenericFormModel model = new GenericFormModel(string.Empty);
            foreach (var item in Entity.Attributes)
            {
                var type = GetTypeFromAttribute(item);
                var defaultValue = InitialValues != null && InitialValues.ContainsKey(item.LogicalName)
                        ? InitialValues[item.LogicalName]
                        : null;

                var attribute = new GenericFormInputModel()
                {
                    Key = item.LogicalName,
                    DisplayName = item.DisplayName,
                    Description = item.Description,
                    IsMandatory = item.IsMandatory,
                    DefaultValue = defaultValue,
                    Type = type,
                    CustomModuleName = item.CustomModule,
                    IsCustomModule = item.IsCustomAttribute,
                };
                if (type == GenericFormInputModel.TypeValue.EntityReference)
                {
                    var relatedEntity = Relationships.First(k => k.RelatedEntity == Entity.LogicalName && k.RelatedAttribute == attribute.Key);
                    attribute.EntityReferenceSuggestionHandler =
                        new EntityReferenceHandler(relatedEntity.MainEntity, GenericManager.RetrieveAllHandler, GenericManager.RetrieveHandler);
                }
                else if (type == GenericFormInputModel.TypeValue.OptionSet)
                {
                    attribute.OptionSetValueOptions = item.Options.Select(k => new Wpf.Models.Inputs.OptionSetValue()
                    {
                        DisplayName = k.DisplayName,
                        Value = k.Value
                    }).ToArray();
                }
                else if (type == GenericFormInputModel.TypeValue.State)
                {
                    var options = new List<Wpf.Models.Inputs.OptionSetValue>();
                    options.Add(new Wpf.Models.Inputs.OptionSetValue() { DisplayName = "Enabled", Value = 1 });
                    options.Add(new Wpf.Models.Inputs.OptionSetValue() { DisplayName = "Disabled", Value = 0 });
                    attribute.OptionSetValueOptions = options.ToArray();
                }
                model.Attributes.Add(attribute);
            }
            FormModel = model;
        }


        private GenericFormInputModel.TypeValue GetTypeFromAttribute(Models.Attribute attribute)
        {
            if (attribute.Type == Models.Attribute.AttributeType.Bool)
            {
                return GenericFormInputModel.TypeValue.Bool;
            }
            else if (attribute.Type == Models.Attribute.AttributeType.Datetime)
            {
                return GenericFormInputModel.TypeValue.DateTime;
            }
            else if (attribute.Type == Models.Attribute.AttributeType.Decimal)
            {
                return GenericFormInputModel.TypeValue.Decimal;
            }
            else if (attribute.Type == Models.Attribute.AttributeType.Double)
            {
                return GenericFormInputModel.TypeValue.Double;
            }
            else if (attribute.Type == Models.Attribute.AttributeType.EntityReference)
            {
                return GenericFormInputModel.TypeValue.EntityReference;
            }
            else if (attribute.Type == Models.Attribute.AttributeType.Guid)
            {
                return GenericFormInputModel.TypeValue.Guid;
            }
            else if (attribute.Type == Models.Attribute.AttributeType.Int)
            {
                return GenericFormInputModel.TypeValue.Int;
            }
            else if (attribute.Type == Models.Attribute.AttributeType.OptionSet)
            {
                return GenericFormInputModel.TypeValue.OptionSet;
            }
            else if (attribute.Type == Models.Attribute.AttributeType.State)
            {
                return GenericFormInputModel.TypeValue.State;
            }
            else if (attribute.Type == Models.Attribute.AttributeType.String)
            {
                return GenericFormInputModel.TypeValue.String;
            }
            throw new NotImplementedException();
        }

        private void SetInitialValues()
        {
            if (Entity != null)
            {
                foreach (var item in Entity.Attributes)
                {
                    Values[item.LogicalName] = GetInitialValue(item.LogicalName);
                }
            }
        }

        private object GetInitialValue(string attribute)
        {
            if (InitialValues != null && InitialValues.ContainsKey(attribute))
            {
                return InitialValues[attribute];
            }
            return null;
        }
    }
}
