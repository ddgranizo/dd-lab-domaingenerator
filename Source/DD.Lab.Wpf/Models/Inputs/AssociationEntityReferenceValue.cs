using DD.Lab.Wpf.Models.Base;
using DD.Lab.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Models.Inputs
{
    public class AssociationEntityReferenceValue : BaseViewModel
    {
        public EntityReferenceValue Value { get { return GetValue<EntityReferenceValue>(); } set { SetValue(value); } }
        public bool IsSelected { get { return GetValue<bool>(); } set { SetValue(value); } }
    }
}
