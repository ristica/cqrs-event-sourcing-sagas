
using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.DbModel.Entities
{
    public class AccountEntity
    {
        [Key]
        public Guid AccountEntityId { get; set; }
        public Money Money { get; set; }

        public virtual CustomerEntity CustomerEntity { get; set; }
        public Guid CustomerEntityId { get; set; }
    }
}
