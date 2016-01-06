using System;
using System.Data.Entity;
using System.Linq;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;
using BankAccount.ValueTypes;

namespace BankAccount.Denormalizers.Dal
{
    public sealed class AccountDatabase : IDatabase<AccountEntity>
    {
        public void Create(AccountEntity item)
        {
            using (var ctx = new BankAccountDbContext())
            {
                ctx.AccountSet.Add(item);
                ctx.SaveChanges();
            }
        }

        public void Update(Guid aggregateId, State state, int version)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var entity = ctx.AccountSet.SingleOrDefault(a => a.AggregateId == aggregateId);
                if (entity == null)
                {
                    throw new ArgumentNullException($"Account entity not found");
                }

                var customerEntityId = ctx.CustomerSet.SingleOrDefault(c => c.AggregateId == entity.CustomerAggregateId);
                if (customerEntityId == null)
                {
                    throw new ArgumentNullException($"Customer entity not found");
                }

                entity.Version = version;
                entity.AccountState = state;

                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
