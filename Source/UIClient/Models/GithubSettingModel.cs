using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class GithubSettingModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public string OauthToken { get { return GetValue<string>(); } set { SetValue(value); } }
        public string Uri { get { return GetValue<string>(); } set { SetValue(value); } }
    }
}
