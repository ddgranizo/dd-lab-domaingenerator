using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class RepositoryMethodModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool IsCustom { get { return GetValue<bool>(); } set { SetValue(value); } }

        public List<DataParameterModel> InputParameters { get { return GetValue<List<DataParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, InputParametersCollection); } }
        public ObservableCollection<DataParameterModel> InputParametersCollection { get; set; } = new ObservableCollection<DataParameterModel>();

        public List<DataParameterModel> OutputParameters { get { return GetValue<List<DataParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, OutputParametersCollection); } }
        public ObservableCollection<DataParameterModel> OutputParametersCollection { get; set; } = new ObservableCollection<DataParameterModel>();
    }
}
