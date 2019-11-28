using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class UseCaseExecutionModel : BaseModel
    {

        public UseCaseModel UseCase { get { return GetValue<UseCaseModel>(); } set { SetValue(value); } }
        public bool IsRootExecutionSentence { get { return GetValue<bool>(); } set { SetValue(value); } }
        public UseCaseExecutionContextModel ExecutionContext { get { return GetValue<UseCaseExecutionContextModel>(); } set { SetValue(value); } }

        public List<UseCaseExecutionSentenceModel> ExecutionSentences { get { return GetValue<List<UseCaseExecutionSentenceModel>>(); } set { SetValue(value); UpdateListToCollection(value, ExecutionSentencesCollection); } }
        public ObservableCollection<UseCaseExecutionSentenceModel> ExecutionSentencesCollection { get; set; } = new ObservableCollection<UseCaseExecutionSentenceModel>();

        public UseCaseLinkOutputExecutionParameterModel OutputParameter { get { return GetValue<UseCaseLinkOutputExecutionParameterModel>(); } set { SetValue(value); } }
    }
}
