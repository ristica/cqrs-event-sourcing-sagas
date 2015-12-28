using System;
using BankAccount.Infrastructure.Commanding;
using BankAccount.ValueTypes;

namespace BankAccount.Commands
{
    public class ChangeBankAccountCommand : Command
    {
        public Customer Customer { get; private set; }
        public Contact Contact { get; set; }
        public Address Address { get; private set; } 

        public ChangeBankAccountCommand(
            Guid aggregateId, 
            int version, 
            string firstName, 
            string lastName, 
            string idcard,
            string idNumber,
            DateTime dob,
            string email, 
            string phone,
            string street, 
            string zip, 
            string hausnumber, 
            string city, 
            string state) 
            : base(aggregateId, version)
        {
            this.Customer = new Customer
            {
                LastName = lastName,
                FirstName = firstName,
                IdCard = idcard,
                IdNumber = idNumber,
                Dob = dob
            };
            this.Contact = new Contact
            {
                Email = email,
                PhoneNumber = phone
            };
            this.Address = new Address
            {
                Street = street,
                Hausnumber = hausnumber,
                City = city,
                Zip = zip,
                State = state
            };
        }
    }
}
