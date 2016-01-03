using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.DbModel.Entities
{
    public class CustomerEntity
    {
        [Key]
        public Int64 CustomerEntityId { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<AccountEntity> Accounts { get; set; } 
    }
}
