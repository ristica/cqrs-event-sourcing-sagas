using System;
using BankAccount.Events;
using BankAccount.Infrastructure.Domain;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.ValueTypes;

namespace BankAccount.Domain
{
    public class AccountDomainModel : AggregateRoot,
        IHandle<AccountAddedEvent>
    {
        #region Properties

        public Guid CustomerId { get; set; }
        public Money Money { get; set; }

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
                    Money = new Money
                    {
                        Balance = 0,
                        Currency = currency
                    }});
        }

        #endregion

        #region Handles

        public void Handle(AccountAddedEvent e)
        {
            this.Id = e.AggregateId;
            this.Version = e.Version;
            this.CustomerId = e.CustomerId;
            this.Money = e.Money;
        }

        #endregion


    }
}
