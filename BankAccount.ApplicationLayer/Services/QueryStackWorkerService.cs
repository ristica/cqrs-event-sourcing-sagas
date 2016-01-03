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
    public sealed class QueryStackWorkerService
    {
        public static List<BankAccountViewModel> GetAllBankAccounts()
        {
            var accounts = IoCServiceLocator.QueryStackRepository.GetCustomers();
            return accounts.Select(acc => new BankAccountViewModel
            {
                Id              = acc.AggregateId,
                FirstName       = acc.FirstName,
                LastName        = acc.LastName
            }).ToList();
        }

        public static DetailsBankAccountViewModel GetDetails(Guid id)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetCustomerById(id);
            return new DetailsBankAccountViewModel
            {
                AggregateId = account.AggregateId,
                Version = account.Version,
                LastName = account.LastName,
                FirstName = account.FirstName,
                IdCard = account.IdCard,
                IdNumber = account.IdNumber,
                Dob = account.Dob,
                Phone = account.Phone,
                Email = account.Email,
                Street = account.Street,
                Hausnumber = account.Hausnumber,
                Zip = account.Zip,
                State = account.State,
                City = account.City
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

        public static PersonViewModel GetPersonForBankAccount(Guid id)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetCustomerById(id);
            return new PersonViewModel
            {
                AggregateId     = account.AggregateId,
                Version         = account.Version,
                LastName        = account.LastName,
                FirstName       = account.FirstName,
                IdCard          = account.IdCard,
                IdNumber        = account.IdNumber
            };
        }

        public static ContactViewModel GetContactForBankAccount(Guid id)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetCustomerById(id);
            return new ContactViewModel
            {
                AggregateId     = account.AggregateId,
                Version         = account.Version,
                Email           = account.Email,
                PhoneNumber     = account.Phone
            };
        }

        public static AddressViewModel GetAddressForBankAccount(Guid id)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetCustomerById(id);
            return new AddressViewModel
            {
                AggregateId     = account.AggregateId,
                Version         = account.Version,
                Hausnumber      = account.Hausnumber,
                State           = account.State,
                Street          = account.Street,
                City            = account.City,
                Zip             = account.Zip
            };
        }

        public static IEnumerable<AccountViewModel> GetAccountsByCustomerId(Guid aggregateId)
        {
            // find all accounts for the customer
            var accounts = IoCServiceLocator.QueryStackRepository.GetAccountsByCustomerId(aggregateId).ToList();

            var list = new List<AccountViewModel>();

            foreach (var acc in accounts)
            {
                // find all events for the 
                var commits = IoCServiceLocator.Container.Resolve<IStoreEvents>().Advanced.GetFrom(acc.Id, 0, int.MaxValue);
                var transactions = new List<int>();
                foreach (var c in commits)
                {
                    transactions.AddRange(
                        c.Events
                        .Select(@event => Converter.ChangeTo(@event.Body, @event.Body.GetType()))
                        .OfType<BalanceChangedEvent>()
                        .Select(x => x.Amount));
                }

                list.Add(new AccountViewModel
                {
                    Currency = acc.Currency,
                    AggregateId = acc.Id,
                    CurrentBalance = transactions.Sum(b => b)
                });
            }

            return list;
        }

        public static TransferViewModel GetAccountById(Guid aggregateId)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetAccountById(aggregateId);
            return new TransferViewModel
            {
                AggregateId = account.Id,
                CustomerId = account.CustomerId
            };
        }
    }
}
