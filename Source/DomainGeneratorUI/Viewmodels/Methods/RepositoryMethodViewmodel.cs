using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Viewmodels.Methods
{
    public class RepositoryMethodViewmodel : BaseViewModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public EntityReferenceValue RepositoryId { get { return GetValue<EntityReferenceValue>(); } set { SetValue(value); } }
        public OptionSetValue Type { get { return GetValue<OptionSetValue>(); } set { SetValue(value); } }
        public string Content { get { return GetValue<string>(); } set { SetValue(value); } }
    }
}
