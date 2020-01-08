using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.UseCases.Sentences
{
    public class ExecuteServiceSentence : UseCaseSentence
    {
        public UseCase RegardingUseCase { get; set; }
    }
}
