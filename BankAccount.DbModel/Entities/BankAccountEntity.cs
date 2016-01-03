using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.DbModel.Entities
{
    public class BankAccountEntity
    {
        [Key]
        public Int64 BankAccountEntityId { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<BankAccountBalanceEntity> Accounts { get; set; } 
    }
}
