using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BankAccount.ApplicationLayer.Models
{
    public class AccountViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid BankAccountId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string FirstName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string LastName { get; set; }

        [Required]
        public string Currency { get; set; }
    }
}
