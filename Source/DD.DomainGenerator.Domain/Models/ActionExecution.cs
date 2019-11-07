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
        public Dictionary<string, object> InputParameters { get; set; }
        public Dictionary<string, object> OutputParameters { get; set; }
        public List<ActionParameter> ActionParameters { get; set; }

        public ActionExecution()
        {

        }

        public ActionExecution(string actionName, Dictionary<string, object> parameters)
        {
            Id = Guid.NewGuid();
            ActionName = actionName ?? throw new ArgumentNullException(nameof(actionName));
            InputParameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            OutputParameters = new Dictionary<string, object>();
            State = ActionExecutionState.NoQueued;
        }
    }
}
