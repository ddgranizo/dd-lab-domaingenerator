using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class AzurePipelineSettingModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public string OrganizationUri { get { return GetValue<string>(); } set { SetValue(value); } }
        public string Token { get { return GetValue<string>(); } set { SetValue(value); } }
        public Guid ProjectId { get { return GetValue<Guid>(); } set { SetValue(value); } }
    }
}
