﻿using DomainGeneratorUI.Models.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.UseCases.Sentences.Base
{
    public class UseCaseSentence
    {
        public enum SentenceType
        {
            ExecuteRepositoryMethod = 10,
            ExecuteService = 20,
        }

        public SentenceType Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public List<MethodParameter> InputParameters { get; set; }
        public List<MethodParameter> OutputParameters { get; set; }
        //public List<SentenceInputReferencedParameter> InputReferencedParameters { get; set; }
        //public List<SentenceOutputReferencedParameter> OutputReferencedParameters { get; set; }

        public List<MethodParameterReferenceValueViewModel> ReferencedInputParametersValues { get; set; }
        public Dictionary<string, object> Values { get; set; }
        public UseCaseSentence()
        {
            ReferencedInputParametersValues = new List<MethodParameterReferenceValueViewModel>();
            //InputReferencedParameters = new List<SentenceInputReferencedParameter>();
            //OutputReferencedParameters = new List<SentenceOutputReferencedParameter>();
            OutputParameters = new List<MethodParameter>();
            InputParameters = new List<MethodParameter>();
            Values = new Dictionary<string, object>();
        }
    }
}
