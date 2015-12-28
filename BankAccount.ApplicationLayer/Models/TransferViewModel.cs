using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BankAccount.ApplicationLayer.Models
{
    public class TransferViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid AggregateId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Version { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}
