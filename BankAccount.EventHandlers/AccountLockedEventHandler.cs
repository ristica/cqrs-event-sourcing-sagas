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
    public class AccountLockedEventHandler : BaseAccountEventHandler, IEventHandler<AccountLockedEvent>
    {
        private readonly ICommandStackRepository<Domain.AccountDomainModel> _repository;

        public AccountLockedEventHandler(ICommandStackDatabase database, ICommandStackRepository<AccountDomainModel> repository) 
            : base (database)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            this._repository = repository;
        }

        public void Handle(AccountLockedEvent handle)
        {
            var account = this._repository.GetById(handle.AggregateId);
            account.State = State.Locked;
            this.Database.Save(account);
        }
    }
}
