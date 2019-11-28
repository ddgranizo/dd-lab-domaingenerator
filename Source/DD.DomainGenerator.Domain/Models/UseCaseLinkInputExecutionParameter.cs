using DD.DomainGenerator.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.Models
{
    public class UseCaseLinkInputExecutionParameter
    {
        public DataParameter Source { get; set; }
        public UseCaseExecutionContextParameter Destination { get; set; }

        public UseCaseLinkInputExecutionParameter(DataParameter source, UseCaseExecutionContextParameter destination)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }

    }
}
