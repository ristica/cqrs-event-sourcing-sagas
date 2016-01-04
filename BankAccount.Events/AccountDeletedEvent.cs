using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class AccountDeletedEvent : Event
    {
        public State State { get; set; }
    }
}
