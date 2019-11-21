using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class ExecutionSentence
    {
        public ExecutionSentence()
        {
            AvailableContextParameters = new List<ExecutionContextParameter>();
            PublishedContextParameters = new List<ExecutionContextParameter>();
            UsedContextParameters = new List<ExecutionContextParameter>();
        }

        public List<ExecutionContextParameter> AvailableContextParameters { get; set; }
        public List<ExecutionContextParameter> UsedContextParameters { get; set; }
        public List<ExecutionContextParameter> PublishedContextParameters { get; set; }

    }
}
