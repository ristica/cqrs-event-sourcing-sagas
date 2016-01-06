using System;
using BankAccount.DbModel.Entities;
using BankAccount.Infrastructure.Domain;
using BankAccount.ValueTypes;

namespace BankAccount.Denormalizers.Dal
{
    public interface IDatabase
    {
        void Create(CustomerEntity item);
        void Create(AccountEntity item);
        void UpdateAccount(Guid aggregateId, State accountState, int version);
        void UpdateCustomer(Guid aggregateId, State customerstate, int version);
    }

    //public interface IDatabase<T> where T : class
    //{
    //    void Create<T>(T item);
    //    void Update<T>(Guid aggregateId, State state, int version);
    //}
}
