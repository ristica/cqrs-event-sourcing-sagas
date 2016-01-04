using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class AccountLockedEvent : Event
    {
        public State State { get; set; }
    }
}
