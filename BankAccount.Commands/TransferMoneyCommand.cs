using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class TransferMoneyCommand : Command
    {
        public int Amount { get; private set; }

        public TransferMoneyCommand(Guid id, int version, int amount) 
            : base(id, version)
        {
            this.Amount = amount;
        }
    }
}
