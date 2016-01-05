using System;
using BankAccount.Commands;
using BankAccount.Configuration;
using BankAccount.ViewModels;

namespace BankAccount.ApplicationLayer
{
    /// <summary>
    /// here we can decide to go all over the 
    /// command bus / command handler / event bus / event handler
    ///     IoCServiceLocator.SagaBus.Send (....)
    /// or we are going to use process manager (aka Saga) to handle commands / events 
    ///     IoCServiceLocator.SagaBus.Send (....)
    /// </summary>
    public sealed class CommandStackWorkerService
    {
        public static void AddCustomer(CustomerViewModel vm)
        {
            IoCServiceLocator.SagaBus.Send(
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
            IoCServiceLocator.SagaBus.Send(
                new DeleteCustomerCommand(id, -1));
        }

        public static void EditPersonDetails(PersonViewModel vm)
        {
            IoCServiceLocator.SagaBus.Send(
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
            IoCServiceLocator.SagaBus.Send(
                new ChangeContactDetailsCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Email,
                    vm.PhoneNumber));
        }

        public static void EditAddressDetails(AddressViewModel vm)
        {
            IoCServiceLocator.SagaBus.Send(
                new ChangeAddressDetailsCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Street,
                    vm.Zip,
                    vm.Hausnumber,
                    vm.City,
                    vm.State));
        }

        public static void TransferMoney(TransferViewModel vm)
        {
            IoCServiceLocator.SagaBus.Send(
                new ChangeBalanceCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Amount));
        }

        public static void AddAccount(AccountViewModel vm)
        {
            IoCServiceLocator.SagaBus.Send(
                new AddAccountCommand(
                    Guid.NewGuid(),
                    0,
                    vm.CustomerId,
                    vm.Currency));
        }

        public static void DeleteAccount(Guid id)
        {
            IoCServiceLocator.SagaBus.Send(
                new DeleteAccountCommand(id, -1));
        }

        public static void LockAccount(Guid id)
        {
            IoCServiceLocator.SagaBus.Send(
                new LockAccountCommand(id, -1));
        }

        public static void UnlockAccount(Guid id)
        {
            IoCServiceLocator.SagaBus.Send(
                new UnlockAccountCommand(id, -1));
        }
    }
}
