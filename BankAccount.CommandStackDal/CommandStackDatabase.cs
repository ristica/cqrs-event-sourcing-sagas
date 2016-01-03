using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BankAccount.CommandStackDal.Abstraction;
using BankAccount.CommandStackDal.Exceptions;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;

namespace BankAccount.CommandStackDal
{
    public sealed class CommandStackDatabase : ICommandStackDatabase
    {
        private static readonly List<Domain.BankAccount> Cache = new List<Domain.BankAccount>();

        #region ICommandStackDatabase implementation

        public void Save(Domain.BankAccount item)
        {
            BankAccountEntity entity;
            using (var ctx = new BankAccountDbContext())
            {
                entity = ctx.BankAccountSet.SingleOrDefault(b => b.AggregateId == item.Id);
            }

            if (entity == null)
            {
                this.AddBankAccount(item);
            }
            else
            {
                this.UpdateBankAccount(item);
            }
        }

        public void Delete(Guid id)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var entity = ctx.BankAccountSet.SingleOrDefault(b => b.AggregateId == id);
                if (entity == null)
                {
                    throw new AggregateNotFoundException($"Aggregate with the id {id} was not found");
                }
                ctx.BankAccountSet.Remove(entity);
                ctx.SaveChanges();
            }
        }

        public void AddToCache(Domain.BankAccount ba)
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
                this.UpdateBankAccount(entity);
            }

            Cache.Clear();
        }

        #endregion

        #region Helpers

        private void AddBankAccount(Domain.BankAccount item)
        {
            using (var ctx = new BankAccountDbContext())
            {
                ctx.BankAccountSet.Add(new BankAccountEntity
                {
                    AggregateId         = item.Id,
                    Version             = item.Version,
                    FirstName = item.Person.FirstName,
                    LastName = item.Person.LastName
                });
                ctx.SaveChanges();
            }
        }

        private void UpdateBankAccount(Domain.BankAccount item)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var entity = ctx.BankAccountSet.SingleOrDefault(b => b.AggregateId == item.Id);
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

        #endregion
    }
}
