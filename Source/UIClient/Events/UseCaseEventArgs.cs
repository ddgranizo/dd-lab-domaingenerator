using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Events
{
    public class UseCaseEventArgs : EventArgs
    {
        public UseCaseEventArgs(UseCaseModel useCase)
        {
            UseCase = useCase;
        }

        public UseCaseModel UseCase { get; }
    }
}
