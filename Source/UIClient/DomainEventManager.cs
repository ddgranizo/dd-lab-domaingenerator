using DD.DomainGenerator.Events;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Events;
using UIClient.Models;

namespace UIClient
{
    public delegate void UseCaseEventHandler(object sender, UseCaseEventArgs args);

    public class DomainEventManager
    {

        public event UseCaseEventHandler OnSelectedUseCase;

        public void RaiseOnSelectedUseCaseCaseEvent(UseCaseModel useCase)
        {
            OnSelectedUseCase?.Invoke(this, new UseCaseEventArgs(useCase));
        }

    }
}
