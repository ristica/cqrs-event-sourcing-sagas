using System;
using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class AccountAddedEvent : DomainEvent
    {
        public Guid CustomerId { get; set; }
        public string Currency { get; set; }
        public State AccountState { get; set; }
    }
}
