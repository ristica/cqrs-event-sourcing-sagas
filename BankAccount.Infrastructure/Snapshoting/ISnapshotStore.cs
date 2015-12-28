using System;
using System.Collections.Generic;

namespace BankAccount.Infrastructure.Snapshoting
{
    public interface ISnapshotStore
    {
        Snapshot GetLastSnapshot(Guid aggregateId);
        void SaveSnapshot<TSnapshot>(TSnapshot snapshot) where TSnapshot : Snapshot;
        List<Snapshot> GetAllSnapshotsForAggregate(Guid aggregateId);
    }
}