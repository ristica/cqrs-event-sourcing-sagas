using System;
using System.Linq;
using System.Web.Mvc;
using BankAccount.ApplicationLayer.Models;
using BankAccount.ApplicationLayer.Services;

namespace BankAccount.Client.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private static UserViewModel _user;

        #endregion

        #region GET

        public ActionResult LogIn()
        {
            return View(new UserViewModel());
        }

        public ActionResult Index()
        {
            var model = QueryStackWorkerService.GetAllBankAccounts();
            ViewBag.User = _user;
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var model = QueryStackWorkerService.GetDetails(id);
            var accounts = QueryStackWorkerService.GetAccountsByCustomerId(model.AggregateId);
            ViewBag.User = _user;
            ViewBag.Accounts = accounts;
            return View(model);
        }

        public ActionResult Add()
        {
            ViewBag.User = _user;
            return View(new CustomerViewModel());
        }

        public ActionResult Create(Guid customerId)
        {
            ViewBag.User = _user;
            return View(new AccountViewModel { CustomerId = customerId });
        }

        public ActionResult EditCustomer(Guid id)
        {
            ViewBag.User = _user;
            var model = QueryStackWorkerService.GetPersonForBankAccount(id);
            return View(model);
        }

        public ActionResult EditContact(Guid id)
        {
            ViewBag.User = _user;
            var model = QueryStackWorkerService.GetContactForBankAccount(id);
            return View(model);
        }

        public ActionResult EditAddress(Guid id)
        {
            ViewBag.User = _user;
            var model = QueryStackWorkerService.GetAddressForBankAccount(id);
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var model = QueryStackWorkerService.GetDetails(id);
            CommandStackWorkerService.DeleteBankAccount(model.AggregateId, model.Version);
            return RedirectToAction("Index");
        }

        public ActionResult History(Guid id, string name, Guid customerId, string currency)
        {
            ViewBag.AccountName = name;
            var model = QueryStackWorkerService.GetBankAccountHistory(id);
            ViewBag.CurrentBalance = model.Sum(b => b.Amount);
            ViewBag.CustomerId = customerId;
            ViewBag.AccountId = id;
            ViewBag.Currency = currency;
            return View(model);
        }

        public ActionResult TransferMoney(Guid aggregateId)
        {
            var account = QueryStackWorkerService.GetAccountById(aggregateId);
            return View(account);
        }

        #endregion

        #region POST

        [HttpPost]
        public ActionResult Index(string Account)
        {
            return RedirectToAction("Details", new { id = new Guid(Account) });
        }

        [HttpPost]
        public ActionResult LogIn(UserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            _user = new UserViewModel
            {
                EmployeeId = vm.EmployeeId,
                Name = "Mr. Pingo",
                UserName = vm.UserName
            };
            return RedirectToAction("Index");
        }
 
        [HttpPost]
        public ActionResult Add(CustomerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            CommandStackWorkerService.AddCustomer(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(AccountViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            CommandStackWorkerService.AddAccount(vm);
            return RedirectToAction("Details", new { id = vm.CustomerId });
        }

        [HttpPost]
        public ActionResult EditCustomer(PersonViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!this.IsPersonDirty(vm))
            {
                return RedirectToAction("Details", new { id = vm.AggregateId });
            }

            CommandStackWorkerService.EditPersonDetails(vm);
            return RedirectToAction("Details", new { id = vm.AggregateId });
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
                return RedirectToAction("Details", new { id = vm.AggregateId });
            }

            CommandStackWorkerService.EditContactDetails(vm);
            return RedirectToAction("Details", new { id = vm.AggregateId });
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
                return RedirectToAction("Details", new { id = vm.AggregateId });
            }

            CommandStackWorkerService.EditAddressDetails(vm);
            return RedirectToAction("Details", new { id = vm.AggregateId });
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
            return RedirectToAction("Details", new { id = vm.CustomerId });
        }

        #endregion

        #region Helpers

        private bool IsPersonDirty(PersonViewModel vm)
        {
            var model = QueryStackWorkerService.GetPersonForBankAccount(vm.AggregateId);
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

        #endregion
    }
}
