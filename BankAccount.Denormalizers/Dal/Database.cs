using System;
using System.Data.Entity;
using System.Linq;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;
using BankAccount.ValueTypes;

namespace BankAccount.Denormalizers.Dal
{
    public sealed class Database : IDatabase
    {
        #region ICommandStackDatabase implementation

        public void Create(CustomerEntity item)
        {
            using (var ctx = new BankAccountDbContext())
            {
                ctx.CustomerSet.Add(item);
                ctx.SaveChanges();
            }
        }

        public void Create(AccountEntity item)
        {
            using (var ctx = new BankAccountDbContext())
            {
                ctx.AccountSet.Add(item);
                ctx.SaveChanges();
            }
        }

        public void UpdateAccount(Guid aggregateId, State accountState, int version)
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
                entity.AccountState = accountState;

                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void UpdateCustomer(Guid aggregateId, State customerState, int version)
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

        #endregion
    }
}
