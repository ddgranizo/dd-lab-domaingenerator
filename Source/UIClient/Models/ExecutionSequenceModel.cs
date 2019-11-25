using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class ExecutionSequenceModel : BaseModel
    {
        public List<ExecutionSentenceExecutionModel> ExecutionSentences { get { return GetValue<List<ExecutionSentenceExecutionModel>>(); } set { SetValue(value); UpdateListToCollection(value, ExecutionSentencesCollection); } }
        public ObservableCollection<ExecutionSentenceExecutionModel> ExecutionSentencesCollection { get; set; } = new ObservableCollection<ExecutionSentenceExecutionModel>();
    }
}
