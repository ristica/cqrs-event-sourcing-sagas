using System;
using BankAccount.CommandStackDal.Storage.Abstraction;
using BankAccount.EventHandlers.Base;
using BankAccount.Events;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.EventHandlers
{
    public class CustomerChangedEventHandler : BaseBankAccountEventHandler, IEventHandler<CustomerChangedEvent>
    {
        private readonly ICommandStackRepository<Domain.BankAccount> _repository;

        public CustomerChangedEventHandler(ICommandStackRepository<Domain.BankAccount> repository, ICommandStackDatabase database)
            : base(database)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            this._repository = repository;
        }

        public void Handle(CustomerChangedEvent handle)
        {
            var ba = this._repository.GetById(handle.AggregateId);

            ba.Customer.FirstName = handle.FirstName;
            ba.Customer.LastName = handle.LastName;

            this.Database.AddToCache(ba);
        }
    }
}
