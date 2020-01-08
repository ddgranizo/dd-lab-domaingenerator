using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.UseCases
{
    public class UseCaseSentenceCollection
    {
        public List<UseCaseSentence> Sentences { get; set; }

        public UseCaseSentenceCollection()
        {
            Sentences = new List<UseCaseSentence>();
        }
    }
}
