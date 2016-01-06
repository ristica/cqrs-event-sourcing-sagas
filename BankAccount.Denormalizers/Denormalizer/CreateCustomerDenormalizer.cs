using BankAccount.DbModel.Entities;
using BankAccount.Denormalizers.Dal;
using BankAccount.Events;
using BankAccount.Infrastructure;

namespace BankAccount.Denormalizers.Denormalizer
{
    public class CreateCustomerDenormalizer : 
        IHandleMessage<CustomerCreatedEvent>
    {
        private readonly IDatabase _db;

        public CreateCustomerDenormalizer(IDatabase db)
        {
            this._db = db;
        }

        public void Handle(CustomerCreatedEvent e)
        {
            this._db.Create(
                new CustomerEntity
                {
                    AggregateId = e.AggregateId,
                    Version = e.Version,
                    CustomerState = e.State
                });
        }
    }
}
