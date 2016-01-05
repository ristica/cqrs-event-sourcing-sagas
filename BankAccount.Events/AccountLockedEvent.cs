using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class AccountLockedEvent : DomainEvent
    {
        public State AccountState { get; set; }
    }
}
