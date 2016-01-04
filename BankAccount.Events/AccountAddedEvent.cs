using System;
using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Events
{
    public class AccountAddedEvent : DomainEvent
    {
        public Guid CustomerId { get; set; }
        public string Currency { get; set; }
    }
}
