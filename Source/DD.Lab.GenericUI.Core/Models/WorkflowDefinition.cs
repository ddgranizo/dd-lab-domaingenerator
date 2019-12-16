using DD.Lab.GenericUI.Core.Models.Workflows;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.GenericUI.Core.Models
{
    public class WorkflowDefinition
    {
        public string EntityLogicalName { get; set; }
        public int Order { get; set; }
        public Action<WorkflowInputParameter> Action { get; }

        public WorkflowDefinition(string entityLogicalName, int order, Action<WorkflowInputParameter> action)
        {
            if (string.IsNullOrEmpty(entityLogicalName))
            {
                throw new ArgumentException("message", nameof(entityLogicalName));
            }

            EntityLogicalName = entityLogicalName;
            Order = order;
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }
    }
}
