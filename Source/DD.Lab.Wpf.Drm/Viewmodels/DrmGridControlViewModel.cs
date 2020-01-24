
using DD.Lab.Wpf.Commands;
using DD.Lab.Wpf.Commands.Base;
using DD.Lab.Wpf.Drm.Controls;
using DD.Lab.Wpf.Drm.Extensions;
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Models.Data;
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
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;


namespace DD.Lab.Wpf.Drm.Viewmodels
{
    public class DrmGridControlViewModel : BaseViewModel
    {

        public DrmGridInputData DrmGridInputData { get { return GetValue<DrmGridInputData>(); } set { SetValue(value, UpdatedDrmGridInputData); } }

        public Entity Entity { get { return GetValue<Entity>(); } set { SetValue(value); } }
        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }
        public GenericEventManager GenericEventManager { get { return GetValue<GenericEventManager>(); } set { SetValue(value); } }
        public Relationship FilterRelationship { get { return GetValue<Relationship>(); } set { SetValue(value); } }
        public Guid FilterRelationshipId { get { return GetValue<Guid>(); } set { SetValue(value); } }
        public string FilterRelationshipRecordDisplayName { get { return GetValue<string>(); } set { SetValue(value); } }
        public List<Relationship> Relationships { get { return GetValue<List<Relationship>>(); } set { SetValue(value); UpdateListToCollection(value, RelationshipsCollection); } }
        public ObservableCollection<Relationship> RelationshipsCollection { get; set; } = new ObservableCollection<Relationship>();
        public WpfEventManager WpfEventManager { get { return GetValue<WpfEventManager>(); } set { SetValue(value); } }


        public DataSet DataSetModel { get { return GetValue<DataSet>(); } set { SetValue(value, UpdatedDataSet); } }
        public DataSet DisplayableDataSetModel { get { return GetValue<DataSet>(); } set { SetValue(value); } }
        public bool IsVisibleGridRibbon { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsVisibleSubGridRibbon { get { return GetValue<bool>(); } set { SetValue(value); } }
        public string FirstEntityAssociation { get { return GetValue<string>(); } set { SetValue(value); } }
        public string SecondEntityAssociation { get { return GetValue<string>(); } set { SetValue(value); } }



        public ICommand CreateCommand { get; set; }
        public ICommand AddNewRelatedCommand { get; set; }
        public ICommand AssociateCommand { get; set; }

        private DrmGridControlView _view;

        public DrmGridControlViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CreateCommand = new RelayCommandHandled((data) =>
            {
                GenericEventManager.RaiseOnCreateRequested(Entity);
            });

            AddNewRelatedCommand = new RelayCommandHandled((data) =>
            {
                var initialValues = new Dictionary<string, object>();
                initialValues.Add(FilterRelationship.RelatedAttribute, new EntityReferenceValue()
                {
                    Id = FilterRelationshipId,
                    LogicalName = FilterRelationship.RelatedEntity,
                    DisplayName = FilterRelationshipRecordDisplayName,
                });
                GenericEventManager.RaiseOnCreateRequested(Entity, initialValues);
            });

            AssociateCommand = new RelayCommandHandled((data) =>
            {

                var availableValues = GenericManager.RetrieveAll(FirstEntityAssociation);
                var availablesEntityReferences = availableValues
                    .Values
                    .Select(k => new EntityReferenceValue() { Id = k.Id, LogicalName = FirstEntityAssociation, DisplayName = (string)k.Values["Name"] })
                    .ToList();

                var initialEntityReferences = DataSetModel
                    .Values
                    .Select(k => new EntityReferenceValue() { Id = k.Id, LogicalName = FirstEntityAssociation, DisplayName = (string)k.Values["Name"] })
                    .ToList();

                var associateWindow = new MultipleAssociationWindow(
                    $"Associate {FirstEntityAssociation}(s) to this {SecondEntityAssociation}",
                    "Association",
                    availablesEntityReferences,
                    initialEntityReferences);

                associateWindow.ShowDialog();
                var response = associateWindow.Response;
                if (response == MultipleAssociationWindow.MultipleAssociationResponse.OK)
                {
                    var firstRecordEntityLogicalName = Entity.LogicalName;
                    var secondRecordEntityLogicalName = firstRecordEntityLogicalName == FilterRelationship.MainEntity
                        ? FilterRelationship.RelatedEntity
                        : FilterRelationship.MainEntity;

                    var selectedValues = associateWindow.SelectedValues;
                    foreach (var item in initialEntityReferences)
                    {
                        GenericManager.Disassociate(secondRecordEntityLogicalName, FilterRelationshipId, FilterRelationship.IntersectionName, firstRecordEntityLogicalName, item.Id);
                    }
                    foreach (var item in selectedValues)
                    {
                        GenericManager.Associate(secondRecordEntityLogicalName, FilterRelationshipId, FilterRelationship.IntersectionName, firstRecordEntityLogicalName, item.Id);
                    }
                    GetValues();
                }
            });

            RegisterCommand(AddNewRelatedCommand);
            RegisterCommand(CreateCommand);
            RegisterCommand(AssociateCommand);
        }

