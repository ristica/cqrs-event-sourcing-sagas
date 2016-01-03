using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class AddAccountCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public string Currency { get; private set; }

        public AddAccountCommand(Guid id, int version, Guid customerId, string currency) 
            : base(id, version)
        {
            this.Currency = currency;
            this.CustomerId = customerId;
        }
    }
}
