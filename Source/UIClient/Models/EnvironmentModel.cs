using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class EnvironmentModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public string ShortName { get { return GetValue<string>(); } set { SetValue(value); } }
        public int Order { get { return GetValue<int>(); } set { SetValue(value); } }

    }
}
