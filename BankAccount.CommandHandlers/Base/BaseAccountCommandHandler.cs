using System;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers.Base
{
    public abstract class BaseAccountCommandHandler
    {
        protected readonly ICommandStackRepository<Domain.AccountDomainModel> Repository;

        protected BaseAccountCommandHandler(ICommandStackRepository<Domain.AccountDomainModel> repository)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            this.Repository = repository;
        }
    }
}
