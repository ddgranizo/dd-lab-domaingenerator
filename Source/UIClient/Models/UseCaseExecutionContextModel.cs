using System.Collections.Generic;
using System.Collections.ObjectModel;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class UseCaseExecutionContextModel : BaseModel
    {
        public List<UseCaseExecutionContextParameterModel> ContextItems { get { return GetValue<List<UseCaseExecutionContextParameterModel>>(); } set { SetValue(value); UpdateListToCollection(value, ContextItemsCollection); } }
        public ObservableCollection<UseCaseExecutionContextParameterModel> ContextItemsCollection { get; set; } = new ObservableCollection<UseCaseExecutionContextParameterModel>();

    }
}