using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class ActionBaseModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public string Description { get { return GetValue<string>(); } set { SetValue(value); } }

        //public List<DeployActionUnitModel> DeployActions { get { return GetValue<List<DeployActionUnitModel>>(); } set { SetValue(value); UpdateListToCollection(value, DeployActionsCollection); } }
        //public ObservableCollection<DeployActionUnitModel> DeployActionsCollection { get; set; } = new ObservableCollection<DeployActionUnitModel>();

    }
}
