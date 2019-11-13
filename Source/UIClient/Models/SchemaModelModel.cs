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

        public List<SchemaModelPropertyModel> Properties { get { return GetValue<List<SchemaModelPropertyModel>>(); } set { SetValue(value); UpdateListToCollection(value, PropertiesCollection); } }
        public ObservableCollection<SchemaModelPropertyModel> PropertiesCollection { get; set; } = new ObservableCollection<SchemaModelPropertyModel>();

        public List<UseCaseModel> UseCases { get { return GetValue<List<UseCaseModel>>(); } set { SetValue(value); UpdateListToCollection(value, UseCasesCollection); } }
        public ObservableCollection<UseCaseModel> UseCasesCollection { get; set; } = new ObservableCollection<UseCaseModel>();

        public bool NeedsAuthorization { get { return GetValue<bool>(); } set { SetValue(value); } }

        public List<SchemaViewModel> Views { get { return GetValue<List<SchemaViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, ViewsCollection); } }
        public ObservableCollection<SchemaViewModel> ViewsCollection { get; set; } = new ObservableCollection<SchemaViewModel>();
    }
}
