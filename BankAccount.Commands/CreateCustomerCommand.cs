using System;
using BankAccount.Infrastructure.Commanding;
using BankAccount.ValueTypes;

namespace BankAccount.Commands
{
    public class CreateCustomerCommand : Command
    {
        public Contact Contact { get; private set; }
        public Person Person { get; private set; }
        public Address Address { get; private set; }

        public CreateCustomerCommand(
            Guid aggregateId, 
            int version, 
            string firstName, 
            string lastName, 
            string idCard,
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
            this.Person  = new Person(firstName, lastName, dob, idCard, idNumber);
            this.Contact = new Contact(email, phone);
            this.Address = new Address(street, zip, hausnumber, city, state);
        }
    }
}
