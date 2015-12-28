using System;

namespace BankAccount.Infrastructure.Eventing
{
    [Serializable]
    public class Event : IEvent
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
    }
}
