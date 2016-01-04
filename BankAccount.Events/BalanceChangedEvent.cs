using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Events
{
    public class BalanceChangedEvent : DomainEvent
    {
        public int Amount { get; set; }
    }
}
