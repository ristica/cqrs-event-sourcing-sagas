using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.DbModel.Entities
{
    public class SnapshotEntity
    {
        [Key]
        public Int64 SnapshotEntityId { get; set; }

        public Guid AggregateId { get; set; }

        public string EntityType { get; set; }

        public int Sequence { get; set; }

        public string Body { get; set; }
    }
}
