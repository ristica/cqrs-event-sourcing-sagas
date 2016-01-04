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
    public class CustomerDeletedEventHandler : BaseCustomerEventHandler, IEventHandler<CustomerDeletedEvent>
    {
        private readonly ICommandStackRepository<Domain.CustomerDomainModel> _repository;

        public CustomerDeletedEventHandler(ICommandStackDatabase database, ICommandStackRepository<CustomerDomainModel> repository) 
            : base (database)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            this._repository = repository;
        }

        public void Handle(CustomerDeletedEvent handle)
        {
            var account = this._repository.GetById(handle.AggregateId);
            account.State = State.Closed;
            this.Database.Save(account);
        }
    }
}
