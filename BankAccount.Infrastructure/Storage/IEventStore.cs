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
        T GetMemento<T>(Guid aggregateId) where T: BaseMemento;
        void SaveMemento(BaseMemento memento);
        IEnumerable<object> GetEvents(Guid aggregateId, int version);
    }
}
