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
        public bool HasModel { get { return Schema != null; } }

        public List<DomainModel> Domains { get { return GetValue<List<DomainModel>>(); } set { SetValue(value); } }
        public ObservableCollection<DomainModel> DomainsCollection { get; set; }

        public DomainModel ParentDomain { get { return GetValue<DomainModel>(); } set { SetValue(value); } }
        public SchemaModelModel Schema { get { return GetValue<SchemaModelModel>(); } set { SetValue(value); } }

        public List<UseCaseModel> UseCases { get { return GetValue<List<UseCaseModel>>(); } set { SetValue(value); } }
        public ObservableCollection<UseCaseModel> UseCasesCollection { get; set; }
        public bool NeedsAuthorization { get { return GetValue<bool>(); } set { SetValue(value); } }
    }
}
