using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAccount.ViewModels
{
    public class MoneyViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid AggregateId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Version { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public int Balance { get; set; }
    }
}
