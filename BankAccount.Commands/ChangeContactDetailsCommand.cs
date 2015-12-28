using System;
using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Commands
{
    public class ChangeContactDetailsCommand : Command
    {
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public ChangeContactDetailsCommand(Guid id, int version, string email, string phone) 
            : base(id, version)
        {
            this.Email = email;
            this.Phone = phone;
        }
    }
}
