
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class DomainModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }

        public List<SchemaModel> Schemas { get { return GetValue<List<SchemaModel>>(); } set { SetValue(value); UpdateListToCollection(value, SchemasCollection); } }
        public ObservableCollection<SchemaModel> SchemasCollection { get; set; } = new ObservableCollection<SchemaModel>();

    }
}
