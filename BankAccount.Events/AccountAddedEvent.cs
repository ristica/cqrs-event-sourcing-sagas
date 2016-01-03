using System;
using BankAccount.Infrastructure.Eventing;

namespace BankAccount.Events
{
    public class AccountAddedEvent : Event
    {
        public Guid CustomerId { get; set; }
        public string Currency { get; set; }
    }
}
