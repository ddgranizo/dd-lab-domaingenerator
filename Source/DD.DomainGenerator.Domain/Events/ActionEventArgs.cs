using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Events
{
    public class ActionEventArgs : EventArgs
    {
        public ActionBase Action { get; set; }
        public List<ActionParameter> ActionParameters { get; }
        public Dictionary<string, object> Parameters { get; set; }
        public ActionEventArgs(ActionBase action, List<ActionParameter> actionParameters)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            ActionParameters = actionParameters ?? throw new ArgumentNullException(nameof(actionParameters));
        }
    }
}
