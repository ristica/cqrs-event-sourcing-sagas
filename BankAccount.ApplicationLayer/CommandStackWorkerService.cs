using System;
using BankAccount.Commands;
using BankAccount.Configuration;
using BankAccount.ValueTypes;
using BankAccount.ViewModels;

namespace BankAccount.ApplicationLayer
{
    public sealed class CommandStackWorkerService
    {
        #region CustomerDomainModel

        public static void AddCustomer(CustomerViewModel vm)
        {
            IoCServiceLocator.Bus.Send(
                new CreateCustomerCommand(
                    Guid.NewGuid(),
                    0,
                    vm.FirstName,
                    vm.LastName,
                    vm.IdCard,
                    vm.IdNumber,
                    vm.Dob,
                    vm.Email,
                    vm.Phone,
                    vm.Street,
                    vm.ZIP,
                    vm.Hausnumber,
                    vm.City,
                    vm.State));
        }

        public static void DeleteCustomer(Guid id)
        {
            IoCServiceLocator.Bus.Send(
                new DeleteCustomerCommand(id, -1));
        }

        public static void EditPersonDetails(PersonViewModel vm)
        {
            IoCServiceLocator.Bus.Send(
                new ChangePersonDetailsCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.FirstName,
                    vm.LastName,
                    vm.IdCard,
                    vm.IdNumber));
        }

        public static void EditContactDetails(ContactViewModel vm)
        {
            IoCServiceLocator.Bus.Send(
                new ChangeContactDetailsCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Email,
                    vm.PhoneNumber));
        }

        public static void EditAddressDetails(AddressViewModel vm)
        {
            IoCServiceLocator.Bus.Send(
                new ChangeAddressDetailsCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Street,
                    vm.Zip,
                    vm.Hausnumber,
                    vm.City,
                    vm.State));
        }

        #endregion

        #region AccountDomainModel

        public static void TransferMoney(TransferViewModel vm)
        {
            IoCServiceLocator.Bus.Send(
                new ChangeBalanceCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Amount));
        }

        public static void AddAccount(AccountViewModel vm)
        {
            IoCServiceLocator.Bus.Send(
                new AddAccountCommand(
                    Guid.NewGuid(),
                    0,
                    vm.CustomerId,
                    vm.Currency,
                    State.Open));
        }

        public static void DeleteAccount(Guid id)
        {
            IoCServiceLocator.Bus.Send(
                new DeleteAccountCommand(id, -1));
        }

        public static void LockAccount(Guid id)
        {
            IoCServiceLocator.Bus.Send(
                new LockAccountCommand(id, -1));
        }

        public static void UnlockAccount(Guid id)
        {
            IoCServiceLocator.Bus.Send(
                new UnlockAccountCommand(id, -1));
        }

        #endregion
    }
}
