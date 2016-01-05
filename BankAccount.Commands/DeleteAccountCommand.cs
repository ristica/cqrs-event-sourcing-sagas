using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class DeleteAccountCommand : Command
    {
        public DeleteAccountCommand(
            Guid id, 
            int version) 
            : base(id, version)
        {
        }
    }
}
