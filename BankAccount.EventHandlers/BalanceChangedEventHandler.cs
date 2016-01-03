using System;
using BankAccount.CommandStackDal.Abstraction;
using BankAccount.EventHandlers.Base;
using BankAccount.Events;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.EventHandlers
{
    public class BalanceChangedEventHandler : BaseBankAccountEventHandler, IEventHandler<BalanceChangedEvent>
    {
        private readonly ICommandStackRepository<Domain.BankAccount> _repository;

        public BalanceChangedEventHandler(ICommandStackRepository<Domain.BankAccount> repository, ICommandStackDatabase database) 
            : base(database)
        {
            if (repository == null)
            {
                throw new ArgumentNullException($"Repository was not initialized");
            }

            this._repository = repository;
        }

        public void Handle(BalanceChangedEvent handle)
        {
            //var ba = this._repository.GetById(handle.AggregateId);

            //ba.Money.Balance = ba.Money.Balance;

            //this.Database.Save(ba);
        }
    }
}
