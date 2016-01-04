using System;

namespace BankAccount.Infrastructure.Eventing
{
    public class DomainEvent : Message
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; private set; }

        public DomainEvent()
        {
            this.TimeStamp = DateTime.Now;
        }
    }
}
