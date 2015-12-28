using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.DbModel.Entities
{
    public class Customer
    {
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("IdCard")]
        public string IdCard { get; set; }

        [Column("IdCardNumber")]
        public string IdNumber { get; set; }

        [Column("DateOfBirth")]
        public DateTime Dob { get; set; }
    }
}
