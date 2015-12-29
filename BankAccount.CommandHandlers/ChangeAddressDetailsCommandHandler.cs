using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class ChangeAddressDetailsCommandHandler : BaseBankAccountCommandHandler, ICommandHandler<ChangeAddressDetailsCommand>
    {
        public ChangeAddressDetailsCommandHandler(ICommandStackRepository<Domain.BankAccount> repository) 
            : base(repository)
        {
        }

        public void Execute(ChangeAddressDetailsCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = this.Repository.GetById(command.Id);

            aggregate.ChangeAddress(
                command.Street, 
                command.Hausnumber, 
                command.Zip, 
                command.City, 
                command.State);

            this.Repository.Save(aggregate, aggregate.Version);
        }
    }
}
