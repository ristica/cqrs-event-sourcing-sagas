using System;
using System.Web.Mvc;
using BankAccount.ApplicationLayer.Models;
using BankAccount.ApplicationLayer.Services;

namespace BankAccount.Client.Controllers
{
    public class HomeController : Controller
    {
        #region GET

        public ActionResult Index()
        {
            var model = QueryStackWorkerService.GetAllBankAccounts();
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var model = QueryStackWorkerService.GetDetails(id);
            return View(model);
        }

        public ActionResult Add()
        {
            return View(new NewBankAccountViewModel());
        }

        public ActionResult EditCustomer(Guid id)
        {
            var model = QueryStackWorkerService.GetCustomerForBankAccount(id);
            return View(model);
        }

        public ActionResult EditContact(Guid id)
        {
            var model = QueryStackWorkerService.GetContactForBankAccount(id);
            return View(model);
        }

        public ActionResult EditAddress(Guid id)
        {
            var model = QueryStackWorkerService.GetAddressForBankAccount(id);
            return View(model);
        }

        public ActionResult EditMoney(Guid id)
        {
            var model = QueryStackWorkerService.GetMoneyForBankAccount(id);
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var model = QueryStackWorkerService.GetDetails(id);
            CommandStackWorkerService.DeleteBankAccount(model.AggregateId, model.Version);
            return RedirectToAction("Index");
        }

        public ActionResult History(Guid id, string name, string balance)
        {
            var model = QueryStackWorkerService.GetBankAccountHistory(id);
            ViewBag.AccountName = name;
            ViewBag.CurrentBalance = balance;
            ViewBag.Currency = QueryStackWorkerService.GetDetails(id).Currency;
            return View(model);
        }

        public ActionResult TransferMoney(Guid id)
        {
            var model = QueryStackWorkerService.GetMoneyForBankAccount(id);
            return View(new TransferViewModel
            {
                Version = model.Version,
                AggregateId = model.AggregateId,
                Amount = 0
            });
        }

        #endregion

        #region POST
 
        [HttpPost]
        public ActionResult Add(NewBankAccountViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            CommandStackWorkerService.AddBankAccount(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditCustomer(CustomerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!this.IsCustomerDirty(vm))
            {
                return RedirectToAction("Index");
            }

            CommandStackWorkerService.EditCustomerDetails(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditContact(ContactViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!this.IsContactDirty(vm))
            {
                return RedirectToAction("Index");
            }

            CommandStackWorkerService.EditContactDetails(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditAddress(AddressViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!this.ÎsAddressDirty(vm))
            {
                return RedirectToAction("Index");
            }

            CommandStackWorkerService.EditAddressDetails(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditMoney(MoneyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!this.IsMoneyDirty(vm))
            {
                return RedirectToAction("Index");
            }

            CommandStackWorkerService.EditMoneyDetails(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult TransferMoney(TransferViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Amount == 0)
            {
                return RedirectToAction("Index");
            }

            CommandStackWorkerService.TransferMoney(vm);
            return RedirectToAction("Index");
        }

        #endregion

        #region Helpers

        private bool IsCustomerDirty(CustomerViewModel vm)
        {
            var model = QueryStackWorkerService.GetCustomerForBankAccount(vm.AggregateId);
            return !model.FirstName.Equals(vm.FirstName.Trim())     || 
                   !model.LastName.Equals(vm.LastName.Trim())       || 
                   !model.IdCard.Equals(vm.IdCard.Trim())           || 
                   !model.IdNumber.Equals(vm.IdNumber.Trim());
        }

        private bool IsContactDirty(ContactViewModel vm)
        {
            var model = QueryStackWorkerService.GetContactForBankAccount(vm.AggregateId);
            return !model.Email.Equals(vm.Email.Trim())             || 
                   !model.PhoneNumber.Equals(vm.PhoneNumber.Trim());
        }

        private bool ÎsAddressDirty(AddressViewModel vm)
        {
            var model = QueryStackWorkerService.GetAddressForBankAccount(vm.AggregateId);
            return !model.City.Equals(vm.City.Trim())               || 
                   !model.Hausnumber.Equals(vm.Hausnumber.Trim())   || 
                   !model.State.Equals(vm.State.Trim())             || 
                   !model.Street.Equals(vm.Street.Trim())           || 
                   !model.Zip.Equals(vm.Zip.Trim());
        }

        private bool IsMoneyDirty(MoneyViewModel vm)
        {
            var model = QueryStackWorkerService.GetMoneyForBankAccount(vm.AggregateId);
            return !model.Currency.Equals(vm.Currency.Trim());
        }

        #endregion
    }
}
