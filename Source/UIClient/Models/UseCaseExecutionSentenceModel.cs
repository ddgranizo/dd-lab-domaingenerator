using System.Collections.Generic;
using System.Collections.ObjectModel;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class UseCaseExecutionSentenceModel : BaseModel
    {
        public ExecutionSentenceBaseModel ExecutionSentence { get { return GetValue<ExecutionSentenceBaseModel>(); } set { SetValue(value); } }


        public List<UseCaseLinkInputExecutionParameterModel> ContextInputParameters { get { return GetValue<List<UseCaseLinkInputExecutionParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, ContextInputParametersCollection); } }
        public ObservableCollection<UseCaseLinkInputExecutionParameterModel> ContextInputParametersCollection { get; set; } = new ObservableCollection<UseCaseLinkInputExecutionParameterModel>();

        public List<UseCaseLinkExecutionParameterModel> ContextLinkParameters { get { return GetValue<List<UseCaseLinkExecutionParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, ContextLinkParametersCollection); } }
        public ObservableCollection<UseCaseLinkExecutionParameterModel> ContextLinkParametersCollection { get; set; } = new ObservableCollection<UseCaseLinkExecutionParameterModel>();

        public List<UseCaseLinkOutputExecutionParameterModel> ContextOutputParameters { get { return GetValue<List<UseCaseLinkOutputExecutionParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, ContextOutputParametersCollection); } }
        public ObservableCollection<UseCaseLinkOutputExecutionParameterModel> ContextOutputParametersCollection { get; set; } = new ObservableCollection<UseCaseLinkOutputExecutionParameterModel>();

    }
}