using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class MicroServiceModel: BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }

        
    }
}
