using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.DbModel.Entities
{
    public class Money
    {
        [Column("Currency")]
        public string Currency { get; set; }

        [Column("Balance")]
        public int Balance { get; set; }
    }
}
