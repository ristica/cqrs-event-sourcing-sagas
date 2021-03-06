﻿using BankAccount.Infrastructure.Eventing;
using BankAccount.ValueTypes;

namespace BankAccount.Events
{
    public class AccountDeletedEvent : DomainEvent
    {
        public State AccountState { get; set; }
    }
}
