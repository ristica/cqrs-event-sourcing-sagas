using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class LockAccountCommand : Command
    {
        public LockAccountCommand(
            Guid id, 
            int version) 
            : base(id, version)
        {
        }
    }
}
