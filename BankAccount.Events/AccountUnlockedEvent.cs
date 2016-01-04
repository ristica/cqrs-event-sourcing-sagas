using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class AccountUnlockedEvent : DomainEvent
    {
        public State State { get; set; }
    }
}
