using System;
using System.Collections.Generic;
using BankAccount.Infrastructure.Domain;

namespace BankAccount.Infrastructure.Storage
{
    public interface IEventStore
    {
        IEnumerable<object> GetEvents(Guid aggregateId);
        int? GetLastEventVersion(Guid aggregateId);
        void Save(AggregateRoot aggregate);
        IEnumerable<object> GetEvents(Guid aggregateId, int version);
    }
}
