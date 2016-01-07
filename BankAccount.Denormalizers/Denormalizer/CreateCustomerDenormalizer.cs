using BankAccount.DbModel.Entities;
using BankAccount.Denormalizers.Dal;
using BankAccount.Events;
using BankAccount.Infrastructure;
using Microsoft.Practices.Unity;

namespace BankAccount.Denormalizers.Denormalizer
{
    public class CreateCustomerDenormalizer : 
        IHandleMessage<CustomerCreatedEvent>
    {
        private readonly IDenormalizerRepository<CustomerEntity> _db;

        public CreateCustomerDenormalizer([Dependency("Customer")]  IDenormalizerRepository<CustomerEntity> db)
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
