using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class ErrorExecutionActionModel: BaseModel
    {
        public Exception Exception { get { return GetValue<Exception>(); } set { SetValue(value); } }
        public ActionBaseModel Action { get { return GetValue<ActionBaseModel>(); } set { SetValue(value); } }
    }
}
