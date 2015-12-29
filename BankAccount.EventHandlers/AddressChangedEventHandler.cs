using System;
using BankAccount.CommandStackDal.Storage.Abstraction;
using BankAccount.EventHandlers.Base;
using BankAccount.Events;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.EventHandlers
{
    public class AddressChangedEventHandler : BaseBankAccountEventHandler, IEventHandler<AddressChangedEvent>
    {
        private readonly ICommandStackRepository<Domain.BankAccount> _repository;

        public AddressChangedEventHandler(ICommandStackRepository<Domain.BankAccount> repository, ICommandStackDatabase database) 
            : base(database)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            this._repository = repository;
        }

        public void Handle(AddressChangedEvent handle)
        {
            var ba = this._repository.GetById(handle.AggregateId);

            ba.Address.Street       = handle.Street;
            ba.Address.Hausnumber   = handle.Hausnumber;
            ba.Address.Zip          = handle.Zip;
            ba.Address.City         = handle.City;
            ba.Address.State        = handle.State;

            this.Database.AddToCache(ba);
        }
    }
}
