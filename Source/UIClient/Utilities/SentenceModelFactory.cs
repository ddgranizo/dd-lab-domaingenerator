using DD.DomainGenerator.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Extensions;
using UIClient.Models.Sentences.Base;

namespace UIClient.Utilities
{
    public static class SentenceModelFactory
    {

        public static ExecutionSentenceBaseModel ConvertSentenceWithInnerType(ExecutionSentenceBaseModel instance)
        {
            if (instance.Type == ExecutionSentenceBase.ExecutionSentenceType.ExecuteRepositoryMethod)
            {
                return instance.ToExecuteRepositoryMethodSentence();
            }
            throw new NotImplementedException();
        } 

    }
}
