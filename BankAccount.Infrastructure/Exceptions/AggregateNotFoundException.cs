using System;

namespace BankAccount.Infrastructure.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(string message) : base(message) { }
    }
}
