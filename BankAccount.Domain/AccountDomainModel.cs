using System;
using BankAccount.Events;
using BankAccount.Infrastructure.Domain;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.ValueTypes;

namespace BankAccount.Domain
{
    public class AccountDomainModel : AggregateRoot,
        IHandle<AccountAddedEvent>,
        IHandle<BalanceChangedEvent>
    {
        #region Properties

        public Guid CustomerId { get; set; }
        public string Currency { get; set; }
        public int Balance { get; set; }

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
                    AggregateId = aggregateId,
                    CustomerId = customerId,
                    Version = version,
                    Currency = currency
                });
        }

        public void ChangeBalance(
            int amount,
            int version)
        {
            ApplyChange(
                new BalanceChangedEvent
                {
                    AggregateId = this.Id,
                    Version = version,
                    Amount = amount
                });
        }

        #endregion

        #region Handles

        public void Handle(AccountAddedEvent e)
        {
            this.Id = e.AggregateId;
            this.Version = e.Version;
            this.CustomerId = e.CustomerId;
            this.Currency = e.Currency;
            this.Balance = 0;
        }

        public void Handle(BalanceChangedEvent e)
        {
            this.Id = e.AggregateId;
            this.Version = e.Version;
            this.Balance += e.Amount;
        }

        #endregion


    }
}
