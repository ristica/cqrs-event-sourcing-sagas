using System;

namespace BankAccount.Infrastructure.Domain
{
    public class BaseMemento
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
