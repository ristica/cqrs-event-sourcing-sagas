using System;
using BankAccount.ApplicationLayer.Models;
using BankAccount.Commands;
using BankAccount.Configuration;

namespace BankAccount.ApplicationLayer.Services
{
    public sealed class CommandStackWorkerService
    {
        public static void AddBankAccount(NewBankAccountViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
                new CreateBankAccountCommand(
                    Guid.NewGuid(), 
                    0, 
                    vm.FirstName, 
                    vm.LastName, 
                    vm.IdCard,
                    vm.IdNumber,
                    vm.Dob, 
                    vm.Email,
                    vm.Phone,
                    vm.Currency,
                    vm.Street,
                    vm.ZIP,
                    vm.Hausnumber,
                    vm.City,
                    vm.State));
        }

        public static void DeleteBankAccount(Guid id, int version)
        {
            IoCServiceLocator.CommandBus.Send(
                new DeleteBankAccountCommand(
                    id, 
                    version));
        }

        public static void EditCustomerDetails(CustomerViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
                new ChangeCustomerDetailsCommand(
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

        public static void EditMoneyDetails(MoneyViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
                new ChangeCurrencyCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Currency));
        }

        public static void TransferMoney(TransferViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
                new TransferMoneyCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Amount));
        }
    }
}
