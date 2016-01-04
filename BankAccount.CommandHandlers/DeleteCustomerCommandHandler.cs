using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class DeleteCustomerCommandHandler : BaseCustomerCommandHandler, ICommandHandler<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandHandler(ICommandStackRepository<Domain.CustomerDomainModel> repository) 
            : base(repository)
        {
        }

        public void Execute(DeleteCustomerCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = this.Repository.GetById(command.Id);

            aggregate.DeleteBankAccount();

            this.Repository.Save(aggregate, aggregate.Version);
        } 
    }
}
