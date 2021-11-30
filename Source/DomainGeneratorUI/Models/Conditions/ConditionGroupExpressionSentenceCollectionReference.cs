using DomainGeneratorUI.Models.UseCases;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.Conditions
{
    public class ConditionGroupExpressionSentenceCollectionReference
    {
        public GroupNodeExpression Expression { get; set; }
        public UseCaseSentenceCollection Collection { get; set; }
    }
}
