using DD.DomainGenerator.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class UseCaseExecutionSentence
    {
        public ExecutionSentenceBase ExecutionSentence { get; set; }
        public List<UseCaseLinkInputExecutionParameter> ContextInputParameters { get; set; }
        public List<UseCaseLinkExecutionParameter> ContextLinkParameters { get; set; }
        public List<UseCaseLinkOutputExecutionParameter> ContextOutputParameters { get; set; }

        public UseCaseExecutionSentence(
            ExecutionSentenceBase executionSentence,
            List<UseCaseLinkInputExecutionParameter> contextInputParameters,
            List<UseCaseLinkExecutionParameter> contextParameters)
        {
            ExecutionSentence = executionSentence 
                ?? throw new ArgumentNullException(nameof(executionSentence));
            ContextInputParameters = contextInputParameters ?? throw new ArgumentNullException(nameof(contextInputParameters));
            ContextLinkParameters = contextParameters
                ?? throw new ArgumentNullException(nameof(contextParameters));
        }


    }
}
