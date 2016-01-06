using System;
using System.Data.Entity;
using System.Linq;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;
using BankAccount.ValueTypes;

namespace BankAccount.Denormalizers.Dal
{
    public sealed class CustomerDatabase : IDatabase<CustomerEntity>
    {
        public void Create(CustomerEntity item)
        {
            using (var ctx = new BankAccountDbContext())
            {
                ctx.CustomerSet.Add(item);
                ctx.SaveChanges();
            }
        }

        public void Update(Guid aggregateId, State customerState, int version)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var entity = ctx.CustomerSet.SingleOrDefault(cust => cust.AggregateId == aggregateId);
                if (entity == null)
                {
                    throw new ArgumentNullException($"Customer entity not found");
                }

                entity.CustomerState = customerState;
                entity.Version = version;

                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
