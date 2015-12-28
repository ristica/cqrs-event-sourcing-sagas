using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Infrastructure.EventHandling
{
    public interface IEventHandler<in TEvent>
    {
        void Handle(TEvent handle);
    }
}
