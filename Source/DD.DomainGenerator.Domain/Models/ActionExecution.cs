using DD.DomainGenerator.Actions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class ActionExecution
    {
        public enum ActionExecutionState
        {
            Queued = 1,
            Executing = 2,
            Executed = 3,
        }


        public ActionExecutionState State { get; set; }
        public string ActionName { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public List<ActionParameter> ActionParameters { get; set; }

        public ActionExecution(string actionName, Dictionary<string, object> parameters)
        {
            if (string.IsNullOrEmpty(actionName))
            {
                throw new ArgumentException("message", nameof(actionName));
            }
            ActionName = actionName;
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            State = ActionExecutionState.Queued;
        }


    }
}
