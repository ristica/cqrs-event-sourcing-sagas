using System;

namespace BankAccount.ReadModel
{
    public class AccountReadModel
    {
        public Guid CustomerId { get; set; }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Currency { get; set; }
    }
}
