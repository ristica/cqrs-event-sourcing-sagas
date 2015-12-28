using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.DbModel.Entities
{
    public class Address
    {
        [Column("Street")]
        public string Street { get; set; }

        [Column("ZIP")]
        public string Zip { get; set; }

        [Column("Hausnumber")]
        public string Hausnumber { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("State")]
        public string State { get; set; }
    }
}
