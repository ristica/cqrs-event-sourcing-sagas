using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class ChangeBalanceCommand : Command
    {
        public decimal Amount { get; private set; }

        public ChangeBalanceCommand(
            Guid id, 
            int version, 
            decimal amount) 
            : base(id, version)
        {
            this.Amount = amount;
        }
    }
}
