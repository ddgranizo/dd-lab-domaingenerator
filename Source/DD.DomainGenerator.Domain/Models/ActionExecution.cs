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
            NoQueued = 1,
            Queued = 2,
            Executing = 3,
            Executed = 4,
        }

        public Guid Id { get; set; }
        public ActionExecutionState State { get; set; }
        public string ActionName { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public List<ActionParameter> ActionParameters { get; set; }

        public ActionExecution(string actionName, Dictionary<string, object> parameters)
        {
            Id = Guid.NewGuid();
            ActionName = actionName ?? throw new ArgumentNullException(nameof(actionName));
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            State = ActionExecutionState.Queued;
        }
    }
}
