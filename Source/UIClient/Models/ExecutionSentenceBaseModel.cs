using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class ExecutionSentenceBaseModel : BaseModel
    {

        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public string Description { get { return GetValue<string>(); } set { SetValue(value); } }

    }
}
