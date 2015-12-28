using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Infrastructure.EventHandling
{
    public interface IHandle<in TEvent> where TEvent:Event
    {
        void Handle(TEvent e);
    }
}
