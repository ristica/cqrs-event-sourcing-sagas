using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class DeleteCustomerCommand : Command
    {
        public DeleteCustomerCommand(
            Guid id, 
            int version) 
            : base(id, version)
        {
        }
    }
}
