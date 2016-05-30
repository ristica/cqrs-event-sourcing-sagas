using BankAccount.DbModel.Entities;
using BankAccount.Denormalizers.Dal;
using BankAccount.Events;
using BankAccount.Infrastructure;
using Microsoft.Practices.Unity;

namespace BankAccount.Denormalizers.Denormalizer
{
    /// <summary>
    /// denormalizer are responsible for saving the data to the read db
    /// so that the presentation layer have ready to show data and
    /// do not have to work with some kind of dto's or something else
    /// </summary>
    public class AccountDenormalizer :
        IHandleMessage<AccountAddedEvent>,
        IHandleMessage<AccountLockedEvent>,
        IHandleMessage<AccountUnlockedEvent>,
        IHandleMessage<AccountDeletedEvent>
    {
        private readonly IDenormalizerRepository<AccountEntity> _db;

        public AccountDenormalizer([Dependency("Account")] IDenormalizerRepository<AccountEntity> db)
        {
            this._db = db;
        }

        public void Handle(AccountAddedEvent message)
        {
            this._db.Create(
                new AccountEntity
                {
                    AggregateId             = message.AggregateId,
                    Version                 = message.Version,
                    AccountState            = message.AccountState,
                    CustomerAggregateId     = message.CustomerId,
                    Currency                = message.Currency
                });
        }

        public void Handle(AccountLockedEvent message)
        {
            this._db.Update(message.AggregateId, message.AccountState, message.Version);
        }

        public void Handle(AccountUnlockedEvent message)
        {
            this._db.Update(message.AggregateId, message.AccountState, message.Version);
        }

        public void Handle(AccountDeletedEvent message)
        {
            this._db.Update(message.AggregateId, message.AccountState, message.Version);
        }
    }
}
