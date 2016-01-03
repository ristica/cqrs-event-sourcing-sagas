using System;
using System.Collections.Generic;
using BankAccount.ReadModel;

namespace BankAccount.QueryStackDal
{
    /// <summary>
    /// only read actions
    /// </summary>
    public interface IQueryStackRepository
    {
        CustomerReadModel GetCustomerById(Guid aggregateId);
        IEnumerable<CustomerReadModel> GetCustomers();
        IEnumerable<AccountReadModel> GetAccountsByCustomerId(Guid customerId);
        AccountReadModel GetAccountById(Guid aggregateId);
    }
}