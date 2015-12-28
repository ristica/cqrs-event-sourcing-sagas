using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class DeleteBankAccountCommand : Command
    {
        public DeleteBankAccountCommand(Guid id, int version) : base(id, version)
        {
        }
    }
}
