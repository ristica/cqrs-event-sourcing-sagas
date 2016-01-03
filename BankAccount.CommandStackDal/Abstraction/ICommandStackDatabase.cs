using System;

namespace BankAccount.CommandStackDal.Abstraction
{
    /// <summary>
    /// only write actions
    /// </summary>
    public interface ICommandStackDatabase
    {
        void Save(Domain.CustomerDomainModel item);
        void Save(Domain.AccountDomainModel item);

        void Delete(Guid id);
        void AddToCache(Domain.CustomerDomainModel ba);
        void UpdateFromCache();
    }
}
