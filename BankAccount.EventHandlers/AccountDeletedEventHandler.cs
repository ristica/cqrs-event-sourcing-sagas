using System;
using BankAccount.CommandStackDal.Abstraction;
using BankAccount.Domain;
using BankAccount.EventHandlers.Base;
using BankAccount.Events;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.Infrastructure.Storage;
using BankAccount.ValueTypes;

namespace BankAccount.EventHandlers
{
    public class AccountDeletedEventHandler : BaseAccountEventHandler, IEventHandler<AccountDeletedEvent>
    {
        private readonly ICommandStackRepository<AccountDomainModel> _repository;

        public AccountDeletedEventHandler(ICommandStackDatabase database, ICommandStackRepository<AccountDomainModel> repository) 
            : base (database)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            this._repository = repository;
        }

        public void Handle(AccountDeletedEvent handle)
        {
            var account = this._repository.GetById(handle.AggregateId);
            account.State = State.Closed;
            this.Database.Save(account);
        }
    }
}
