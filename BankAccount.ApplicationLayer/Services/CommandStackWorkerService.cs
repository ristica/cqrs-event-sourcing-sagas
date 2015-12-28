using System;
using BankAccount.ApplicationLayer.Models;
using BankAccount.Commands;
using BankAccount.Configuration;

namespace BankAccount.ApplicationLayer.Services
{
    public class CommandStackWorkerService
    {
        public void AddBankAccount(NewBankAccountViewModel vm)
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

        public void DeleteBankAccount(Guid id)
        {
            var account = IoCServiceLocator.QueryStackRepository.GetBankAccount(id);
            IoCServiceLocator.CommandBus.Send(
                new DeleteBankAccountCommand(id, account.Version));
        }

        public void EditCustomerDetails(CustomerViewModel vm)
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

        public void EditContactDetails(ContactViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
                new ChangeContactDetailsCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Email,
                    vm.PhoneNumber));
        }

        public void EditAddressDetails(AddressViewModel vm)
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

        public void EditMoneyDetails(MoneyViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
                new ChangeCurrencyCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Currency));
        }

        public void TransferMoney(TransferViewModel vm)
        {
            IoCServiceLocator.CommandBus.Send(
                new TransferMoneyCommand(
                    vm.AggregateId,
                    vm.Version,
                    vm.Amount));
        }
    }
}