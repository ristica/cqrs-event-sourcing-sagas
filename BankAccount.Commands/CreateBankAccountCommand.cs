using System;
using BankAccount.Infrastructure.Commanding;
using BankAccount.ValueTypes;
using Microsoft.SqlServer.Server;

namespace BankAccount.Commands
{
    public class CreateBankAccountCommand : Command
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Dob { get; private set; }
        public string IdCard { get; private set; }
        public string IdNumber { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
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
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Dob = dob;
            this.IdCard = idCard;
            this.IdNumber = idNumber;
            this.Email = email;
            this.Phone = phone;
            this.Money = new Money
            {
                Balance = 0,
                Currency = currency
            };
            this.Address = new Address
            {
                State = state,
                Hausnumber = hausnumber,
                City = city,
                Zip = zip,
                Street = street
            };
        }
    }
}
