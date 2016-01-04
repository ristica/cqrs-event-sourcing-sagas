using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class UnlockAccountCommandHandler : BaseAccountCommandHandler, ICommandHandler<UnlockAccountCommand>
    {
        public UnlockAccountCommandHandler(ICommandStackRepository<Domain.AccountDomainModel> repository) 
            : base(repository)
        {
        }

        public void Execute(UnlockAccountCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = this.Repository.GetById(command.Id);

            aggregate.UnlockAccount();

            this.Repository.Save(aggregate, aggregate.Version);
        } 
    }
}
