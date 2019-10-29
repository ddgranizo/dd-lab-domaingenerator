using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class DomainInMicroServiceModel: BaseModel
    {
        public DomainModel Domain { get { return GetValue<DomainModel>(); } set { SetValue(value); } }
        public MicroServiceModel MicroService { get { return GetValue<MicroServiceModel>(); } set { SetValue(value); } }
    }
}
