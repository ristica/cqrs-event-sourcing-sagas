using System;
using BankAccount.Infrastructure.Commanding;
using BankAccount.ValueTypes;

namespace BankAccount.Commands
{
    public class AddAccountCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public string Currency { get; private set; }
        public State AccountState { get; set; }

        public AddAccountCommand(Guid id, int version, Guid customerId, string currency, State acccountState) 
            : base(id, version)
        {
            this.Currency = currency;
            this.CustomerId = customerId;
            this.AccountState = acccountState;
        }
    }
}
