using DD.DomainGenerator.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.Models
{
    public class UseCaseLinkOutputExecutionParameter
    {
        public UseCaseExecutionContextParameter Source { get; set; }
        public DataParameter Destination { get; set; }

        public UseCaseLinkOutputExecutionParameter(UseCaseExecutionContextParameter source, DataParameter destination)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }

    }
}
