using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BankAccount.CommandStackDal.Abstraction;
using BankAccount.CommandStackDal.Exceptions;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;
using BankAccount.Domain;

namespace BankAccount.CommandStackDal
{
    public sealed class CommandStackDatabase : ICommandStackDatabase
    {
        private static readonly List<CustomerDomainModel> Cache = new List<CustomerDomainModel>();

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

        public void Delete(Guid id)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var entity = ctx.CustomerSet.SingleOrDefault(b => b.AggregateId == id);
                if (entity == null)
                {
                    throw new AggregateNotFoundException($"Aggregate with the id {id} was not found");
                }
                ctx.CustomerSet.Remove(entity);
                ctx.SaveChanges();
            }
        }

        public void AddToCache(CustomerDomainModel ba)
        {
            var acc = Cache.SingleOrDefault(b => b.Id == ba.Id);
            if (acc == null)
            {
                Cache.Add(ba);
            }
            else
            {
                Cache.Remove(acc);
                Cache.Add(ba);
            }
        }

        public void UpdateFromCache()
        {
            if (!Cache.Any())
                return;

            foreach (var entity in Cache)
            {
                this.UpdateCustomer(entity);
            }

            Cache.Clear();
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
                    FirstName = item.Person.FirstName,
                    LastName = item.Person.LastName
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
                entity.FirstName            = item.Person.FirstName;
                entity.LastName             = item.Person.LastName;

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
                    AggregateId = item.Id,
                    Version = item.Version,
                    CustomerEntityId = customerEntityId.CustomerEntityId,
                    CustomerAggregateId = item.CustomerId,
                    Money = new Money
                    {
                        Balance = item.Money.Balance,
                        Currency = item.Money.Currency
                    }
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
                entity.Money = new Money
                {
                    Balance = item.Money.Balance,
                    Currency = item.Money.Currency
                };
                entity.CustomerEntityId = customerEntityId.CustomerEntityId;
                entity.CustomerAggregateId = item.CustomerId;

                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}
