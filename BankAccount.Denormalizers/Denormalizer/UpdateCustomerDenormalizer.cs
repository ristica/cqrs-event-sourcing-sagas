using System;
using System.Data.Entity;
using System.Linq;
using BankAccount.DbModel.ItemDb;
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
        public void Handle(PersonChangedEvent e)
        {
            //// we could save all chabges if we do CRUD
            //// but we are using ES 
            //using (var ctx = new BankAccountDbContext())
            //{
            //    var entity = ctx.CustomerSet.SingleOrDefault(cust => cust.AggregateId == e.AggregateId);
            //    if (entity == null)
            //    {
            //        throw new ArgumentNullException($"customer");
            //    }

            //    entity.FirstName = e.FirstName;
            //    entity.LastName = e.LastName;
            //    entity.CustomerState = e.State;
            //    entity.Version = e.Version;

            //    ctx.Entry(entity).State = EntityState.Modified;
            //    ctx.SaveChanges();
            //}
        }

        public void Handle(ContactChangedEvent message)
        {
            //// we could save all chabges if we do CRUD
            //// but we are using ES 
            //// ...
        }

        public void Handle(AddressChangedEvent message)
        {
            //// we could save all chabges if we do CRUD
            //// but we are using ES 
            //// ...
        }

        public void Handle(CustomerDeletedEvent message)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var entity = ctx.CustomerSet.SingleOrDefault(cust => cust.AggregateId == message.AggregateId);
                if (entity == null)
                {
                    throw new ArgumentNullException($"customer");
                }

                entity.CustomerState = message.State;
                entity.Version = message.Version;

                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
