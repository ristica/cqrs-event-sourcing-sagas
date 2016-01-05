using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class ChangeBalanceCommand : Command
    {
        public int Amount { get; private set; }

        public ChangeBalanceCommand(
            Guid id, 
            int version, 
            int amount) 
            : base(id, version)
        {
            this.Amount = amount;
        }
    }
}
