using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Infrastructure.Buses
{
    public interface IEventBus : IBus
    {
        void Publish<T>(T @event) where T : Event;
    }
}
