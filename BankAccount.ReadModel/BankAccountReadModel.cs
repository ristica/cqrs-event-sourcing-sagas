using System;

namespace BankAccount.ReadModel
{
    public class BankAccountReadModel
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public CustomerReadModel Customer { get; set; }
        public ContactReadModel Contact { get; set; }
        public MoneyReadModel Money { get; set; }
        public AddressReadModel Address { get; set; }
    }
}
