using DD.DomainGenerator.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.Models
{
    public class UseCaseExecutionContextParameter
    {
        public enum ParameterDirection
        {
            Input = 1,
            Output = 2,
        }
        public ParameterDirection Direction { get; set; }
        public DataParameter Parameter { get; set; }
        public ExecutionSentenceBase Source { get; set; }
        public object ConstantValue { get; set; }
        public bool IsFromUseCase { get; set; }

        public UseCaseExecutionContextParameter()
        {
        }

        public UseCaseExecutionContextParameter(ParameterDirection direction,
            DataParameter parameter,
            ExecutionSentenceBase source,
            object constantValue = null)
        {
            Direction = direction;
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            Source = source;
            IsFromUseCase = source == null;
            ConstantValue = constantValue;
        }

    }
}
