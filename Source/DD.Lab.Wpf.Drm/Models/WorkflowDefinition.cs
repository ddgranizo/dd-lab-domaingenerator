using DD.Lab.Wpf.Drm.Models.Workflows;
using DD.Lab.Wpf.Drm;
using System;
using System.Collections.Generic;
using System.Text;
using DD.Lab.Wpf.Drm.Services;

namespace DD.Lab.Wpf.Drm.Models
{
    public class WorkflowDefinition
    {
        public string EntityLogicalName { get; set; }
        public int Order { get; set; }
        public IWorkflowAction Action { get; }

        public WorkflowDefinition(string entityLogicalName, int order, IWorkflowAction action)
        {
            if (string.IsNullOrEmpty(entityLogicalName))
            {
                throw new ArgumentException("message", nameof(entityLogicalName));
            }

            EntityLogicalName = entityLogicalName;
            Order = order;
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public WorkflowDefinition(string entityLogicalName, IWorkflowAction action)
            : this(entityLogicalName, 0, action)
        {

        }
    }
}
