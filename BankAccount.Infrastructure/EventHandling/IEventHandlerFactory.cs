using System.Collections.Generic;
using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Infrastructure.EventHandling
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event;
    }
}
