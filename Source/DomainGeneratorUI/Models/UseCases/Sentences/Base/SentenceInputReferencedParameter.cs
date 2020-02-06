using DomainGeneratorUI.Models.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.UseCases.Sentences.Base
{
    public class SentenceInputReferencedParameter
    {
        public MethodParameter RegardingParameter { get; set; }

        public enum SentenceSourceTpye
        {
            UseCaseInput = 5,
            SentenceOutput = 10,
            Constant = 20,
        }

        public SentenceSourceTpye Type { get; set; }

        public UseCaseSentence RegardingSentence { get; set; }
        public UseCaseSentence RegardingSentenceOutputParameter { get; set; }
        public MethodParameter RegardingUseCaseParameter { get; set; }
    }
}
