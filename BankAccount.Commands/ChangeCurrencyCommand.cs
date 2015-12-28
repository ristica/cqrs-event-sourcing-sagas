using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class ChangeCurrencyCommand : Command
    {
        public string Currency { get; private set; }

        public ChangeCurrencyCommand(Guid id, int version, string currency) 
            : base(id, version)
        {
            this.Currency = currency;
        }
    }
}
