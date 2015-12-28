using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class TransferMoneyCommandHandler : BaseBankAccountCommandHandler, ICommandHandler<TransferMoneyCommand>
    {
        public TransferMoneyCommandHandler(ICommandStackRepository<Domain.BankAccount> repository) : base(repository)
        {
        }

        public void Execute(TransferMoneyCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = this.Repository.GetById(command.Id);

            aggregate.ChangeBalance(command.Amount);

            this.Repository.Save(aggregate, aggregate.Version);
        }
    }
}
