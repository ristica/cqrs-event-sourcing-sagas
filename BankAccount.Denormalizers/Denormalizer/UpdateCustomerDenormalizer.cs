using BankAccount.Denormalizers.Dal;
using BankAccount.Events;
using BankAccount.Infrastructure;

namespace BankAccount.Denormalizers.Denormalizer
{
    public class UpdateCustomerDenormalizer : 
        IHandleMessage<PersonChangedEvent>,
        IHandleMessage<ContactChangedEvent>,
        IHandleMessage<AddressChangedEvent>,
        IHandleMessage<CustomerDeletedEvent>
    {
        private readonly CustomerDatabase _db;

        public UpdateCustomerDenormalizer()
        {
            this._db = new CustomerDatabase();
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
