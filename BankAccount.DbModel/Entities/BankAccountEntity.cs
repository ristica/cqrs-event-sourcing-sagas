using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.DbModel.Entities
{
    public class BankAccountEntity
    {
        [Key]
        public Int64 BankAccountEntityId { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Contact Contact { get; set; } 
        public virtual Money Money { get; set; }
        public virtual Address Address { get; set; }
    }
}
