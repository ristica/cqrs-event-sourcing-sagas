using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BankAccount.CommandStackDal.Exceptions;
using BankAccount.CommandStackDal.Storage.Abstraction;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;

namespace BankAccount.CommandStackDal.Storage
{
    public class CommandStackDatabase : ICommandStackDatabase
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
                    throw new AggregateNotFoundException();
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
                    AggregateId = item.Id,
                    Version = item.Version,
                    Money = new Money
                    {
                        Balance = item.Money.Balance,
                        Currency = item.Money.Currency
                    },
                    Customer = new Customer
                    {
                        Dob = item.Customer.Dob,
                        FirstName = item.Customer.FirstName,
                        LastName = item.Customer.LastName,
                        IdCard = item.Customer.IdCard,
                        IdNumber = item.Customer.IdNumber
                    },
                    Contact = new Contact
                    {
                        Email = item.Contact.Email,
                        Phone = item.Contact.PhoneNumber
                    },
                    Address = new Address
                    {
                        Street = item.Address.Street,
                        Zip = item.Address.Zip,
                        Hausnumber = item.Address.Hausnumber,
                        City = item.Address.City,
                        State = item.Address.State
                    }
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

                entity.Version = item.Version;
                entity.Customer.FirstName = item.Customer.FirstName;
                entity.Customer.LastName = item.Customer.LastName;
                entity.Customer.IdCard = item.Customer.IdCard;
                entity.Customer.IdNumber = item.Customer.IdNumber;
                entity.Customer.Dob = item.Customer.Dob;

                entity.Contact.Email = item.Contact.Email;
                entity.Contact.Phone = item.Contact.PhoneNumber;

                entity.Money.Balance = item.Money.Balance;
                entity.Money.Currency = item.Money.Currency;

                entity.Address.Street = item.Address.Street;
                entity.Address.Zip = item.Address.Zip;
                entity.Address.Hausnumber = item.Address.Hausnumber;
                entity.Address.City = item.Address.City;
                entity.Address.State = item.Address.State;

                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}
