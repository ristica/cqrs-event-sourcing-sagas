using System;

namespace BankAccount.ApplicationLayer.Models
{
    public class BankAccountViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Balance { get; set; }

        public string Currency { get; set; }
    }
}