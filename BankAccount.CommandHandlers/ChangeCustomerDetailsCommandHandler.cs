using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class ChangeCustomerDetailsCommandHandler : BaseBankAccountCommandHandler, ICommandHandler<ChangeCustomerDetailsCommand>
    {
        public ChangeCustomerDetailsCommandHandler(ICommandStackRepository<Domain.BankAccount> repository) 
            : base(repository)
        {
        }

        public void Execute(ChangeCustomerDetailsCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = this.Repository.GetById(command.Id);

            aggregate.ChangeCustomer(command.FirstName, command.LastName, command.IdCard, command.IdNumber);

            this.Repository.Save(aggregate, aggregate.Version);
        }
    }
}
