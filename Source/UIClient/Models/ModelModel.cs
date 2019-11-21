using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class ModelModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool IsCustom { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsMainModel { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool AllProperties { get { return GetValue<bool>(); } set { SetValue(value); } }

        public List<string> Properties { get { return GetValue<List<string>>(); } set { SetValue(value); UpdateListToCollection(value, PropertiesCollection); } }
        public ObservableCollection<string> PropertiesCollection { get; set; } = new ObservableCollection<string>();

    }
}
