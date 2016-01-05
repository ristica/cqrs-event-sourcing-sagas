using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class AccountUnlockedEvent : DomainEvent
    {
        public State AccountState { get; set; }
    }
}
