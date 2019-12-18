
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
    public class DataSetGridControlViewModel : BaseViewModel
    {
        public Entity Entity { get { return GetValue<Entity>(); } set { SetValue(value, UpdatedEntity); } }

        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value, UpdatedGenericManager); } }

        public GenericEventManager GenericEventManager { get { return GetValue<GenericEventManager>(); } set { SetValue(value, UpdatedGenericEventManager); } }

        public Relationship FilterRelationsip { get { return GetValue<Relationship>(); } set { SetValue(value, UpdatedFilterRelationship); } }
        public Guid FilterRelationsipId { get { return GetValue<Guid>(); } set { SetValue(value, UpdatedFilterRelationshipId); } }


        public DataSetModel DataSetModel { get { return GetValue<DataSetModel>(); } set { SetValue(value, UpdatedDataSet); } }
        public DataSetModel DisplayableDataSetModel { get { return GetValue<DataSetModel>(); } set { SetValue(value); } }

        public List<Relationship> Relationships { get { return GetValue<List<Relationship>>(); } set { SetValue(value); UpdateListToCollection(value, RelationshipsCollection); } }
        public ObservableCollection<Relationship> RelationshipsCollection { get; set; } = new ObservableCollection<Relationship>();



        public bool IsVisibleGridRibbon { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsVisibleSubGridRibbon { get { return GetValue<bool>(); } set { SetValue(value); } }


        public string FirstEntityAssociation { get { return GetValue<string>(); } set { SetValue(value); } }
        public string SecondEntityAssociation { get { return GetValue<string>(); } set { SetValue(value); } }


        private bool _eventSuscribed = false;

        public ICommand CreateCommand { get; set; }
        public ICommand AddNewRelatedCommand { get; set; }
        public ICommand AssociateCommand { get; set; }

        private DataSetGridControlView _view;

        public DataSetGridControlViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CreateCommand = new RelayCommand((data) =>
            {
                GenericEventManager.RaiseOnCreateRequested(Entity);
            });

            AddNewRelatedCommand = new RelayCommand((data) =>
            {
                var initialValues = new Dictionary<string, object>();
                initialValues.Add(FilterRelationsip.RelatedAttribute, new EntityReferenceValue() { Id = FilterRelationsipId });
                GenericEventManager.RaiseOnCreateRequested(Entity, initialValues);
            });

            AssociateCommand = new RelayCommand((data) =>
            {

                var availableValues = GenericManager.RetrieveAll(FirstEntityAssociation);
                var availablesEntityReferences = availableValues
                    .Values
                    .Select(k => new EntityReferenceValue() { Id = k.Id, DisplayName = (string)k.Values["Name"] })
                    .ToList();

                var initialEntityReferences = DataSetModel
                    .Values
                    .Select(k => new EntityReferenceValue() { Id = k.Id, DisplayName = (string)k.Values["Name"] })
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
                    var selectedValues = associateWindow.SelectedValues;
                    foreach (var item in initialEntityReferences)
                    {
                        GenericManager.Disassociate(FilterRelationsip.MainEntity, FilterRelationsipId, FilterRelationsip.IntersectionName, FilterRelationsip.RelatedEntity, item.Id);
                    }
                    foreach (var item in selectedValues)
                    {
                        GenericManager.Associate(FilterRelationsip.MainEntity, FilterRelationsipId, FilterRelationsip.IntersectionName, FilterRelationsip.RelatedEntity, item.Id);
                    }
                    GetValues();
                }

            });

            RegisterCommand(AddNewRelatedCommand);
            RegisterCommand(CreateCommand);
            RegisterCommand(AssociateCommand);
        }

        public void SelectedDataRow(DataRowModel dataRowModel)
        {
            GenericEventManager.RaiseOnSelectedEntity(Entity, dataRowModel.Id);
        }

        private void UpdatedGenericEventManager(GenericEventManager businessEventManager)
        {
            if (!_eventSuscribed)
            {
                _eventSuscribed = true;
                businessEventManager.OnCreatedEntity += BusinessEventManager_OnCreatedEntity;
                businessEventManager.OnUpdatedEntity += BusinessEventManager_OnUpdatedEntity;
                businessEventManager.OnDeletedEntity += BusinessEventManager_OnDeletedEntity;
            }
        }

        private void BusinessEventManager_OnDeletedEntity(object sender, Events.EntityEventArgs eventArgs)
        {
            GetValues();
        }

        private void BusinessEventManager_OnUpdatedEntity(object sender, Events.EntityEventArgs eventArgs)
        {
            GetValues();
        }

        private void BusinessEventManager_OnCreatedEntity(object sender, Events.EntityEventArgs eventArgs)
        {
            GetValues();
        }

        public void Initialize(DataSetGridControlView v)
        {
            _view = v;
        }

        private void UpdatedEntity(Entity entity)
        {
            GetValues();
        }


        private void UpdatedGenericManager(GenericManager genericManager)
        {
            GetValues();
        }

        private void GetValues()
        {
            if (Entity != null && GenericManager != null)
            {
                IsVisibleGridRibbon = true;
                IsVisibleSubGridRibbon = false;
                var data = new DataSetModel();
                data = GenericManager.RetrieveAll(Entity.LogicalName);

                if (FilterRelationsip != null && FilterRelationsipId != Guid.Empty)
                {
                    IsVisibleGridRibbon = false;
                    IsVisibleSubGridRibbon = true;
                    if (FilterRelationsip.IsManyToMany)
                    {
                        var firstEntity = Entity.LogicalName;
                        var secondEntity = FilterRelationsip.MainEntity == firstEntity
                                ? FilterRelationsip.RelatedEntity
                                : FilterRelationsip.MainEntity;
                        FirstEntityAssociation = firstEntity;
                        SecondEntityAssociation = secondEntity;

                        var values = GenericManager.RetrieveAllAssociated(secondEntity, FilterRelationsipId, FilterRelationsip.IntersectionName, firstEntity);
                        data = values;
                    }
                    else
                    {
                        var filteredValues = data.Values.Where(row =>
                        {
                            var containsRecord = row.Values.ContainsKey(FilterRelationsip.RelatedAttribute);
                            if (containsRecord)
                            {
                                var record = (EntityReferenceValue)row.Values[FilterRelationsip.RelatedAttribute];
                                return record.Id == FilterRelationsipId;
                            }
                            return false;
                        }).ToList();
                        data.Values = filteredValues;
                    }
                }
                DataSetModel = data;
                _view.UpdateColumns(Entity.Attributes);
            }
        }


        private void UpdatedDataSet(DataSetModel model)
        {
            DisplayableDataSetModel = model.ToDisplayableDataSet();
        }

        private void UpdatedFilterRelationship(Relationship relationship)
        {
            GetValues();
            IsVisibleGridRibbon = false;
            IsVisibleSubGridRibbon = true;
        }

        private void UpdatedFilterRelationshipId(Guid id)
        {
            GetValues();
        }
    }
}
