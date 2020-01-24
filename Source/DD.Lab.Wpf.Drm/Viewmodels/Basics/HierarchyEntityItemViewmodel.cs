using DD.Lab.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Viewmodels.Basics
{
    public class HierarchyEntityItemViewmodel : BaseViewModel
    {
        public Guid Id { get { return GetValue<Guid>(); } set { SetValue(value); } }
        //public string DisplayName { get { return GetValue<string>(); } set { SetValue(value); } }
        //public string EntityLogicalName { get { return GetValue<string>(); } set { SetValue(value); } }
        public string DisplayName { get { return GetValue<string>(); } set { SetValue(value); } }
        public string EntityLogicalName { get { return GetValue<string>(); } set { SetValue(value); } }

    }
}
