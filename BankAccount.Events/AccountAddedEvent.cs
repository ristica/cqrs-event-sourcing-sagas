using System;
using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class AccountAddedEvent : Event
    {
        public Guid CustomerId { get; set; }
        public Money Money { get; set; }
    }
}
