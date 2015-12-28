using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.DbModel.Entities
{
    public class Contact
    {
        [Column("EmailAddress")]
        public string Email { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }
    }
}
