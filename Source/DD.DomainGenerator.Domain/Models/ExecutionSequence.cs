using DD.DomainGenerator.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class ExecutionSequence
    {
        public List<ExecutionSentenceExecution> ExecutionSentences { get; set; }
    }
}
