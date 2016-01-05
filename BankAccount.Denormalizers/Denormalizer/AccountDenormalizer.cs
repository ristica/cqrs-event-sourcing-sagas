using System;
using System.Data.Entity;
using System.Linq;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;
using BankAccount.Events;
using BankAccount.Infrastructure;
using BankAccount.ValueTypes;

namespace BankAccount.Denormalizers.Denormalizer
{
    public class AccountDenormalizer :
        IHandleMessage<AccountAddedEvent>,
        IHandleMessage<AccountLockedEvent>,
        IHandleMessage<AccountUnlockedEvent>,
        IHandleMessage<AccountDeletedEvent>
    {
        public void Handle(AccountAddedEvent message)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var c = new AccountEntity
                {
                    AggregateId = message.AggregateId,
                    Version = message.Version,
                    AccountState = message.AccountState,
                    CustomerAggregateId = message.CustomerId,
                    Currency = message.Currency
                };

                ctx.Entry(c).State = EntityState.Added;
                ctx.SaveChanges();
            }
        }

        public void Handle(AccountLockedEvent message)
        {
            this.UpdateAccountEntityState(message.AggregateId, message.AccountState, message.Version);
        }

        public void Handle(AccountUnlockedEvent message)
        {
            this.UpdateAccountEntityState(message.AggregateId, message.AccountState, message.Version);
        }

        public void Handle(AccountDeletedEvent message)
        {
            this.UpdateAccountEntityState(message.AggregateId, message.AccountState, message.Version);
        }

        private void UpdateAccountEntityState(Guid aggregateId, State accountState, int version)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var entity = ctx.AccountSet.SingleOrDefault(a => a.AggregateId == aggregateId);
                if (entity == null)
                {
                    throw new ArgumentNullException($"entity");
                }

                entity.Version = version;
                entity.AccountState = accountState;

                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        
    }
}
