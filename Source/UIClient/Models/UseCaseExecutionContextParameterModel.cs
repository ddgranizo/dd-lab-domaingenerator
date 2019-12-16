using UIClient.Models.Base;
using UIClient.Models.Sentences.Base;
using static DD.DomainGenerator.Models.UseCaseExecutionContextParameter;

namespace UIClient.Models
{
    public class UseCaseExecutionContextParameterModel : BaseModel
    {
  
        public ParameterDirection Direction { get { return GetValue<ParameterDirection>(); } set { SetValue(value); } }
        public DataParameterModel Parameter { get { return GetValue<DataParameterModel>(); } set { SetValue(value); } }
        public ExecutionSentenceBaseModel Source { get { return GetValue<ExecutionSentenceBaseModel>(); } set { SetValue(value); } }
        public object ConstantValue { get { return GetValue<object>(); } set { SetValue(value); } }
        public bool IsFromUseCase { get { return GetValue<bool>(); } set { SetValue(value); } }

    }
}