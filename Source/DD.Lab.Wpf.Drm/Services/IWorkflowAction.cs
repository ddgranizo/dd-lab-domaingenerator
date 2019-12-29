using DD.Lab.Wpf.Drm.Models.Workflows;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Services
{
    public interface IWorkflowAction
    {
        void Execute(GenericManager manager, WorkflowInputParameter inputParameter);
            
    }
}
