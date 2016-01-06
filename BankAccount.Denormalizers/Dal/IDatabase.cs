using System;
using BankAccount.DbModel.Entities;
using BankAccount.ValueTypes;

namespace BankAccount.Denormalizers.Dal
{
    public interface IDatabase
    {
        void Create(CustomerEntity item);
        void Create(AccountEntity item);
        void Update(Guid aggregateId, State accountState, int version);
    }
}
