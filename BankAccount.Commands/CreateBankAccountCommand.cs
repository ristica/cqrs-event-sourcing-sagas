using System;
using BankAccount.Infrastructure.Commanding;
using BankAccount.ValueTypes;

namespace BankAccount.Commands
{
    public class CreateBankAccountCommand : Command
    {
        public Contact Contact { get; private set; }
        public Customer Customer { get; private set; }
        public Money Money { get; private set; }
        public Address Address { get; private set; }

        public CreateBankAccountCommand(
            Guid aggregateId, 
            int version, 
            string firstName, 
            string lastName, 
            string idCard,
            string idNumber,
            DateTime dob, 
            string email,
            string phone,
            string currency,
            string street,
            string zip,
            string hausnumber,
            string city,
            string state) 
            : base(aggregateId, version)
        {
            this.Customer = new Customer
            {
                FirstName       = firstName,
                LastName        = lastName,
                IdCard          = idCard,
                IdNumber        = idNumber,
                Dob             = dob
            };
            this.Contact = new Contact
            {
                Email           = email,
                PhoneNumber     = phone
            };
            this.Money = new Money
            {
                Balance         = 0,
                Currency        = currency
            };
            this.Address = new Address
            {
                State           = state,
                Hausnumber      = hausnumber,
                City            = city,
                Zip             = zip,
                Street          = street
            };
        }
    }
}
