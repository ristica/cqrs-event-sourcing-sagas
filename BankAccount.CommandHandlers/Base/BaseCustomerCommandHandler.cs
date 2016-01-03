using System;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers.Base
{
    public abstract class BaseCustomerCommandHandler
    {
        protected readonly ICommandStackRepository<Domain.CustomerDomainModel> Repository;

        protected BaseCustomerCommandHandler(ICommandStackRepository<Domain.CustomerDomainModel> repository)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            this.Repository = repository;
        }
    }
}
