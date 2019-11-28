using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Sentences.Base.ExecutionSentenceBase;

namespace UIClient.Models
{
    public class ExecutionSentenceBaseModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public string Description { get { return GetValue<string>(); } set { SetValue(value); } }
        public ExecutionSentenceType Type { get { return GetValue<ExecutionSentenceType>(); } set { SetValue(value); } }

        public List<UseCaseExecutionContextParameterModel> InputContextParameters { get { return GetValue<List<UseCaseExecutionContextParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, InputContextParametersCollection); } }
        public ObservableCollection<UseCaseExecutionContextParameterModel> InputContextParametersCollection { get; set; } = new ObservableCollection<UseCaseExecutionContextParameterModel>();

        public List<UseCaseExecutionContextParameterModel> OutputContextParameters { get { return GetValue<List<UseCaseExecutionContextParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, OutputContextParametersCollection); } }
        public ObservableCollection<UseCaseExecutionContextParameterModel> OutputContextParametersCollection { get; set; } = new ObservableCollection<UseCaseExecutionContextParameterModel>();

    }
}
