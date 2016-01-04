using System;
using BankAccount.ValueTypes;

namespace BankAccount.ReadModel
{
    public class AccountReadModel
    {
        public Guid CustomerId { get; set; }
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Currency { get; set; }
        public State State { get; set; }
    }
}
