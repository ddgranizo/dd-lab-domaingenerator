using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Events
{
    public class UseCaseEventArgs : EventArgs
    {
        public UseCaseEventArgs(UseCase useCase)
        {
            UseCase = useCase;
        }

        public UseCase UseCase { get; }
    }
}
