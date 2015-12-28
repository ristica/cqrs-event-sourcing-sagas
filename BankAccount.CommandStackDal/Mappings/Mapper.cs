using System.Collections.Generic;
using System.Linq;
using BankAccount.DbModel.Entities;
using BankAccount.Infrastructure.Snapshoting;

namespace BankAccount.CommandStackDal.Mappings
{
    public class Mapper
    {
        public static Snapshot ConvertSnapshotEntityToSnapshot(SnapshotEntity e)
        {
            return new Snapshot
            {
                Body = e.Body,
                AggregateRootId = e.AggregateId,
                LastEventSequence = e.Sequence,
                EntityTape = e.EntityType
            };
        }

        public static List<Snapshot> ConvertSnapshotEntitiesToSnapshots(IEnumerable<SnapshotEntity> entities)
        {
            return entities.Select(e => new Snapshot
            {
                AggregateRootId = e.AggregateId,
                LastEventSequence = e.Sequence,
                Body = e.Body,
                EntityTape = e.EntityType
            }).ToList();
        }
    }
}
