using UIClient.Models.Base;

namespace UIClient.Models
{
    public class UseCaseLinkExecutionParameterModel : BaseModel
    {
        public UseCaseExecutionContextParameterModel Source { get { return GetValue<UseCaseExecutionContextParameterModel>(); } set { SetValue(value); } }
        public UseCaseExecutionContextParameterModel Destination { get { return GetValue<UseCaseExecutionContextParameterModel>(); } set { SetValue(value); } }

    }
}