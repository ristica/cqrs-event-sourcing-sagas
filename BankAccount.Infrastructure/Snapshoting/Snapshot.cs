using System;

namespace BankAccount.Infrastructure.Snapshoting
{
    public class Snapshot
    {
        public Guid AggregateRootId { get; set; }
        public int LastEventSequence { get; set; }
        public string EntityTape { get; set; }
        public string Body { get; set; }
    }
}