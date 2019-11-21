using DD.DomainGenerator.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public delegate void UseCaseEventHandler(object sender, UseCaseEventArgs args);

    public class DomainEventManager
    {

        public event UseCaseEventHandler OnSelectedUseCase;

        public void RaiseOnSelectedUseCaseCaseEvent(UseCase useCase)
        {
            OnSelectedUseCase?.Invoke(this, new UseCaseEventArgs(useCase));
        }

    }
}
