using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DD.DomainGenerator.Models
{
    public class DeployActionUnit
    {
        public enum DeployState
        {
            NotInitiated = 1,
            Queued = 2,
            Executing = 3,
            Completed = 4,
            Error = 9,
        }

        public string Name { get; }

        public DeployState State { get; set; }
        public DeployManager.Phases StartFromPhase { get; set; }
        public int StartFromLine { get; set; }
        public int StartFromPosition { get; set; }
        public string Description { get; }
        public Func<ActionExecution, ProjectState, DeployActionUnitResponse> Action { get; set; }
        public Dictionary<string, object> ResponseParameters { get; set; }
        public Exception Exception { get; set; }
        public DeployActionUnit()
        {
            State = DeployState.NotInitiated;
        }

        public DeployActionUnit(
            string name,
            DeployManager.Phases startFromPhase,
            int startFromLine,
            int startFromPosition,
            string description,
            Func<ActionExecution, ProjectState, DeployActionUnitResponse> action)
            :this()
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("message", nameof(description));
            }

            Name = name ?? throw new ArgumentNullException(nameof(name));
            StartFromPhase = startFromPhase;
            StartFromLine = startFromLine;
            StartFromPosition = startFromPosition;
            Description = description;
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void SetResponseParameters(Dictionary<string, object> responseParameters)
        {
            ResponseParameters = responseParameters;
        }
        public void SetResponseException(Exception exception)
        {
            Exception = exception;
        }
    }
}
