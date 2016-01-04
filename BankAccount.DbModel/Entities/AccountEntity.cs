
using System;
using System.ComponentModel.DataAnnotations;
using BankAccount.ValueTypes;

namespace BankAccount.DbModel.Entities
{
    public class AccountEntity
    {
        [Key]
        public Guid AggregateId { get; set; }
        
        public int Version { get; set; }
        public string Currency { get; set; }
        public State AccountState { get; set; }

        public Guid CustomerAggregateId { get; set; }
    }
}
