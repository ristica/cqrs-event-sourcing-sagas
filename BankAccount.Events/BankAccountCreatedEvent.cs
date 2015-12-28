using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class BankAccountCreatedEvent : Event
    {
        public Customer Customer { get; set; }
        public Contact Contact { get; set; }
        public Money Money { get; set; }
        public Address Address { get; set; }
    }
}
