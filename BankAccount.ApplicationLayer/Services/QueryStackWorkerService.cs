using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.ApplicationLayer.Models;
using BankAccount.Configuration;
using BankAccount.Events;
using BankAccount.Infrastructure;
using EventStore;
using Microsoft.Practices.Unity;

namespace BankAccount.ApplicationLayer.Services
{
    public class QueryStackWorkerService
    {
        public static List<BankAccountViewModel> GetAllBankAccounts()
        {
            var accounts = IoCServiceLocator.QueryStackRepository.GetAccounts();
            return accounts.Select(acc => new BankAccountViewModel
            {
                Id              = acc.AggregateId,
                FirstName       = acc.Customer.FirstName,
                LastName        = acc.Customer.LastName,
                Balance         = acc.Money.Balance,
                Currency        = acc.Money.Currency
            }).ToList();
        }

        public static DetailsBankAccountViewModel GetDetails(Guid id)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetBankAccount(id);
            return new DetailsBankAccountViewModel
            {
                AggregateId     = account.AggregateId,
                Version         = account.Version,
                Balance         = account.Money.Balance,
                LastName        = account.Customer.LastName,
                FirstName       = account.Customer.FirstName,
                IdCard          = account.Customer.IdCard,
                IdNumber        = account.Customer.IdNumber,
                Dob             = account.Customer.Dob,
                Email           = account.Contact.Email,
                Phone           = account.Contact.Phone,
                Hausnumber      = account.Address.Hausnumber,
                State           = account.Address.State,
                Zip             = account.Address.Zip,
                Street          = account.Address.Street,
                City            = account.Address.City,
                Currency        = account.Money.Currency
            };
        }

        public static List<BalanceHistoryViewModel> GetBankAccountHistory(Guid id)
        {
            var commits = IoCServiceLocator.Container.Resolve<IStoreEvents>().Advanced.GetFrom(id, 0, int.MaxValue);
            var transactions = new List<BalanceHistoryViewModel>();
            foreach (var c in commits)
            {
                transactions.AddRange(
                    c.Events
                    .Select(@event => Converter.ChangeTo(@event.Body, @event.Body.GetType()))
                    .OfType<BalanceChangedEvent>()
                    .Select(x => new BalanceHistoryViewModel(x.Amount)));
            }
            return transactions;
        }

        public static CustomerViewModel GetCustomerForBankAccount(Guid id)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetBankAccount(id);
            return new CustomerViewModel
            {
                AggregateId     = account.AggregateId,
                Version         = account.Version,
                LastName        = account.Customer.LastName,
                FirstName       = account.Customer.FirstName,
                IdCard          = account.Customer.IdCard,
                IdNumber        = account.Customer.IdNumber
            };
        }

        public static ContactViewModel GetContactForBankAccount(Guid id)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetBankAccount(id);
            return new ContactViewModel
            {
                AggregateId     = account.AggregateId,
                Version         = account.Version,
                Email           = account.Contact.Email,
                PhoneNumber     = account.Contact.Phone
            };
        }

        public static AddressViewModel GetAddressForBankAccount(Guid id)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetBankAccount(id);
            return new AddressViewModel
            {
                AggregateId     = account.AggregateId,
                Version         = account.Version,
                Hausnumber      = account.Address.Hausnumber,
                State           = account.Address.State,
                Street          = account.Address.Street,
                City            = account.Address.City,
                Zip             = account.Address.Zip
            };
        }

        public static MoneyViewModel GetMoneyForBankAccount(Guid id)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetBankAccount(id);
            return new MoneyViewModel
            {
                AggregateId     = account.AggregateId,
                Version         = account.Version,
                Balance         = account.Money.Balance,
                Currency        = account.Money.Currency
            };
        }
    }
}
