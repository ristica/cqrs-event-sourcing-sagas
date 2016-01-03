using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.ApplicationLayer.Models
{
    public class CustomerViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "ID Card")]
        public string IdCard { get; set; }

        [Required]
        [Display(Name = "ID Card's number")]
        public string IdNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime Dob { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail address")]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        [MaxLength(20)]
        public string Street { get; set; }

        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        public string ZIP { get; set; }

        [Required]
        [MaxLength(10)]
        public string Hausnumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }
    }
}