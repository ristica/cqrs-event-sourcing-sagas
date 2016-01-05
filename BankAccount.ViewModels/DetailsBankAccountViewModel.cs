using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.ViewModels
{
    public class DetailsBankAccountViewModel
    {
        [Display(Name = "ID")]
        public Guid AggregateId { get; set; }

        public int Version { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Id card")]
        public string IdCard { get; set; }

        [Display(Name = "Id card's number")]
        public string IdNumber { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime Dob { get; set; }

        public string Phone { get; set; }
        [Display(Name = "E-Mail address")]
        public string Email { get; set; }

        public string Street { get; set; }

        public string Zip { get; set; }

        public string Hausnumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
