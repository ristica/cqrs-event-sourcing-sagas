
using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.DbModel.Entities
{
    public class BankAccountBalanceEntity
    {
        [Key]
        public Guid BankAccountBalanceEntityId { get; set; }
        public Money Money { get; set; }

        public virtual BankAccountEntity BankAccountEntity { get; set; }
        public Guid BankAccountEntityId { get; set; }
    }
}
