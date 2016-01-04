using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class DeleteAccountCommandHandler : BaseAccountCommandHandler, ICommandHandler<DeleteAccountCommand>
    {
        public DeleteAccountCommandHandler(ICommandStackRepository<Domain.AccountDomainModel> repository) 
            : base(repository)
        {
        }

        public void Execute(DeleteAccountCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = this.Repository.GetById(command.Id);

            aggregate.DeleteAccount();

            this.Repository.Save(aggregate, aggregate.Version);
        } 
    }
}
