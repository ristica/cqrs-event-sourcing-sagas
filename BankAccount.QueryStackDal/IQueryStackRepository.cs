using System;
using System.Collections.Generic;
using BankAccount.ReadModel;

namespace BankAccount.QueryStackDal
{
    public interface IQueryStackRepository
    {
        BankAccountReadModel GetBankAccount(Guid aggregateId);
        IEnumerable<BankAccountReadModel> GetAccounts();
    }
}