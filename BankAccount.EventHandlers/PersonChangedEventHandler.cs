using System;
using BankAccount.CommandStackDal.Abstraction;
using BankAccount.EventHandlers.Base;
using BankAccount.Events;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.EventHandlers
{
    public class PersonChangedEventHandler : BaseBankAccountEventHandler, IEventHandler<PersonChangedEvent>
    {
        private readonly ICommandStackRepository<Domain.CustomerDomainModel> _repository;

        public PersonChangedEventHandler(ICommandStackRepository<Domain.CustomerDomainModel> repository, ICommandStackDatabase database)
            : base(database)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            this._repository = repository;
        }

        public void Handle(PersonChangedEvent handle)
        {
            var ba = this._repository.GetById(handle.AggregateId);

            ba.Person.FirstName   = handle.FirstName;
            ba.Person.LastName    = handle.LastName;

            this.Database.AddToCache(ba);
        }
    }
}
