
using System;
using System.ComponentModel.DataAnnotations;
using BankAccount.ValueTypes;

namespace BankAccount.DbModel.Entities
{
    public class AccountEntity
    {
        [Key]
        public Int64 AccountEntityId { get; set; }
        public Guid AggregateId { get; set; }
        public Guid CustomerAggregateId { get; set; }
        public int Version { get; set; }
        public string Currency { get; set; }
        public State AccountState { get; set; }

        public virtual CustomerEntity CustomerEntity { get; set; }
        public Int64 CustomerEntityId { get; set; }
    }
}
