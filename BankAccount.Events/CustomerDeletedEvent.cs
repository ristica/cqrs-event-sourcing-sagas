using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class CustomerDeletedEvent : Event
    {
        public State State { get; set; }
    }
}
