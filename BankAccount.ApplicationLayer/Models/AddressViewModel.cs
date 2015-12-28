using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAccount.ApplicationLayer.Models
{
    public class AddressViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid AggregateId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Version { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Hausnumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }
    }
}
