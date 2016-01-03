using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Domain;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class AddAccountCommandHandler : BaseCustomerCommandHandler, ICommandHandler<AddAccountCommand>
    {
        public AddAccountCommandHandler(ICommandStackRepository<CustomerDomainModel> repository) 
            : base(repository)
        {
        }

        public void Execute(AddAccountCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = new AccountDomainModel();

            aggregate.CreateNewAccount(
                command.Id,
                command.CustomerId,
                command.Version,
                command.Currency);

            this.Repository.Save(aggregate, aggregate.Version);
        }
    }
}
