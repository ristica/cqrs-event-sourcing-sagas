using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class ChangeAddressDetailsCommand : Command
    {
        public string Street { get; private set; }
        public string Zip { get; private set; }
        public string Hausnumber { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        public ChangeAddressDetailsCommand(Guid id, int version, string street, string zip, string hausnumber, string city, string state) 
            : base(id, version)
        {
            this.City           = city;
            this.Hausnumber     = hausnumber;
            this.State          = state;
            this.Street         = street;
            this.Zip            = zip;
        }
    }
}
