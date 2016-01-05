using System.Data.Entity;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;
using BankAccount.Events;
using BankAccount.Infrastructure;

namespace BankAccount.Denormalizers.Denormalizer
{
    public class CreateCustomerDenormalizer : 
        IHandleMessage<CustomerCreatedEvent>
    {
        public void Handle(CustomerCreatedEvent e)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var c = new CustomerEntity
                {
                    AggregateId = e.AggregateId,
                    Version = e.Version,
                    CustomerState = e.State
                };

                ctx.Entry(c).State = EntityState.Added;
                ctx.SaveChanges();
            }
        }
    }
}
