using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Events
{
    public class CurrencyChangedEvent : Event
    {
        public string Currency { get; set; }
    }
}
