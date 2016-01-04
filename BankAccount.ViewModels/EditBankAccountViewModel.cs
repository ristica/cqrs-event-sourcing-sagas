using System;
using System.Web.Mvc;

namespace BankAccount.ViewModels
{
    public class EditBankAccountViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Int64 BankAccountId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid AggregateId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Version { get; set; }

        public CustomerViewModel Customer { get; set; }
        public ContactViewModel Contact { get; set; }
        public AddressViewModel Address { get; set; }
        public MoneyViewModel Money { get; set; }
    }
}