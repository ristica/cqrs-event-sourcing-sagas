using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class AccountUnlockedEvent : Event
    {
        public State State { get; set; }
    }
}
