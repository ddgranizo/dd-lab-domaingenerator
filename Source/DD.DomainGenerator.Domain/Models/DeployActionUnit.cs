using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.DomainGenerator.Models
{
    public class DeployActionUnit
    {
        public enum DeployState
        {
            NotInitiated = 1,
            CheckingCurrentState = 2,
            QueuedForExecution = 3,
            Executing = 4,
            Completed = 5,
            Error = 6,
        }

        public struct Positions
        {
            public const int First = 1;
            public const int Second = 2;
            public const int Third = 3;
            public const int Forth = 4;
            public const int BeforeLast = 99;
            public const int Last = 100;
        }

        public ActionExecution ActionExecution { get; set; }
        public DeployState State { get; set; }
        public DeployManager.Phases StartFromPhase { get; set; }
        public int StartFromLine { get; set; }
        public int StartFromPosition { get; set; }
        public string Name { get; set; }
        public string Description { get; }
        public Dictionary<string, object> ResponseParameters { get; set; }
        public Exception Exception { get; set; }

        public DeployActionUnit()
        {
            State = DeployState.NotInitiated;
        }

        public DeployActionUnit(
            ActionExecution actionExecution,
            string name,
            string description,
            DeployManager.Phases startFromPhase,
            int startFromLine = 1,
            int startFromPosition = 1) : this()
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("message", nameof(description));
            }
            Name = name;
            StartFromPhase = startFromPhase;
            StartFromLine = startFromLine;
            StartFromPosition = startFromPosition;
            ActionExecution = actionExecution;
            Description = description;
        }

        public void SetResponseParameters(Dictionary<string, object> responseParameters)
        {
            ResponseParameters = responseParameters;
        }
        public void SetResponseException(Exception exception)
        {
            Exception = exception;
        }

        public virtual DeployActionUnitResponse ExecuteDeploy(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            throw new NotImplementedException();
        }

        public virtual DeployActionUnitResponse ExecuteCheck(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            throw new NotImplementedException();
        }


        internal T GetDependency<T>(ActionExecution sourceActionExecution, List<DeployActionUnit> currentExecutionDeployActions)
        {
            var sameSourceDeployActions = currentExecutionDeployActions
                    .Where(k => k.ActionExecution.Id == sourceActionExecution.Id && k.State == DeployState.Completed);
            var dependency = sameSourceDeployActions
                .OfType<T>();
            if (dependency.Count() == 0)
            {
                throw new Exception($"Can't find any '{typeof(T).Name}' deploy action required before this action");
            }
            return dependency.First();
        }
    }
}
