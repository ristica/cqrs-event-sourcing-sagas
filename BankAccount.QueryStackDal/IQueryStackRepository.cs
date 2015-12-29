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
        BankAccountReadModel GetBankAccount(Guid aggregateId);
        IEnumerable<BankAccountReadModel> GetAccounts();
    }
}