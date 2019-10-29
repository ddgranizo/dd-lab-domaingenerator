using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class DeployActionUnitResponse
    {
        public Exception Exception { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, object> Parameters { get; set; }

        public DeployActionUnitResponse()
        {
            IsError = false;
        }
        
        public void Ok()
        {
            IsError = false;
        }

        public DeployActionUnitResponse Ok(Dictionary<string, object> parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            Parameters = parameters;
            IsError = false;
            return this;
        }

        public void Error(Exception exception)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
            ErrorMessage = exception.Message;
        }

        public void Error(string error)
        {
            ErrorMessage = error;
        }
    }
}
