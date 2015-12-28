using System;

namespace BankAccount.CommandStackDal.Storage.Abstraction
{
    public interface ICommandStackDatabase
    {
        void Save(Domain.BankAccount item);
        void Delete(Guid id);
        void AddToCache(Domain.BankAccount ba);
        void UpdateFromCache();
    }
}
