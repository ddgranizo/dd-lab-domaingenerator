using DD.Lab.Wpf.Drm.Services;
using DD.Lab.Wpf.Drm.Services.Implementations;
using DomainGeneratorUI.Models.Conditions;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.UseCases.Sentences
{
    public class IfSentence : UseCaseSentence
    {
        public IJsonParserService JsonParserService { get; set; }
        public ConditionGroupExpressionSentenceCollectionReference IfExpressionReference { get; set; }
        public List<ConditionGroupExpressionSentenceCollectionReference> ElseIfExpressionReferences { get; set; }
        public ConditionGroupExpressionSentenceCollectionReference ElseExpressionReference { get; set; }

        public IfSentence()
        {
            JsonParserService = new JsonParserService();
            Type = SentenceType.If;
        }

        public void SetIfExpression(ConditionGroupExpressionSentenceCollectionReference expression)
        {
            AddValue(nameof(IfExpressionReference), expression);
        }

        public void SetElseExpression(ConditionGroupExpressionSentenceCollectionReference expression)
        {
            AddValue(nameof(ElseExpressionReference), expression);
        }

        public void RemoveElseExpression()
        {
            RemoveValue(nameof(ElseExpressionReference));
        }

        public void AddElseIfExpression(ConditionGroupExpressionSentenceCollectionReference expression)
        {
            var currentElseIfListValue =
                GetValue<List<ConditionGroupExpressionSentenceCollectionReference>>(nameof(ElseIfExpressionReferences))
                    ?? new List<ConditionGroupExpressionSentenceCollectionReference>();

            currentElseIfListValue.Add(expression);

            AddValue(nameof(ElseIfExpressionReferences), currentElseIfListValue);
        }

        public void RemoveElseIfExpression(int index, ConditionGroupExpressionSentenceCollectionReference expression)
        {
            var currentElseIfListValue =
                GetValue<List<ConditionGroupExpressionSentenceCollectionReference>>(nameof(ElseIfExpressionReferences))
                    ?? new List<ConditionGroupExpressionSentenceCollectionReference>();
            if (index > currentElseIfListValue.Count - 1)
            {
                throw new IndexOutOfRangeException();
            }
            currentElseIfListValue.RemoveAt(index);
            AddValue(nameof(ElseIfExpressionReferences), currentElseIfListValue);
        }
    }
}
