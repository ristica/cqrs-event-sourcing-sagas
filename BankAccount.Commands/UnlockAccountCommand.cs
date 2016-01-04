using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class UnlockAccountCommand : Command
    {
        public UnlockAccountCommand(Guid id, int version) 
            : base(id, version)
        {
        }
    }
}
