using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models.Workflows
{
    public class WorkflowInputParameter
    {
        public WorkflowInputParameter()
        {
            Values = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Values { get; set; }


    }
}
