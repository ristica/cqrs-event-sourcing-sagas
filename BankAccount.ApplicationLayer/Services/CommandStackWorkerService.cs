using System;
using BankAccount.ApplicationLayer.Models;
using BankAccount.Commands;
using BankAccount.Configuration;

namespace BankAccount.ApplicationLayer.Services
{
    /// <summary>
    /// here we can decide to go all over the 
    /// command bus / command handler / event bus / event handler
    ///     IoCServiceLocator.CommandBus.Send (....)
    /// or we are going to use process manager (aka Saga) to handle commands / events 
    ///     IoCServiceLocator.SagaBus.Send (....)
    /// </summary>
    public sealed class CommandStackWorkerService
    {
        public static void AddCustomer(CustomerViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
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

        public static void DeleteBankAccount(Guid id, int version)
        {
            IoCServiceLocator.CommandBus.Send(
                new DeleteCustomerCommand(
                    id, 
                    version));
        }

        public static void EditPersonDetails(PersonViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
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
            IoCServiceLocator.CommandBus.Send(
                new ChangeContactDetailsCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Email,
                    vm.PhoneNumber));
        }

        public static void EditAddressDetails(AddressViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
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
            IoCServiceLocator.CommandBus.Send(
                new ChangeBalanceCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Amount));
        }

        public static void AddAccount(AccountViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
                new AddAccountCommand(
                    Guid.NewGuid(),
                    0,
                    vm.CustomerId,
                    vm.Currency));
        }
    }
}
