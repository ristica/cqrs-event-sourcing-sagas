using System;
using BankAccount.Events;
using BankAccount.Infrastructure.Domain;
using BankAccount.ValueTypes;

namespace BankAccount.Domain
{
    public class AccountDomainModel : AggregateRoot
    {
        #region Properties

        public Guid CustomerId { get; set; }
        public string Currency { get; set; }
        public int Balance { get; set; }
        public State State { get; set; }

        #endregion

        #region Public methods called by command handlers

        public void CreateNewAccount(
            Guid aggregateId,
            Guid customerId,
            int version,
            string currency)
        {
            ApplyChange(
                new AccountAddedEvent { 
                    AggregateId     = aggregateId,
                    CustomerId      = customerId,
                    Version         = version,
                    Currency        = currency
                });
        }

        public void ChangeBalance(
            int amount,
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
            this.Id             = e.AggregateId;
            this.Version        = e.Version;
            this.CustomerId     = e.CustomerId;
            this.Currency       = e.Currency;
            this.Balance        = 0;
            this.State          = e.AccountState;
        }

        public void Handle(BalanceChangedEvent e)
        {
            this.Version        = e.Version;
            this.Balance        += e.Amount;
        }

        public void Handle(AccountDeletedEvent e)
        {
            this.Version        = e.Version;
            this.State          = e.AccountState;
        }

        public void Handle(AccountLockedEvent e)
        {
            this.Version        = e.Version;
            this.State          = e.AccountState;
        }

        public void Handle(AccountUnlockedEvent e)
        {
            this.Version        = e.Version;
            this.State          = e.AccountState;
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
                    AggregateId = id,
                    CustomerId = customerId,
                    Version = version,
                    Currency = currency,
                    AccountState = accountState
                };
                var account = new AccountDomainModel();
                account.ApplyChange(@event);
                return account;
            }
        }

        #endregion
    }
}
