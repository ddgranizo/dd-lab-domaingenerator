using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.UseCases.Sentences
{
    public class ExecuteRepositoryMethodSentence : UseCaseSentence
    {
        public RepositoryMethod RegardingRepositoryMethod { get; set; }
    }
}
