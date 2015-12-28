using System;
using System.Web.Script.Serialization;
using BankAccount.Domain.Memento;
using BankAccount.Events;
using BankAccount.Infrastructure.Domain;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.Infrastructure.Memento;
using BankAccount.Infrastructure.Snapshoting;
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
        IHandle<BalanceChangedEvent>,
        IOriginator,
        ISnapshotOriginator
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
            ApplyChange(new BankAccountCreatedEvent
            {
                AggregateId = id,
                Money = new Money
                {
                    Balance = balance,
                    Currency = currency
                },
                Version = this.Version,
                Address = new Address
                {
                    Street = street,
                    Hausnumber = hausnumber,
                    State = state,
                    City = city,
                    Zip = zip
                },
                Customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    IdCard = idCard,
                    IdNumber = idNumber,
                    Dob = dob
                },
                Contact = new Contact
                {
                    Email = email,
                    PhoneNumber = phone
                }
            });
        }

        public void ChangeCustomer(string firstName, string lastName, string idCard, string idNumber)
        {
            ApplyChange(new CustomerChangedEvent
            {
                AggregateId = this.Id,
                Version = this.Version,
                FirstName = firstName,
                LastName = lastName,
                IdCard = idCard,
                IdNumber = idNumber
            });
        }

        public void ChangeContact(string email, string phone)
        {
            ApplyChange(new ContactChangedEvent
            {
                AggregateId = this.Id,
                Version = this.Version,
                Email = email,
                Phone = phone
            });
        }

        public void ChangeBalance(int amount)
        {
            ApplyChange(new BalanceChangedEvent
            {
                AggregateId = this.Id,
                Version = this.Version,
                Amount = amount
            });
        }

        public void ChangeCurrency(string currency)
        {
            ApplyChange(new CurrencyChangedEvent
            {
                AggregateId = this.Id,
                Version = this.Version,
                Currency = currency
            });
        }

        public void Delete()
        {
            ApplyChange(new BankAccountDeletedEvent
            {
                AggregateId = this.Id,
                Version = this.Version
            });
        }

        public void ChangeAddress(string street, string hausnumber, string zip, string city, string state)
        {
            ApplyChange(
                new AddressChangedEvent
                {
                    AggregateId = this.Id,
                    Version = this.Version,
                    Street = street,
                    Hausnumber = hausnumber,
                    Zip = zip,
                    City = city,
                    State = state
                });
        }

        #endregion

        #region IHandle implementation of events

        public void Handle(BankAccountCreatedEvent e)
        {
            this.Id = e.AggregateId;
            this.Version = e.Version;

            this.Customer = e.Customer;
            this.Contact = e.Contact;
            this.Address = e.Address;
            this.Money = e.Money;
        }

        public void Handle(CustomerChangedEvent e)
        {
            this.Version = e.Version;

            this.Customer.FirstName = e.FirstName;
            this.Customer.LastName = e.LastName;
            this.Customer.IdCard = e.IdCard;
            this.Customer.IdNumber = e.IdNumber;
        }

        public void Handle(ContactChangedEvent e)
        {
            this.Version = e.Version;

            this.Contact.Email = e.Email;
            this.Contact.PhoneNumber = e.Phone;
        }

        public void Handle(BankAccountDeletedEvent e)
        {
            this.Version = e.Version;
        }

        public void Handle(BalanceChangedEvent e)
        {
            this.Version = e.Version;

            this.Money.Balance = this.Money.Balance + e.Amount;
        }

        public void Handle(CurrencyChangedEvent e)
        {
            this.Version = e.Version;

            this.Money.Currency = e.Currency;
        }

        public void Handle(AddressChangedEvent e)
        {
            this.Version = e.Version;

            this.Address.Street = e.Street;
            this.Address.Hausnumber = e.Hausnumber;
            this.Address.Zip = e.Zip;
            this.Address.City = e.City;
            this.Address.State = e.State;
        }

        #endregion

        #region IOriginator

        public BaseMemento GetMemento()
        {
            return new BankAccountMemento(Id, Version, Customer, Contact, Money, Address);
        }

        public void SetMemento(BaseMemento memento)
        {
            Version = memento.Version;
            Id = memento.Id;
            Customer = ((BankAccountMemento) memento).Customer;
            Address = ((BankAccountMemento) memento).Address;
            Money = ((BankAccountMemento) memento).Money;
            Contact = ((BankAccountMemento) memento).Contact;
        }

        #endregion

        #region ISnapshotOriginator

        public Snapshot GetCurrentSnapshot(int? lastEventVersion)
        {
            this.Version = lastEventVersion ?? 0;
            return new Snapshot
            {
                AggregateRootId = this.Id,
                Body = new JavaScriptSerializer().Serialize(this),
                EntityTape = this.ToString()
            };
        }

        public void LoadSnapshot(Snapshot snapshot)
        {
            throw new NotImplementedException();
        }

        public bool ShouldTakeSnapshot()
        {
            return true;
        }

        #endregion
    }
}
