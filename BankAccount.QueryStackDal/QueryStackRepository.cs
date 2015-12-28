using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;
using BankAccount.ReadModel;

namespace BankAccount.QueryStackDal
{
    public class QueryStackRepository : IQueryStackRepository
    {
        public BankAccountReadModel GetBankAccount(Guid aggregateId)
        {
            using (var ctx = new BankAccountDbContext())
            {
                var entity = ctx.BankAccountSet.SingleOrDefault(b => b.AggregateId == aggregateId);
                if (entity == null)
                {
                    return null;
                }
                return new BankAccountReadModel
                {
                    AggregateId = entity.AggregateId,
                    Version = entity.Version,
                    Money = SetMoney(entity.Money),
                    Customer = SetCustomer(entity.Customer),
                    Address = SetAddress(entity.Address),
                    Contact = SetContact(entity.Contact)
                };
            }
        }

        public IEnumerable<BankAccountReadModel> GetAccounts()
        {
            using (var ctx = new BankAccountDbContext())
            {
                return ctx.BankAccountSet.Select(e => new BankAccountReadModel
                {
                    AggregateId = e.AggregateId,
                    Version = e.Version,
                    Money = new MoneyReadModel
                    {
                        Currency = e.Money.Currency,
                        Balance = e.Money.Balance
                    },
                    Address = new AddressReadModel
                    {
                        State = e.Address.State,
                        Street = e.Address.Street,
                        City = e.Address.City,
                        Hausnumber = e.Address.Hausnumber,
                        Zip = e.Address.Zip
                    },
                    Customer = new CustomerReadModel
                    {
                        FirstName = e.Customer.FirstName,
                        LastName = e.Customer.LastName,
                        Dob = e.Customer.Dob
                    },
                    Contact = new ContactReadModel
                    {
                        Email = e.Contact.Email,
                        Phone = e.Contact.Phone
                    }
                }).ToList();
            }
        }

        #region Helpers

        private MoneyReadModel SetMoney(Money m)
        {
            return new MoneyReadModel
            {
                Currency = m.Currency,
                Balance = m.Balance
            };
        }

        private CustomerReadModel SetCustomer(Customer c)
        {
            return new CustomerReadModel
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                IdCard = c.IdCard,
                IdNumber = c.IdNumber,
                Dob = c.Dob
            };
        }

        private ContactReadModel SetContact(Contact c)
        {
            return new ContactReadModel
            {
                Email = c.Email,
                Phone = c.Phone
            };
        }

        private AddressReadModel SetAddress(Address a)
        {
            return new AddressReadModel
            {
                State = a.State,
                Street = a.Street,
                City = a.City,
                Hausnumber = a.Hausnumber,
                Zip = a.Zip
            };
        }

        #endregion
    }
}
