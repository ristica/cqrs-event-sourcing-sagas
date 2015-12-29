using System;
using BankAccount.CommandStackDal.Storage.Abstraction;
using BankAccount.EventHandlers.Base;
using BankAccount.Events;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.EventHandlers
{
    public class ContactChangedEventHandler : BaseBankAccountEventHandler, IEventHandler<ContactChangedEvent>
    {
        private readonly ICommandStackRepository<Domain.BankAccount> _repository;

        public ContactChangedEventHandler(ICommandStackRepository<Domain.BankAccount> repository, ICommandStackDatabase database) 
            : base(database)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            this._repository = repository;
        }

        public void Handle(ContactChangedEvent handle)
        {
            var ba = this._repository.GetById(handle.AggregateId);

            ba.Contact.Email        = handle.Email;
            ba.Contact.PhoneNumber  = handle.Phone;

            this.Database.AddToCache(ba);
        }
    }
}
