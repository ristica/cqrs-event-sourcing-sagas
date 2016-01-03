using System;

namespace BankAccount.ApplicationLayer.Models
{
    public class DetailsBankAccountViewModel
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdCard { get; set; }
        public string IdNumber { get; set; }
        public DateTime Dob { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string Hausnumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
