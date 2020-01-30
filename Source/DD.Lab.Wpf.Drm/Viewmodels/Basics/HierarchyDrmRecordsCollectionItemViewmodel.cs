using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Drm.Controls.Basics;
using DD.Lab.Wpf.Drm.Inputs;
using DD.Lab.Wpf.Drm.Models.Data;
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


namespace HierarchyDrmRecordsCollectionItem
{
    public class HierarchyDrmRecordsCollectionItemViewModel : BaseViewModel
    {
        public DataRecord Record { get { return GetValue<DataRecord>(); } set { SetValue(value); } }
        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }
        public string ContextEntity { get { return GetValue<string>(); } set { SetValue(value, UpdatedContextEntity); } }
        public string ParentContextEntity { get { return GetValue<string>(); } set { SetValue(value); } }
        public Guid ParentContextEntityId { get { return GetValue<Guid>(); } set { SetValue(value); } }
        public string TargetEntityLogicalName { get { return GetValue<string>(); } set { SetValue(value, UpdatedTargetEntityLogicalName); } }



        public bool IsOpen { get { return GetValue<bool>(); } set { SetValue(value, UpdatedIsOpen); } }
        private bool _alreadyLoaded = false;


        public bool IsTargetEntity { get { return GetValue<bool>(); } set { SetValue(value); } }



        private HierarchyDrmRecordsCollectionItemView _view;

        public HierarchyDrmRecordsCollectionItemViewModel()
        {
            
        }

        public void Initialize(HierarchyDrmRecordsCollectionItemView v)
        {
            _view = v;
        }

        private void UpdatedContextEntity(string data)
        {
            CheckIfTargetEntity();
        }

        private void UpdatedTargetEntityLogicalName(string data)
        {
            CheckIfTargetEntity();
        }

        private void CheckIfTargetEntity()
        {
            if (!string.IsNullOrEmpty(TargetEntityLogicalName) 
                && !string.IsNullOrEmpty(ContextEntity))
            {
                if (TargetEntityLogicalName == ContextEntity)
                {
                    IsTargetEntity = true;
                }
            }
        }

        private void UpdatedIsOpen(bool isOpen)
        {
            if (isOpen && !_alreadyLoaded)
            {
                _alreadyLoaded = true;
                _view.SetViewRecordData(new HierarchyDrmRecordInputData()
                {
                    GenericManager = GenericManager,
                    ParentContextEntity = ParentContextEntity,
                    ParentContextEntityId = ParentContextEntityId,
                    TargetEntityLogicalName = TargetEntityLogicalName,
                    Record = Record,
                    ContextEntity = ContextEntity,
                    ContextEntityId = Record.Id,
                });
            }
        }

    }
}
