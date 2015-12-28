using System;
using System.Web.Mvc;
using BankAccount.ApplicationLayer.Models;
using BankAccount.ApplicationLayer.Services;

namespace BankAccount.Client.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly QueryStackWorkerService _queryService;
        private readonly CommandStackWorkerService _commandService;

        #endregion

        #region C-Tor

        public HomeController()
        {
            this._queryService = new QueryStackWorkerService();
            this._commandService = new CommandStackWorkerService();
        }

        #endregion

        #region GET

        public ActionResult Index()
        {
            var model = this._queryService.GetAllBankAccounts();
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var model = this._queryService.GetDetails(id);
            return View(model);
        }

        public ActionResult Add()
        {
            return View(new NewBankAccountViewModel());
        }

        public ActionResult EditCustomer(Guid id)
        {
            var model = this._queryService.GetCustomerForBankAccount(id);
            return View(model);
        }

        public ActionResult EditContact(Guid id)
        {
            var model = this._queryService.GetContactForBankAccount(id);
            return View(model);
        }

        public ActionResult EditAddress(Guid id)
        {
            var model = this._queryService.GetAddressForBankAccount(id);
            return View(model);
        }

        public ActionResult EditMoney(Guid id)
        {
            var model = this._queryService.GetMoneyForBankAccount(id);
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var model = this._queryService.GetDetails(id);
            this._commandService.DeleteBankAccount(model.AggregateId, model.Version);
            return RedirectToAction("Index");
        }

        public ActionResult History(Guid id, string name, string balance)
        {
            var model = this._queryService.GetBankAccountHistory(id);
            ViewBag.AccountName = name;
            ViewBag.CurrentBalance = balance;
            ViewBag.Currency = this._queryService.GetDetails(id).Currency;
            return View(model);
        }

        public ActionResult TransferMoney(Guid id)
        {
            var model = this._queryService.GetMoneyForBankAccount(id);
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

            this._commandService.AddBankAccount(vm);
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

            this._commandService.EditCustomerDetails(vm);
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

            this._commandService.EditContactDetails(vm);
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

            this._commandService.EditAddressDetails(vm);
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

            this._commandService.EditMoneyDetails(vm);
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

            this._commandService.TransferMoney(vm);
            return RedirectToAction("Index");
        }

        #endregion

        #region Helpers

        private bool IsCustomerDirty(CustomerViewModel vm)
        {
            var model = this._queryService.GetCustomerForBankAccount(vm.AggregateId);
            if (model.FirstName.Equals(vm.FirstName.Trim()) &&
                model.LastName.Equals(vm.LastName.Trim()) &&
                model.IdCard.Equals(vm.IdCard.Trim()) && 
                model.IdNumber.Equals(vm.IdNumber.Trim()))
            {
                return false;
            }
            return true;
        }

        private bool IsContactDirty(ContactViewModel vm)
        {
            var model = this._queryService.GetContactForBankAccount(vm.AggregateId);
            if (model.Email.Equals(vm.Email.Trim()) &&
                model.PhoneNumber.Equals(vm.PhoneNumber.Trim()))
            {
                return false;
            }
            return true;
        }

        private bool ÎsAddressDirty(AddressViewModel vm)
        {
            var model = this._queryService.GetAddressForBankAccount(vm.AggregateId);
            if (model.City.Equals(vm.City.Trim()) && 
                model.Hausnumber.Equals(vm.Hausnumber.Trim()) &&
                model.State.Equals(vm.State.Trim()) &&
                model.Street.Equals(vm.Street.Trim()) &&
                model.Zip.Equals(vm.Zip.Trim()))
            {
                return false;
            }
            return true;
        }

        private bool IsMoneyDirty(MoneyViewModel vm)
        {
            var model = this._queryService.GetMoneyForBankAccount(vm.AggregateId);
            if (model.Currency.Equals(vm.Currency.Trim()))
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
