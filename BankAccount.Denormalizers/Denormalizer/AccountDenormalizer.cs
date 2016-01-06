using BankAccount.DbModel.Entities;
using BankAccount.Denormalizers.Dal;
using BankAccount.Events;
using BankAccount.Infrastructure;

namespace BankAccount.Denormalizers.Denormalizer
{
    public class AccountDenormalizer :
        IHandleMessage<AccountAddedEvent>,
        IHandleMessage<AccountLockedEvent>,
        IHandleMessage<AccountUnlockedEvent>,
        IHandleMessage<AccountDeletedEvent>
    {
        private readonly IDatabase _db;

        public AccountDenormalizer(IDatabase db)
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
