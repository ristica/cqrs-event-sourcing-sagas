using System;
using BankAccount.Events;
using BankAccount.Infrastructure.Domain;
using BankAccount.ValueTypes;

namespace BankAccount.Domain
{
    /// <summary>
    /// represents current model of the currently used domain
    /// implements methods that saga can call
    /// implements a factory that saga can call to create the first snapshot
    /// and handles changes that are saved/replayed by aggregate root
    /// in other words - it is some kind of transaction object
    /// </summary>
    public class AccountDomainModel : AggregateRoot
    {
        #region Properties

        public Guid CustomerId { get; private set; }
        public string Currency { get; private set; }
        public decimal Balance { get; private set; }
        public State State { get; private set; }

        #endregion

        #region Public methods called by saga

        public void ChangeBalance(
            decimal amount,
            int version)
        {
            ApplyChange(
                new BalanceChangedEvent
                {
                    AggregateId     = this.Id,
                    Version         = version,
                    Amount          = amount
                });
        }

        public void DeleteAccount()
        {
            ApplyChange(
                new AccountDeletedEvent
                {
                    AggregateId     = this.Id,
                    Version         = this.Version,
                    AccountState    = State.Closed
                });
        }

        public void LockAccount()
        {
            ApplyChange(
                new AccountLockedEvent
                {
                    AggregateId     = this.Id,
                    Version         = this.Version,
                    AccountState    = State.Locked
                });
        }

        public void UnlockAccount()
        {
            ApplyChange(
                new AccountUnlockedEvent
                {
                    AggregateId     = this.Id,
                    Version         = this.Version,
                    AccountState    = State.Unlocked
                });
        }

        #endregion

        #region Handles

        public void Handle(AccountAddedEvent e)
        {
            this.Id                 = e.AggregateId;
            this.Version            = e.Version;
            this.CustomerId         = e.CustomerId;
            this.Currency           = e.Currency;
            this.Balance            = 0;
            this.State              = e.AccountState;
        }

        public void Handle(BalanceChangedEvent e)
        {
            this.Version            = e.Version;
            this.Balance            += e.Amount;
        }

        public void Handle(AccountDeletedEvent e)
        {
            this.Version            = e.Version;
            this.State              = e.AccountState;
        }

        public void Handle(AccountLockedEvent e)
        {
            this.Version            = e.Version;
            this.State              = e.AccountState;
        }

        public void Handle(AccountUnlockedEvent e)
        {
            this.Version            = e.Version;
            this.State              = e.AccountState;
        }

        #endregion

        #region Factory

        public static class Factory
        {
            public static AccountDomainModel CreateNewInstance(
                Guid id,
                Guid customerId,
                int version,
                string currency,
                State accountState)
            {
                var @event = new AccountAddedEvent
                {
                    AggregateId         = id,
                    CustomerId          = customerId,
                    Version             = version,
                    Currency            = currency,
                    AccountState        = accountState
                };
                var account = new AccountDomainModel();
                account.ApplyChange(@event);
                return account;
            }
        }

        #endregion
    }
}
