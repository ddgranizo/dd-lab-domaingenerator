using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class ServiceModel : BaseModel
    {
        public List<DomainModel> Domains { get { return GetValue<List<DomainModel>>(); } set { DomainsCollection = SetCollection(value); } }
        public ObservableCollection<DomainModel> DomainsCollection { get; set; }
    }
}
