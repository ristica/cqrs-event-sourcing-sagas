using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAccount.ApplicationLayer.Models
{
    public class AccountViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid CustomerId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid AggregateId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string FirstName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string LastName { get; set; }

        [Required]
        public string Currency { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CurrentBalance { get; set; }
    }
}
