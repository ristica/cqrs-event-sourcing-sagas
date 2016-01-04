using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class CustomerDeletedEvent : DomainEvent
    {
        public State State { get; set; }
    }
}
