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
        private readonly AccountDatabase _db;

        public AccountDenormalizer()
        {
            this._db = new AccountDatabase();
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
