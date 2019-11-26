using DD.DomainGenerator.Events;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Events;
using UIClient.Models;
using UIClient.Models.Inputs;

namespace UIClient
{
    public delegate void UseCaseEventHandler(object sender, UseCaseEventArgs args);
    public delegate void GenericFormRequestHandler(object sender, GenericFormRequestEventArgs args);
    public delegate void GenericFormResponseHandler(object sender, GenericFormResponseEventArgs args);

    public class DomainEventManager
    {
        public event UseCaseEventHandler OnSelectedUseCase;
        public event GenericFormRequestHandler OnGenericFormInputRequested;
        public event GenericFormResponseHandler OnGenericFormInputResponsed;

        public void RaiseOnSelectedUseCaseCaseEvent(UseCaseModel useCase)
        {
            OnSelectedUseCase?.Invoke(this, new UseCaseEventArgs(useCase));
        }

        public void RaiseOnGenericFormInputRequestedEvent(Guid requestId, GenericFormModel formModel)
        {
            OnGenericFormInputRequested?.Invoke(this, new GenericFormRequestEventArgs(requestId, formModel));
        }

        public void RaiseOnGenericFormInputResponsedEvent(Guid requestId, bool completed, Dictionary<string, object> values)
        {
            OnGenericFormInputResponsed?.Invoke(this, new GenericFormResponseEventArgs(requestId, completed, values));
        }
    }
}
