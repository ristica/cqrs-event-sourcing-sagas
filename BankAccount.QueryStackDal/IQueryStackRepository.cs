using System;
using System.Collections.Generic;
using BankAccount.ViewModels;

namespace BankAccount.QueryStackDal
{
    /// <summary>
    /// only read actions
    /// </summary>
    public interface IQueryStackRepository
    {
        DetailsBankAccountViewModel GetCustomerById(Guid aggregateId);
        IEnumerable<BankAccountViewModel> GetCustomers();
        IEnumerable<AccountViewModel> GetAccountsByCustomerId(Guid customerId);
        TransferViewModel GetAccountById(Guid aggregateId);
    }
}