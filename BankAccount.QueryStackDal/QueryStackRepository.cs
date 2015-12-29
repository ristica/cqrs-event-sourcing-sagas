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
                    AggregateId         = entity.AggregateId,
                    Version             = entity.Version,
                    Customer = new CustomerReadModel
                    {
                        FirstName       = entity.Customer.FirstName,
                        LastName        = entity.Customer.LastName,
                        IdCard          = entity.Customer.IdCard,
                        IdNumber        = entity.Customer.IdNumber,
                        Dob             = entity.Customer.Dob
                    },
                    Contact = new ContactReadModel
                    {
                        Email           = entity.Contact.Email,
                        Phone           = entity.Contact.Phone
                    },
                    Address = new AddressReadModel
                    {
                        State           = entity.Address.State,
                        Street          = entity.Address.Street,
                        City            = entity.Address.City,
                        Hausnumber      = entity.Address.Hausnumber,
                        Zip             = entity.Address.Zip
                    },
                    Money = new MoneyReadModel
                    {
                        Currency        = entity.Money.Currency,
                        Balance         = entity.Money.Balance
                    }
                };
            }
        }

        public IEnumerable<BankAccountReadModel> GetAccounts()
        {
            using (var ctx = new BankAccountDbContext())
            {
                return ctx.BankAccountSet.Select(e => new BankAccountReadModel
                {
                    AggregateId         = e.AggregateId,
                    Version             = e.Version,
                    Customer = new CustomerReadModel
                    {
                        FirstName       = e.Customer.FirstName,
                        LastName        = e.Customer.LastName,
                        Dob             = e.Customer.Dob
                    },
                    Contact = new ContactReadModel
                    {
                        Email           = e.Contact.Email,
                        Phone           = e.Contact.Phone
                    },
                    Address = new AddressReadModel
                    {
                        State           = e.Address.State,
                        Street          = e.Address.Street,
                        City            = e.Address.City,
                        Hausnumber      = e.Address.Hausnumber,
                        Zip             = e.Address.Zip
                    },
                    Money = new MoneyReadModel
                    {
                        Currency        = e.Money.Currency,
                        Balance         = e.Money.Balance
                    }
                }).ToList();
            }
        }
    }
}
