using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class DeployActionUnitResponse
    {
        public enum DeployActionResponseType
        {
            AlreadyCompletedJob = 1,
            NotCompletedJob = 2,
            ErrorChekingIfJobCompleted = 3,
        }
        public Exception Exception { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public int MyProperty { get; set; }
        public DeployActionResponseType ResponseType { get; set; }

        public DeployActionUnitResponse()
        {
            IsError = false;
        }

        public DeployActionUnitResponse Ok()
        {
            IsError = false;
            return this;
        }

        public DeployActionUnitResponse Ok(DeployActionResponseType deployActionResponseType)
        {
            ResponseType = deployActionResponseType;
            IsError = false;
            return this;
        }

        public DeployActionUnitResponse Ok(Dictionary<string, object> parameters)
        {
            Parameters = parameters;
            IsError = false;
            return this;
        }
        public DeployActionUnitResponse Ok(Dictionary<string, object> parameters, DeployActionResponseType deployActionResponseType)
        {
            ResponseType = deployActionResponseType;
            Parameters = parameters;
            IsError = false;
            return this;
        }


        public DeployActionUnitResponse Error(Exception exception)
        {
            IsError = true;
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
            ErrorMessage = exception.Message;
            ResponseType = DeployActionResponseType.ErrorChekingIfJobCompleted;
            return this;
        }

        public DeployActionUnitResponse Error(string error)
        {
            IsError = true;
            ErrorMessage = error;
            return this;
        }

        public DeployActionUnitResponse Error()
        {
            IsError = true;
            return this;
        }
    }
}
