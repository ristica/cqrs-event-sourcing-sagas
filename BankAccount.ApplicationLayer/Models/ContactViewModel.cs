using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAccount.ApplicationLayer.Models
{
    public class ContactViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid AggregateId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Version { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail address")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
