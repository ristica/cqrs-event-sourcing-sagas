using BankAccount.DbModel.Entities;
using BankAccount.Denormalizers.Dal;
using BankAccount.Events;
using BankAccount.Infrastructure;
using Microsoft.Practices.Unity;

namespace BankAccount.Denormalizers.Denormalizer
{
    public class UpdateCustomerDenormalizer : 
        IHandleMessage<PersonChangedEvent>,
        IHandleMessage<ContactChangedEvent>,
        IHandleMessage<AddressChangedEvent>,
        IHandleMessage<CustomerDeletedEvent>
    {
        private readonly IDenormalizerRepository<CustomerEntity> _db;

        public UpdateCustomerDenormalizer([Dependency("Customer")]  IDenormalizerRepository<CustomerEntity> db)
        {
            this._db = db;
        }

        public void Handle(PersonChangedEvent e)
        {
            // we could save all chabges if we do CRUD
            // but we are using ES 
            // ...
        }

        public void Handle(ContactChangedEvent message)
        {
            // we could save all chabges if we do CRUD
            // but we are using ES 
            // ...
        }

        public void Handle(AddressChangedEvent message)
        {
            // we could save all chabges if we do CRUD
            // but we are using ES 
            // ...
        }

        public void Handle(CustomerDeletedEvent message)
        {
            this._db.Update(message.AggregateId, message.State, message.Version);
        }
    }
}
