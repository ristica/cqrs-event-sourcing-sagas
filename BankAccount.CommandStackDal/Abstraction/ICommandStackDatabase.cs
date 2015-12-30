using System;

namespace BankAccount.CommandStackDal.Abstraction
{
    /// <summary>
    /// only write actions
    /// </summary>
    public interface ICommandStackDatabase
    {
        void Save(Domain.BankAccount item);
        void Delete(Guid id);
        void AddToCache(Domain.BankAccount ba);
        void UpdateFromCache();
    }
}
