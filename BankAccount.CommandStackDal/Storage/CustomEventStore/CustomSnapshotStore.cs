using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.CommandStackDal.Mappings;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.EventDb;
using BankAccount.Infrastructure.Snapshoting;

namespace BankAccount.CommandStackDal.Storage.CustomEventStore
{
    public class CustomSnapshotStore : ISnapshotStore
    {
        public Snapshot GetLastSnapshot(Guid aggregateRootId)
        {
            using (var ctx = new EventDbContext())
            {
                var snapshot = ctx.SnapshotSet.ToList().LastOrDefault(s => s.AggregateId == aggregateRootId);
                if (snapshot == null)
                    return null;

                return Mapper.ConvertSnapshotEntityToSnapshot(snapshot);
            }
        }

        public void SaveSnapshot<TSnapshot>(TSnapshot snapshot) where TSnapshot : Snapshot
        {
            using (var ctx = new EventDbContext())
            {
                ctx.SnapshotSet.Add(new SnapshotEntity
                {
                    Body = snapshot.Body,
                    Sequence = snapshot.LastEventSequence,
                    AggregateId = snapshot.AggregateRootId,
                    EntityType = snapshot.EntityTape
                });
                ctx.SaveChanges();
            }
        }

        public List<Snapshot> GetAllSnapshotsForAggregate(Guid aggregateId)
        {
            using (var ctx = new EventDbContext())
            {
                return Mapper.ConvertSnapshotEntitiesToSnapshots(ctx.SnapshotSet.ToList());
            }
        }
    }
}
