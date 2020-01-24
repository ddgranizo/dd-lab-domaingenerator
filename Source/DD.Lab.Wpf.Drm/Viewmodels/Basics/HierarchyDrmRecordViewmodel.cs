using DD.Lab.Wpf.Drm.Controls.Basics;
using DD.Lab.Wpf.Drm.Inputs;
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Models.Data;
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
    public class HierarchyDrmRecordViewModel : BaseViewModel
    {
        public HierarchyDrmRecordInputData HierarchyRecordInputData { get { return GetValue<HierarchyDrmRecordInputData>(); } set { SetValue(value, UpdatedHierarchyRecordInputData); } }

        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }
        public DataRecord Record { get { return GetValue<DataRecord>(); } set { SetValue(value); } }
        public string ParentContextEntity { get { return GetValue<string>(); } set { SetValue(value); } }
        public Guid ParentContextEntityId { get { return GetValue<Guid>(); } set { SetValue(value); } }
        public string TargetEntityLogicalName { get { return GetValue<string>(); } set { SetValue(value); } }
        public string ContextEntity { get { return GetValue<string>(); } set { SetValue(value); } }

        public List<Relationship> Relationships { get { return GetValue<List<Relationship>>(); } set { SetValue(value); UpdateListToCollection(value, RelationshipsCollection); } }
        public ObservableCollection<Relationship> RelationshipsCollection { get; set; } = new ObservableCollection<Relationship>();


        private HierarchyDrmRecordView _view;

        public HierarchyDrmRecordViewModel()
        {

        }

        public void Initialize(HierarchyDrmRecordView v)
        {
            _view = v;
        }


        private void UpdatedHierarchyRecordInputData(HierarchyDrmRecordInputData data)
        {
            GenericManager = null;
            Record = null;
            ParentContextEntity = null;
            ParentContextEntityId = Guid.Empty;
            TargetEntityLogicalName = null;
            ContextEntity = null;

            if (data != null)
            {
                GenericManager = data.GenericManager;
                Record = data.Record;
                ParentContextEntity = data.ParentContextEntity;
                ParentContextEntityId = data.ParentContextEntityId;
                TargetEntityLogicalName = data.TargetEntityLogicalName;
                ContextEntity = data.ContextEntity;
                Relationships = GenericManager.Model.Relationships
                    .Where(k => !k.IsManyToMany && k.MainEntity == ContextEntity
                              || k.IsManyToMany && (k.RelatedEntity == ContextEntity || k.MainEntity == ContextEntity))
                    .ToList();
            }
        }
    }
}
