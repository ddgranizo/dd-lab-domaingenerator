using DD.Lab.Wpf.Drm.Controls.Basics;
using DD.Lab.Wpf.Drm.Inputs;
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
    public class HierarchyDrmRecordsCollectionViewModel : BaseViewModel
    {

        public HierarchyDrmRecordCollectionInputData HierarchyDrmEntityCollectionInputData { get { return GetValue<HierarchyDrmRecordCollectionInputData>(); } set { SetValue(value, UpdatedHierarchyDrmEntityCollectionInputData); } }

        public List<DataRecord> Records { get { return GetValue<List<DataRecord>>(); } set { SetValue(value); UpdateListToCollection(value, RecordsCollection); } }
        public ObservableCollection<DataRecord> RecordsCollection { get; set; } = new ObservableCollection<DataRecord>();
        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }
        public string ContextEntity { get { return GetValue<string>(); } set { SetValue(value); } }
        public string ParentContextEntity { get { return GetValue<string>(); } set { SetValue(value); } }
        public Guid ParentContextEntityId { get { return GetValue<Guid>(); } set { SetValue(value); } }
        public string TargetEntityLogicalName { get { return GetValue<string>(); } set { SetValue(value); } }


        private HierarchyDrmRecordsCollectionView _view;

        public HierarchyDrmRecordsCollectionViewModel()
        {

        }

        public void Initialize(HierarchyDrmRecordsCollectionView v)
        {
			_view = v;
        }

        public void UpdatedHierarchyDrmEntityCollectionInputData(HierarchyDrmRecordCollectionInputData data)
        {
            Records = null;
            GenericManager = null;
            ParentContextEntityId = Guid.Empty;
            ParentContextEntity = null;
            TargetEntityLogicalName = null;
            ContextEntity = null;
            if (data != null)
            {
                GenericManager = data.GenericManager;
                Records = data.Records;
                ParentContextEntityId = data.ParentContextEntityId;
                ParentContextEntity = data.ParentContextEntity;
                TargetEntityLogicalName = data.TargetEntityLogicalName;
                ContextEntity = data.ContextEntity;
            }
        }

    }
}
