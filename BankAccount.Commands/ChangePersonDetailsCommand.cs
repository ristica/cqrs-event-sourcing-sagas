using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class ChangePersonDetailsCommand : Command
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string IdCard { get; private set; }
        public string IdNumber { get; private set; }

        public ChangePersonDetailsCommand(Guid id, int version, string firstName, string lastname, string idCard, string idNumber) 
            : base(id, version)
        {
            this.FirstName      = firstName;
            this.LastName       = lastname;
            this.IdCard         = idCard;
            this.IdNumber       = idNumber;
        }
    }
}
