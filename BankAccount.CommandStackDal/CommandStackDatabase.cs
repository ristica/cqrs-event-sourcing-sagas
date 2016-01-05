using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BankAccount.CommandStackDal.Abstraction;
using BankAccount.CommandStackDal.Exceptions;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;
using BankAccount.Domain;
using BankAccount.ValueTypes;

namespace BankAccount.CommandStackDal
{
    public sealed class CommandStackDatabase : ICommandStackDatabase
    {
        #region ICommandStackDatabase implementation

        public void Save(CustomerDomainModel item)
        {
            CustomerEntity entity;
            using (var ctx = new BankAccountDbContext())
            {
                entity = ctx.CustomerSet.SingleOrDefault(b => b.AggregateId == item.Id);
            }

            if (entity == null)
            {
                this.AddCustomer(item);
            }
            else
            {
                this.UpdateCustomer(item);
            }
        }

        public void Save(AccountDomainModel item)
        {
            AccountEntity entity;
            using (var ctx = new BankAccountDbContext())
            {
                entity = ctx.AccountSet.SingleOrDefault(a => a.AggregateId == item.Id);
            }

            if (entity == null)
            {
                this.AddAccount(item);
            }
            else
            {
                this.UpdateAccount(item);
            }
        }

        #endregion

        #region Helpers

        private void AddCustomer(CustomerDomainModel item)
        {
            using (var ctx = new BankAccountDbContext())
            {
                ctx.CustomerSet.Add(new CustomerEntity
                {
                    AggregateId         = item.Id,
                    Version             = item.Version,
                    CustomerState       = State.Open
                });
                ctx.SaveChanges();
            }
        }

        private void UpdateCustomer(CustomerDomainModel item)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var entity = ctx.CustomerSet.SingleOrDefault(b => b.AggregateId == item.Id);
                if (entity == null)
                {
                    throw new AggregateNotFoundException("Bank account");
                }

                entity.Version              = item.Version;
                entity.CustomerState        = item.CustomerState;

                ctx.Entry(entity).State     = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        private void AddAccount(AccountDomainModel item)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var customerEntityId =
                    ctx.CustomerSet.SingleOrDefault(c => c.AggregateId == item.CustomerId);
                if (customerEntityId == null)
                {
                    throw new AggregateNotFoundException("Bank account");
                }

                ctx.AccountSet.Add(new AccountEntity
                {
                    AggregateId             = item.Id,
                    Version                 = item.Version,
                    CustomerAggregateId     = item.CustomerId,
                    Currency                = item.Currency,
                    AccountState            = State.Open
                });
                ctx.SaveChanges();
            }
        }

        private void UpdateAccount(AccountDomainModel item)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var entity = ctx.AccountSet.SingleOrDefault(b => b.AggregateId == item.Id);
                if (entity == null)
                {
                    throw new AggregateNotFoundException("account");
                }

                var customerEntityId =
                    ctx.CustomerSet.SingleOrDefault(c => c.AggregateId == item.CustomerId);
                if (customerEntityId == null)
                {
                    throw new AggregateNotFoundException("Bank account");
                }

                entity.Version = item.Version;
                entity.Currency = item.Currency;
                entity.CustomerAggregateId = item.CustomerId;
                entity.AccountState = item.State;

                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}
