using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static DomainGeneratorUI.Models.Methods.MethodParameterReference;

namespace DomainGeneratorUI.Viewmodels.Methods
{
    public class MethodParameterReferenceViewModel : BaseViewModel
    {

        public string DisplayName
        {
            get
            {
                return GetDisplayName();
            }
        }

        public UseCaseSentenceViewModel Sentence { get { return GetValue<UseCaseSentenceViewModel>(); } set { SetValue(value); } }
        public MethodParameterViewModel MethodParameter { get { return GetValue<MethodParameterViewModel>(); } set { SetValue(value); } }
        public MethodParameterReferenceType ReferenceType { get { return GetValue<MethodParameterReferenceType>(); } set { SetValue(value); } }

        public MethodParameterReferenceViewModel()
        {
        }

        public MethodParameterReferenceViewModel(
            UseCaseSentenceViewModel sentence,
            MethodParameterViewModel methodParameter,
            MethodParameterReferenceType referenceType)
        {
            Sentence = sentence ;
            MethodParameter = methodParameter ?? throw new ArgumentNullException(nameof(methodParameter));
            ReferenceType = referenceType;
        }

        public MethodParameterReferenceViewModel(
            MethodParameterViewModel methodParameter)
        {
            MethodParameter = methodParameter ?? throw new ArgumentNullException(nameof(methodParameter));
            ReferenceType = MethodParameterReferenceType.UseCase;
        }

        public MethodParameterReferenceViewModel(
            UseCaseSentenceViewModel sentence,
            MethodParameterViewModel methodParameter)
        {
            Sentence = sentence ;
            MethodParameter = methodParameter ?? throw new ArgumentNullException(nameof(methodParameter));
            ReferenceType = MethodParameterReferenceType.UseCaseSentence;
        }

        public string GetDisplayName(int reepatedIteration = 0)
        {
            var builder = new StringBuilder();
            if (ReferenceType == MethodParameterReferenceType.UseCase && MethodParameter != null)
            {
                 builder.Append($"[Use Case input] {MethodParameter.Name} ({MethodParameter.Type})");
            }
            else if (ReferenceType == MethodParameterReferenceType.UseCaseSentence)
            {
                if (Sentence != null)
                {
                    var repeatedString = reepatedIteration == 0
                            ? string.Empty
                            : $"({reepatedIteration})";
                    builder.Append($"[{Sentence.Name}{repeatedString}]");
                }
                if (MethodParameter != null)
                {
                    builder.Append(" ");
                    builder.Append($"{MethodParameter.Name} ({MethodParameter.Type})");
                }
            }
            return builder.ToString();
        }
    }
}
