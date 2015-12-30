using System;
using BankAccount.Events;
using BankAccount.Infrastructure.Domain;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.ValueTypes;

namespace BankAccount.Domain
{
    public class BankAccount : AggregateRoot,
        IHandle<BankAccountCreatedEvent>,
        IHandle<BankAccountDeletedEvent>,
        IHandle<CustomerChangedEvent>,
        IHandle<ContactChangedEvent>,
        IHandle<AddressChangedEvent>,
        IHandle<CurrencyChangedEvent>,
        IHandle<BalanceChangedEvent>
    {
        #region Properties

        public Customer Customer { get; set; }
        public Contact Contact { get; set; }
        public Money Money { get; set; }
        public Address Address { get; set; }

        #endregion

        #region Public methods called by command handlers

        public void CreateNewBankAccount(
            Guid id, 
            string firstName, 
            string lastName, 
            string idCard,
            string idNumber,
            DateTime dob,
            string email, 
            string phone,
            int balance,
            string currency,
            string street,
            string zip,
            string hausnumber,
            string city,
            string state)
        {
            ApplyChange(
                new BankAccountCreatedEvent
                {
                    AggregateId         = id,
                    Version             = this.Version,
                    Customer = new Customer
                    {
                        FirstName       = firstName,
                        LastName        = lastName,
                        IdCard          = idCard,
                        IdNumber        = idNumber,
                        Dob             = dob
                    },
                    Contact = new Contact
                    {
                        Email           = email,
                        PhoneNumber     = phone
                    },
                    Address = new Address
                    {
                        Street          = street,
                        Hausnumber      = hausnumber,
                        State           = state,
                        City            = city,
                        Zip             = zip
                    },
                    Money = new Money
                    {
                        Balance         = balance,
                        Currency        = currency
                    }
                });
        }

        public void ChangeCustomer(
            string firstName, 
            string lastName, 
            string idCard, 
            string idNumber)
        {
            ApplyChange(
                new CustomerChangedEvent
                {
                    AggregateId         = this.Id,
                    Version             = this.Version,
                    FirstName           = firstName,
                    LastName            = lastName,
                    IdCard              = idCard,
                    IdNumber            = idNumber
                });
        }

        public void ChangeContact(
            string email, 
            string phone)
        {
            ApplyChange(
                new ContactChangedEvent
                {
                    AggregateId         = this.Id,
                    Version             = this.Version,
                    Email               = email,
                    Phone               = phone
                });
        }

        public void ChangeAddress(
            string street, 
            string hausnumber, 
            string zip, 
            string city, 
            string state)
        {
            ApplyChange(
                new AddressChangedEvent
                {
                    AggregateId         = this.Id,
                    Version             = this.Version,
                    Street              = street,
                    Hausnumber          = hausnumber,
                    Zip                 = zip,
                    City                = city,
                    State               = state
                });
        }

        public void ChangeBalance(
            int amount)
        {
            ApplyChange(
                new BalanceChangedEvent
                {
                    AggregateId         = this.Id,
                    Version             = this.Version,
                    Amount              = amount
                });
        }

        public void ChangeCurrency(
            string currency)
        {
            ApplyChange(
                new CurrencyChangedEvent
                {
                    AggregateId         = this.Id,
                    Version             = this.Version,
                    Currency            = currency
                });
        }

        public void DeleteBankAccount()
        {
            ApplyChange(
                new BankAccountDeletedEvent
                {
                    AggregateId         = this.Id,
                    Version             = this.Version
                });
        }

        #endregion

        #region IHandle implementation of events

        public void Handle(BankAccountCreatedEvent e)
        {
            this.Id                     = e.AggregateId;
            this.Version                = e.Version;
            this.Customer               = e.Customer;
            this.Contact                = e.Contact;
            this.Address                = e.Address;
            this.Money                  = e.Money;
        }

        public void Handle(CustomerChangedEvent e)
        {
            this.Version                = e.Version;
            this.Customer.FirstName     = e.FirstName;
            this.Customer.LastName      = e.LastName;
            this.Customer.IdCard        = e.IdCard;
            this.Customer.IdNumber      = e.IdNumber;
        }

        public void Handle(ContactChangedEvent e)
        {
            this.Version                = e.Version;
            this.Contact.Email          = e.Email;
            this.Contact.PhoneNumber    = e.Phone;
        }

        public void Handle(AddressChangedEvent e)
        {
            this.Version                = e.Version;
            this.Address.Street         = e.Street;
            this.Address.Hausnumber     = e.Hausnumber;
            this.Address.Zip            = e.Zip;
            this.Address.City           = e.City;
            this.Address.State          = e.State;
        }

        public void Handle(BalanceChangedEvent e)
        {
            this.Version                = e.Version;
            this.Money.Balance          = this.Money.Balance + e.Amount;
        }

        public void Handle(CurrencyChangedEvent e)
        {
            this.Version                = e.Version;
            this.Money.Currency         = e.Currency;
        }

        public void Handle(BankAccountDeletedEvent e)
        {
            this.Version                = e.Version;
        }

        #endregion

        #region Factory

        public static class Factory
        {
            public static BankAccount CreateNewInstance(
                Guid id,
                int version,
                string firstName,
                string lastName,
                string idCard,
                string idNumber,
                DateTime dob,
                string email,
                string phone,
                int balance,
                string currency,
                string street,
                string zip,
                string hausnumber,
                string city,
                string state)
            {
                var @event = new BankAccountCreatedEvent
                {
                    AggregateId         = id,
                    Version             = version,
                    Customer = new Customer
                    {
                        FirstName       = firstName,
                        LastName        = lastName,
                        IdCard          = idCard,
                        IdNumber        = idNumber,
                        Dob             = dob
                    },
                    Contact = new Contact
                    {
                        Email           = email,
                        PhoneNumber     = phone
                    },
                    Address = new Address
                    {
                        Street          = street,
                        Hausnumber      = hausnumber,
                        State           = state,
                        City            = city,
                        Zip             = zip
                    },
                    Money = new Money
                    {
                        Balance         = balance,
                        Currency        = currency
                    }
                };
                var ba = new BankAccount();
                ba.ApplyChange(@event);
                return ba;
            }
        }

        #endregion
    }
}
