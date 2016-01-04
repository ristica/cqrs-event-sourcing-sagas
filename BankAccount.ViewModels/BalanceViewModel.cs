using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAccount.ViewModels
{
    public class BalanceViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Int64 BankAccountId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid AggregateId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string FirstName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string LastName { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public int Amount { get; set; }

        public int Version { get; set; }
    }
}