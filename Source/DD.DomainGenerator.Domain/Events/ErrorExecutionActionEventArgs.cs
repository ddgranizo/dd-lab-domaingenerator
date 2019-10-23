using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Events
{
    public class ErrorExecutionActionEventArgs : EventArgs
    {
        public ActionBase Action { get; set; }
        public Exception Exception { get; }

        public ErrorExecutionActionEventArgs(ActionBase action, Exception ex)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Exception = ex ?? throw new ArgumentNullException(nameof(ex));
        }
    }
}
