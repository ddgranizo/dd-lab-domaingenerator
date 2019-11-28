using UIClient.Models.Base;

namespace UIClient.Models
{
    public class UseCaseLinkOutputExecutionParameterModel : BaseModel
    {

        public UseCaseExecutionContextParameterModel Source { get { return GetValue<UseCaseExecutionContextParameterModel>(); } set { SetValue(value); } }
        public DataParameterModel Destination { get { return GetValue<DataParameterModel>(); } set { SetValue(value); } }

    }
}