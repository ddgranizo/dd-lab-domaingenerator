using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;
using UIClient.ViewModels.Base;
using static DD.DomainGenerator.Models.ActionExecution;

namespace UIClient.Models
{
    public class ActionExecutionModel : BaseModel
    {
        public Guid Id { get { return GetValue<Guid>(); } set { SetValue(value); } }
        public string ActionName { get { return GetValue<string>(); } set { SetValue(value); } }
        public ActionExecutionState State { get { return GetValue<ActionExecutionState>(); } set { SetValue(value); } }
        public Dictionary<string, object> InputParameters { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }
        public Dictionary<string, object> OutputParameters { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }
    }
}
