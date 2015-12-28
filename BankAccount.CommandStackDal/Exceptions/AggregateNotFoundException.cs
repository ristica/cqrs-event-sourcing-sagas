using System;

namespace BankAccount.CommandStackDal.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException()
        {
        }

        public AggregateNotFoundException(string message) : base(message) { }
    }
}
