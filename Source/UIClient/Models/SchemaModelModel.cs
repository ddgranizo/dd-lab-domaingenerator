using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class SchemaModelModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool HasId { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool HasDates { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool HasUserRelationship { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool HasState { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool HasOwner { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsIntersection { get { return GetValue<bool>(); } set { SetValue(value); } }

        public List<SchemaModelPropertyModel> Properties { get { return GetValue<List<SchemaModelPropertyModel>>(); } set { PropertiesCollection = SetCollection(value); } }
        public ObservableCollection<SchemaModelPropertyModel> PropertiesCollection { get; set; }

        public List<UseCaseModel> UseCases { get { return GetValue<List<UseCaseModel>>(); } set { SetValue(value); } }
        public ObservableCollection<UseCaseModel> UseCasesCollection { get; set; }

        public bool NeedsAuthorization { get { return GetValue<bool>(); } set { SetValue(value); } }
    }
}
