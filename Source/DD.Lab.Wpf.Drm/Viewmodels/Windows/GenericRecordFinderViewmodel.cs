using DD.Lab.Wpf.Drm.Inputs;
using DD.Lab.Wpf.Drm.Viewmodels.Basics;
using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DD.Lab.Wpf.Drm.Viewmodels.Windows
{

    public class GenericRecordFinderViewmodel: BaseViewModel
    {
        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value);  } }
        public string MainEntityLogicalName { get; set; }
        public string TargetEntityLogicalName { get; set; }
        public Guid MainEntityId { get; set; }

        public HierarchyDrmRecordCollectionInputData HierarchyRecordCollection { get { return GetValue<HierarchyDrmRecordCollectionInputData>(); } set { SetValue(value); } }

        public GenericRecordFinderViewmodel()
        {

        }

        private GenericRecordFinderWindow _view;
        public void Initialize(
            GenericRecordFinderWindow view,
            GenericManager manager,
            string mainEntityLogicalName,
            Guid mainEntityId,
            string targetEntityLogicalName)
        {
            
            if (string.IsNullOrEmpty(mainEntityLogicalName))
            {
                throw new ArgumentException("message", nameof(mainEntityLogicalName));
            }

            if (string.IsNullOrEmpty(targetEntityLogicalName))
            {
                throw new ArgumentException("message", nameof(targetEntityLogicalName));
            }
            MainEntityId = mainEntityId;
            _view = view ?? throw new ArgumentNullException(nameof(view));
            GenericManager = manager ?? throw new ArgumentNullException(nameof(manager));
            MainEntityLogicalName = mainEntityLogicalName;
            TargetEntityLogicalName = targetEntityLogicalName;


            if (mainEntityId == Guid.Empty)
            {
                // Show first record list (grid)
                HierarchyRecordCollection = new HierarchyDrmRecordCollectionInputData()
                {
                    GenericManager = GenericManager,
                    TargetEntityLogicalName = TargetEntityLogicalName,
                    Records = GenericManager.RetrieveAll(MainEntityLogicalName).Values,
                    ParentContextEntity = null,
                    ParentContextEntityId = Guid.Empty,
                    ContextEntity = MainEntityLogicalName,
                };
            }
            else
            {
                // Show first record 
            }

        }

    }
}
