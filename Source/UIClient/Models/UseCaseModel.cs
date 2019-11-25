using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Models.UseCase;

namespace UIClient.Models
{
    public class UseCaseModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public UseCaseTypes Type { get { return GetValue<UseCaseTypes>(); } set { SetValue(value); } }
        public bool IsCustom { get { return GetValue<bool>(); } set { SetValue(value); } }
        public SchemaModel Schema { get { return GetValue<SchemaModel>(); } set { SetValue(value); } }

        public string DisplayName { get { return GetValue<string>(); } set { SetValue(value); } }
        public string Description { get { return GetValue<string>(); } set { SetValue(value); } }
        public ExecutionSequenceModel ExecutionSequence { get { return GetValue<ExecutionSequenceModel>(); } set { SetValue(value); } }

        public List<UseCaseParameterModel> InputParameters { get { return GetValue<List<UseCaseParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, InputParametersCollection); } }
        [JsonIgnore]
        public ObservableCollection<UseCaseParameterModel> InputParametersCollection { get; set; } = new ObservableCollection<UseCaseParameterModel>();

        public List<UseCaseParameterModel> OutputParameters { get { return GetValue<List<UseCaseParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, OutputParametersCollection); } }
        [JsonIgnore]
        public ObservableCollection<UseCaseParameterModel> OutputParametersCollection { get; set; } = new ObservableCollection<UseCaseParameterModel>();

    }
}
