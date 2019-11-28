using DD.DomainGenerator.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class UseCaseExecution
    {
        public UseCase UseCase { get; }
        public bool IsRootExecutionSentence { get; set; }

        public UseCaseExecutionContext ExecutionContext { get; set; }
        public List<UseCaseExecutionSentence> ExecutionSentences { get; set; }

        public UseCaseLinkOutputExecutionParameter OutputParameter { get; set; }

        public UseCaseExecution(UseCase useCase, bool isRootExecutionSentence = false)
        {
            ExecutionContext = new UseCaseExecutionContext();
            ExecutionSentences = new List<UseCaseExecutionSentence>();
            UseCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
            IsRootExecutionSentence = isRootExecutionSentence;
        }

        public void RecalculateContext()
        {
            ExecutionContext.CleanContextItems();
            foreach (var item in UseCase.OutputParameters)
            {
                ExecutionContext.AddContextParameter(
                    new UseCaseExecutionContextParameter(UseCaseExecutionContextParameter.ParameterDirection.Output, item, null));
            }
            
            foreach (var sentence in ExecutionSentences)
            {
                foreach (var item in sentence.ExecutionSentence.InputContextParameters)
                {
                    ExecutionContext.AddContextParameter(item);
                }
            }
        }

        public void AddExecutionSentence(UseCaseExecutionSentence sentence)
        {
            ExecutionSentences.Add(sentence);
            RecalculateContext();
        }

        public void SetExecutionOutputParameter(UseCaseLinkOutputExecutionParameter outputParameter)
        {
            OutputParameter = outputParameter ?? throw new ArgumentNullException(nameof(outputParameter));
        }
    }
}
