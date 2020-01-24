using DD.Lab.Wpf.Drm.Controls.Basics;
using DD.Lab.Wpf.Drm.Inputs;
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Models.Inputs;
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


namespace DD.Lab.Wpf.Drm.Viewmodels.Basics
{
    public class HierarchyDrmRecordRelationshipViewModel : BaseViewModel
    {

        public DataRecord Record { get { return GetValue<DataRecord>(); } set { SetValue(value, UpdatedRecord); } }
        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }
        public string ContextEntity { get { return GetValue<string>(); } set { SetValue(value); } }
        public string ParentContextEntity { get { return GetValue<string>(); } set { SetValue(value); } }
        public Guid ParentContextEntityId { get { return GetValue<Guid>(); } set { SetValue(value); } }
        public string TargetEntityLogicalName { get { return GetValue<string>(); } set { SetValue(value); } }
        public Relationship Relationship { get { return GetValue<Relationship>(); } set { SetValue(value, UpdatedRelationship); } }

        public string RelatedEntityLogicalName { get { return GetValue<string>(); } set { SetValue(value); } }

        public string RecordName { get { return GetValue<string>(); } set { SetValue(value); } }


        public bool IsOpen { get { return GetValue<bool>(); } set { SetValue(value, UpdatedIsOpen); } }
        private bool _isLoaded = false;

        private HierarchyDrmRecordRelationshipView _view;

        public HierarchyDrmRecordRelationshipViewModel()
        {

        }

        public void Initialize(HierarchyDrmRecordRelationshipView v)
        {
            _view = v;
        }


        private void UpdatedRecord(DataRecord data)
        {
            RecordName = null;
            if (data != null)
            {
                RecordName = data.Values.ContainsKey("Name") ? (string)data.Values["Name"] : "No name";
            }
        }

        private void UpdatedRelationship(Relationship data)
        {
            if (data.IsManyToMany)
            {
                RelatedEntityLogicalName = data.MainEntity == ContextEntity
                        ? data.RelatedEntity
                        : data.MainEntity;
            }
            else
            {
                RelatedEntityLogicalName = data.RelatedEntity;
            }
        }

        private void UpdatedIsOpen(bool data)
        {
            if (data && !_isLoaded)
            {
                var records = Relationship.IsManyToMany
                           ? GenericManager.RetrieveAllAssociated(ContextEntity, Record.Id, Relationship.IntersectionName, RelatedEntityLogicalName)
                                   .Values
                                   .ToList()
                           : GenericManager.RetrieveAll(RelatedEntityLogicalName)
                                   .Values.Where(k => ((EntityReferenceValue)k.Values[Relationship.RelatedAttribute]).Id == Record.Id)
                                   .ToList();

                _view.SetRecords(new HierarchyDrmRecordCollectionInputData()
                {
                    ContextEntity = RelatedEntityLogicalName,
                    GenericManager = GenericManager,
                    ParentContextEntity = ContextEntity,
                    ParentContextEntityId = Record.Id,
                    TargetEntityLogicalName = TargetEntityLogicalName,
                    Records = records

                });
                _isLoaded = true;
            }
        }

    }
}
