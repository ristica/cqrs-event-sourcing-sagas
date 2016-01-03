using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class ChangeCurrencyCommandHandler : BaseBankAccountCommandHandler, ICommandHandler<ChangeCurrencyCommand>
    {
        public ChangeCurrencyCommandHandler(ICommandStackRepository<Domain.CustomerDomainModel> repository) 
            : base(repository)
        {
        }

        public void Execute(ChangeCurrencyCommand command)
        {
            //if (command == null)
            //{
            //    throw new ArgumentNullException(nameof(command));
            //}

            //var aggregate = this.Repository.GetById(command.Id);

            //aggregate.ChangeCurrency(command.Currency);

            //this.Repository.Save(aggregate, aggregate.Version);
        }
    }
}
