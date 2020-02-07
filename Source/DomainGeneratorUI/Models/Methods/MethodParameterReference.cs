using DomainGeneratorUI.Interfaces;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.Methods
{
    public class MethodParameterReference
    {
        public enum MethodParameterReferenceType
        {
            UseCase = 1,
            UseCaseSentence = 9,
        }

        public UseCaseSentence Sentence { get; set; }
        public MethodParameter MethodParameter { get; set; }
        public MethodParameterReferenceType ReferenceType { get; set; }


        public MethodParameterReference()
        {

        }


        public MethodParameterReference(
            UseCaseSentence sentence,
            MethodParameter methodParameter,
            MethodParameterReferenceType referenceType)
        {
            Sentence = sentence ;
            MethodParameter = methodParameter ;
            ReferenceType = referenceType;
        }

        public MethodParameterReference(
            MethodParameter methodParameter)
        {
            MethodParameter = methodParameter;
            ReferenceType = MethodParameterReferenceType.UseCase;
        }

        public MethodParameterReference(
            UseCaseSentence sentence,
            MethodParameter methodParameter)
        {
            Sentence = sentence ;
            MethodParameter = methodParameter;
            ReferenceType = MethodParameterReferenceType.UseCaseSentence;
        }
    }
}
