using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class LockAccountCommandHandler : BaseAccountCommandHandler, ICommandHandler<LockAccountCommand>
    {
        public LockAccountCommandHandler(ICommandStackRepository<Domain.AccountDomainModel> repository) 
            : base(repository)
        {
        }

        public void Execute(LockAccountCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = this.Repository.GetById(command.Id);

            aggregate.LockAccount();

            this.Repository.Save(aggregate, aggregate.Version);
        } 
    }
}
