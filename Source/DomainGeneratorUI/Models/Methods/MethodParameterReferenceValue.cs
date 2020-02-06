using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.Methods
{
    public class MethodParameterReferenceValue
    {
        public enum ValueType
        {
            UseCaseInput = 5,
            SentenceOutput = 10,
            Constant = 20,
        }

        public MethodParameter RegardingMethodParameter { get; set; }
        public MethodParameterReference RegardingReferenceMethodParameter { get; set; }
        public ValueType Type { get; set; }
        public object ConstantValue { get; set; }
    }
}
