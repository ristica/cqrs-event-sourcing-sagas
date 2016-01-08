using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Events
{
    public class BalanceChangedEvent : DomainEvent
    {
        public decimal Amount { get; set; }
    }
}
