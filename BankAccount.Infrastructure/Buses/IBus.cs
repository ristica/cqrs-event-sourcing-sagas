using BankAccount.Infrastructure.Commanding;
using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Infrastructure.Buses
{
    public interface IBus
    {
        void Send<T>(T command) where T : Command;
        void RaiseEvent<T>(T @event) where T : DomainEvent;
        void RegisterSaga<T>();
        void RegisterHandler<T>();
    }
}