        public void SelectedDataRow(DataRecord dataRowModel)
        {
            GenericEventManager.RaiseOnSelectedEntity(Entity, dataRowModel.Id);
        }

        //private void UpdatedGenericEventManager(GenericEventManager businessEventManager)
        //{
        //    if (!_eventSuscribed)
        //    {
        //        _eventSuscribed = true;
        //        businessEventManager.OnCreatedEntity += BusinessEventManager_OnCreatedEntity;
        //        businessEventManager.OnUpdatedEntity += BusinessEventManager_OnUpdatedEntity;
        //        businessEventManager.OnDeletedEntity += BusinessEventManager_OnDeletedEntity;
        //    }
        //}

        //private void BusinessEventManager_OnDeletedEntity(object sender, Events.EntityEventArgs eventArgs)
        //{
        //    GetValues();
        //}

        //private void BusinessEventManager_OnUpdatedEntity(object sender, Events.EntityEventArgs eventArgs)
        //{
        //    GetValues();
        //}

        //private void BusinessEventManager_OnCreatedEntity(object sender, Events.EntityEventArgs eventArgs)
        //{
        //    GetValues();
        //}

        public void Initialize(DrmGridControlView v)
        {
            _view = v;
        }

        private void UpdatedDrmGridInputData(DrmGridInputData data)
        {
            Entity = null;
            GenericManager = null;
            WpfEventManager = null;
            GenericEventManager = null;
            FilterRelationship = null;
            FilterRelationshipRecordDisplayName = null;
            FilterRelationshipId = Guid.Empty;

            if (data != null)
            {
                Entity = data.Entity;
                GenericManager = data.GenericManager;
                WpfEventManager = data.WpfEventManager;
                GenericEventManager = data.GenericEventManager;
                FilterRelationship = data.FilterRelationship;
                FilterRelationshipRecordDisplayName = data.FilterRelationshipRecordDisplayName;
                FilterRelationshipId = data.FilterRelationshipId;
                Relationships = data.Relationships;
                if (FilterRelationship != null)
                {
                    IsVisibleGridRibbon = false;
                    IsVisibleSubGridRibbon = true;
                }
                else
                {
                    IsVisibleGridRibbon = true;
                    IsVisibleSubGridRibbon = false;
                }
                GetValues();
            }
        }


        private void GetValues()
        {

            var data = new DataSet(Entity.LogicalName);
            data = GenericManager.RetrieveAll(Entity.LogicalName);

            if (FilterRelationship != null)
            {
                if (FilterRelationship.IsManyToMany)
                {
                    var firstEntity = Entity.LogicalName;
                    var secondEntity = FilterRelationship.MainEntity == firstEntity
                            ? FilterRelationship.RelatedEntity
                            : FilterRelationship.MainEntity;
                    FirstEntityAssociation = firstEntity;
                    SecondEntityAssociation = secondEntity;

                    var values = GenericManager.RetrieveAllAssociated(firstEntity, FilterRelationshipId, FilterRelationship.IntersectionName, secondEntity);
                    data = values;
                }
                else
                {
                    var filteredValues = data.Values.Where(row =>
                    {
                        var containsRecord = row.Values.ContainsKey(FilterRelationship.RelatedAttribute);
                        if (containsRecord)
                        {
                            var record = (EntityReferenceValue)row.Values[FilterRelationship.RelatedAttribute];
                            return record.Id == FilterRelationshipId;
                        }
                        return false;
                    }).ToList();
                    data.Values = filteredValues;
                }
            }
            DataSetModel = data;
            _view.UpdateColumns(Entity.Attributes);
        }


        private void UpdatedDataSet(DataSet model)
        {
            DisplayableDataSetModel = new DataSet();
            if (model.EntityLogicalName == Entity.LogicalName)
            {
                DisplayableDataSetModel = model.ToDisplayableDataSet(Entity);
            }
        }

    }
}
