using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class DomainModel : BaseModel
    {
        public bool IsRootDomain { get { return GetValue<bool>(); } set { SetValue(value); } }
        public string Namespace { get { return GetValue<string>(); } set { SetValue(value); } }
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }

        

        
    }
}
