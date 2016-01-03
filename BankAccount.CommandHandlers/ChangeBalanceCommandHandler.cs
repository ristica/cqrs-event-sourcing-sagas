using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class ChangeBalanceCommandHandler : BaseAccountCommandHandler, ICommandHandler<ChangeBalanceCommand>
    {
        public ChangeBalanceCommandHandler(ICommandStackRepository<Domain.AccountDomainModel> repository) : base(repository)
        {
        }

        public void Execute(ChangeBalanceCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = this.Repository.GetById(command.Id);

            aggregate.ChangeBalance(command.Amount, aggregate.Version);

            this.Repository.Save(aggregate, aggregate.Version);
        }
    }
}
