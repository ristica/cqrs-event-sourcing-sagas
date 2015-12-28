using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Events
{
    public class BalanceChangedEvent : Event
    {
        public int Amount { get; set; }
    }
}
