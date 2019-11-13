using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class SchemaViewModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool IsCustom { get { return GetValue<bool>(); } set { SetValue(value); } }

        public List<ViewParameterModel> Parameters { get { return GetValue<List<ViewParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, ParametersCollection); } }
        public ObservableCollection<ViewParameterModel> ParametersCollection { get; set; } = new ObservableCollection<ViewParameterModel>();
    }
}
