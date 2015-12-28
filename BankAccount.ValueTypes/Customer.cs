using System;

namespace BankAccount.ValueTypes
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string IdCard { get; set; }
        public string IdNumber { get; set; }
    }
}
