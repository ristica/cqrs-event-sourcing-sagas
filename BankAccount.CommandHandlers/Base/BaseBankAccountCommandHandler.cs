using System;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers.Base
{
    public abstract class BaseBankAccountCommandHandler
    {
        protected readonly ICommandStackRepository<Domain.BankAccount> Repository;

        protected BaseBankAccountCommandHandler(ICommandStackRepository<Domain.BankAccount> repository)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            this.Repository = repository;
        }
    }
}
