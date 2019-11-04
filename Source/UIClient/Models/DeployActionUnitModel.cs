using DD.DomainGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Models.DeployActionUnit;

namespace UIClient.Models
{
    public class DeployActionUnitModel: BaseModel
    {

        public ActionExecutionModel ActionExecution { get { return GetValue<ActionExecutionModel>(); } set { SetValue(value); } }

        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public string Description { get { return GetValue<string>(); } set { SetValue(value); } }
        public DeployManager.Phases StartFromPhase { get { return GetValue<DeployManager.Phases>(); } set { SetValue(value); } }
        public int StartFromLine { get { return GetValue<int>(); } set { SetValue(value); } }
        public int StartFromPosition { get { return GetValue<int>(); } set { SetValue(value); } }

        public Dictionary<string, object> ResponseParameters { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }
        public Exception Exception { get { return GetValue<Exception>(); } set { SetValue(value); } }
        public DeployState State { get { return GetValue<DeployState>(); } set { SetValue(value); } }
    }
}
