using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAccount.ApplicationLayer.Models
{
    public class PersonViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid AggregateId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Version { get; set; }

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
    }
}
