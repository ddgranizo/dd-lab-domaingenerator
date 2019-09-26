using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;
using UIClient.ViewModels.Base;

namespace UIClient.Models
{
    public class ArchitectureSetupModel : BaseModel
    {
        public List<EnvironmentModel> Environments { get { return GetValue<List<EnvironmentModel>>(); } set { EnvironmentsCollection = SetCollection(value); } }
        public ObservableCollection<EnvironmentModel> EnvironmentsCollection { get; set; }

        public List<ServiceModel> Services { get { return GetValue<List<ServiceModel>>(); } set { ServicesCollection = SetCollection(value); } }
        public ObservableCollection<ServiceModel> ServicesCollection { get; set; }
    }
}
