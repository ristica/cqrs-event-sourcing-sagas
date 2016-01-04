using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class CustomerCreatedEvent : Event
    {
        public Person Person { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public State State { get; set; }
    }
}
